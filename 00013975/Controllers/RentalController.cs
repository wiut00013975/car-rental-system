using CarRentalSystem.Data;
using CarRentalSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class RentalController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public RentalController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/Rental
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Rental>>> GetRentals()
    {
        return await _context.Rentals.Include(r => r.Car).ToListAsync();
    }

    // GET: api/Rental/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Rental>> GetRental(int id)
    {
        var rental = await _context.Rentals.Include(r => r.Car).FirstOrDefaultAsync(r => r.RentalID == id);

        if (rental == null)
        {
            return NotFound();
        }

        return rental;
    }

    // POST: api/Rental
    [HttpPost]
    public async Task<ActionResult<Rental>> PostRental(Rental rental)
    {
        rental.Car = _context.Cars.Where(c => c.CarID == rental.CarID).FirstOrDefault();
        _context.Rentals.Add(rental);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetRental", new { id = rental.RentalID }, rental);
    }

    // PUT: api/Rental/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutRental(int id, Rental rental)
    {
        if (id != rental.RentalID)
        {
            return BadRequest();
        }
        rental.Car = _context.Cars.Where(c => c.CarID == rental.CarID).FirstOrDefault();
        _context.Update(rental);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!RentalExists(id))
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

    // DELETE: api/Rental/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRental(int id)
    {
        var rental = await _context.Rentals.FindAsync(id);
        if (rental == null)
        {
            return NotFound();
        }

        _context.Rentals.Remove(rental);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool RentalExists(int id)
    {
        return _context.Rentals.Any(e => e.RentalID == id);
    }

}

