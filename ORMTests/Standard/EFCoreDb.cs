using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using R1.MarketplaceManagement.OrderReturnService.DataAccess.Model;
using ConsoleApp1;

namespace ConsoleApp20
{
    public abstract class EFCoreBase : DbContext, ISomeAll
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            SqlServerDbContextOptionsExtensions.UseSqlServer(
                optionsBuilder,
                ConsoleApp1.Connection.String);
            base.OnConfiguring(optionsBuilder);
        }

        public abstract IQueryable<OrderReturnSimple> GetOrderReturnsNoIncludes();
        public abstract IQueryable<OrderReturn> GetOrderReturnsWithAllIncludes();
    }

    public class EFCoreDbSimple : EFCoreBase
    {
        public DbSet<OrderReturnSimple> OrderReturnSimple { get; set; }

        public override IQueryable<OrderReturnSimple> GetOrderReturnsNoIncludes()
        {
            return OrderReturnSimple;
        }

        public override IQueryable<OrderReturn> GetOrderReturnsWithAllIncludes()
        {
            throw new NotImplementedException();
        }
    }

    public class EFCoreDb : EFCoreBase
    {
        public EFCoreDb() : base()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            SqlServerDbContextOptionsExtensions.UseSqlServer(
                optionsBuilder,
                "Server=192.168.100.7;Database=OrderReturnService;user=yapaxi;password=test1234");
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<OrderReturn> OrderReturn { get; set; }
        public DbSet<OrderReturnLine> OrderReturnLine { get; set; }
        public DbSet<OrderReturnRawData> OrderReturnRawData { get; set; }
        public DbSet<OrderReturnLineFeedback> OrderReturnLineFeedback { get; set; }
        public DbSet<OrderReturnPollingSettings> OrderReturnPollingSettings { get; set; }
        public DbSet<OrderReturnInternalRefund> OrderReturnInternalRefund { get; set; }
        public DbSet<Decision> Decision { get; set; }
        public DbSet<DecisionLine> DecisionLine { get; set; }
        public DbSet<DecisionLabel> DecisionLabel { get; set; }
        public DbSet<DecisionRefund> DecisionRefund { get; set; }

        public override IQueryable<OrderReturnSimple> GetOrderReturnsNoIncludes()
        {
            throw new NotImplementedException();
        }

        public override IQueryable<OrderReturn> GetOrderReturnsWithAllIncludes()
        {
            return OrderReturn
                    .Include(e => e.ReturnLines).ThenInclude(e => e.Reason)
                    .Include(e => e.ReturnLines).ThenInclude(e => e.Comments)
                    .Include(e => e.InternalRefunds)
                    .Include(e => e.Files)
                    .Include(e => e.CurrentDecision.Labels)
                    .Include(e => e.CurrentDecision.DecisionLine)
                    .Include(e => e.CurrentDecision.DecisionLine).ThenInclude(e => e.Refund)
                    .Include(e => e.CurrentDecision.DecisionLine).ThenInclude(e => e.ReducedMoneyReason)
                    ;
        }
    }
}
