using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ConversionWebMVC.Models;
using ConversionWebMVC.ViewModels;

namespace ConversionWebMVC.Controllers
{
    public class HistoricoModelsController : Controller
    {
        private readonly Contexto _context;

        public HistoricoModelsController(Contexto context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index(string email)
        {
            if (_context.Historico == null)
            {
                return Problem("Entity set 'Contexto.Historico' is null.");
            }

            IQueryable<HistoricoModel> historicoQuery = _context.Historico;

            if (!string.IsNullOrEmpty(email))
            {
                historicoQuery = historicoQuery.Where(h => h.NombreUsuario == email);
            }

            var historicoList = await historicoQuery.ToListAsync();

            return View(historicoList);
        }



        // GET: HistoricoModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Historico == null)
            {
                return NotFound();
            }

            var historicoModel = await _context.Historico
                .FirstOrDefaultAsync(m => m.id == id);
            if (historicoModel == null)
            {
                return NotFound();
            }

            return View(historicoModel);
        }

        // GET: HistoricoModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: HistoricoModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,tiempo,ValorInicial,ValorFinal,NombreUsuario")] HistoricoModel historicoModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(historicoModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(historicoModel);
        }

        // GET: HistoricoModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Historico == null)
            {
                return NotFound();
            }

            var historicoModel = await _context.Historico.FindAsync(id);
            if (historicoModel == null)
            {
                return NotFound();
            }
            return View(historicoModel);
        }

        // POST: HistoricoModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,tiempo,ValorInicial,ValorFinal,NombreUsuario")] HistoricoModel historicoModel)
        {
            if (id != historicoModel.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(historicoModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HistoricoModelExists(historicoModel.id))
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
            return View(historicoModel);
        }

        // GET: HistoricoModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Historico == null)
            {
                return NotFound();
            }

            var historicoModel = await _context.Historico
                .FirstOrDefaultAsync(m => m.id == id);
            if (historicoModel == null)
            {
                return NotFound();
            }

            return View(historicoModel);
        }

        // POST: HistoricoModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Historico == null)
            {
                return Problem("Entity set 'Contexto.Historico'  is null.");
            }
            var historicoModel = await _context.Historico.FindAsync(id);
            if (historicoModel != null)
            {
                _context.Historico.Remove(historicoModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HistoricoModelExists(int id)
        {
          return (_context.Historico?.Any(e => e.id == id)).GetValueOrDefault();
        }

        public IActionResult VolverAHome()
        {
            return RedirectToAction("Divisas", "Divisas");
        }

    }
}
