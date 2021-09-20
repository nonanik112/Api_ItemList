using App_List.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App_List.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppListItemController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AppListItemController(ApplicationDbContext context)
        {
            _context = context;

        }
        // GET: api/AppListItem
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppListItemModel>>> GetToDoItems()
        {
            return await _context.AppListItems.ToListAsync();
        }
        // GET: api/AppListItem/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AppListItemModel>> GetToDoItemModel(int id)
        {
            var appListItemModel = await _context.AppListItems.FindAsync(id);

            if (appListItemModel == null)
            {
                return NotFound();
            }

            return appListItemModel;
        }

        // PUT: api/AppListItem/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAppListItemModel(int id, AppListItemModel appListItemModel)
        {
            if (id != appListItemModel.ItemId)
            {
                return BadRequest();
            }
            _context.Entry(appListItemModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppListItemModelExists(id))
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



        // POST: api/ToDoItem
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AppListItemModel>> PostApplistItemModel(AppListItemModel appListItemModel)
        {
            _context.AppListItems.Add(appListItemModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAppListItemModel", new { id = appListItemModel.ItemId }, appListItemModel);
        }

        // DELETE: api/ToDoItem/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApplistItemModel(int id)
        {
            var appListItemModel = await _context.AppListItems.FindAsync(id);
            if (appListItemModel == null)
            {
                return NotFound();
            }

            _context.AppListItems.Remove(appListItemModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AppListItemModelExists(int id)
        {
            return _context.AppListItems.Any(e => e.ItemId == id);
        }
    }
}