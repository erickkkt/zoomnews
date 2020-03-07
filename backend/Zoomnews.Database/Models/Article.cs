using Microsoft.AspNetCore.Http;
using Zoomnews.Database.Models.BaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Zoomnews.Database.Models
{
    public class Article : BaseEntity, IBaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid CategoryArticleId { get; set; }

        public int ViewCount { get; set; }


        [StringLength(256)]
        public string Image { get; set; }

        [Required]
        [StringLength(256)]
        public string Title { get; set; }


        [StringLength(4000)]
        public string BriefContent { get; set; }

        public string FullContent { get; set; }


        [StringLength(256)]
        public string Source { get; set; }

        public int? Index { get; set; }

        [Required]
        public bool IsVisible { get; set; }

        public bool IsHot { get; set; }

        public int? Position { get; set; }

        public string Ext { get; set; }

        public string Ext1 { get; set; }

        public string Ext2 { get; set; }

        public string Ext3 { get; set; }

        [ForeignKey("CategoryArticleId")]
        public virtual Category Category { get; set; }
    }

    public class ArticleViewModel : Article
    {
        public IFormFile PresentImage { get; set; }
        public string Author { get; set; }
    }

    public class CreateArticleViewModel
    {
        public string Title { get; set; }
        public string CreatedBy { get; set; }
        public string Tags { get; set; }
        public string Image { get; set; }
        public string FullContent { get; set; }
    }

    public class ArticleImageViewModel
    {
        public IFormFile Image { get; set; }
        //public string source { get; set; }
        //public long Size { get; set; }
        //public int Width { get; set; }
        //public int Height { get; set; }
        //public string Extension { get; set; }
    }
}
