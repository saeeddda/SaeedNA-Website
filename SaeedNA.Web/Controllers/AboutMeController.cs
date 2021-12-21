using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SaeedNA.Data.DTOs.MService;
using SaeedNA.Data.DTOs.Resume;
using SaeedNA.Service.Interfaces;

namespace SaeedNA.Web.Controllers
{
    public class AboutMeController : Controller
    {
        #region constractor

        private readonly IHistoryService _historyService;
        private readonly ISkillService _skillService;
        private readonly ICounterService _counterService;
        private readonly IMSService _myService;
        private readonly IPersonalService _personalService;

        public AboutMeController(IHistoryService historyService, ISkillService skillService, ICounterService counterService, IMSService myService, IPersonalService personalService)
        {
            _historyService = historyService;
            _skillService = skillService;
            _counterService = counterService;
            _myService = myService;
            _personalService = personalService;
        }

        #endregion

        #region actions

        [HttpGet("about-me")]
        public async Task<IActionResult> Index()
        {
            ViewBag.Histories = await _historyService.FilterHistory(new HistoryFilterDTO{IsDescending = true});
            ViewBag.Skills = await _skillService.FilterSkill(new SkillFilterDTO());
            ViewBag.Counters = await _counterService.FilterCounter(new CounterFilterDTO());
            ViewBag.MyServices = await _myService.FilterService(new MyServiceFilterDTO());
            ViewBag.PersonalInfo = await _personalService.GetDefaultInfo();

            return View("Index");
        }

        #endregion
    }
}
