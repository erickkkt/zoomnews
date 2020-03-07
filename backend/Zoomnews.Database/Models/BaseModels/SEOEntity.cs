using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Zoomnews.Database.Models.BaseModels
{
    public class SEOEntity: ISEOEntity
    {
        [StringLength(4000)]
        public string SEOName { get; set; }

        [StringLength(4000)]
        public string SEOTitle { get; set; }

        [StringLength(4000)]
        public string SEODescription { get; set; }

        [StringLength(4000)]
        public string SEOKeywords { get; set; }
    }
}
