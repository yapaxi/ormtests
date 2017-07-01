using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Attributes.Jobs;
using BenchmarkDotNet.Running;
using ConsoleApp20;
using LinqToDB;
using LinqToDB.Data;
using LinqToDB.DataProvider;
using R1.MarketplaceManagement.OrderReturnService.DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Toolchains;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public static class Connection
    {
        public static readonly string String = "Server=192.168.100.7;Database=OrderReturnService;user=yapaxi;password=test1234";
    }

    [SimpleJob(launchCount: 1, targetCount: 10, invocationCount: 50)]
    public class Benchmarks
    {
        private readonly int _min;
        private readonly int _any;
        private readonly int _max;

        public Benchmarks()
        {
            LinqToDB.Common.Configuration.Linq.AllowMultipleQuery = true;
            using (var db = new EFCoreDb())
            {
                _min = db.OrderReturn.Min(e => e.Id);
                _max = db.OrderReturn.Max(e => e.Id);
                _any = (_min + _max) / 2;

                Console.WriteLine($"{_min},{_any},{_max}");
            }
        }

        [Benchmark]
        public int linq2db_join20times() => TestQueryLinq2Db(e => from q in e.OrderReturn
                                                            join l in e.OrderReturnLine on q.Id equals l.OrderReturnId
                                                            join dl in e.DecisionLines on new { A = (int)q.CurrentDecisionId, B = l.Id } 
                                                                                        equals new { A = dl.DecisionId, B = dl.OrderReturnLineId }
                                                            join rf in e.DecisionRefund on dl.Id equals rf.DecisionLineId
                                                            where q.Id == _any
                                                            select new { q.ReturnWorkflowStepId, l.Quantity, rf.SaleAmount });

        [Benchmark]
        public int efCore_join20times() => TestQueryEF(e => from q in e.OrderReturnSimple
                                                            join l in e.OrderReturnLineSimple on q.Id equals l.OrderReturnId
                                                            join dl in e.DecisionLineSimple on new { A = (int)q.CurrentDecisionId, B = l.Id }
                                                                                        equals new { A = dl.DecisionId, B = dl.OrderReturnLineId }
                                                            join rf in e.DecisionRefundSimple on dl.Id equals rf.DecisionLineId
                                                            where q.Id == _any
                                                            select new { q.ReturnWorkflowStepId, l.Quantity, rf.SaleAmount });
        [Benchmark]
        public int efCoreNoTrack_join20times() => TestQueryEFNT(e => from q in e.OrderReturnSimple
                                                              join l in e.OrderReturnLineSimple on q.Id equals l.OrderReturnId
                                                            join dl in e.DecisionLineSimple on new { A = (int)q.CurrentDecisionId, B = l.Id }
                                                                                        equals new { A = dl.DecisionId, B = dl.OrderReturnLineId }
                                                            join rf in e.DecisionRefundSimple on dl.Id equals rf.DecisionLineId
                                                            where q.Id == _any
                                                            select new { q.ReturnWorkflowStepId, l.Quantity, rf.SaleAmount });
        
        [Benchmark] public Task<int> linq2db_no_includes_concurrent20each5_burst() => TestNoIncludes_Concurrent5Times_Burst<ReturnManagementDB>(count: 20);
        [Benchmark] public Task<int> efCore_no_includes_concurrent20each5_burst() => TestNoIncludes_Concurrent5Times_Burst<EFCoreDbSimple>(count: 20);
        [Benchmark] public Task<int> efCoreNoTrack_no_includes_concurrent20each5_burst() => TestNoIncludes_Concurrent5Times_Burst<EFCoreDbSimple>(count: 20, config: e => Microsoft.EntityFrameworkCore.EntityFrameworkQueryableExtensions.AsNoTracking(e));

        [Benchmark] public int linq2db_no_includes20times_20root() => TestNoIncludes20Times<ReturnManagementDB>(count: 20);
        [Benchmark] public int efCore_no_includes20times_20root() => TestNoIncludes20Times<EFCoreDbSimple>(count: 20);
        [Benchmark] public int efCoreNoTrack_no_includes20times_20root() => TestNoIncludes20Times<EFCoreDbSimple>(count: 20, config: e => Microsoft.EntityFrameworkCore.EntityFrameworkQueryableExtensions.AsNoTracking(e));

        [Benchmark] public int linq2db_no_includes20times_1root() => TestNoIncludes20Times<ReturnManagementDB>(count: 1);
        [Benchmark] public int efCore_no_includes20times_1root() => TestNoIncludes20Times<EFCoreDbSimple>(count: 1);
        [Benchmark] public int efCoreNoTrack_no_includes20times_1root() => TestNoIncludes20Times<EFCoreDbSimple>(count: 1, config: e => Microsoft.EntityFrameworkCore.EntityFrameworkQueryableExtensions.AsNoTracking(e));

        [Benchmark] public int linq2db_many_includes_1root() => TestAllIncludes<ReturnManagementDB>(count: 1);
        [Benchmark] public int efCore_many_includes_1root() => TestAllIncludes<EFCoreDb>(count: 1);
        [Benchmark] public int efCoreNoTrack_many_includes_1root() => TestAllIncludes<EFCoreDb>(count: 1, config: e => Microsoft.EntityFrameworkCore.EntityFrameworkQueryableExtensions.AsNoTracking(e));

        [Benchmark] public int linq2db_many_includes_20roots() => TestAllIncludes<ReturnManagementDB>(count: 20);
        [Benchmark] public int efCore_many_includes_20roots() => TestAllIncludes<EFCoreDb>(count: 20);
        [Benchmark] public int efCoreNoTrack_many_includes_20roots() => TestAllIncludes<EFCoreDb>(count: 20, config: e => Microsoft.EntityFrameworkCore.EntityFrameworkQueryableExtensions.AsNoTracking(e));

        private async Task<int> TestNoIncludes_Concurrent5Times_Burst<TDB>(int count, Func<IQueryable<OrderReturnSimple>, IQueryable<OrderReturnSimple>> config = null)
            where TDB : ISomeAll, new()
        {
            config = config ?? new Func<IQueryable<OrderReturnSimple>, IQueryable<OrderReturnSimple>>(e => e);

            using (var slim = new System.Threading.ManualResetEventSlim(false))
            {
                var taskFactory = new TaskFactory(TaskCreationOptions.LongRunning, TaskContinuationOptions.None);
                var tasks = Enumerable.Range(0, count).Select(e => taskFactory.StartNew(() =>
                {
                    var x = 0;
                    slim.Wait();
                    for (var i = 0; i < 5; i++)
                    {
                        using (var db = new TDB())
                        {
                            if (config(db.GetOrderReturnsNoIncludes()).Where(q => q.Id == _any).FirstOrDefault() != null)
                            {
                                x++;
                            }
                        }
                    }
                    return x;
                })).ToArray();

                slim.Set();
                await Task.WhenAll(tasks);
                return tasks.Sum(e => e.Result);
            }
        }
        private int TestQueryEF<TResult>(Func<EFCoreDbSimple, IQueryable<TResult>> query) => TestQuery(query);
        private int TestQueryEFNT<TResult>(Func<IEFCoreDbSimpleNoTracking, IQueryable<TResult>> query) 
            => TestQuery<EFCoreDbSimple, IEFCoreDbSimpleNoTracking, TResult>(query);
        private int TestQueryLinq2Db<TResult>(Func<ReturnManagementDB, IQueryable<TResult>> query) => TestQuery(query);

        private int TestQuery<TDB, TResult>(Func<TDB, IQueryable<TResult>> query)
            where TDB : class, IDisposable, new()
        {
            return TestQuery<TDB, TDB, TResult>(query);
        }

        private int TestQuery<TDB, TVAR, TResult>(Func<TVAR, IQueryable<TResult>> query)
            where TDB : class, TVAR, IDisposable, new()
            where TVAR : class
        {
            var x = 0;
            for (int i = 0; i < 20; i++)
            {
                using (var db = new TDB())
                {
                    var rt = query(db as TVAR).ToArray();
                    if (rt.Any())
                    {
                        x++;
                    }
                }
            }

            return x;
        }

        private int TestNoIncludes20Times<TDB>(int count, Func<IQueryable<OrderReturnSimple>, IQueryable<OrderReturnSimple>> config = null)
            where TDB : ISomeAll, new()
        {
            var x = 0;
            config = config ?? new Func<IQueryable<OrderReturnSimple>, IQueryable<OrderReturnSimple>>(e => e);
            for (int i = 0; i < 20; i++)
            {
                using (var db = new TDB())
                {
                    if (count == 1)
                    {
                        if (config(db.GetOrderReturnsNoIncludes()).Where(q => q.Id == _any).FirstOrDefault() != null)
                        {
                            x++;
                        }
                    }
                    else
                    {
                        var rt = config(db.GetOrderReturnsNoIncludes()).Where(e => e.Id >= _any && e.Id < _any + count).ToArray();
                        if (rt.Any())
                        {
                            x++;
                        }
                    }
                }
            }

            return x;
        }

        private int TestAllIncludes<TDB>(int count, Func<IQueryable<OrderReturn>, IQueryable<OrderReturn>> config = null)
            where TDB : ISomeAll, new()
        {
            var x = 0;
            config = config ?? new Func<IQueryable<OrderReturn>, IQueryable<OrderReturn>>(e => e);
            using (var db = new TDB())
            {
                if (count == 1)
                {
                    if (config(db.GetOrderReturnsWithAllIncludes()).Where(q => q.Id == _any).FirstOrDefault() != null)
                    {
                        x++;
                    }
                }
                else
                {
                    var rt = config(db.GetOrderReturnsWithAllIncludes()).Where(e => e.Id >= _any && e.Id < _any + count).ToArray();
                    if (rt.Any())
                    {
                        x++;
                    }
                }
            }

            return x;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            LinqToDB.Common.Configuration.Linq.AllowMultipleQuery = true;

            //Console.WriteLine("ready");
            //Console.ReadKey(false);
            //new Benchmarks().linq2db_join();
            //Console.ReadKey(false);
            //new Benchmarks().efCore_join();
            //Console.ReadKey(false);
            //new Benchmarks().efCoreNoTrack_join();

            var summary = BenchmarkRunner.Run<Benchmarks>();

            return;
        }
    }

    public interface ISomeAll : IDisposable
    {
        IQueryable<OrderReturn> GetOrderReturnsWithAllIncludes();
        IQueryable<OrderReturnSimple> GetOrderReturnsNoIncludes();
    }

    public class ReturnManagementDB : DataConnection, IDisposable, ISomeAll
    {
        public ReturnManagementDB()
            : base("SqlServer.2014", ConsoleApp1.Connection.String)
        {
        }

        public ITable<OrderReturn> OrderReturn => GetTable<OrderReturn>();
        public ITable<OrderReturnSimple> OrderReturnSimple => GetTable<OrderReturnSimple>();
        public ITable<OrderReturnReason> OrderReturnReason => GetTable<OrderReturnReason>();
        public ITable<OrderReturnLine> OrderReturnLine => GetTable<OrderReturnLine>();
        public ITable<OrderReturnRawData> OrderReturnRawData => GetTable<OrderReturnRawData>();
        public ITable<OrderReturnLineFeedback> OrderReturnLineFeedback => GetTable<OrderReturnLineFeedback>();
        public ITable<OrderReturnPollingSettings> OrderReturnPollingSettings => GetTable<OrderReturnPollingSettings>();
        public ITable<OrderReturnInternalRefund> OrderReturnInternalRefund => GetTable<OrderReturnInternalRefund>();
        public ITable<Decision> Decision => GetTable<Decision>();
        public ITable<DecisionLine> DecisionLines => GetTable<DecisionLine>();
        public ITable<DecisionLabel> DecisionLabel => GetTable<DecisionLabel>();
        public ITable<DecisionRefund> DecisionRefund => GetTable<DecisionRefund>();

        public IQueryable<OrderReturnSimple> GetOrderReturnsNoIncludes()
        {
            return OrderReturnSimple;
        }

        public IQueryable<OrderReturn> GetOrderReturnsWithAllIncludes()
        {
            return OrderReturn
                    .LoadWith(e => e.ReturnLines)
                    .LoadWith(e => e.ReturnLines.First().Reason)
                    .LoadWith(e => e.ReturnLines.First().Comments)
                    .LoadWith(e => e.InternalRefunds)
                    .LoadWith(e => e.Files)
                    .LoadWith(e => e.CurrentDecision.Labels)
                    .LoadWith(e => e.CurrentDecision.DecisionLine)
                    .LoadWith(e => e.CurrentDecision.DecisionLine.First().ReducedMoneyReason)
                    .LoadWith(e => e.CurrentDecision.DecisionLine.First().Refund)
                    ;
        }
    }
}