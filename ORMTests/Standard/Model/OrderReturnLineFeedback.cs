using LinqToDB.Mapping;

namespace R1.MarketplaceManagement.OrderReturnService.DataAccess.Model
{
    [System.ComponentModel.DataAnnotations.Schema.Table("OrderReturnLineFeedback")]
    public class OrderReturnLineFeedback
    {
        [PrimaryKey]
        public virtual int Id { get; set; }

        public virtual int SellingVendorId { get; set; }

        [NotNull]
        public virtual string SellingVendorReason { get; set; }

        public virtual int R1FeedbackId { get; set; }
    }
}