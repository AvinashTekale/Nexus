using Microsoft.AspNetCore.Mvc;
using Nexus.Entities;
using Nexus.Repositories;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nexus.Interfaces;

namespace Nexus.Controllers
{
    public class EquipmentController : Controller
    {
        private readonly IEquipmentRepository _equipmentRepository;
        private readonly IEquipmentCategoryRepository _equipmentCategoryRepository;
        private readonly IEquipmentModelRepository _equipmentModelRepository; // Added EquipmentModel repository
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IHospitalRepository _hospitalRepository;

        public EquipmentController(IEquipmentRepository equipmentRepository,
                                   IEquipmentCategoryRepository equipmentCategoryRepository,
                                   IEquipmentModelRepository equipmentModelRepository, // Inject EquipmentModel repository
                                   IDepartmentRepository departmentRepository,
                                   IHospitalRepository hospitalRepository)
        {
            _equipmentRepository = equipmentRepository;
            _equipmentCategoryRepository = equipmentCategoryRepository;
            _equipmentModelRepository = equipmentModelRepository; // Assign to local variable
            _departmentRepository = departmentRepository;
            _hospitalRepository = hospitalRepository;
        }

        // Check Session Authorization
        private bool IsAuthorized()
        {
            var userEmail = HttpContext.Session.GetString("UserEmail");
            var userRole = HttpContext.Session.GetString("UserRole");
            return !string.IsNullOrEmpty(userEmail) && userRole == "Hospital";
        }

        // Handle Unauthorized Access
        private IActionResult HandleUnauthorized()
        {
            return RedirectToAction("Login", "Auth");
        }

        // Get data for dropdown lists (EquipmentModel, EquipmentCategory, Department)
        private async Task LoadDropdowns()
        {
            var hspId = 0;
            var hospitalId = HttpContext.Session.GetString("HospitalId");
            if (int.TryParse(hospitalId, out var id))
            {
                hspId = id;
            }
            ViewBag.EquipmentCategories = new SelectList(await _equipmentCategoryRepository.GetAllAsync(hspId), "Id", "Name");
            ViewBag.EquipmentModels = new SelectList(await _equipmentModelRepository.GetAllAsync(hspId), "Id", "Name"); // Added EquipmentModel dropdown
            ViewBag.Departments = new SelectList(await _departmentRepository.GetAllAsync(hspId), "Id", "Name");
        }

        // GET: Equipment
        public async Task<IActionResult> Index()
        {
            try
            {

                if (!IsAuthorized())
                    return HandleUnauthorized();

                var hospitalId = HttpContext.Session.GetString("HospitalId");
                if (int.TryParse(hospitalId, out var id))
                {
                    var equipmentList = await _equipmentRepository.GetAllAsync(id);
                    return View(equipmentList);
                }

                return HandleUnauthorized();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // GET: Equipment/Create
        public async Task<IActionResult> Create()
        {
            if (!IsAuthorized())
                return HandleUnauthorized();

            await LoadDropdowns(); // Load dropdown data for EquipmentModel, EquipmentCategory, and Department
            return View();
        }

        // POST: Equipment/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Equipment equipment)
        {
            if (!IsAuthorized())
                return HandleUnauthorized();

            if (ModelState.IsValid)
            {
                var hospitalId = HttpContext.Session.GetString("HospitalId");
                if (int.TryParse(hospitalId, out var id))
                {
                    equipment.HospitalId = id;  // Set HospitalId from session
                    await _equipmentRepository.AddAsync(equipment);
                    return RedirectToAction(nameof(Index));
                }
            }

            await LoadDropdowns(); // Load dropdown data if validation fails
            return View(equipment);
        }

        // GET: Equipment/Edit/{serialNumber}
        public async Task<IActionResult> Edit(string serialNumber)
        {
            if (!IsAuthorized())
                return HandleUnauthorized();

            var equipment = await _equipmentRepository.GetByIdAsync(serialNumber);
            if (equipment == null)
                return NotFound();

            await LoadDropdowns(); // Load dropdown data for EquipmentModel, EquipmentCategory, and Department
            return View(equipment);
        }

        // POST: Equipment/Edit/{serialNumber}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string serialNumber, Equipment equipment)
        {
            if (!IsAuthorized())
                return HandleUnauthorized();

            if (serialNumber != equipment.SerialNumber)
                return BadRequest();

            if (ModelState.IsValid)
            {
                await _equipmentRepository.UpdateAsync(equipment);
                return RedirectToAction(nameof(Index));
            }

            await LoadDropdowns(); // Load dropdown data if validation fails
            return View(equipment);
        }

        // GET: Equipment/Delete/{serialNumber}
        public async Task<IActionResult> Delete(string serialNumber)
        {
            if (!IsAuthorized())
                return HandleUnauthorized();

            var equipment = await _equipmentRepository.GetByIdAsync(serialNumber);
            if (equipment == null)
                return NotFound();
            await _equipmentRepository.DeleteAsync(serialNumber);
            return RedirectToAction(nameof(Index));
        }

    }
}
