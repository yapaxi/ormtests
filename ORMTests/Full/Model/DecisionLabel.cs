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
        public int Id { get; set; }

        [Column, NotNull]
        public int DecisionId { get; set; }

        [Column, NotNull]
        public string TrackingNumber { get; set; }

        [Column, NotNull]
        public string LabelGifUri { get; set; }

        [Column, NotNull]
        public int ShippingCarrier { get; set; }

        [Association(ThisKey = nameof(Model.DecisionLabel.DecisionId), OtherKey = nameof(Model.Decision.Id), CanBeNull = false)]
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey(nameof(DecisionId))]
        public Decision Decision { get; set; }
    }
}
