using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nexus.Entities;
using Nexus.Interfaces;
using Nexus.Repositories; // Ensure correct namespace

namespace Nexus.Controllers
{
    public class PartnerController : Controller
    {
        private readonly IPartnerRepository _partnerRepository;
        private readonly IAuthRepository _authRepository;

        public PartnerController(IPartnerRepository partnerRepository, IAuthRepository authRepository)
        {
            _partnerRepository = partnerRepository;
            _authRepository = authRepository;
        }

        // GET: Partner
        public async Task<IActionResult> Index()
        {
            if (!_authRepository.IsAuthorized())
            {
                return RedirectToAction("Login", "Auth");
            }

            var hospitalId = HttpContext.Session.GetString("HospitalId");
            if (int.TryParse(hospitalId, out var id))
            {
                var partners = await _partnerRepository.GetByHospitalIdAsync(id);
                return View(partners);
            }

            return RedirectToAction("Login", "Auth");
        }

        // GET: Partner/Create
        public IActionResult Create()
        {
            if (!_authRepository.IsAuthorized())
            {
                return RedirectToAction("Login", "Auth");
            }

            return View();
        }

        // POST: Partner/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Partner partner)
        {
            if (!_authRepository.IsAuthorized())
            {
                return RedirectToAction("Login", "Auth");
            }

            var hospitalId = HttpContext.Session.GetString("HospitalId");
            if (int.TryParse(hospitalId, out var id))
            {
                partner.HospitalId = id;
                if (ModelState.IsValid)
                {
                    await _partnerRepository.AddAsync(partner);
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(partner);
        }

        // GET: Partner/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (!_authRepository.IsAuthorized())
            {
                return RedirectToAction("Login", "Auth");
            }

            var partner = await _partnerRepository.GetByIdAsync(id);
            if (partner == null)
            {
                return NotFound();
            }

            return View(partner);
        }

        // POST: Partner/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Partner partner)
        {
            if (!_authRepository.IsAuthorized())
            {
                return RedirectToAction("Login", "Auth");
            }

            if (id != partner.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                await _partnerRepository.UpdateAsync(partner);
                return RedirectToAction(nameof(Index));
            }

            return View(partner);
        }

        // GET: Partner/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (!_authRepository.IsAuthorized())
            {
                return RedirectToAction("Login", "Auth");
            }

            var partner = await _partnerRepository.GetByIdAsync(id);
            if (partner == null)
            {
                return NotFound();
            }

            return View(partner);
        }

        // POST: Partner/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!_authRepository.IsAuthorized())
            {
                return RedirectToAction("Login", "Auth");
            }

            await _partnerRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
