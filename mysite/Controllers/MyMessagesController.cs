using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using mysite.Data;
using mysite.Models;
using mysite.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mysite.Controllers
{
    [Authorize(Roles = "Admin")]
    public class MyMessagesController : Controller
    {
        private DataContext _context;

        public MyMessagesController(DataContext context)
        {
            _context = context;
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

      
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult New()
        {
            var viewModel = new MyMessagesViewModel(); 
            return View(viewModel);
        }
        [HttpPost]
        public IActionResult Create(MyMessage mymessage)
        {
            _context.MyMessages.Add(mymessage);
            _context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

    }
}
