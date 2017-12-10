    using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCBankTransactions.Data;
using MVCBankTransactions.Models;

namespace MVCBankTransactions.Controllers
{
    public class TransactionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TransactionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Transactions
        public async Task<IActionResult> Index(string searchType, string searchNameDest, string searchIsFraud, string searchStep)
        {
            //ViewData["CurrentFilter"] = searchType;
            var trans = from s in _context.Transaction
                           select s;
            if (!String.IsNullOrEmpty(searchType))
            {
                trans = trans.Where(s => s.Type.Contains(searchType));
            }
            if (!String.IsNullOrEmpty(searchNameDest))
            {
                trans = trans.Where(s => s.NameDest.Contains(searchNameDest));
            }
            if (!String.IsNullOrEmpty(searchIsFraud))
            {
                if(searchIsFraud == "1")
                {
                    trans = trans.Where(s => s.IsFraud.Equals(true));
                }
                if (searchIsFraud == "0")
                {
                    trans = trans.Where(s => s.Step.Equals(false));
                }

            }
            if (!String.IsNullOrEmpty(searchStep))
            {
                trans = trans.Where(s => s.Step.Equals(Int32.Parse(searchStep)));
            }

            return View(await trans.AsNoTracking().ToListAsync());
        }

        // GET: Transactions/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transaction
                .SingleOrDefaultAsync(m => m.ID == id);
            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        // GET: Transactions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Transactions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Step,Type,Amount,NameOrig,OldbalanceOrig,NewbalanceOrig,NameDest,OldbalanceDest,NewbalanceDest,IsFraud,IsFlaggedFraud")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                _context.Add(transaction);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(transaction);
        }

        // GET: Transactions/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transaction.SingleOrDefaultAsync(m => m.ID == id);
            if (transaction == null)
            {
                return NotFound();
            }
            return View(transaction);
        }

        // POST: Transactions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("ID,Step,Type,Amount,NameOrig,OldbalanceOrig,NewbalanceOrig,NameDest,OldbalanceDest,NewbalanceDest,IsFraud,IsFlaggedFraud")] Transaction transaction)
        {
            if (id != transaction.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(transaction);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransactionExists(transaction.ID))
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
            return View(transaction);
        }

        // GET: Transactions/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transaction
                .SingleOrDefaultAsync(m => m.ID == id);
            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        // POST: Transactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var transaction = await _context.Transaction.SingleOrDefaultAsync(m => m.ID == id);
            _context.Transaction.Remove(transaction);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TransactionExists(decimal id)
        {
            return _context.Transaction.Any(e => e.ID == id);
        }
    }
}
