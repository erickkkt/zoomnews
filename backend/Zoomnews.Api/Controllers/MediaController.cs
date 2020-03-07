using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zoomnews.Database.Data;
using Zoomnews.Database.Models;
using Zoomnews.Services.IServices;

namespace Zoomnews.Api.Controllers
{
    /// <summary>
    /// Media Controller
    /// </summary>
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/media")]
    [Produces("application/json")]
    [ApiController]
    public class MediaController : ControllerBase
    {
        private readonly IMediaService _mediaService;

        public MediaController(IMediaService mediaService)
        {
            _mediaService = mediaService;
        }

        /// <summary>
        /// Get medias
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Media>>> GetMedias()
        {
            return Ok(await _mediaService.GetAllMedias());
        }

        /// <summary>
        /// Get media by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Media>> GetMedia([FromRoute] string id)
        {
            if (!Guid.TryParse(id, out var mediaGuid))
            {
                return BadRequest(id);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var media = await _mediaService.GetMedia(mediaGuid);

            if (media == null)
            {
                return NotFound();
            }

            return Ok(media);
        }

        /// <summary>
        /// Update media
        /// </summary>
        /// <param name="id"></param>
        /// <param name="media"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Media>> PutMedia([FromRoute] string id, [FromBody] Media media)
        {
            if (!Guid.TryParse(id, out var mediaGuid))
            {
                return BadRequest(id);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (mediaGuid != media.Id)
            {
                return BadRequest();
            }


            return Ok(await _mediaService.UpdateMedia(media));
        }

        /// <summary>
        /// Create media
        /// </summary>
        /// <param name="media"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Guid>> PostMedia([FromBody] Media media)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(await _mediaService.CreateMedia(media));
        }

        /// <summary>
        /// Delete media
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<bool>> DeleteMedia([FromRoute] string id)
        {
            if (!Guid.TryParse(id, out var mediaGuid))
            {
                return BadRequest(id);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _mediaService.DeleteMedia(mediaGuid));
        }
    }
}