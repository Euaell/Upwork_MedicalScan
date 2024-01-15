using System.Diagnostics;
using MedicalScan.DataContext;
using Microsoft.AspNetCore.Mvc;
using MedicalScan.Models;
using MedicalScan.ViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MedicalScan.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly MedicalScanDbContext _context;

    public HomeController(ILogger<HomeController> logger, MedicalScanDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        // Retrieve all Specialties and put them in ViewBag
        ViewBag.Specialties = new SelectList(_context.Specialities.ToList(), "Id", "Name");
        
        IEnumerable<DoctorViewModel> doctors = 
            _context.Doctors
            .Select(d => new DoctorViewModel
            {
                Id = d.Id,
                Name = d.Name,
                Specialties = d.Specialties.Select(s => s.Name).ToList()
            })
            .ToList();

        return View(doctors);
    }

    public IActionResult Privacy()
    {
        return View();
    }
    
    public ActionResult FilterDoctors([FromQuery]string? selectedSpecialty)
    {
        // Retrieve filtered doctors based on selected specialty
        var doctors = _context.Doctors
            .Include(d => d.Specialties)
            .Where(d => selectedSpecialty == null || d.Specialties.Any(s => s.Id.ToString() == selectedSpecialty))
            .Select(d => new DoctorViewModel
            {
                Id = d.Id,
                Name = d.Name,
                Specialties = d.Specialties.Select(s => s.Name).ToList()
            })
            .ToList();

        return PartialView("_DoctorCards", doctors); // Render a partial view to update the data
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}