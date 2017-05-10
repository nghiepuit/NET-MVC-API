using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Model.Models
{
    [Table("Functions")]
    public class Function
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        [Required]
        [MaxLength(50)]
        public string Name { set; get; }

        [Required]
        [MaxLength(256)]
        public string URL { set; get; }

        public int DisplayOrder { set; get; }

        public int? ParentId { set; get; }

        [ForeignKey("ParentId")]
        public virtual Function Parent { set; get; }


        public bool Status { set; get; }

        public string IconCss { get; set; }
    }
}