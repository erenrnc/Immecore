using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PeopleApi.Business;

namespace ImmedisWeb.Controllers
{
    [Authorize]
    public class DepartmentController : Controller
    {
        private readonly DepartmentBusiness _business;
        private readonly ILogger<DepartmentController> _logger;

        public DepartmentController(ILogger<DepartmentController> logger, DepartmentBusiness business)
        {
            _logger = logger;
            _business = business;
        }

        public async Task<IActionResult> Index()
        {
            var list = new List<DepartmentDto>();
            list = await _business.GetList();
            return View(list);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            DepartmentDto dto = await _business.GetById(Id);
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(DepartmentDto dto)
        {
            var res = await _business.Update(dto);
            if (res.Success)
                return RedirectToAction("Index");
            else
                return View(res);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new DepartmentDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create(DepartmentDto dto)
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
