using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Zoomnews.Api.Controllers
{
    /// <summary>
    /// All authentication configuration 
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/configuration")]
    [Produces("application/json")]
    public class ConfigurationController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private class Configuration
        {
            public string AdminPortalUrl { get; set; }

            public string ApiBaseUrl { get; set; }

            public string ClientId { get; set; }

            public string IdentityServerAddress { get; set; }

            public string RedirectUrl { get; set; }

            public string Scope { get; set; }

            public string SilentRefreshUrl { get; set; }
        }

        /// <summary>
        /// Configuration Constructor
        /// </summary>
        /// <param name="configuration"></param>
        public ConfigurationController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// All Admin settings for Authentication and Authorization
        /// </summary>
        /// <returns>Configuration type</returns>
        [HttpGet("admin")]
        public IActionResult GetLeiAdminConfiguration()
        {
            var configuration = new Configuration
            {
                ApiBaseUrl = _configuration["Admin:ApiBaseUrl"],
                ClientId = _configuration["Admin:ClientId"],
                IdentityServerAddress = _configuration["IdentityServerAuthentication:Authority"],
                RedirectUrl = _configuration["Admin:BaseUrl"],
                Scope = _configuration["Admin:Scope"],
                SilentRefreshUrl = _configuration["Admin:SilentRefreshUrl"]
            };

            return Ok(configuration);
        }
    }
}
