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
    public class TurnosController : Controller
    {
        private readonly DbDetailing _context;
        private readonly UserManager<IdentityUser> _userManager; //identity

        public TurnosController(DbDetailing context, UserManager<IdentityUser> userManager)//identity
        {
            _context = context;
            _userManager = userManager;//identity
        }

        // GET: Turnos
        [Authorize(Roles = "EMPLEADO, ADMIN, CLIENTE")]
        public async Task<IActionResult> Index(int id)
        {
            Servicio s = await _context.Servicio.FindAsync(id);
            TempData["ServicioId2"] = s.ServicioId;
            TempData["ServicioNom"] = s.Nombre;
            TempData["ServicioPre"] = s.PrecioFinal;

            List<Turno> turnos = await _context.Turno.ToListAsync();

            return View(turnos);
        }



        // GET: Turnos/Details/5
        [Authorize(Roles = "EMPLEADO, ADMIN")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Turno == null)
            {
                return NotFound();
            }

            var turno = await _context.Turno
                //.Include(t => t.Empleado)
                .Include(t => t.Servicio)
                .Include(t => t.cliente)
                .FirstOrDefaultAsync(m => m.TurnoId == id);

            if (turno == null)
            {
                return NotFound();
            }

            return View(turno);
        }

        // GET: Turnos/Create
        [Authorize(Roles = "ADMIN")]
        public IActionResult Create()
        {
            ViewData["EmpleadoId"] = new SelectList(_context.Empleado, "UsuarioId", "Apellido");
            ViewData["ServicioId"] = new SelectList(_context.Servicio, "ServicioId", "Descripcion");
            ViewData["ClienteId"] = new SelectList(_context.Cliente, "UsuarioId", "Apellido");
            return View();
        }

        // POST: Turnos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "EMPLEADO, ADMIN, CLIENTE")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TurnoId,Fecha,Realizado,ClienteId,ServicioId,EmpleadoId")] Turno turno)
        {
            if (ModelState.IsValid)
            {
                _context.Add(turno);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["EmpleadoId"] = new SelectList(_context.Empleado, "UsuarioId", "Apellido", turno.EmpleadoId);
            ViewData["ServicioId"] = new SelectList(_context.Servicio, "ServicioId", "Descripcion", turno.ServicioId);
            ViewData["ClienteId"] = new SelectList(_context.Cliente, "UsuarioId", "Apellido", turno.ClienteId);
            return View(turno);
        }

        // GET: Turnos/Edit/5
       [Authorize(Roles = "EMPLEADO, ADMIN, CLIENTE")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Turno == null)
            {
                return NotFound();
            }

            var turno = await _context.Turno.FindAsync(id);
            if (turno == null)
            {
                return NotFound();
            }
            //ViewData["EmpleadoId"] = new SelectList(_context.Empleado, "UsuarioId", "Apellido", turno.EmpleadoId);
            ViewData["ServicioId"] = new SelectList(_context.Servicio, "ServicioId", "Descripcion", turno.ServicioId);
            ViewData["ClienteId"] = new SelectList(_context.Cliente, "UsuarioId", "Apellido", turno.ClienteId);
            return View(turno);
        }

        // POST: Turnos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "EMPLEADO, ADMIN, CLIENTE")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TurnoId,Fecha,Realizado,ClienteId,ServicioId,EmpleadoId")] Turno turno)
        {
            if (id != turno.TurnoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(turno);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TurnoExists(turno.TurnoId))
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
            //ViewData["EmpleadoId"] = new SelectList(_context.Empleado, "UsuarioId", "Apellido", turno.EmpleadoId);
            ViewData["ServicioId"] = new SelectList(_context.Servicio, "ServicioId", "Descripcion", turno.ServicioId);
            ViewData["ClienteId"] = new SelectList(_context.Cliente, "UsuarioId", "Apellido", turno.ClienteId);
            return View(turno);
        }

        // GET: Turnos/Delete/5
        [Authorize(Roles = "EMPLEADO, ADMIN, CLIENTE")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Turno == null)
            {
                return NotFound();
            }

            var turno = await _context.Turno
                //.Include(t => t.Empleado)
                .Include(t => t.Servicio)
                .Include(t => t.cliente)
                .FirstOrDefaultAsync(m => m.TurnoId == id);

            if (turno == null)
            {
                return NotFound();
            }

            return View(turno);
        }

        // POST: Turnos/Delete/5
        [Authorize(Roles = "EMPLEADO, ADMIN, CLIENTE")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Turno == null)
            {
                return Problem("Entity set 'DbDetailing.Turno'  is null.");
            }
            var turno = await _context.Turno.FindAsync(id);

            if (turno != null)
            {
                _context.Turno.Remove(turno);
                await _context.SaveChangesAsync();
            }
            
            
            return RedirectToAction(nameof(Index));
        }

        private bool TurnoExists(int id)
        {
          return _context.Turno.Any(e => e.TurnoId == id);
        }

        [Authorize(Roles = "EMPLEADO, ADMIN, CLIENTE")]
        [HttpGet]
        public async Task<IActionResult> CargarTurno(int id)
        {
            string mail = _userManager.GetUserName(User);
            Cliente cliente = await ClienteExists2(mail);
            int idU = cliente.UsuarioId;
            TempData["UserID"] = idU;
            Turno? t = await _context.Turno.FindAsync(id);
            return View(t);
        }

        [HttpPost]
        public async Task<IActionResult> CargarTurno(Turno turno)
        {
            if (ModelState.IsValid)
            {

                _context.Update(turno);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(turno);
        }

        private async Task<Cliente> ClienteExists2(string? Email)
        {
            var cliente = await _context.Cliente.Where(e => e.Email == Email).FirstOrDefaultAsync();

            return cliente;
        }
    }
}
