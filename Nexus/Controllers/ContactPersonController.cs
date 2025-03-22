using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nexus.DTOs;
using Nexus.Interfaces;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Nexus.Controllers
{
    public class ContactPersonController : Controller
    {
        private readonly IContactPersonRepository _contactPersonRepository;
        private readonly IPartnerRepository _partnerRepository;
        private readonly IAuthRepository _authRepository;

        public ContactPersonController(
            IContactPersonRepository contactPersonRepository,
            IPartnerRepository partnerRepository,
            IAuthRepository authRepository)
        {
            _contactPersonRepository = contactPersonRepository;
            _partnerRepository = partnerRepository;
            _authRepository = authRepository;
        }

        // GET: ContactPerson
        public async Task<IActionResult> Index()
        {
            if (!_authRepository.IsAuthorized())
            {
                return RedirectToAction("Login", "Auth");
            }

            var contactPersons = await _contactPersonRepository.GetAllAsync();
            return View(contactPersons);
        }

        // GET: ContactPerson/Create
        public IActionResult Create()
        {
            if (!_authRepository.IsAuthorized())
            {
                return RedirectToAction("Login", "Auth");
            }

            PopulateDropdowns();
            return View();
        }

        // POST: ContactPerson/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ContactPersonDTO contactPersonDto)
        {
            if (!_authRepository.IsAuthorized())
            {
                return RedirectToAction("Login", "Auth");
            }

            if (ModelState.IsValid)
            {
                await _contactPersonRepository.AddAsync(contactPersonDto);
                return RedirectToAction(nameof(Index));
            }
            PopulateDropdowns();
            return View(contactPersonDto);
        }

        // GET: ContactPerson/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (!_authRepository.IsAuthorized())
            {
                return RedirectToAction("Login", "Auth");
            }

            var contactPerson = await _contactPersonRepository.GetByIdAsync(id);
            if (contactPerson == null)
            {
                return NotFound();
            }
            PopulateDropdowns();
            return View(contactPerson);
        }

        // POST: ContactPerson/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ContactPersonDTO contactPersonDto)
        {
            if (!_authRepository.IsAuthorized())
            {
                return RedirectToAction("Login", "Auth");
            }

            if (id != contactPersonDto.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                await _contactPersonRepository.UpdateAsync(contactPersonDto);
                return RedirectToAction(nameof(Index));
            }
            PopulateDropdowns();
            return View(contactPersonDto);
        }

        // GET: ContactPerson/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (!_authRepository.IsAuthorized())
            {
                return RedirectToAction("Login", "Auth");
            }

            var contactPerson = await _contactPersonRepository.GetByIdAsync(id);
            if (contactPerson == null)
            {
                return NotFound();
            }

            return View(contactPerson);
        }

        // POST: ContactPerson/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!_authRepository.IsAuthorized())
            {
                return RedirectToAction("Login", "Auth");
            }

            await _contactPersonRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private void PopulateDropdowns(ContactPersonDTO model = null)
        {
            var hospitalId = HttpContext.Session.GetString("HospitalId");
            if (int.TryParse(hospitalId, out var id))
            {
                var partners = _partnerRepository.GetByHospitalIdAsync(id);
                ViewBag.Partners = new SelectList(partners.Result, "Id", "Name", model?.PartnerId);

                var roles = new List<SelectListItem>
                {
                    new SelectListItem { Text = "Admin", Value = "Admin", Selected = model?.Role == "Admin" },
                    new SelectListItem { Text = "User", Value = "User", Selected = model?.Role == "User" },
                    new SelectListItem { Text = "Manager", Value = "Manager", Selected = model?.Role == "Manager" }
                };
                ViewBag.Roles = roles;
            }
        }
    }
}
