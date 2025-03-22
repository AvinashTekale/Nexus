using Microsoft.AspNetCore.Mvc;
using Nexus.Entities;
using Nexus.Interfaces;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Nexus.Controllers
{
    public class EquipmentCategoryController : Controller
    {
        private readonly IEquipmentCategoryRepository _equipmentCategoryRepository;
        private readonly IAuthRepository _authRepository;

        public EquipmentCategoryController(
            IEquipmentCategoryRepository equipmentCategoryRepository,
            IAuthRepository authRepository)
        {
            _equipmentCategoryRepository = equipmentCategoryRepository;
            _authRepository = authRepository;
        }

        // GET: EquipmentCategory/Index
        public async Task<IActionResult> Index()
        {
            if (!_authRepository.IsAuthorized())
                return RedirectToAction("Login", "Auth");

            var hospitalId = HttpContext.Session.GetString("HospitalId");
            if (int.TryParse(hospitalId, out var id))
            {
                var equipmentList = await _equipmentCategoryRepository.GetAllAsync(id);
                return View(equipmentList);
            }

            return RedirectToAction("Login", "Auth");
        }

        // GET: EquipmentCategory/Create
        public IActionResult Create()
        {
            if (!_authRepository.IsAuthorized())
                return RedirectToAction("Login", "Auth");

            return View();
        }

        // POST: EquipmentCategory/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EquipmentCategory equipmentCategory)
        {
            if (!_authRepository.IsAuthorized())
                return RedirectToAction("Login", "Auth");

            var hospitalId = HttpContext.Session.GetString("HospitalId");
            if (int.TryParse(hospitalId, out var id))
            {
                equipmentCategory.HospitalId = id;
            }

            if (ModelState.IsValid)
            {
                await _equipmentCategoryRepository.AddAsync(equipmentCategory);
                return RedirectToAction(nameof(Index));
            }

            return View(equipmentCategory);
        }

        // GET: EquipmentCategory/Edit/{id}
        public async Task<IActionResult> Edit(int id)
        {
            if (!_authRepository.IsAuthorized())
                return RedirectToAction("Login", "Auth");

            var equipmentCategory = await _equipmentCategoryRepository.GetByIdAsync(id);
            if (equipmentCategory == null)
                return NotFound();

            return View(equipmentCategory);
        }

        // POST: EquipmentCategory/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EquipmentCategory equipmentCategory)
        {
            if (!_authRepository.IsAuthorized())
                return RedirectToAction("Login", "Auth");

            if (id != equipmentCategory.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                await _equipmentCategoryRepository.UpdateAsync(equipmentCategory);
                return RedirectToAction(nameof(Index));
            }

            return View(equipmentCategory);
        }

        // GET: EquipmentCategory/Delete/{id}
        public async Task<IActionResult> Delete(int id)
        {
            if (!_authRepository.IsAuthorized())
                return RedirectToAction("Login", "Auth");

            var equipmentCategory = await _equipmentCategoryRepository.GetByIdAsync(id);
            if (equipmentCategory == null)
                return NotFound();

            await _equipmentCategoryRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
