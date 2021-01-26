using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mysite.Data;
using mysite.Models;
using mysite.ViewModels;

namespace mysite.Controllers
{
    public class HomeController : Controller
    {
        private DataContext _context;

        public HomeController(DataContext context)
        {
            _context = context;
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Demo() 
        {
            return View();
        }

        public IActionResult UserPanel() 
        {
            return View();
        }


        public IActionResult Edit(int id)
        {
            var home = _context.Home.SingleOrDefault(h => h.Id == id);
    
            var viewModel = new HomeViewModel
            {
                Home = home
            };
            return View("HomeForm", viewModel);
        }

        public ActionResult New()
        {
            var viewModel = new HomeViewModel
            {
                Home = new Home()
              
            };

            return View("HomeForm", viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Home home)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new HomeViewModel
                {
                    Home = home
                };
                return View("HomeForm", viewModel);
            }

            if (home.Id == 0)
                _context.Home.Add(home);
            else
            {
                var homeInDb = _context.Home.Single(c => c.Id == home.Id);
                homeInDb.Title = home.Title;
                homeInDb.Name = home.Name;
                homeInDb.Service = home.Service;
                homeInDb.Description = home.Description;
                homeInDb.Email = home.Email;
                homeInDb.PhoneNumber = home.PhoneNumber;
                
            }
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");


        }


        public IActionResult HomeEdit()
        {
            return View();
        }

        public IActionResult YouTube()
        {
            return View();
        }

        public IActionResult MyVideos() 
        {

            return View();
        }

    }
}