using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TPDetailing2.Models;
using static System.Net.WebRequestMethods;

namespace TPDetailing2.Controllers
{
    public class ServiciosController : Controller
    {
        private readonly DbDetailing _context;
        private readonly UserManager<IdentityUser> _userManager; //identity
        //variables de globales de controlador
        private const string _fotoDefault = "https://img2.freepng.es/20210403/toq/transparent-car-icon-shopping-center-icon-60688c68647ff2.3049220116174644244117.jpg";
       

        public ServiciosController(DbDetailing context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;//identity
        }

        // GET: Servicios
       
        public async Task<IActionResult> Index()
        {
            List<Servicio> servicios = await _context.Servicio.ToListAsync();            
            return View(servicios);
        }

        // GET: Servicios/Details/5
        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Servicio == null)
            {
                return RedirectToAction("MensajeError", "Home");
            }

            var servicio = await _context.Servicio.FindAsync(id);
            if (servicio == null)
            {
                return RedirectToAction("MensajeError", "Home");
            }

            return View(servicio);
        }

        // GET: Servicios/Create
        [Authorize(Roles = "ADMIN")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Servicios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ServicioId,Nombre,Descripcion,FotoUrl,Costo,PrecioFinal")] Servicio servicio)
        {   
            if (ModelState.IsValid)
            {
                if(servicio.FotoUrl==null){
                    servicio.FotoUrl = _fotoDefault;
                }
                _context.Add(servicio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(servicio);
        }

        // GET: Servicios/Edit/5
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Servicio == null)
            {
                return NotFound();
            }

            var servicio = await _context.Servicio.FindAsync(id);
            if (servicio == null)
            {
                return NotFound();
            }
            return View(servicio);
        }

        // POST: Servicios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ServicioId,Nombre,Descripcion,FotoUrl,Costo,PrecioFinal")] Servicio servicio)
        {
            if (id != servicio.ServicioId)
            {
                return RedirectToAction("MensajeError", "Home");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(servicio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServicioExists(servicio.ServicioId))
                    {
                        return RedirectToAction("MensajeError", "Home");
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(servicio);
        }

        // GET: Servicios/Delete/5
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Servicio == null)
            {
                return RedirectToAction("MensajeError", "Home");
            }

            var servicio = await _context.Servicio.FindAsync(id);
            
            if (servicio == null)
            {
                return RedirectToAction("MensajeError", "Home");
            }

            return View(servicio);
        }

        // POST: Servicios/Delete/5
        [Authorize(Roles = "ADMIN")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Servicio == null)
            {
                return Problem("Entity set 'DbDetailing.Servicio'  is null.");
            }
            var servicio = await _context.Servicio.FindAsync(id);
            if (servicio != null)
            {
                _context.Servicio.Remove(servicio);
                await _context.SaveChangesAsync();
            }            
            return RedirectToAction(nameof(Index));
        }

        private bool ServicioExists(int id)
        {
            return _context.Servicio.Any(e => e.ServicioId == id);
        }
        
    }
}
