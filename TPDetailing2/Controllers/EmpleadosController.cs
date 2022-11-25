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

namespace TPDetailing2.Controllers
{
    public class EmpleadosController : Controller
    {
        private readonly DbDetailing _context;
        private readonly UserManager<IdentityUser> _userManager; //identity

        public EmpleadosController(DbDetailing context, UserManager<IdentityUser> userManager) //identity)
        {
            _context = context;
            _userManager = userManager;//identity
        }

        // GET: Empleados
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Index()
        {
                        
            List<Empleado> empleados = await _context.Empleado.OrderBy(e => e.Legajo).ToListAsync();
            
            return View(empleados);
        }

        // GET: Empleados/Details/5
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Empleado == null)
            {
                return NotFound();
            }

            var empleado = await _context.Empleado.FindAsync(id);

            if (empleado == null)
            {
                return NotFound();
            }

            return View(empleado);
        }


        //identity

        [Authorize(Roles = "ADMIN")]

        public async Task<IActionResult> PersonalDetail()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null || _context.Empleado == null)
            {
                return NotFound();
            }

            var empleado = await _context.Empleado.Where(e => e.Email.ToUpper() == user.NormalizedEmail)
                .FirstOrDefaultAsync();

            if (empleado == null)
            {
                return NotFound();
            }
            return View("Details", empleado);
        }


        // GET: Empleados/Create
        [Authorize(Roles = "ADMIN")]
        public IActionResult Create()
        {
            ViewBag.MensajeError = null;
            return View();
        }

        // POST: Empleados/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nombre,Apellido,Email,Telefono")] Empleado empleado)
        {
            if (ModelState.IsValid)            
            {

                bool existe = await EmpleadoExists2(empleado.Email);

                if (!existe)
                {
                    empleado.Legajo = await AsignarLegajo();
                    _context.Add(empleado);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    string mensajeError = "Mail ya registrado";
                    ViewBag.MensajeError = mensajeError;
                }               
            }
            return View(empleado);
        }

        // GET: Empleados/Edit/5
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Empleado == null)
            {
                return NotFound();
            }

            var empleado = await _context.Empleado.FindAsync(id);
            if (empleado == null)
            {
                return NotFound();
            }
            return View(empleado);
        }

        // POST: Empleados/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("legajo,UsuarioId,Nombre,Apellido,Email,Telefono")] Empleado empleado)
        {
            if (id != empleado.UsuarioId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                //validacion del Entity para cuando dos personas quieren editar lo mismo simultaneamente
                try
                {
                    _context.Update(empleado);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmpleadoExists(empleado.UsuarioId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(empleado);
        }

        // GET: Empleados/Delete/5
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Empleado == null)
            {
                return NotFound();
            }

            var empleado = await _context.Empleado
                .FirstOrDefaultAsync(m => m.UsuarioId == id);
            if (empleado == null)
            {
                return NotFound();
            }

            return View(empleado);
        }

        // POST: Empleados/Delete/5
        [Authorize(Roles = "ADMIN")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Empleado == null)
            {
                return Problem("Entity set 'DbDetailing.Empleado'  is null.");
            }
            var empleado = await _context.Empleado.FindAsync(id);
            if (empleado != null)
            {
                _context.Empleado.Remove(empleado);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmpleadoExists(int id)
        {
          return _context.Empleado.Any(e => e.UsuarioId == id);
        }

        private async Task<bool> EmpleadoExists2(string? Email)
        {         
            var empleado = await _context.Empleado.Where(e => e.Email == Email).FirstOrDefaultAsync();
            
            if(empleado == null)
            {
                return false;
            }

            return true;
        }


        private async Task<int> AsignarLegajo()
        {
            int legajoAsignado=0;
            
            Empleado? empleado = await _context.Empleado.OrderByDescending(e=>e.Legajo).FirstOrDefaultAsync();
            
            if (empleado == null)
            {
                return legajoAsignado;
            }
            
            return empleado.Legajo++;
        }
    }
}
