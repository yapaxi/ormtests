using LinqToDB;
using LinqToDB.Data;
using R1.MarketplaceManagement.OrderReturnService.DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Standard
{
    public class Linq2DBDb : DataConnection, IDisposable, INavigationSource
    {
        public Linq2DBDb()
            : base("SqlServer.2014", Standard.Connection.String)
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
