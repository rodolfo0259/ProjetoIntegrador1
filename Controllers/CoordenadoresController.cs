using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ControleCelulasWebMvc.Data;
using ControleCelulasWebMvc.Models;

namespace ControleCelulasWebMvc.Controllers
{
    public class CoordenadoresController : Controller
    {
        private readonly WebDbContext _context;

        public CoordenadoresController(WebDbContext context)
        {
            _context = context;
        }

        // GET: Coordenadores
        public async Task<IActionResult> Index()
        {
            return View(await _context.Coordenadores
                .OrderBy(c => c.Nome)
                .ToListAsync());
        }

        // GET: Coordenadores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coordenador = await _context.Coordenadores
                .FirstOrDefaultAsync(m => m.Id == id);
            if (coordenador == null)
            {
                return NotFound();
            }

            return View(coordenador);
        }

        // GET: Coordenadores/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Coordenadores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Telefone,Status")] Coordenador coordenador)
        {
            if (ModelState.IsValid)
            {
                _context.Add(coordenador);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(coordenador);
        }

        // GET: Coordenadores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coordenador = await _context.Coordenadores.FindAsync(id);
            if (coordenador == null)
            {
                return NotFound();
            }
            return View(coordenador);
        }

        // POST: Coordenadores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Telefone,Status")] Coordenador coordenador)
        {
            if (id != coordenador.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(coordenador);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CoordenadorExists(coordenador.Id))
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
            return View(coordenador);
        }

        // GET: Coordenadores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coordenador = await _context.Coordenadores
                .FirstOrDefaultAsync(m => m.Id == id);
            if (coordenador == null)
            {
                return NotFound();
            }

            return View(coordenador);
        }

        // POST: Coordenadores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var coordenador = await _context.Coordenadores.FindAsync(id);
            _context.Coordenadores.Remove(coordenador);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CoordenadorExists(int id)
        {
            return _context.Coordenadores.Any(e => e.Id == id);
        }
    }
}
