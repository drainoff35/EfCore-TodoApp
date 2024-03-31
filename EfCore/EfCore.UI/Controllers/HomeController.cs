using EfCore.Business.Interfaces;
using EfCore.Common.ResponseObjects;
using EfCore.Dtos.WorkDtos;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EfCore.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWorkService _workService;


        public HomeController(IWorkService workService)
        {
            _workService = workService;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _workService.GetAll();
            return View(response.Data);
        }

        public IActionResult Create()
        {
            return View(new WorkCreateDto());
        }
        [HttpPost]
        public async Task<IActionResult> Create(WorkCreateDto workCreateDto)
        {

            var response = await _workService.Create(workCreateDto);
            if (response.ResponseType == ResponseType.ValidationError)
            {
                foreach (var error in response.ValidationErrors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return View(workCreateDto);

            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public async Task<IActionResult> Update(int id)
        {
            var response = await _workService.GetById<WorkUpdateDto>(id);
            if (response.ResponseType == ResponseType.NotFound)
                return NotFound();
            return View(response.Data);
        }
        [HttpPost]
        public async Task<IActionResult> Update(WorkUpdateDto workUpdateDto)
        {
            var response = await _workService.Update(workUpdateDto);
            if (response.ResponseType == ResponseType.ValidationError)
            {
                foreach (var error in response.ValidationErrors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return View(workUpdateDto);
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _workService.Delete(id);
            if (response.ResponseType == ResponseType.NotFound) { return NotFound(); }
            return RedirectToAction("Index");
        }
    }
}
