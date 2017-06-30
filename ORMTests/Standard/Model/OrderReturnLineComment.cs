using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R1.MarketplaceManagement.OrderReturnService.DataAccess.Model
{
    [System.ComponentModel.DataAnnotations.Schema.Table("OrderReturnLineComment")]
    public class OrderReturnLineComment 
    {
        [PrimaryKey, Identity, NotNull]
        public virtual int Id { get; set; }

        [NotNull]
        [Column]
        public virtual int OrderReturnLineId { get; set; }

        [NotNull]
        [Column]
        public virtual string Comment { get; set; }

        [Association(ThisKey = nameof(OrderReturnLineId), OtherKey = nameof(Model.OrderReturnLine.Id), CanBeNull = false)]
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey(nameof(OrderReturnLineId))]
        public virtual OrderReturnLine OrderReturnLine { get; set; }
    }
}
