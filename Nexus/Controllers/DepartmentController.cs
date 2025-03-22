using Microsoft.AspNetCore.Mvc;
using Nexus.Entities;
using Nexus.Interfaces;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Nexus.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IHospitalRepository _hospitalRepository;
        private readonly IAuthRepository _authRepository;

        public DepartmentController(
            IDepartmentRepository departmentRepository,
            IHospitalRepository hospitalRepository,
            IAuthRepository authRepository)
        {
            _departmentRepository = departmentRepository;
            _hospitalRepository = hospitalRepository;
            _authRepository = authRepository;
        }

        // GET: Department/Index
        public async Task<IActionResult> Index()
        {
            if (!_authRepository.IsAuthorized())
            {
                return RedirectToAction("Login", "Auth");
            }

            var hospitalId = HttpContext.Session.GetString("HospitalId");
            if (int.TryParse(hospitalId, out var id))
            {
                var departments = await _departmentRepository.GetAllAsync(id);
                return View(departments);
            }

            return RedirectToAction("Login", "Auth");
        }

        // GET: Department/Create
        public IActionResult Create()
        {
            if (!_authRepository.IsAuthorized())
            {
                return RedirectToAction("Login", "Auth");
            }

            var hospitalId = HttpContext.Session.GetString("HospitalId");
            if (int.TryParse(hospitalId, out var id))
            {
                ViewBag.HospitalId = id;
            }
            return View();
        }

        // POST: Department/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Department department)
        {
            if (!_authRepository.IsAuthorized())
            {
                return RedirectToAction("Login", "Auth");
            }

            var hospitalId = HttpContext.Session.GetString("HospitalId");
            if (int.TryParse(hospitalId, out var id))
            {
                department.HospitalId = id;
            }

            if (ModelState.IsValid)
            {
                await _departmentRepository.AddAsync(department);
                return RedirectToAction(nameof(Index), new { hospitalId = department.HospitalId });
            }
            return View(department);
        }

        // GET: Department/Edit/{id}
        public async Task<IActionResult> Edit(int id)
        {
            if (!_authRepository.IsAuthorized())
            {
                return RedirectToAction("Login", "Auth");
            }

            var department = await _departmentRepository.GetByIdAsync(id);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        // POST: Department/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Department department)
        {
            if (!_authRepository.IsAuthorized())
            {
                return RedirectToAction("Login", "Auth");
            }

            if (id != department.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _departmentRepository.UpdateAsync(department);
                return RedirectToAction(nameof(Index), new { hospitalId = department.HospitalId });
            }
            return View(department);
        }

        // GET: Department/Delete/{id}
        public async Task<IActionResult> Delete(int id)
        {
            if (!_authRepository.IsAuthorized())
            {
                return RedirectToAction("Login", "Auth");
            }

            var department = await _departmentRepository.GetByIdAsync(id);
            if (department == null)
            {
                return NotFound();
            }

            await _departmentRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index), new { hospitalId = department.HospitalId });
        }
    }
}
