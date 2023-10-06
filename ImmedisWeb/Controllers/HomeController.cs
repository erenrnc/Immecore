using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PeopleApi.Business;

namespace ImmedisWeb.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly PeopleBusiness _business;
        private readonly ILogger<HomeController> _logger;
        private readonly DepartmentBusiness _businessDepartment;

        public HomeController(ILogger<HomeController> logger, PeopleBusiness business, DepartmentBusiness businessDepartment)
        {
            _logger = logger;
            _business = business;
            _businessDepartment = businessDepartment;
        }

        public async Task<IActionResult> Index()
        {
            var list = new List<PeopleDto>();
            list = await _business.GetList();
            return View(list);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            PeopleDto dto = await _business.GetById(Id);
            dto.DepartmentList = await _businessDepartment.GetList();
            dto.Department.Id = dto.DepartmentId;
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PeopleDto dto)
        {
            var res = await _business.Update(dto);
            if (res.Success)
                return RedirectToAction("Index");
            else
                return View(res);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View(new PeopleDto { DepartmentList = await _businessDepartment.GetList() });
        }

        [HttpPost]
        public async Task<IActionResult> Create(PeopleDto dto)
        {
            var res = await _business.Insert(dto);
            if (res.Success)
                return RedirectToAction("Index");
            else
                return View(res);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int Id)
        {
            var res = await _business.Delete(Id);
            if (res.Success)
                return RedirectToAction("Index");
            else
                return View(res);
        }
    }
}
