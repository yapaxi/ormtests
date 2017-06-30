using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R1.MarketplaceManagement.OrderReturnService.DataAccess.Model
{
    [System.ComponentModel.DataAnnotations.Schema.Table("OrderReturnLine")]
    public class OrderReturnLine
    {
        [PrimaryKey, Identity, NotNull]
        public virtual int Id { get; set; }

        [Column, NotNull]
        public virtual int OrderReturnId { get; set; }

        [Column, NotNull]
        public virtual string VenueReturnLineId { get; set; }

        [Column, NotNull]
        public virtual string PurchaseOrderLineId { get; set; }

        [Column, NotNull]
        public virtual int Quantity { get; set; }

        [Column, NotNull]
        public virtual int ReasonId { get; set; }

        [Column, NotNull]
        public virtual string CurrencyCode { get; set; }

        [Column, NotNull]
        public virtual decimal SaleAmount { get; set; }

        [Column, NotNull]
        public virtual decimal TaxAmount { get; set; }

        [Column, NotNull]
        public virtual decimal ShippingAmount { get; set; }

        [Column, NotNull]
        public virtual decimal ShippingTaxAmount { get; set; }

        [Association(ThisKey = nameof(Model.OrderReturnLine.OrderReturnId), OtherKey = nameof(Model.OrderReturn.Id))]
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey(nameof(OrderReturnId))]
        public virtual OrderReturn OrderReturn { get; set; }

        [Association(ThisKey = nameof(Model.OrderReturnLine.ReasonId), OtherKey = nameof(Model.OrderReturnReason.Id))]
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey(nameof(ReasonId))]
        public virtual OrderReturnReason Reason { get; set; }

        [Association(ThisKey = nameof(Model.OrderReturnLine.Id), OtherKey = nameof(Model.OrderReturnLineComment.OrderReturnLineId))]
        public virtual ICollection<OrderReturnLineComment> Comments { get; set; }
    }
}
