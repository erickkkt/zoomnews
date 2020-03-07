using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Zoomnews.Database.Models.BaseModels
{
    public interface IAuditEntity
    {
        DateTime? CreatedDate { get; set; }
        Guid? CreatedById { get; set; }

        [StringLength(256)]
        string CreatedByName { get; set; }

        DateTime? UpdatedDate { get; set; }
        Guid? UpdatedById { get; set; }

        [StringLength(256)]
        string UpdatedByName { get; set; }

        bool? IsDeleted { get; set; }
        DateTime? DeletedDate { get; set; }
        Guid? DeletedById { get; set; }
        [StringLength(256)]
        string DeletedByName { get; set; }
    }
}
