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
    /// Comments Controller
    /// </summary>
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/comments")]
    [Produces("application/json")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        /// <summary>
        /// Get comments
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Comment>>> GetComments()
        {
            return Ok(await _commentService.GetAllComments());
        }

        /// <summary>
        /// Get Comment
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Comment>> GetComment([FromRoute] string id)
        {
            if (!Guid.TryParse(id, out var commentGuid))
            {
                return BadRequest(id);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var comment = await _commentService.GetComment(commentGuid);

            if (comment == null)
            {
                return NotFound();
            }

            return Ok(comment);
        }

        /// <summary>
        /// Update comment
        /// </summary>
        /// <param name="id"></param>
        /// <param name="comment"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Comment>> PutComment([FromRoute] string id, [FromBody] Comment comment)
        {
            if (!Guid.TryParse(id, out var commentGuid))
            {
                return BadRequest(id);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (commentGuid != comment.Id)
            {
                return BadRequest();
            }

            return Ok(await _commentService.UpdateComment(comment));
        }

        /// <summary>
        /// Create comment
        /// </summary>
        /// <param name="comment"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Guid>> PostComment([FromBody] Comment comment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            comment.CreatedDate = DateTime.Now;
            return Ok(await _commentService.CreateComment(comment));
        }

        /// <summary>
        /// Delete comment
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<bool>> DeleteComment([FromRoute] string id)
        {
            if (!Guid.TryParse(id, out var commentGuid))
            {
                return BadRequest(id);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _commentService.DeleteComment(commentGuid));
        }
    }
}