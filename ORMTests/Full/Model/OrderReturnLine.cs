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
        public int Id { get; set; }

        [Column, NotNull]
        public int OrderReturnId { get; set; }

        [Column, NotNull]
        public string VenueReturnLineId { get; set; }

        [Column, NotNull]
        public string PurchaseOrderLineId { get; set; }

        [Column, NotNull]
        public int Quantity { get; set; }

        [Column, NotNull]
        public int ReasonId { get; set; }

        [Column, NotNull]
        public string CurrencyCode { get; set; }

        [Column, NotNull]
        public decimal SaleAmount { get; set; }

        [Column, NotNull]
        public decimal TaxAmount { get; set; }

        [Column, NotNull]
        public decimal ShippingAmount { get; set; }

        [Column, NotNull]
        public decimal ShippingTaxAmount { get; set; }

        [Association(ThisKey = nameof(Model.OrderReturnLine.OrderReturnId), OtherKey = nameof(Model.OrderReturn.Id), CanBeNull = false)]
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey(nameof(OrderReturnId))]
        public OrderReturn OrderReturn { get; set; }

        [Association(ThisKey = nameof(Model.OrderReturnLine.ReasonId), OtherKey = nameof(Model.OrderReturnReason.Id), CanBeNull = false)]
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey(nameof(ReasonId))]
        public OrderReturnReason Reason { get; set; }

        [Association(ThisKey = nameof(Model.OrderReturnLine.Id), OtherKey = nameof(Model.OrderReturnLineComment.OrderReturnLineId), CanBeNull = false, IsBackReference = true)]
        public ICollection<OrderReturnLineComment> Comments { get; set; }
    }
}
