using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Attributes.Jobs;
using BenchmarkDotNet.Running;
using LinqToDB;
using LinqToDB.Data;
using LinqToDB.DataProvider;
using R1.MarketplaceManagement.OrderReturnService.DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp20
{
    [SimpleJob(launchCount: 1, targetCount: 10, invocationCount: 300)]
    public class Benchmarks
    {
        public Benchmarks()
        {
            LinqToDB.Common.Configuration.Linq.AllowMultipleQuery = true;
        }

        [Benchmark]
        public int linq2db() 
            => Test<ReturnManagementDB>();

        [Benchmark]
        public int ef6() 
            => Test<EF6Db>();

        [Benchmark]
        public int ef6NoTrack() 
            => Test<EF6Db>(e => e.AsNoTracking());

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
                var rt = config(db.GetAll()).Where(e => e.Id == 1011).FirstOrDefault();
                if (rt != null)
                {
                    x++;
                }
                rt = config(db.GetAll()).Where(e => e.Id == 11010).FirstOrDefault();
                if (rt != null)
                {
                    x++;
                }
                rt = config(db.GetAll()).Where(e => e.Id == 5011).FirstOrDefault();
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
        public static int X { get; set; }
        private static Dictionary<string, List<long>> _results = new Dictionary<string, List<long>>()
        {
            ["linq2db"] = new List<long>(),
            ["ef-6"] = new List<long>(),
            ["ef-6-no-track"] = new List<long>(),
            ["ef-core"] = new List<long>(),
            ["ef-core-no-track"] = new List<long>()
        };

        private static long Median(List<long> lst) => lst.OrderBy(e => e).ElementAt(lst.Count / 2);

        static void Main(string[] args)
        {
            LinqToDB.Common.Configuration.Linq.AllowMultipleQuery = true;

            new Benchmarks().efCore();

        //   var summary = BenchmarkRunner.Run<Benchmarks>();

            return;
        }
    }

    public interface ISomeAll : IDisposable
    {
        IQueryable<OrderReturn> GetAll();
    }

    public class EF6Db : DbContext, ISomeAll
    {
        public EF6Db() : base()
        {

        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=.;Database=OrderReturnService;Integrated Security=SSPI");
        //    base.OnConfiguring(optionsBuilder);
        //}

        public DbSet<OrderReturn> OrderReturn { get; set; }
        public DbSet<OrderReturnReason> OrderReturnReason { get; set; }
        public DbSet<OrderReturnLine> OrderReturnLine { get; set; }
        public DbSet<OrderReturnRawData> OrderReturnRawData { get; set; }
        public DbSet<OrderReturnLineFeedback> OrderReturnLineFeedback { get; set; }
        public DbSet<OrderReturnPollingSettings> OrderReturnPollingSettings { get; set; }
        public DbSet<OrderReturnInternalRefund> OrderReturnInternalRefund { get; set; }
        public DbSet<Decision> Decision { get; set; }
        public DbSet<DecisionLine> DecisionLine { get; set; }
        public DbSet<DecisionLabel> DecisionLabel { get; set; }
        public DbSet<DecisionRefund> DecisionRefund { get; set; }

        public IQueryable<OrderReturn> GetAll()
        {
            return OrderReturn
                    .Include(e => e.ReturnLines.Select(q => q.Reason))
                    .Include(e => e.ReturnLines.Select(q => q.Comments))
                    .Include(e => e.InternalRefunds)
                    .Include(e => e.Files)
                    .Include(e => e.CurrentDecision.Labels)
                    .Include(e => e.CurrentDecision.DecisionLine)
                    .Include(e => e.CurrentDecision.DecisionLine.Select(q => q.Refund))
                    .Include(e => e.CurrentDecision.DecisionLine.Select(q => q.ReducedMoneyReason))
                    ;
        }
    }

    public class ReturnManagementDB : DataConnection, IDisposable, ISomeAll
    {
        public ReturnManagementDB(IDataProvider dataProvider, string connectionString)
            : base(dataProvider, connectionString)
        {
            CommandTimeout = 200;
        }

        public ReturnManagementDB()
        {
        }

        public ReturnManagementDB(string configuration)
            : base(configuration)
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
