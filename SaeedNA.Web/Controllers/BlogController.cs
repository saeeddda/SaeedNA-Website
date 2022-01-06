using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language;
using SaeedNA.Application.Utilities;
using SaeedNA.Data.DTOs.Common;
using SaeedNA.Data.DTOs.Pages;
using SaeedNA.Service.Interfaces;

namespace SaeedNA.Web.Controllers
{
    public class BlogController : Controller
    {
        private readonly IPostService _postService;
        private readonly ISiteSettingService _settingService;
        private readonly IPortfolioService _portfolioService;

        public BlogController(IPostService postService, ISiteSettingService settingService, IPortfolioService portfolioService)
        {
            _postService = postService;
            _settingService = settingService;
            _portfolioService = portfolioService;
        }

        [HttpGet("blog")]
        public async Task<IActionResult> Article(PostFilterDTO filter)
        {
            filter.State = PagesPublishState.Published;
            return View("Article", await _postService.FilterPost(filter));
        }

        [HttpGet("post/{id}/{title}")]
        public async Task<IActionResult> Single(long id, string title)
        {
            var post = await _postService.GetPostShow(id);
            return View("ArticleSingle",post);
        }

        [HttpGet("portfolio")]
        public async Task<IActionResult> Project(PortfolioFilterDTO filter)
        {
            filter.State = PagesPublishState.Published;
            return View("Portfolio",await _portfolioService.FilterPortfolio(filter));
        }
    }
}
