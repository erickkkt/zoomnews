using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Zoomnews.Database.Models.BaseModels
{
    public class AuditEntity : IAuditEntity
    {
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public Guid? CreatedById { get; set; }
        [StringLength(256)]
        public string CreatedByName { get; set; }
        public Guid? UpdatedById { get; set; }
        [StringLength(256)]
        public string UpdatedByName { get; set; }
        public Guid? DeletedById { get; set; }
        [StringLength(256)]
        public string DeletedByName { get; set; }
    }
}
