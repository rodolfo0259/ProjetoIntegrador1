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
    public class CelulasController : Controller
    {
        private readonly WebDbContext _context;

        public CelulasController(WebDbContext context)
        {
            _context = context;
        }

        // GET: Celulas
        public async Task<IActionResult> Index()
        {
            var webDbContext = _context.Celula.Include(c => c.Area);
            return View(await webDbContext.OrderBy(x => x.Nome).ToListAsync());
        }

        // GET: Celulas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var celula = await _context.Celula
                .Include(c => c.Area)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (celula == null)
            {
                return NotFound();
            }

            return View(celula);
        }

        // GET: Celulas/Create
        public IActionResult Create()
        {
            ViewData["AreaId"] = new SelectList(_context.Area, "Id", "Nome");
            return View();
        }

        // POST: Celulas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Endereco,Bairro,Cidade,Uf,NomeResponsavel,Telefone,DiaHoraReuniao,Status,AreaId")] Celula celula)
        {
            if (ModelState.IsValid)
            {
                _context.Add(celula);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AreaId"] = new SelectList(_context.Area, "Id", "Nome", celula.AreaId);
            return View(celula);
        }

        // GET: Celulas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var celula = await _context.Celula.FindAsync(id);
            if (celula == null)
            {
                return NotFound();
            }
            ViewData["AreaId"] = new SelectList(_context.Area, "Id", "Nome", celula.AreaId);
            return View(celula);
        }

        // POST: Celulas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Endereco,Bairro,Cidade,Uf,NomeResponsavel,Telefone,DiaHoraReuniao,Status,AreaId")] Celula celula)
        {
            if (id != celula.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(celula);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CelulaExists(celula.Id))
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
            ViewData["AreaId"] = new SelectList(_context.Area, "Id", "Nome", celula.AreaId);
            return View(celula);
        }

        // GET: Celulas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var celula = await _context.Celula
                .Include(c => c.Area)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (celula == null)
            {
                return NotFound();
            }

            return View(celula);
        }

        // POST: Celulas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var celula = await _context.Celula.FindAsync(id);
            _context.Celula.Remove(celula);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CelulaExists(int id)
        {
            return _context.Celula.Any(e => e.Id == id);
        }
    }
}
