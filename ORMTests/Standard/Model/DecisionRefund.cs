using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R1.MarketplaceManagement.OrderReturnService.DataAccess.Model
{
    [System.ComponentModel.DataAnnotations.Schema.Table("DecisionRefund")]
    public class DecisionRefund
    {
        [PrimaryKey, Identity, NotNull]
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey(nameof(DecisionLine))]
        public virtual int Id { get; set; }

        [Column, NotNull]
        public virtual int DecisionLineId { get; set; }

        [Column, NotNull]
        public virtual decimal SaleAmount { get; set; }

        [Column, NotNull]
        public virtual decimal TaxAmount { get; set; }

        [Column, NotNull]
        public virtual decimal ShippingAmount { get; set; }
        
        [Column, NotNull]
        public virtual decimal ShippingTaxAmount { get; set; }

        [Column, NotNull]
        public virtual string CurrencyCode { get; set; }

        [Association(ThisKey = nameof(DecisionLineId), OtherKey = nameof(Model.DecisionLine.Id), CanBeNull = false)]
        public virtual DecisionLine DecisionLine { get; set; }
    }
}
