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
        public virtual int Id { get; set; }

        [Column, NotNull]
        public virtual string VenueReturnId { get; set; }

        [Column, NotNull]
        public virtual string PurchaseOrderId { get; set; }

        [Column, NotNull]
        public virtual int SellingVendorId { get; set; }

        [Column, NotNull]
        public virtual DateTime VenueCreatedOn { get; set; }

        [Column, NotNull]
        public virtual DateTime CreatedOn { get; set; }

        [Column, Nullable]
        public virtual DateTime? UpdatedOn { get; set; }

        [Column, NotNull]
        public virtual bool RefundWithoutReturn { get; set; }

        [Column, NotNull]
        public virtual OrderReturnStatus StatusId { get; set; }

        [Column, NotNull]
        public virtual int ReturnWorkflowStepId { get; set; }

        [Column, Nullable]
        public virtual string StatusMessage { get; set; }

        [Column, Nullable]
        public virtual string ShippingCarrier { get; set; }

        [Column, Nullable]
        public virtual string ShippingTrackingNumber { get; set; }

        [Column, NotNull]
        public virtual decimal ReturnCharge { get; set; }

        [Column, NotNull]
        public virtual bool IsInRecovery { get; set; }

        [Column, Nullable]
        public virtual int? CurrentDecisionId { get; set; }

        [Association(ThisKey = nameof(Id), OtherKey = nameof(OrderReturnLine.OrderReturnId), CanBeNull = false)]
        public virtual ICollection<OrderReturnLine> ReturnLines { get; set; }

        [Association(ThisKey = nameof(Id), OtherKey = nameof(Model.OrderReturnRawData.Id), CanBeNull = false)]
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey(nameof(Id))]
        public virtual OrderReturnRawData OrderReturnRawData { get; set; }

        [Association(ThisKey = nameof(Id), OtherKey = nameof(OrderReturnFile.OrderReturnId), CanBeNull = false)]
        public virtual ICollection<OrderReturnFile> Files { get; set; }

        [Association(ThisKey = nameof(Id), OtherKey = nameof(OrderReturnInternalRefund.OrderReturnId), CanBeNull = true)]
        public virtual ICollection<OrderReturnInternalRefund> InternalRefunds { get; set; }

        [Association(ThisKey = nameof(Model.OrderReturn.CurrentDecisionId), OtherKey = nameof(Model.Decision.Id), CanBeNull = true)]
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey(nameof(CurrentDecisionId))]
        public virtual Decision CurrentDecision { get; set; }
    }

    [System.ComponentModel.DataAnnotations.Schema.Table("OrderReturn")]
    public class OrderReturnSimple
    {
        [PrimaryKey, Identity, NotNull]
        public virtual int Id { get; set; }

        [Column, NotNull]
        public virtual string VenueReturnId { get; set; }

        [Column, NotNull]
        public virtual string PurchaseOrderId { get; set; }

        [Column, NotNull]
        public virtual int SellingVendorId { get; set; }

        [Column, NotNull]
        public virtual DateTime VenueCreatedOn { get; set; }

        [Column, NotNull]
        public virtual DateTime CreatedOn { get; set; }

        [Column, Nullable]
        public virtual DateTime? UpdatedOn { get; set; }

        [Column, NotNull]
        public virtual bool RefundWithoutReturn { get; set; }

        [Column, NotNull]
        public virtual OrderReturnStatus StatusId { get; set; }

        [Column, NotNull]
        public virtual int ReturnWorkflowStepId { get; set; }

        [Column, Nullable]
        public virtual string StatusMessage { get; set; }

        [Column, Nullable]
        public virtual string ShippingCarrier { get; set; }

        [Column, Nullable]
        public virtual string ShippingTrackingNumber { get; set; }

        [Column, NotNull]
        public virtual decimal ReturnCharge { get; set; }

        [Column, NotNull]
        public virtual bool IsInRecovery { get; set; }

        [Column, Nullable]
        public virtual int? CurrentDecisionId { get; set; }
    }
}
