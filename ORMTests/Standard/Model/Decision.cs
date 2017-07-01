using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R1.MarketplaceManagement.OrderReturnService.DataAccess.Model
{
    [System.ComponentModel.DataAnnotations.Schema.Table("Decision")]
    public class Decision : DecisionSimple
    {
        [Association(ThisKey = nameof(Id), OtherKey = nameof(DecisionLabel.DecisionId), CanBeNull = true)]
        public virtual ICollection<DecisionLabel> Labels { get; set; }

        [Association(ThisKey = nameof(Decision.Id), OtherKey = nameof(Model.DecisionLine.DecisionId), CanBeNull = false)]
        public virtual ICollection<DecisionLine> DecisionLine { get; set; }
    }

    [System.ComponentModel.DataAnnotations.Schema.Table("Decision")]
    public class DecisionSimple
    {
        [PrimaryKey, Identity, NotNull]
        public virtual int Id { get; set; }

        [Column, NotNull]
        public virtual Guid PublicDecisionId { get; set; }

        [Column, NotNull]
        public virtual string Code { get; set; }

        [Column, NotNull]
        public virtual DateTime CreatedOn { get; set; }

        [Column, NotNull]
        public virtual int OrderReturnId { get; set; }
    }
}
