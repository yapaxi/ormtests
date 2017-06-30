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

namespace ConsoleApp1
{
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
        public int linq2db()
            => Test<ReturnManagementDB>();

        [Benchmark]
        public int efCore()
            => Test<EFCoreDb>();

        [Benchmark]
        public int efCoreNoTrack()
            => Test<EFCoreDb>(e => Microsoft.EntityFrameworkCore.EntityFrameworkQueryableExtensions.AsNoTracking(e));

        private int Test<TDB>(Func<IQueryable<OrderReturn>, IQueryable<OrderReturn>> config = null)
            where TDB : ISomeAll, new()
        {
            var x = 0;
            config = config ?? new Func<IQueryable<OrderReturn>, IQueryable<OrderReturn>>(e => e);
            using (var db = new TDB())
            {
                var rt = config(db.GetAll()).Where(e => e.Id == _min).FirstOrDefault();
                if (rt != null)
                {
                    x++;
                }
                rt = config(db.GetAll()).Where(e => e.Id == _max).FirstOrDefault();
                if (rt != null)
                {
                    x++;
                }
                rt = config(db.GetAll()).Where(e => e.Id == _any).FirstOrDefault();
                if (rt != null)
                {
                    x++;
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

            // new Benchmarks().linq2db();

            var summary = BenchmarkRunner.Run<Benchmarks>();

            return;
        }
    }

    public interface ISomeAll : IDisposable
    {
        IQueryable<OrderReturn> GetAll();
    }

    public class ReturnManagementDB : DataConnection, IDisposable, ISomeAll
    {
        public ReturnManagementDB()
            : base("SqlServer.2014", "Server=.;Database=OrderReturnService;Integrated Security=SSPI")
        {
        }

        public ITable<OrderReturn> OrderReturn => GetTable<OrderReturn>();
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

        public IQueryable<OrderReturn> GetAll()
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