using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zoomnews.Api.Setting;
using Zoomnews.Common.Authorize;

namespace Zoomnews.Api.Controllers
{
    /// <summary>
    /// Action Roles Resource 
    /// </summary>
    //[Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/action-roles")]
    [Produces("application/json")]
    [ApiController]
    public class ActionRolesController : ControllerBase
    {
        private readonly IOptions<List<ActionRoles>> _actionRoles;
        private readonly IConfiguration _configuration;

        /// <summary>
        /// ActionRolesController
        /// </summary>
        /// <param name="actionRoles"></param>
        /// <param name="configuration"></param>
        public ActionRolesController(
            IOptions<List<ActionRoles>> actionRoles,
            IConfiguration configuration)
        {
            _actionRoles = actionRoles;
            _configuration = configuration;
        }

        /// <summary>
        /// Get List of Action Roles
        /// </summary>
        /// <returns>Return a list of Action Roles</returns>
        [HttpGet]
        public async Task<ActionResult<IReadOnlyCollection<ActionRoles>>> GetActionRoles()
        {
            var result = (List<ActionRoles>)_actionRoles.Value;
            return Ok(result);
        }

        /// <summary>
        /// List of Authorized Navigation Items
        /// </summary>
        /// <returns>List of Authorized Navigation Items</returns>
        [HttpGet("authorized-nav-items")]
        public async Task<ActionResult<IReadOnlyCollection<AuthorizedNavigationItem>>> GetAuthorizedNavigationItems()
        {
            var items = AuthorizedNavigationItem.GetAll(_configuration);
            var actionRoles = _actionRoles.Value;
            var claims = HttpContext.User.Claims.Where(c => c.Type == "role").Select(c => c.Value).Distinct().ToList();
            foreach (var item in items)
            {
                var roles = actionRoles.Where(a => a.Action == item.Action).Select(a => a.Roles).FirstOrDefault();
                if (roles != null && roles.Any(r => claims.Contains(r)))
                {
                    item.IsAuthorized = true;
                    if (item.Children != null && item.Children.Any())
                    {
                        foreach (var child in item.Children)
                        {
                            var rolesForChild = actionRoles.Where(a => a.Action == child.Action).Select(a => a.Roles)
                                .FirstOrDefault();
                            if (rolesForChild != null && rolesForChild.Any(r => claims.Contains(r)))
                            {
                                child.IsAuthorized = true;
                            }
                        }
                    }
                }
            }

            var authorizedItems = items.Where(i => i.IsAuthorized).Select(i =>
                new AuthorizedNavigationItem
                {
                    DisplayName = i.DisplayName,
                    Route = i.Route,
                    IsAuthorized = i.IsAuthorized,
                    Url = i.Url,
                    Children = i.Children?.Where(c => c.IsAuthorized).Select(t =>
                        new AuthorizedNavigationItem
                        {
                            DisplayName = t.DisplayName,
                            Route = t.Route,
                            IsAuthorized = t.IsAuthorized,
                            Url = t.Url,
                        }).ToList(),
                });

            return Ok(authorizedItems);
        }
    }
}