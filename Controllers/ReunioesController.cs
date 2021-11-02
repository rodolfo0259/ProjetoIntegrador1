using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ControleCelulasWebMvc.Data;
using ControleCelulasWebMvc.Models;
using ControleCelulasWebMvc.Services;
using ControleCelulasWebMvc.Models.ViewModels;
using System.Diagnostics;

namespace ControleCelulasWebMvc.Controllers
{
    public class ReunioesController : Controller
    {
        private readonly WebDbContext _context;
        private readonly RegistrosService _service;

        public ReunioesController(WebDbContext context, RegistrosService service)
        {
            _context = context;
            _service = service;
        }

        // GET: Reunioes
        public async Task<IActionResult> Index()
        {
            var webDbContext = _context.Reuniao.Include(r => r.Celula).Include(r => r.Pessoa);
            return View(await webDbContext.OrderBy(x => x.DataHoraReuniao).ToListAsync());
        }

        public IActionResult Relatorios()
        {
            var celulas = from c in _context.Celula
                          orderby c.Nome
                          select c;

            ViewBag.CelulaId = new SelectList(celulas.AsNoTracking(), "Id", "Nome", "");

            return View("Relatorios");
        }
        
        [HttpPost]
        public async Task<IActionResult> Pesquisa(DateTime? data, int celulaId)
        {
            if (data == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Data não informada" });
            }

            if (celulaId <= 0)
            {
                return RedirectToAction(nameof(Error), new { message = "Célula não informada" });
            }

            ViewData["Data"] = data.Value.ToString("yyyy-MM-dd");
            ViewData["CelulaId"] = celulaId;

            var result = await _service.FindByDateAsync(data, celulaId);
            return View(result);
        }

        // GET: Reunioes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reuniao = await _context.Reuniao
                .Include(r => r.Celula)
                .Include(r => r.Pessoa)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reuniao == null)
            {
                return NotFound();
            }

            return View(reuniao);
        }

        // GET: Reunioes/Create
        public IActionResult Create()
        {
            ViewData["CelulaId"] = new SelectList(_context.Celula, "Id", "Nome");
            ViewData["PessoaId"] = new SelectList(_context.Pessoa, "Id", "Nome");
            return View();
        }

        // POST: Reunioes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DataHoraReuniao,CelulaId,PessoaId")] Reuniao reuniao)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reuniao);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(HomeController.Index));
            }

            ViewData["CelulaId"] = new SelectList(_context.Celula, "Id", "Nome", reuniao.CelulaId);
            ViewData["PessoaId"] = new SelectList(_context.Pessoa, "Id", "Nome", reuniao.PessoaId);
            return View(reuniao);
        }

        // GET: Reunioes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reuniao = await _context.Reuniao.FindAsync(id);
            if (reuniao == null)
            {
                return NotFound();
            }
            ViewData["CelulaId"] = new SelectList(_context.Celula, "Id", "Nome", reuniao.CelulaId);
            ViewData["PessoaId"] = new SelectList(_context.Pessoa, "Id", "Nome", reuniao.PessoaId);
            return View(reuniao);
        }

        // POST: Reunioes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DataHoraReuniao,CelulaId,PessoaId")] Reuniao reuniao)
        {
            if (id != reuniao.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reuniao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReuniaoExists(reuniao.Id))
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
            ViewData["CelulaId"] = new SelectList(_context.Celula, "Id", "Nome", reuniao.CelulaId);
            ViewData["PessoaId"] = new SelectList(_context.Pessoa, "Id", "Nome", reuniao.PessoaId);
            return View(reuniao);
        }

        // GET: Reunioes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reuniao = await _context.Reuniao
                .Include(r => r.Celula)
                .Include(r => r.Pessoa)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reuniao == null)
            {
                return NotFound();
            }

            return View(reuniao);
        }

        // POST: Reunioes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reuniao = await _context.Reuniao.FindAsync(id);
            _context.Reuniao.Remove(reuniao);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReuniaoExists(int id)
        {
            return _context.Reuniao.Any(e => e.Id == id);
        }

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };

            return View(viewModel);
        }
    }
}
