using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zoomnews.Database.Data;
using Zoomnews.Database.Models;
using Zoomnews.Services.IServices;

namespace Zoomnews.Api.Controllers
{
    /// <summary>
    /// Categories Controller
    /// </summary>
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/categories")]
    [Produces("application/json")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        /// <summary>
        /// Get Categories
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            return Ok(await _categoryService.GetAllCategories());
        }

        /// <summary>
        /// Get Category
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Category>> GetCategory([FromRoute] string id)
        {
            if (!Guid.TryParse(id, out var categoryGuid))
            {
                return BadRequest(id);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var category = await _categoryService.GetCategory(categoryGuid);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        /// <summary>
        /// Update Category
        /// </summary>
        /// <param name="id"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Category>> PutCategory([FromRoute] string id, [FromBody] Category category)
        {
            if (!Guid.TryParse(id, out var categoryGuid))
            {
                return BadRequest(id);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (categoryGuid != category.Id)
            {
                return BadRequest();
            }

            return Ok(await _categoryService.UpdateCategory(category));
        }

        /// <summary>
        /// Create Category
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<string>> PostCategory([FromBody] Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(await _categoryService.CreateCategory(category));
        }

        /// <summary>
        /// Delete Category
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<bool>> DeleteCategory([FromRoute] string id)
        {
            if (!Guid.TryParse(id, out var categoryGuid))
            {
                return BadRequest(id);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var category = await _categoryService.GetCategory(categoryGuid);
            if (category == null)
            {
                return NotFound();
            }

            return Ok(await _categoryService.DeleteCategory(categoryGuid));
        }
    }
}