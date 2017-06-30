using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using R1.MarketplaceManagement.OrderReturnService.DataAccess.Model;

namespace ConsoleApp20
{


    public class EFCoreDb : DbContext, ISomeAll
    {
        public EFCoreDb() : base()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            SqlServerDbContextOptionsExtensions.UseSqlServer(
                optionsBuilder,
                "Server=.;Database=OrderReturnService;Integrated Security=SSPI");
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

        public IQueryable<OrderReturn> GetAll()
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
