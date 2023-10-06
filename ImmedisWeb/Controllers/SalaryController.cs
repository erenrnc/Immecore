using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PeopleApi.Business;

namespace ImmedisWeb.Controllers
{
    [Authorize]
    public class SalaryController : Controller
    {
        private readonly SalaryBusiness _business;
        private readonly PeopleBusiness _businessPeople;
        private readonly ILogger<SalaryController> _logger;

        public SalaryController(ILogger<SalaryController> logger, SalaryBusiness business, PeopleBusiness businessPeople)
        {
            _logger = logger;
            _business = business;
            _businessPeople = businessPeople;
        }

        public async Task<IActionResult> Index(int peopleId)
        {
            var list = new List<SalaryDto>();
            list = await _business.GetList(peopleId);
            ViewBag.PeopleId = peopleId;
            return View(list);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int Id, int peopleId)
        {
            SalaryDto dto = await _business.GetById(Id);
            dto.People = await _businessPeople.GetById(peopleId);
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SalaryDto dto)
        {
            var res = await _business.Update(dto);
            if (res.Success)
                return RedirectToAction("Index", new { peopleId = dto.PeopleId });
            else
                return View(res);
        }

        [HttpGet]
        public async Task<IActionResult> Create(int peopleId)
        {
            return View(new SalaryDto
            {
                PeopleId = peopleId,
                People = await _businessPeople.GetById(peopleId)
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(SalaryDto dto)
        {
            var res = await _business.Insert(dto);
            if (res.Success)
                return RedirectToAction("Index", new { peopleId = dto.PeopleId });
            else
                return View(res);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int Id, int peopleId)
        {
            var res = await _business.Delete(Id);
            if (res.Success)
                return RedirectToAction("Index", new { peopleId = peopleId });
            else
                return View(res);
        }
    }
}
