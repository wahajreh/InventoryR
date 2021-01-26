using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mysite.Data;
using mysite.Data.FileManager;
using mysite.Data.Repository;
using mysite.Models;
using mysite.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mysite.Controllers
{
    public class CustomersController : Controller
    {
       
        private readonly ICustomerRepository _repo;
        private readonly IFileManager _fileManager;

        public CustomersController(ICustomerRepository repo, IFileManager fileManager)
        {
    
            _repo = repo;
            _fileManager = fileManager;
        }
        public IActionResult Index()
        {
            var customers = _repo.GetAllCustomers();
            if (User.IsInRole("Admin"))
            {
                return View("Index", customers);
            }
            else
            {
                return View("ReadOnlyList", customers);
            }
        }

        public IActionResult Details(int id)
        {
            return View(_repo.GetCustomer(id));
         
        }

        public ActionResult New()
        {
            if (User.IsInRole("Admin"))
            {
                return View("New");
            }
            return View("Error");
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
                return View(new CustomerViewModel());
            else
            {
                var customer = _repo.GetCustomer((int)id);
                return View(new CustomerViewModel
                {
                    Id = customer.Id,
                    CustomerFirst= customer.CustomerFirst,
                    CustomerLast = customer.CustomerLast,
                    Email = customer.Email,
                    MobilPhone = customer.MobilPhone,
                    BillingAddress = customer.BillingAddress,
                    City = customer.City,
                    State = customer.State,
                    PostalCode = customer.PostalCode,
                    WorkOrder= customer.WorkOrder,
                    CurrentImage = customer.ModelImage
                });
            }

        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(CustomerViewModel vm)
        {
            var customer = new Customer
            {
                Id = vm.Id,
                CustomerFirst = vm.CustomerFirst,
                CustomerLast = vm.CustomerLast,
                Email = vm.Email,
                MobilPhone = vm.MobilPhone,
                BillingAddress = vm.BillingAddress,
                City = vm.City,
                State = vm.State,
                PostalCode = vm.PostalCode,
                WorkOrder = vm.WorkOrder
            };

            if (vm.ModelImage == null)
            {
                customer.ModelImage = vm.CurrentImage;
            }
            else
            {
                customer.ModelImage = await _fileManager.SaveImage(vm.ModelImage);

            }

            if (customer.Id > 0)
                _repo.UpdateCustomer(customer);
            else
                _repo.AddCustomer(customer);

            if (await _repo.SaveChangesAsync())
                return RedirectToAction("Index");
            else
                return View(customer);
        }

        [HttpGet("/ModelImage/{modelImage}")]
        [ResponseCache(CacheProfileName = "Monthly")]
        public IActionResult Image(string modelImage)
        {
            var mine = modelImage.Substring(modelImage.LastIndexOf('.') + 1);
            return new FileStreamResult(_fileManager.ImageStream(modelImage), $"modelImage/{mine}");
        }

        [HttpGet]
        public async Task<IActionResult> Remove(int id)
        {
            _repo.RemoveCustomer(id);
            await _repo.SaveChangesAsync();
            return RedirectToAction("Index");
        }

    }
}
