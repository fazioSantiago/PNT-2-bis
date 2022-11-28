using System;
using System.Collections.Generic;
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
    public class ClientesController : Controller
    {
        private readonly DbDetailing _context;
        private readonly UserManager<IdentityUser> _userManager; //identity

        public ClientesController(DbDetailing context, UserManager<IdentityUser> userManager)//identity
        {
            _context = context;
            _userManager = userManager;//identity
        }

        // GET: Clientes
        [Authorize(Roles ="EMPLEADO, ADMIN")]
        public async Task<IActionResult> Index()
        {
            
            //List<Cliente> clientes = await _context.Cliente.ToListAsync();

            List<Cliente> clientes = await _context.Cliente.OrderBy(c => c.UsuarioId).ToListAsync();

            return View(clientes);
        }

        // GET: Clientes/Details/5
        [Authorize(Roles = "EMPLEADO, ADMIN, CLIENTE")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Cliente == null)
            {
                return RedirectToAction("MensajeError", "Home");
            }

            var cliente = await _context.Cliente.FindAsync(id);

            if (cliente == null)
            {
                return RedirectToAction("MensajeError", "Home");
            }

            return View(cliente);
        }
        // GET: Clientes/Create
        public IActionResult Create(IdentityUser? user)
        {
            if (user==null)
            {
                return RedirectToAction("MensajeError", "Home");
            }
            Cliente cliente = new Cliente();
            
            cliente.Email=user.Email;

            return View(cliente);
        }

        //identity

        [Authorize(Roles = "CLIENTE")]

        public async Task<IActionResult> PersonalDetail()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user==null || _context.Cliente==null)
            {
                return NotFound();
            }

            var cliente = await _context.Cliente.Where(c=>c.Email.ToUpper() == user.NormalizedEmail)
                .FirstOrDefaultAsync();
            
            if (cliente==null)
            {
                return NotFound();
            }
            return View("Details", cliente);
        }




        // POST: Clientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UsuarioId,Nombre,Apellido,Email,Telefono")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                bool existe = await ClienteExists2(cliente.Email);

                if (!existe)
                {
                    _context.Add(cliente);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "Servicios");
                }                
            }
            return View(cliente);
        }

        // GET: Clientes/Edit/5
        [Authorize(Roles = "EMPLEADO, ADMIN, CLIENTE")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Cliente == null)
            {
                return RedirectToAction("MensajeError", "Home");
            }

            var cliente = await _context.Cliente.FindAsync(id);
            if (cliente == null)
            {
                return RedirectToAction("MensajeError", "Home");
            }
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "EMPLEADO, ADMIN, CLIENTE")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UsuarioId,Nombre,Apellido,Email,Telefono")] Cliente cliente)
        {
            if (id != cliente.UsuarioId)
            {
                return RedirectToAction("MensajeError", "Home");
            }

            if (ModelState.IsValid)
            {
                //puedo chequear que en la edicion no ponga mail/telefono repetido
                
                try
                {
                    Cliente? clienteCheck = await ClienteExists3(cliente.Email, cliente.Telefono);
                    
                    if (clienteCheck==null)
                    {
                    _context.Update(cliente);
                    await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteExists(cliente.UsuarioId))
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
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        [Authorize(Roles = "EMPLEADO, ADMIN, CLIENTE")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Cliente == null)
            {
                return RedirectToAction("MensajeError", "Home");
            }

            var cliente = await _context.Cliente.FindAsync(id);

            if (cliente == null)
            {
                return RedirectToAction("MensajeError", "Home");
            }

            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [Authorize(Roles = "EMPLEADO, ADMIN, CLIENTE")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Cliente == null)
            {
                return Problem("Entity set 'DbDetailing.Cliente'  is null.");
            }
            var cliente = await _context.Cliente.FindAsync(id);

            if (cliente != null)
            {
                _context.Cliente.Remove(cliente);
                await _context.SaveChangesAsync();
            }
            
           
            return RedirectToAction(nameof(Index));
        }

        private bool ClienteExists(int id)
        {
          return _context.Cliente.Any(e => e.UsuarioId == id);
        }

        private async Task<bool> ClienteExists2(string? Email)
        {
            var cliente = await _context.Cliente.Where(e => e.Email == Email).FirstOrDefaultAsync();

            if (cliente == null)
            {
                return false;
            }

            return true;
        }

        private async Task<Cliente> ClienteExists3(string? Telefono, string ?Email)
        {
            Cliente? cliente = await _context.Cliente.Where(e => e.Telefono == Telefono || e.Email==Email).FirstOrDefaultAsync();

            return cliente;
        }


    }
}
