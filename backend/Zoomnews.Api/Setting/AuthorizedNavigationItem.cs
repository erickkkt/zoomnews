using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace Zoomnews.Api.Setting
{
    /// <summary>
    /// Admin Portal Side Navigation Items
    /// </summary>
    public class AuthorizedNavigationItem
    {
        public string DisplayName { get; set; }
        public string Route { get; set; }

        public string Action { get; set; }

        public bool IsAuthorized { get; set; }

        public string Url { get; set; }

        public IList<AuthorizedNavigationItem> Children { get; set; }

        /// <summary>
        /// Get all authorized navigation items
        /// </summary>
        /// <returns>List of AuthorizedNavigationItem</returns>
        public static IList<AuthorizedNavigationItem> GetAll(IConfiguration configuration)
        {
            var items = new List<AuthorizedNavigationItem>
            {
                new AuthorizedNavigationItem
                {
                  DisplayName = "Categories",
                  Route = "categories",
                  Action = "categories-management",
                },
                 new AuthorizedNavigationItem
                {
                  DisplayName = "Articles",
                  Route = "articles",
                  Action = "articles-management",
                },
                  new AuthorizedNavigationItem
                {
                  DisplayName = "Comments",
                  Route = "comments",
                  Action = "comments-management",
                },

                    new AuthorizedNavigationItem
                {
                  DisplayName = "Medias",
                  Route = "medias",
                  Action = "medias-management",
                },
                    new AuthorizedNavigationItem
                {
                  DisplayName = "Settings",
                  Route = "settings",
                  Action = "settings-management",
                },
            };
            return items;
        }
    }
}
