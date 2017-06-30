using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R1.MarketplaceManagement.OrderReturnService.DataAccess.Model
{
    [System.ComponentModel.DataAnnotations.Schema.Table("DecisionLine")]
    public class DecisionLine
    {
        [PrimaryKey, Identity, NotNull]
        public virtual int Id { get; set; }

        [Column, NotNull]
        public virtual int OrderReturnLineId { get; set; }

        [Column, NotNull]
        public virtual int DecisionId { get; set; }

        [Association(ThisKey = nameof(Id), OtherKey = nameof(DecisionRefund.DecisionLineId), CanBeNull = true)]
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey(nameof(Id))]
        public virtual DecisionRefund Refund { get; set; }

        [Association(ThisKey = nameof(Id), OtherKey = nameof(DecisionReducedMoneyReason.DecisionLineId), CanBeNull = true)]
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey(nameof(Id))]
        public virtual DecisionReducedMoneyReason ReducedMoneyReason { get; set; }

        [Association(ThisKey = nameof(DecisionId), OtherKey = nameof(Model.Decision.Id), CanBeNull = false)]
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey(nameof(DecisionId))]
        public virtual Decision Decision { get; set; }

        [Association(ThisKey = nameof(OrderReturnLineId), OtherKey = nameof(Model.OrderReturnLine.Id), CanBeNull = false)]
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey(nameof(OrderReturnLineId))]
        public virtual OrderReturnLine OrderReturnLine { get; set; }
    }
}
