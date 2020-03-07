using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Zoomnews.Database.Models.BaseModels
{
    public interface IBaseModel
    {
        [Key]
        Guid Id { get; set; }
    }
}
