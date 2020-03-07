using Zoomnews.Database.Models.BaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Zoomnews.Database.Models
{
    public class Comment : AuditEntity, IBaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid ParentId { get; set; }
        public string Type { get; set; }

        [Required]
        public bool IsApproved { get; set; }

        [Required]

        [StringLength(4000)]
        public string Content { get; set; }


        [StringLength(4000)]
        public string Ext { get; set; }

        public Guid ReferenceId { get; set; }


        [StringLength(20)]
        public string ReferenceType { get; set; }

        [ForeignKey("ParentId")]
        public virtual Comment ParentComment { get; set; }

        public virtual ICollection<Comment> Children { get; set; }
    }
}
