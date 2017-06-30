using LinqToDB.Mapping;

namespace R1.MarketplaceManagement.OrderReturnService.DataAccess.Model
{
    [System.ComponentModel.DataAnnotations.Schema.Table("OrderReturnLineFeedback")]
    public class OrderReturnLineFeedback
    {
        [PrimaryKey]
        public int Id { get; set; }

        public int SellingVendorId { get; set; }

        [NotNull]
        public string SellingVendorReason { get; set; }

        public int R1FeedbackId { get; set; }
    }
}