using Zoomnews.Database.Models.BaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Zoomnews.Database.Models
{
    public class Media : BaseEntity, IBaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid CategoryMediaId { get; set; }


        [StringLength(256)]
        public string CategoryMediaName { get; set; }


        [StringLength(256)]
        public string Name { get; set; }


        [StringLength(256)]
        public string Source { get; set; }


        [StringLength(20)]
        public string Type { get; set; }


        [StringLength(256)]
        public string Ext { get; set; }

        public Guid ReferenceId { get; set; }


        [StringLength(20)]
        public string ReferenceType { get; set; }


        [StringLength(256)]
        public string Description { get; set; }


        [StringLength(256)]
        public string Link { get; set; }


        [StringLength(256)]
        public string ReferenceSource { get; set; }

        [ForeignKey("CategoryMediaId")]
        public virtual Category Category { get; set; }
    }
}
