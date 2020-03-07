using Zoomnews.Database.Models.BaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Zoomnews.Database.Models
{
    public class Audit : IBaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public Guid EntityId { get; set; }

        [Required, MaxLength(200)]
        public string EntityName { get; set; }

        [Required]
        public string Changes { get; set; }

        [Required]
        public DateTime? ChangedAt { get; set; }

        [Required]
        public Guid ChangedByUserId { get; set; }

        [Required, MaxLength(250)]
        public string ChangedByUserName { get; set; }
    }
}
