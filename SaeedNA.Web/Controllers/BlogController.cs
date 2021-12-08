using Microsoft.AspNetCore.Mvc;
using SaeedNA.Application.Configuration;
using SaeedNA.Service.Repositories;

namespace SaeedNA.Web.Controllers
{
    public class BlogController : Controller
    {
        private readonly IPostService _post;
        private readonly SettingManager _settingManager;

        public BlogController(
            IPostService post, 
            ISettingService siteSettings)
        {
            _post = post;
            _settingManager = new SettingManager(siteSettings);
        }

        [Route("blog/{page?}")]
        public IActionResult Article(int page = 1)
        {
            var set = _settingManager.GetAllSettings();

            //Site Settings
            ViewBag.SiteLogo = set.SiteLogo;
            ViewBag.SiteFavIcon = set.SiteFavIcon;
            ViewBag.SiteColor = set.SiteColor;
            ViewBag.SiteMode = set.SiteMode;
            ViewBag.SiteTitle = set.SiteTitle;
            ViewBag.SiteUrl = set.SiteUrl;
            ViewBag.MetaTags = set.MetaTags.Split(',');
            ViewBag.MetaDescription = set.MetaDescription;
            ViewBag.GoogleAnalytics = set.GoogleAnalytics;
            ViewBag.MainMenu = set.MainMenu;
            ViewBag.PortfolioMenu = set.PortfolioMenu;
            ViewBag.BlogMenu = set.BlogMenu;
            ViewBag.ContactMeMenu = set.ContactMeMenu;
            ViewBag.AboutMeMenu = set.AboutMeMenu;
          
            var take = 6;
            var skip = (page - 1) * take;

            var postCount = _post.GetAllPostCount();

            ViewBag.PageCount = (postCount / take)+1;
            ViewBag.Page = page;

            var post = _post.GetAllPostDes(skip, take);
            return View("Article", post);
        }

        [Route("post/{id}/{title}")]
        public IActionResult Single(string id, string title)
        {
            var set = _settingManager.GetAllSettings();

            //Site Settings
            ViewBag.SiteLogo = set.SiteLogo;
            ViewBag.SiteFavIcon = set.SiteFavIcon;
            ViewBag.SiteColor = set.SiteColor;
            ViewBag.SiteMode = set.SiteMode;
            ViewBag.SiteTitle = set.SiteTitle;
            ViewBag.SiteUrl = set.SiteUrl;
            ViewBag.MetaTags = set.MetaTags.Split(',');
            ViewBag.MetaDescription = set.MetaDescription;
            ViewBag.GoogleAnalytics = set.GoogleAnalytics;
            ViewBag.MainMenu = set.MainMenu;
            ViewBag.PortfolioMenu = set.PortfolioMenu;
            ViewBag.BlogMenu = set.BlogMenu;
            ViewBag.ContactMeMenu = set.ContactMeMenu;
            ViewBag.AboutMeMenu = set.AboutMeMenu;

            if(string.IsNullOrEmpty(id))
                return BadRequest();

            ViewBag.SiteTitle = title;

            var post = _post.GetPostByIdWithCategory(int.Parse(id));

            if(post == null)
                return NotFound();

            post.PostVisit += 1;
            _post.UpdatePost(post);

            return View("ArticleSingle", post);
        }

        [Route("portfolio/{page?}")]
        public IActionResult Project(int page = 1)
        {
            var set = _settingManager.GetAllSettings();

            //Site Settings
            ViewBag.SiteLogo = set.SiteLogo;
            ViewBag.SiteFavIcon = set.SiteFavIcon;
            ViewBag.SiteColor = set.SiteColor;
            ViewBag.SiteMode = set.SiteMode;
            ViewBag.SiteTitle = set.SiteTitle;
            ViewBag.SiteUrl = set.SiteUrl;
            ViewBag.MetaTags = set.MetaTags.Split(',');
            ViewBag.MetaDescription = set.MetaDescription;
            ViewBag.GoogleAnalytics = set.GoogleAnalytics;
            ViewBag.MainMenu = set.MainMenu;
            ViewBag.PortfolioMenu = set.PortfolioMenu;
            ViewBag.BlogMenu = set.BlogMenu;
            ViewBag.ContactMeMenu = set.ContactMeMenu;
            ViewBag.AboutMeMenu = set.AboutMeMenu;

            //var take = 6;
            //var skip = (page - 1) * take;

            //var postCount = _post.GetAllPortfolioCount();

            //ViewBag.PageCount = (postCount / take) + 1;
            //ViewBag.Page = page;

            var post = _post.GetPortfolioWithCategory();
            return View("Portfolio", post);
        }

        [Route("portfolio/single/{id}/{title}")]
        public IActionResult Portfolio(string id, string title)
        {
            var set = _settingManager.GetAllSettings();

            //Site Settings
            ViewBag.SiteLogo = set.SiteLogo;
            ViewBag.SiteFavIcon = set.SiteFavIcon;
            ViewBag.SiteColor = set.SiteColor;
            ViewBag.SiteMode = set.SiteMode;
            ViewBag.SiteTitle = set.SiteTitle;
            ViewBag.SiteUrl = set.SiteUrl;
            ViewBag.MetaTags = set.MetaTags;
            ViewBag.MetaDescription = set.MetaDescription;
            ViewBag.GoogleAnalytics = set.GoogleAnalytics;
            ViewBag.MainMenu = set.MainMenu;
            ViewBag.PortfolioMenu = set.PortfolioMenu;
            ViewBag.BlogMenu = set.BlogMenu;
            ViewBag.ContactMeMenu = set.ContactMeMenu;
            ViewBag.AboutMeMenu = set.AboutMeMenu;

            if(string.IsNullOrEmpty(id))
                return BadRequest();

            ViewBag.SiteTitle = title;

            var post = _post.GetPortfolioByIdWithCategory(int.Parse(id));

            if(post == null)
                return NotFound();

            post.PostVisit += 1;
            _post.UpdatePost(post);

            return View("PortfolioSingle", post);
        }
    }
}
