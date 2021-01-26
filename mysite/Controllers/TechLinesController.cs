using Microsoft.AspNetCore.Mvc;
using mysite.Data;
using mysite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mysite.Controllers
{
    public class TechLinesController : Controller
    {
        private readonly DataContext _context;

        public TechLinesController(DataContext context)
        {
            _context = context;
        }

        public ActionResult Index() 
        {
            var techline = _context.Techlines.ToList();
            return View(techline);
        
        }

        public ActionResult New()
        {
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Techline techline)
        {
            _context.Techlines.Add(techline);
            _context.SaveChanges();
            return RedirectToAction("Index", "Home");
        
        }

    }
}
