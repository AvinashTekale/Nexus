using Microsoft.AspNetCore.Mvc;
using Nexus.DTOs;
using Nexus.Interfaces;

namespace Nexus.Controllers
{
    public class BMEController : Controller
    {
        private readonly IBMEAccountRepository _repository;

        public BMEController(IBMEAccountRepository repository)
        {
            _repository = repository;
        }
        // GET: BME/Index
        public async Task<IActionResult> Index()
        {
            var userEmail = HttpContext.Session.GetString("UserEmail");
            var user = HttpContext.Session.GetString("UserRole");
            var HospitalId = HttpContext.Session.GetString("HospitalId");
            if (string.IsNullOrEmpty(userEmail) && user != "Hospital")
            {
                return RedirectToAction("Login", "Auth");
            }
            var bmeAccount = await _repository.GetAllAsync(Convert.ToInt32(HospitalId));
            return View(bmeAccount);
        }

        // GET: BME/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BME/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BMEAccountDTO dto)
        {
            if (ModelState.IsValid)
            {
                var HospitalId = HttpContext.Session.GetString("HospitalId");
                dto.HospitalId = Convert.ToInt32(HospitalId);
                await _repository.CreateAsync(dto);
                return RedirectToAction(nameof(Index));
            }
            return View(dto);
        }

        // GET: BME/Edit/{id}
        public async Task<IActionResult> Edit(int id)
        {
            var bmeAccount = await _repository.GetByIdAsync(id);
            if (bmeAccount == null)
            {
                return NotFound();
            }

            var HospitalId = HttpContext.Session.GetString("HospitalId");
            var dto = new BMEAccountDTO
            {
                Name = bmeAccount.Name,
                Phone = bmeAccount.Phone,
                HospitalId = Convert.ToInt32(HospitalId),
                Email = bmeAccount.User?.Email ?? string.Empty,
                Password = string.Empty // Don't expose existing password
            };

            return View(dto);
        }

        // POST: BME/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BMEAccountDTO dto)
        {
            if (ModelState.IsValid)
            {
                var updatedBME = await _repository.UpdateAsync(id, dto);
                if (updatedBME == null)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(dto);
        }

        // GET: BME/Delete/{id}
        public async Task<IActionResult> Delete(int id)
        {
            var bmeAccount = await _repository.GetByIdAsync(id);
            if (bmeAccount == null)
            {
                return NotFound();
            }
            return View(bmeAccount);
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
