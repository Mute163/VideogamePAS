/*
 * Controller for Player CRUD operations
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Videogame.Data;
using Videogame.Models;

namespace Videogame.Controllers
{
    public class PlayerController : Controller

    {
        private readonly VideogameContext _context;

        public PlayerController(VideogameContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(string playerNationality)
        {
          
            IQueryable<string> nationalityQuery = from m in _context.Player
                                            orderby m.Nationality
                                            select m.Nationality;

            var players = from m in _context.Player
                         select m;


            if (!string.IsNullOrEmpty(playerNationality))
            {
                players = players.Where(x => x.Nationality.Contains(playerNationality));
            }

            var playerNationalityV = new ListPlayer
            {
                Nationality = new SelectList(await nationalityQuery.Distinct().ToListAsync()),
                Players = await players.ToListAsync()
            };

            return View(playerNationalityV);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var player = await _context.Player
                .FirstOrDefaultAsync(p => p.PlayerId == id);
            if (player == null)
            {
                return NotFound();
            }

            return View(player);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PlayerId,PlayerName,PlayerLastName,BirthDate,Nationality,email")] Player player)
        {
            if (ModelState.IsValid)
            {
                player.CreateDate = DateTime.Now;
                _context.Add(player);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(player);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var player = await _context.Player.FindAsync(id);
            if (player == null)
            {
                return NotFound();
            }
            return View(player);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PlayerId,PlayerName,PlayerLastName,BirthDate,Nationality,email")] Player player)
        {
            if (id != player.PlayerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                player.CreateDate = player.CreateDate;
                try
                {
                    _context.Update(player);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlayerExists(player.PlayerId))
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
            return View(player);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var player = await _context.Player
                .FirstOrDefaultAsync(p => p.PlayerId == id);
            if (player == null)
            {
                return NotFound();
            }

            return View(player);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var player = await _context.Player.FindAsync(id);
            _context.Player.Remove(player);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlayerExists(int id)
        {
            return _context.Player.Any(e => e.PlayerId == id);
        }
    }
}
    

