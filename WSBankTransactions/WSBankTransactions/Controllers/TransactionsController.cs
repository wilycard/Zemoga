using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WSBankTransactions.Models;

namespace WSBankTransactions.Controllers
{
    [Produces("application/json")]
    [Route("api/Transactions")]
    public class TransactionsController : Controller
    {
        private readonly WSBankTransactionsContext _context;

        public TransactionsController(WSBankTransactionsContext context)
        {
            _context = context;
        }

        // GET: api/Transactions
        [HttpGet]
        public IEnumerable<Transaction> GetTransaction()
        {
            return _context.Transaction;
        }

        // GET: api/Transactions/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTransaction([FromRoute] decimal id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var transaction = await _context.Transaction.SingleOrDefaultAsync(m => m.ID == id);

            if (transaction == null)
            {
                return NotFound();
            }

            return Ok(transaction);
        }

        // PUT: api/Transactions/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTransaction([FromRoute] decimal id, [FromBody] Transaction transaction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != transaction.ID)
            {
                return BadRequest();
            }

            _context.Entry(transaction).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransactionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Transactions
        [HttpPost]
        public async Task<IActionResult> PostTransaction([FromBody] Transaction transaction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.Transaction.Add(transaction);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TransactionExists(transaction.ID))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTransaction", new { id = transaction.ID }, transaction);
        }

        // DELETE: api/Transactions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaction([FromRoute] decimal id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var transaction = await _context.Transaction.SingleOrDefaultAsync(m => m.ID == id);
            if (transaction == null)
            {
                return NotFound();
            }

            _context.Transaction.Remove(transaction);
            await _context.SaveChangesAsync();

            return Ok(transaction);
        }

        private bool TransactionExists(decimal id)
        {
            return _context.Transaction.Any(e => e.ID == id);
        }
    }
}