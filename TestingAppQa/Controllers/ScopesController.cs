﻿ using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TestingAppQa.Data;
using TestingAppQa.Models;

namespace TestingAppQa.Controllers
{
    public class ScopesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ScopesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Scopes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Scope.ToListAsync());
        }

        // GET: Scopes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scope = await _context.Scope
                .FirstOrDefaultAsync(m => m.IdTool == id);
            if (scope == null)
            {
                return NotFound();
            }

            return View(scope);
        }

        // GET: Scopes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Scopes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdTool,Name,Version,Specification")] Scope scope)
        {
            if (ModelState.IsValid)
            {
                _context.Add(scope);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(scope);
        }

        // GET: Scopes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scope = await _context.Scope.FindAsync(id);
            if (scope == null)
            {
                return NotFound();
            }
            return View(scope);
        }

        // POST: Scopes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdTool,Name,Version,Specification")] Scope scope)
        {
            if (id != scope.IdTool)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(scope);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ScopeExists(scope.IdTool))
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
            return View(scope);
        }

        // GET: Scopes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scope = await _context.Scope
                .FirstOrDefaultAsync(m => m.IdTool == id);
            if (scope == null)
            {
                return NotFound();
            }

            return View(scope);
        }

        // POST: Scopes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var scope = await _context.Scope.FindAsync(id);
            _context.Scope.Remove(scope);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ScopeExists(int id)
        {
            return _context.Scope.Any(e => e.IdTool == id);
        }
    }
}