using Microsoft.AspNetCore.Mvc;
using Nexus.Entities;
using Nexus.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Nexus.Controllers
{
    public class EquipmentModelController : Controller
    {
        private readonly IEquipmentModelRepository _equipmentModelRepository;
        private readonly IPartnerRepository _partnerRepository;
        private readonly IAuthRepository _authRepository;

        public EquipmentModelController(
            IEquipmentModelRepository equipmentModelRepository,
            IPartnerRepository partnerRepository,
            IAuthRepository authRepository)
        {
            _equipmentModelRepository = equipmentModelRepository;
            _partnerRepository = partnerRepository;
            _authRepository = authRepository;
        }

        // GET: Equipment
        public async Task<IActionResult> Index()
        {
            if (!_authRepository.IsAuthorized())
                return RedirectToAction("Login", "Auth");

            var hospitalId = HttpContext.Session.GetString("HospitalId");
            if (int.TryParse(hospitalId, out var id))
            {
                var equipmentList = await _equipmentModelRepository.GetAllAsync(id);
                return View(equipmentList);
            }

            return RedirectToAction("Login", "Auth");
        }

        // GET: Equipment/Create
        public async Task<IActionResult> Create()
        {
            if (!_authRepository.IsAuthorized())
                return RedirectToAction("Login", "Auth");

            var hospitalId = HttpContext.Session.GetString("HospitalId");
            ViewBag.Partners = new List<Partner>();

            if (int.TryParse(hospitalId, out var id))
            {
                ViewBag.Partners = await _partnerRepository.GetByHospitalIdAsync(id);
            }

            return View();
        }

        // POST: Equipment/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EquipmentModel equipmentModel)
        {
            if (!_authRepository.IsAuthorized())
                return RedirectToAction("Login", "Auth");

            if (ModelState.IsValid)
            {
                await _equipmentModelRepository.AddAsync(equipmentModel);
                return RedirectToAction(nameof(Index));
            }

            var hospitalId = HttpContext.Session.GetString("HospitalId");
            ViewBag.Partners = new List<Partner>();

            if (int.TryParse(hospitalId, out var id))
            {
                ViewBag.Partners = await _partnerRepository.GetByHospitalIdAsync(id);
            }

            return View(equipmentModel);
        }

        // GET: Equipment/Edit/{id}
        public async Task<IActionResult> Edit(int id)
        {
            if (!_authRepository.IsAuthorized())
                return RedirectToAction("Login", "Auth");

            var equipment = await _equipmentModelRepository.GetByIdAsync(id);
            if (equipment == null)
                return NotFound();

            var hospitalId = HttpContext.Session.GetString("HospitalId");
            ViewBag.Partners = new List<Partner>();

            if (int.TryParse(hospitalId, out var hospId))
            {
                ViewBag.Partners = await _partnerRepository.GetByHospitalIdAsync(hospId);
            }

            return View(equipment);
        }

        // POST: Equipment/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EquipmentModel equipmentModel)
        {
            if (!_authRepository.IsAuthorized())
                return RedirectToAction("Login", "Auth");

            if (id != equipmentModel.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                var existingEquipment = await _equipmentModelRepository.GetByIdAsync(id);
                if (existingEquipment == null)
                    return NotFound();

                existingEquipment.Name = equipmentModel.Name;
                existingEquipment.PartnerId = equipmentModel.PartnerId;

                await _equipmentModelRepository.UpdateAsync(existingEquipment);
                return RedirectToAction(nameof(Index));
            }

            var hospitalId = HttpContext.Session.GetString("HospitalId");
            ViewBag.Partners = new List<Partner>();

            if (int.TryParse(hospitalId, out var hospId))
            {
                ViewBag.Partners = await _partnerRepository.GetByHospitalIdAsync(hospId);
            }

            return View(equipmentModel);
        }

        // GET: Equipment/Delete/{id}
        public async Task<IActionResult> Delete(int id)
        {
            if (!_authRepository.IsAuthorized())
                return RedirectToAction("Login", "Auth");

            var equipment = await _equipmentModelRepository.GetByIdAsync(id);
            if (equipment == null)
                return NotFound();

            return View(equipment);
        }

        // POST: Equipment/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!_authRepository.IsAuthorized())
                return RedirectToAction("Login", "Auth");

            var equipment = await _equipmentModelRepository.GetByIdAsync(id);
            if (equipment == null)
                return NotFound();

            await _equipmentModelRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
