using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SaeedNA.Domain.Models.Resume;
using SaeedNA.Framework.Configuration;
using SaeedNA.Service.Repositories;

namespace SaeedNA.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class SkillController : Controller
    {
        #region Ctor

        private readonly ISkillService _skill;
        private readonly SettingManager _settingManager;

        public SkillController(ISkillService skill,ISettingService siteSettings)
        {
            _skill = skill;
            _settingManager = new SettingManager(siteSettings);
        }

        #endregion

        #region Skill Actions

        public IActionResult Index()
        {
            var set = _settingManager.GetAllSettings();

            ViewBag.SiteFavIcon = set.SiteFavIcon;
            ViewBag.SiteLogo = set.SiteLogo;
            ViewBag.FullName = set.FullName;
            ViewBag.AvatarImage = set.AvatarImage;

            var skill = _skill.GetAllSkill();
            return View("Index",skill);
        }

        public IActionResult Add()
        {
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(Skill skill)
        {
            if(ModelState.IsValid)
            {
                if(skill == null)
                    return BadRequest();

                _skill.AddSkill(skill);
                return RedirectToAction("Index");

            }

            ModelState.Clear();
            ModelState.AddModelError("skill","مشکلی به وجود آمده!");

            return PartialView("Add",skill);
        }

        public IActionResult Edit(string id)
        {
            if(string.IsNullOrEmpty(id))
                return BadRequest();

            var skill = _skill.GetSkillById(int.Parse(id));

            if(skill == null)
                return NotFound();

            return PartialView("Edit",skill);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Skill skill)
        {
            if(ModelState.IsValid)
            {
                if(skill == null)
                    return BadRequest();

                _skill.UpdateSkill(skill);
                return RedirectToAction("Index");
            }

            ModelState.Clear();
            ModelState.AddModelError("skill", "مشکلی به وجود آمده!");

            return PartialView("Edit", skill);
        }

        public JsonResult Delete(string id)
        {
            if(id == "" || string.IsNullOrEmpty(id))
                return Json(new { status = "error", msg = "شناسه نامعتبر است!" });

            var skill = _skill.GetSkillById(int.Parse(id));

            if(skill == null)
                return Json(new { status = "error", msg = "تجربه پیدا نشد!" });

            _skill.DeleteSkill(skill.SkillId);

            return Json(new { status = "ok", msg = "مورد پاک شد." });
        }

        #endregion
    }
}
