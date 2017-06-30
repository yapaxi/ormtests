using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R1.MarketplaceManagement.OrderReturnService.DataAccess.Model
{
    [System.ComponentModel.DataAnnotations.Schema.Table("OrderReturn")]
    public class OrderReturn 
    {
        [PrimaryKey, Identity, NotNull]
        public int Id { get; set; }

        [Column, NotNull]
        public string VenueReturnId { get; set; }

        [Column, NotNull]
        public string PurchaseOrderId { get; set; }

        [Column, NotNull]
        public int SellingVendorId { get; set; }

        [Column, NotNull]
        public DateTime VenueCreatedOn { get; set; }

        [Column, NotNull]
        public DateTime CreatedOn { get; set; }

        [Column, Nullable]
        public DateTime? UpdatedOn { get; set; }

        [Column, NotNull]
        public bool RefundWithoutReturn { get; set; }

        [Column, NotNull]
        public OrderReturnStatus StatusId { get; set; }

        [Column, NotNull]
        public int ReturnWorkflowStepId { get; set; }

        [Column, Nullable]
        public string StatusMessage { get; set; }

        [Column, Nullable]
        public string ShippingCarrier { get; set; }

        [Column, Nullable]
        public string ShippingTrackingNumber { get; set; }

        [Column, NotNull]
        public decimal ReturnCharge { get; set; }

        [Column, NotNull]
        public bool IsInRecovery { get; set; }

        [Column, Nullable]
        public int? CurrentDecisionId { get; set; }

        [Association(ThisKey = nameof(Id), OtherKey = nameof(OrderReturnLine.OrderReturnId), CanBeNull = false)]
        public ICollection<OrderReturnLine> ReturnLines { get; set; }

        [Association(ThisKey = nameof(Id), OtherKey = nameof(Model.OrderReturnRawData.Id), CanBeNull = false)]
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey(nameof(Id))]
        public OrderReturnRawData OrderReturnRawData { get; set; }

        [Association(ThisKey = nameof(Id), OtherKey = nameof(OrderReturnFile.OrderReturnId), CanBeNull = false)]
        public ICollection<OrderReturnFile> Files { get; set; }

        [Association(ThisKey = nameof(Id), OtherKey = nameof(OrderReturnInternalRefund.OrderReturnId), CanBeNull = true)]
        public ICollection<OrderReturnInternalRefund> InternalRefunds { get; set; }

        [Association(ThisKey = nameof(Model.OrderReturn.CurrentDecisionId), OtherKey = nameof(Model.Decision.Id), CanBeNull = true)]
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey(nameof(CurrentDecisionId))]
        public Decision CurrentDecision { get; set; }
    }
}
