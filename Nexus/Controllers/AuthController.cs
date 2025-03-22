using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Nexus.Entities;
using Nexus.Data;
using Nexus.DTOs;
using Microsoft.AspNetCore.Authorization;

public class AuthController : Controller
{
    private readonly NexusDBContext _context;

    public AuthController(NexusDBContext context)
    {
        _context = context;
    }

    // GET: /Auth/Register
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Register(RegisterDTO dto)
    {
        if (_context.Users.Any(a => a.Email == dto.Email))
        {
            return Json(new { success = false, message = "Email already exists." });
        }

        // Generate PasswordHash and PasswordKey
        var hmac = new HMACSHA512();
        var user = new User
        {
            Name = dto.Name,
            Email = dto.Email,
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(dto.Password)),
            PasswordKey = hmac.Key,
            Role = dto.Role // Set the role
        };

        _context.Users.Add(user);
        _context.SaveChanges();

        return Json(new { success = true, message = "User registered successfully." });
    }


    [AllowAnonymous]
    // GET: /Auth/Login
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(LoginDto dto)
    {
        var user = _context.Users.FirstOrDefault(a => a.Email == dto.Email);
        if (user == null)
        {
            TempData["ErrorMessage"] = "Invalid email or password.";
            return View("Login"); // Return the login view with an error message
        }

        using (var hmac = new HMACSHA512(user.PasswordKey))
        {
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(dto.Password));
            if (!computedHash.SequenceEqual(user.PasswordHash))
            {
                TempData["ErrorMessage"] = "Invalid email or password.";
                return View("Login"); // Return the login view with an error message
            }
        }

        // Set session and authentication
        HttpContext.Session.SetString("UserEmail", user.Email);
        HttpContext.Session.SetString("UserRole", user.Role);
        HttpContext.Session.SetString("UserId", user.Id.ToString());

        if (user.Role == "Hospital")
        {
            var hospital = _context.Hospitals.FirstOrDefault(a => a.UserId == user.Id);
            HttpContext.Session.SetString("HospitalId", hospital.Id.ToString());
            return RedirectToAction("Index", "Dashboard"); // Redirect to hospital dashboard
        }
        else if (user.Role == "BME")
        {
            var bmeAccount = _context.BMEAccounts.FirstOrDefault(a => a.UserId == user.Id);
            HttpContext.Session.SetString("bmeAccountId", bmeAccount.Id.ToString());
            HttpContext.Session.SetString("HospitalId", bmeAccount.HospitalId.ToString());
            return RedirectToAction("Index", "BMEDashboard"); // Redirect to BME dashboard
        }

        return RedirectToAction("Index", "AdminDashboard"); // Redirect to admin dashboard
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        TempData["LogoutMessage"] = "You have been logged out successfully.";
        return RedirectToAction("Login");
    }

}