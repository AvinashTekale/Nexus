using Microsoft.AspNetCore.Mvc;
using Nexus.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nexus.Data;

namespace Nexus.Controllers
{
    public class PurchaseOrderController : Controller
    {
        private readonly NexusDBContext _context; 

        public PurchaseOrderController(NexusDBContext context)
        {
            _context = context;
        }

        // GET: PurchaseOrder/Index
        public async Task<IActionResult> Index()
        {
            var purchaseOrders = await _context.PurchaseOrders
                .Include(p => p.InstallationBases)
                .ToListAsync();
            return View(purchaseOrders);
        }

        // GET: PurchaseOrder/Create
        public IActionResult Create()
        {
            var model = new PurchaseOrderViewModel
            {
                PurchaseOrder = new PurchaseOrderEntity { CreatedBy = 1 }, 
                ChildEntities = new List<PurchaseChildEntity> { new PurchaseChildEntity { CreatedBy = 1 } } 
            };
            return View(model);
        }

        // POST: PurchaseOrder/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PurchaseOrderViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.PurchaseOrder.DateCreated = DateTime.UtcNow;
                _context.Add(model.PurchaseOrder);
                await _context.SaveChangesAsync();

                foreach (var child in model.ChildEntities)
                {
                    child.PurchaseId = model.PurchaseOrder.Id;
                    child.CreatedBy = 1;
                    child.DateCreated = DateTime.UtcNow;
                    child.DateModified = DateTime.UtcNow;
                    _context.Add(child);
                }
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return View(model);
        }
    }

    public class PurchaseOrderViewModel
    {
        public PurchaseOrderEntity PurchaseOrder { get; set; }
        public List<PurchaseChildEntity> ChildEntities { get; set; }
    }
}