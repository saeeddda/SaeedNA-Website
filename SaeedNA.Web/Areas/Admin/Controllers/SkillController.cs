using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SaeedNA.Data.DTOs.Common;
using SaeedNA.Data.DTOs.Resume;
using SaeedNA.Service.Interfaces;
using System.Threading.Tasks;

namespace SaeedNA.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class SkillController : Controller
    {
        #region Ctor

        private readonly ISkillService _skillService;

        public SkillController(ISkillService skillService)
        {
            _skillService = skillService;
        }

        #endregion

        #region Skill Actions

        public async Task<IActionResult> Index(SkillFilterDTO filter)
        {
            return View("Index", await _skillService.FilterSkill(filter));
        }

        public IActionResult Add()
        {
            return PartialView();
        }

        [HttpPost,ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(SkillCreateDTO skill)
        {
            if(ModelState.IsValid)
            {
                if(skill == null)
                    return BadRequest();

                await _skillService.AddNewSkill(skill);
                return RedirectToAction("Index");

            }

            ModelState.Clear();
            ModelState.AddModelError("skill","مشکلی به وجود آمده!");

            return PartialView("Add",skill);
        }

        public async Task<IActionResult> Edit(long id)
        {
            var skill = _skillService.GetSkillById(id);

            if(skill == null)
                return NotFound();

            return PartialView("Edit",skill);
        }

        [HttpPost,ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SkillEditDTO skill)
        {
            if(ModelState.IsValid)
            {
                if(skill == null)
                    return BadRequest();

                await _skillService.EditSkill(skill);
                return RedirectToAction("Index");
            }

            ModelState.Clear();
            ModelState.AddModelError("skill", "مشکلی به وجود آمده!");

            return PartialView("Edit", skill);
        }

        public async Task<JsonResult> Delete(long id)
        {
            var result = await _skillService.DeleteSkill(id);

            switch(result)
            {
                case ServiceResult.NotFond:
                    return Json(new { status = "error", msg = "Skill not found!" });
                case ServiceResult.Error:
                    return Json(new { status = "error", msg = "ID not valid!" });
                case ServiceResult.Success:
                    return Json(new { status = "ok", msg = "Skill deleted" });
                default:
                    return Json(new { status = "", msg = "" });
            }
        }

        #endregion
    }
}
