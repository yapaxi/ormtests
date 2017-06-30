using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R1.MarketplaceManagement.OrderReturnService.DataAccess.Model
{
    [System.ComponentModel.DataAnnotations.Schema.Table("OrderReturnReason")]
    public class OrderReturnReason
    {
        [PrimaryKey, NotNull]
        public int Id { get; set; }

        [Column, NotNull]
        public int MarketplaceId { get; set; }

        [Column, NotNull]
        public string MarketplaceReason { get; set; }

        [Column, NotNull]
        public int R1ReturnReasonId { get; set; }
    }
}
