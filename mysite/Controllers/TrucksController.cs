using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    public class TrucksController : Controller
    {
        private readonly ItruckRepository _repo;
        private readonly IFileManager _fileManager;

        public TrucksController(ItruckRepository repo,
                                   IFileManager fileManager)
        {
            _repo = repo;
            _fileManager = fileManager;
        }


        public IActionResult Index()
        {
            var trucks = _repo.GetAllTrucks();
            return View(trucks);
        }

        public ActionResult Details(int id)
        {
            var truck = _repo.GetTruck(id);
            return View(truck);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
                return View(new TruckViewModel());
            else
            {
                var truck = _repo.GetTruck((int)id);
                return View(new TruckViewModel
                {
                    Id = truck.Id,
                    Name = truck.Name,
                    TruckNumber = truck.TruckNumber,
                    Details = truck.Details,
                    Category = truck.Category,
                    Link = truck.Link,
                    CurrentImage = truck.TruckPhoto

                });

            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> Edit(TruckViewModel vm)
        {
            var truck = new Truck
            {
                Id = vm.Id,
                Name = vm.Name,
                TruckNumber = vm.TruckNumber,
                Details = vm.Details,
                Category = vm.Category,
                Link = vm.Link
                
            };
            if (vm.TruckPhoto == null)
            {
                truck.TruckPhoto = vm.CurrentImage;
            }
            else
            {
                truck.TruckPhoto = await _fileManager.SaveImage(vm.TruckPhoto);
            }

            if (truck.Id > 0)
                _repo.UpdateTruck(truck);
            else
                _repo.AddTruck(truck);

            if (await _repo.SaveChangesAsync())
                return RedirectToAction("Index");
            else
                return View(truck);

        }


        [HttpGet("/TruckPhoto/{truckPhoto}")]
        [ResponseCache(CacheProfileName = "Monthly")]
        public IActionResult TruckPhoto(string truckPhoto)
        {
            var mine = truckPhoto.Substring(truckPhoto.LastIndexOf('.') + 1);
            return new FileStreamResult(_fileManager.ImageStream(truckPhoto), $"truckPhoto/{mine}");
        }

    }
}
