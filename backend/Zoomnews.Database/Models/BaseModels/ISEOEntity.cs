using System;
using System.Collections.Generic;
using System.Text;

namespace Zoomnews.Database.Models.BaseModels
{
    public interface ISEOEntity
    {
        string SEOName { get; set; }

        string SEOTitle { get; set; }

        string SEODescription { get; set; }

        string SEOKeywords { get; set; }
    }
}
