using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using my_online_portfolio.Models;

namespace my_online_portfolio.Controllers;

public class GalleryController : Controller
{
    private readonly MyDbContext _context;
    private readonly IWebHostEnvironment _env;

    public GalleryController(MyDbContext context, IWebHostEnvironment env)
    {
        _context = context;
        _env = env;
    }

    // Viewers: see all gallery items
    public async Task<IActionResult> Index()
    {
        var items = await _context.Gallery.OrderByDescending(g => g.UploadedAt).ToListAsync();
        return View(items);
    }

    // Upload form
    public IActionResult Upload()
    {
        return View();
    }

    // Handle upload submission
    [HttpPost]
    public async Task<IActionResult> Upload(string title, IFormFile imageFile)
    {
        if (imageFile == null || imageFile.Length == 0)
        {
            ViewBag.Error = "Please select an image.";
            return View();
        }

        // Save image to wwwroot/uploads/
        var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads");
        Directory.CreateDirectory(uploadsFolder);

        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
        var filePath = Path.Combine(uploadsFolder, fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await imageFile.CopyToAsync(stream);
        }

        // Save record to DB
        var item = new Gallery
        {
            Title = title,
            ImagePath = "/uploads/" + fileName,
            UploadedAt = DateTime.UtcNow
        };

        _context.Gallery.Add(item);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }

    // Deleting of image
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _context.Gallery.FindAsync(id);
        if (item == null) return NotFound();

        // Delete the image file from wwwroot/uploads/
        var filePath = Path.Combine(_env.WebRootPath, item.ImagePath.TrimStart('/'));
        if (System.IO.File.Exists(filePath))
        {
            System.IO.File.Delete(filePath);
        }

        // Remove from DB
        _context.Gallery.Remove(item);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }

}