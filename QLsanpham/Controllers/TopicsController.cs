using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using QLsanpham.Models;
using QLsanpham.Models.EF;

namespace QLsanpham.Controllers
{
    public class TopicsController : Controller
    {
        private readonly MusicWebContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        public TopicsController(MusicWebContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        // GET: Topics
        public async Task<IActionResult> Index()
        {
              return _context.Topics != null ? 
                          View(await _context.Topics.ToListAsync()) :
                          Problem("Entity set 'MusicWebContext.Topics'  is null.");
        }

        // GET: Topics/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Topics == null)
            {
                return NotFound();
            }

            var topic = await _context.Topics
                .FirstOrDefaultAsync(m => m.Id == id);
            if (topic == null)
            {
                return NotFound();
            }

            return View(topic);
        }

        // GET: Topics/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Topics/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(topic topic)
        {
            if (ModelState.IsValid)
            {
                Topic tp = new Topic();
                //Save image to wwwroot/image
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(topic.Img.FileName);
                string extension = Path.GetExtension(topic.Img.FileName);
                topic.TopicImg = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                string path = Path.Combine(wwwRootPath + "/image/", topic.TopicImg);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await topic.Img.CopyToAsync(fileStream);
                }
                tp.TopicName = topic.TopicName;
                tp.TopicImg = "/image/" +topic.TopicImg;
                tp.TopicDescription = topic.TopicDescription;
                tp.Alias = topic.Alias;
                tp.CreatedDate = topic.CreatedDate;
                tp.ModifiedDate = topic.ModifiedDate;
                _context.Add(tp);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(topic);
        }

        // GET: Topics/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Topics == null)
            {
                return NotFound();
            }

            var topic = await _context.Topics.FindAsync(id);
            if (topic == null)
            {
                return NotFound();
            }
            return View(topic);
        }

        // POST: Topics/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TopicName,TopicDescription,TopicImg,CreatedDate,ModifiedDate,Alias")] Topic topic)
        {
            if (id != topic.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(topic);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TopicExists(topic.Id))
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
            return View(topic);
        }

        // GET: Topics/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Topics == null)
            {
                return NotFound();
            }

            var topic = await _context.Topics
                .FirstOrDefaultAsync(m => m.Id == id);
            if (topic == null)
            {
                return NotFound();
            }

            return View(topic);
        }

        // POST: Topics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Topics == null)
            {
                return Problem("Entity set 'MusicWebContext.Topics'  is null.");
            }
            var topic = await _context.Topics.FindAsync(id);
            if (topic != null)
            {
                _context.Topics.Remove(topic);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TopicExists(int id)
        {
          return (_context.Topics?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
