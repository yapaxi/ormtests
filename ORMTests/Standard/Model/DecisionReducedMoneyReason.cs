using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R1.MarketplaceManagement.OrderReturnService.DataAccess.Model
{
    [System.ComponentModel.DataAnnotations.Schema.Table("DecisionReducedMoneyReason")]
    public class DecisionReducedMoneyReason
    {
        [PrimaryKey, Identity, NotNull]
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey(nameof(DecisionLine))]
        public int Id { get; set; }

        [Column, NotNull]
        public int DecisionLineId { get; set; }

        [Column, NotNull]
        public string ReducedMoneyReason { get; set; }

        [Association(ThisKey = nameof(DecisionLineId), OtherKey = nameof(Model.DecisionLine.Id), CanBeNull = false)]
        public DecisionLine DecisionLine { get; set; }
    }
}
