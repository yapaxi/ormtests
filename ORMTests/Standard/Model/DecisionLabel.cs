using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R1.MarketplaceManagement.OrderReturnService.DataAccess.Model
{
    [System.ComponentModel.DataAnnotations.Schema.Table("DecisionLabel")]
    public class DecisionLabel
    {
        [PrimaryKey, Identity, NotNull]
        public virtual int Id { get; set; }

        [Column, NotNull]
        public virtual int DecisionId { get; set; }

        [Column, NotNull]
        public virtual string TrackingNumber { get; set; }

        [Column, NotNull]
        public virtual string LabelGifUri { get; set; }

        [Column, NotNull]
        public virtual int ShippingCarrier { get; set; }

        [Association(ThisKey = nameof(Model.DecisionLabel.DecisionId), OtherKey = nameof(Model.Decision.Id), CanBeNull = false)]
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey(nameof(DecisionId))]
        public virtual Decision Decision { get; set; }
    }
}
