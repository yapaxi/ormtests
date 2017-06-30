using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R1.MarketplaceManagement.OrderReturnService.DataAccess.Model
{
    [Table("OrderReturnInternalRefund", Schema = "dbo")]
    [System.ComponentModel.DataAnnotations.Schema.Table("OrderReturnInternalRefund")]
    public class OrderReturnInternalRefund
    {
        [Identity, PrimaryKey, Column]
        public int Id { get; set; }

        [Column]
        public int OrderReturnId { get; set; }

        [Column]
        public string OrderRefundRequestId { get; set; }

        [Association(ThisKey = nameof(OrderReturnId), OtherKey = nameof(Model.OrderReturn.Id))]
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey(nameof(OrderReturnId))]
        public OrderReturn OrderReturn { get; set; }
    }
}
