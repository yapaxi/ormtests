using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R1.MarketplaceManagement.OrderReturnService.DataAccess.Model
{
    public enum OrderReturnFileType
    {
        Other = 0,
        Base64Image = 1
    }

    [Table(Schema = "dbo", Name = "OrderReturnFile")]
    [System.ComponentModel.DataAnnotations.Schema.Table("OrderReturnFile")]
    public class OrderReturnFile
    {
        [PrimaryKey, Identity, NotNull, Column]
        public virtual int Id { get; set; }

        [Column]
        public virtual int OrderReturnId { get; set; }

        [NotNull, Column]
        public virtual string FileName { get; set; }

        [Column]
        public virtual OrderReturnFileType FileTypeId { get; set; }

        [NotNull, Column]
        public virtual string VenueFileId { get; set; }

        [NotNull, Column]
        public virtual byte[] Data { get; set; }

        [NotNull, Column]
        public virtual string Purpose { get; set; }

        [NotNull, Column]
        public virtual string Submitter { get; set; }

        [Association(IsBackReference = false, CanBeNull = false, ThisKey = nameof(OrderReturnId), OtherKey = nameof(Model.OrderReturn.Id))]
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey(nameof(OrderReturnId))]
        public virtual OrderReturn OrderReturn { get; set; }
    }
}
