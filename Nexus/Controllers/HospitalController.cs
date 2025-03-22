using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nexus.DTOs;
using Nexus.Interfaces;

namespace Nexus.Controllers
{
    public class HospitalController : BaseAdminController
    {
        private readonly IHospitalRepository _repository;

        public HospitalController(IHospitalRepository repository)
        {
            _repository = repository;
        }
        // GET: Hospital/Index
        public async Task<IActionResult> Index()
        {
            var userEmail = HttpContext.Session.GetString("UserEmail");
            var user = IsAdminUser();
            if (string.IsNullOrEmpty(userEmail) && user)
            {
                return RedirectToAction("Login", "Auth");
            }
            var hospitals = await _repository.GetAllAsync();
            return View(hospitals);
        }

        // GET: Hospital/Create
        public IActionResult Create()
        {
            var states = _repository.GetStates(); // Assuming GetStates() returns a list of states
            ViewBag.States = new SelectList(states, "StateID", "StateName");
            return View();
        }

        // GET: Fetch cities based on state
        public JsonResult GetCities(int stateId)
        {
            // Fetch cities for the selected state
            var cities = _repository.GetCitiesByState(stateId); // Assuming GetCitiesByState() returns a list of cities for the state
            return Json(cities);
        }
        // POST: Hospital/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(HospitalDTO dto)
        {
            if (ModelState.IsValid)
            {
                await _repository.CreateAsync(dto);
                return RedirectToAction(nameof(Index));
            }
            return View(dto);
        }

        // GET: Hospital/Edit/{id}
        public async Task<IActionResult> Edit(int id)
        {
            var hospital = await _repository.GetByIdAsync(id);
            if (hospital == null)
            {
                return NotFound();
            }

            var dto = new HospitalDTO
            {
                Name = hospital.Name,
                Phone = hospital.Phone,
                Address = hospital.Address,
                CityId = hospital.CityId,
                StateId = hospital.City.StateID,
                NumberOfBME = hospital.NumberOfBME,
                NumberOfBeds = hospital.NumberOfBeds,
                Email = hospital.User?.Email ?? string.Empty,
                Password = string.Empty // Don't expose existing password
            };

            return View(dto);
        }

        // POST: Hospital/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, HospitalDTO dto)
        {
            if (ModelState.IsValid)
            {
                var updatedHospital = await _repository.UpdateAsync(id, dto);
                if (updatedHospital == null)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(dto);
        }

        // GET: Hospital/Delete/{id}
        public async Task<IActionResult> Delete(int id)
        {
            var hospital = await _repository.GetByIdAsync(id);
            if (hospital == null)
            {
                return NotFound();
            }
            return View(hospital);
        }

        // POST: Hospital/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _repository.DeleteAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
