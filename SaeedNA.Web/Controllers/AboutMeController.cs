using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SaeedNA.Application.ViewModels;
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
        private readonly IPersonalInfoService _personalService;

        public AboutMeController(IHistoryService historyService, ISkillService skillService, ICounterService counterService, IMSService myService, IPersonalInfoService personalService)
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
            var data = new AboutMeViewModel() 
            {
                History = await _historyService.FilterHistory(new HistoryFilterDTO { IsDescending = true }),
                Skill = await _skillService.FilterSkill(new SkillFilterDTO()),
                Counter = await _counterService.FilterCounter(new CounterFilterDTO()),
                MyService = await _myService.FilterService(new MyServiceFilterDTO()),
                PersonalInfos = await _personalService.GetDefaultInfo()
            };

            return View("Index",data);
        }

        #endregion
    }
}
