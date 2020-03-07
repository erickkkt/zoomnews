using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Zoomnews.Common;
using Microsoft.AspNetCore.Authorization;
using Zoomnews.Database.Models;
using Zoomnews.Services.IServices;

namespace Zoomnews.Api.Controllers
{
    /// <summary>
    /// Articles Controller
    /// </summary>
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/articles")]
    [Produces("application/json")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly IArticleService _articleService;
        private readonly ICategoryService _categoryService;
        private readonly IConfiguration _configuration;

        public ArticlesController(IArticleService articleService, ICategoryService categoryService, IConfiguration configuration)
        {
            _articleService = articleService;
            _categoryService = categoryService;
            _configuration = configuration;
        }

        /// <summary>
        /// Get Articles
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Article>>> Index()
        {
            return Ok(await _articleService.GetAllArticles());
        }

        /// <summary>
        /// Get Article per page
        /// </summary>
        /// <param name="page"></param>
        /// <param name="sortExpression"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Page/{page}")]
        public async Task<ICollection<Article>> GetArticlesPaging(int page = 1, string sortExpression = "CreatedDate")
        {
            var model = await _articleService.GetAllArticles(sortExpression, "asc", page, Constants.PageSizeClient);
            return model?.ToList();
        }

        /// <summary>
        /// Get Article by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Article>> GetArticle([FromRoute] string id)
        {
            if (!Guid.TryParse(id, out var articleGuid))
            {
                return BadRequest(id);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var article = await _articleService.GetArticle(articleGuid);

            if (article == null)
            {
                return NotFound();
            }

            return Ok(article);
        }

        /// <summary>
        /// Update Article
        /// </summary>
        /// <param name="id"></param>
        /// <param name="article"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Article>> PutArticle([FromRoute] string id, [FromBody] Article article)
        {
            if (!Guid.TryParse(id, out var articleGuid))
            {
                return BadRequest(id);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (articleGuid != article.Id)
            {
                return BadRequest();
            }

            return Ok(await _articleService.UpdateArticle(article));
        }

        /// <summary>
        /// Create Article
        /// </summary>
        /// <param name="createArticle"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Guid>> PostArticle([FromBody] CreateArticleViewModel createArticle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tags = createArticle.Tags.Trim();

            var article = new Article()
            {
                BriefContent = createArticle.FullContent.Length > 200 ? createArticle.FullContent.Substring(0, 200) + "..." : "",
                CategoryArticleId = Guid.Empty, //No category
                CreatedByName = createArticle.CreatedBy,
                CreatedDate = DateTime.Now,
                Ext = tags,
                FullContent = createArticle.FullContent,
                Image = createArticle.Image,
                IsDeleted = false,
                IsHot = false,
                IsVisible = true,
                Title = createArticle.Title,
                UpdatedByName = createArticle.CreatedBy,
                UpdatedDate = DateTime.Now
            };

            return Ok(await _articleService.CreateArticle(article));
        }

        /// <summary>
        /// Upload photo
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("UploadPhoto")]
        public async Task<ActionResult<string>> UploadPhoto()
        {
            var file = Request.Form.Files[0];
            var imagePath = string.Empty;
            if (file != null && file.Length != 0)
            {
                var path = Path.Combine(
                            Directory.GetCurrentDirectory(), "wwwroot", "Articles", "Uploaded");

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                var fileName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(file.FileName);
                var physicalFileName = Path.Combine(path, fileName);

                using (var stream = new FileStream(physicalFileName, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                var serverUrl = _configuration["ServerAPIUrl"].ToString();
                var imageSrc = string.Format("{0}{1}/{2}/{3}", serverUrl, "Articles", "Uploaded", fileName);

                imagePath = imageSrc;
            }
            return (Ok(imagePath));
        }
    }
}