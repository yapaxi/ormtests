using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R1.MarketplaceManagement.OrderReturnService.DataAccess.Model
{
    [System.ComponentModel.DataAnnotations.Schema.Table("Decision")]
    public class Decision
    {
        [PrimaryKey, Identity, NotNull]
        public int Id { get; set; }

        [Column, NotNull]
        public Guid PublicDecisionId { get; set; }

        [Column, NotNull]
        public string Code { get; set; }

        [Column, NotNull]
        public DateTime CreatedOn { get; set; }

        [Column, NotNull]
        public int OrderReturnId { get; set; }

        [Association(ThisKey = nameof(Id), OtherKey = nameof(DecisionLabel.DecisionId), CanBeNull = true)]
        public ICollection<DecisionLabel> Labels { get; set; }

        [Association(ThisKey = nameof(Decision.Id), OtherKey = nameof(Model.DecisionLine.DecisionId), CanBeNull = false)]
        public ICollection<DecisionLine> DecisionLine { get; set; }
    }
}
