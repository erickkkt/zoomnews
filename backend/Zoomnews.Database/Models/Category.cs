using Zoomnews.Database.Models.BaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Zoomnews.Database.Models
{
    public class Category: IBaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid ParentId { get; set; }


        [StringLength(256)]
        public string ParentName { get; set; }

        [Required]

        [StringLength(256)]
        public string Name { get; set; }

        //Type = Controller. It should set default by developer.
        [Required]

        [StringLength(20)]
        public string Type { get; set; }

        [Required]

        [StringLength(80)]
        public string Language { get; set; }


        [StringLength(4000)]
        public string Description { get; set; }


        [StringLength(256)]
        public string Image { get; set; }


        [StringLength(256)]
        public string Url { get; set; }

        public bool HasChildren { get; set; }

        public bool HasUrl { get; set; }

        public int Index { get; set; }

        [ForeignKey("ParentId")]
        public virtual Category ParentCategory { get; set; }
        public virtual ICollection<Category> Children { get; set; }
    }
}
