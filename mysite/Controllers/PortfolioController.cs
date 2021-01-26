using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using mysite.Data;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mysite.Data.FileManager;
using mysite.ViewModels;
using mysite.Models;
using mysite.Data.Repository;
using Microsoft.AspNetCore.Authorization;

namespace mysite.Controllers
{
    public class PortfolioController : Controller
    {
        private readonly IPortRepository _repo;
        private readonly IFileManager _fileManager;

        public PortfolioController(IPortRepository repo,
                                   IFileManager fileManager)
        {
            _repo = repo;
            _fileManager = fileManager;
        }


        public IActionResult Index()
        {
            var ports = _repo.GetAllPortfolios();
            return View(ports);
        }

        public ActionResult Details(int id)
        {
            var port = _repo.GetPort(id);
            return View(port);
        }

        [Authorize]
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
                return View(new PortfolioViewModel());
            else
            {
                var port = _repo.GetPort((int)id);
                return View(new PortfolioViewModel
                {
                    Id = port.Id,
                    Name = port.Name,
                    Type = port.Type,
                    Details = port.Details,
                    Language = port.Language,
                    Link = port.Link,
                    CurrentImage = port.PortfolioPhoto
                   
                });

            }
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Edit(PortfolioViewModel vm)
        {
            var port = new Portfolio
            {
                Id = vm.Id,
                Name = vm.Name,
                Type = vm.Type,
                Details = vm.Details,
                Language = vm.Language,
                Link = vm.Link
            };
            if (vm.PortfolioPhoto == null)
            {
                port.PortfolioPhoto = vm.CurrentImage;
            }
            else 
            {
                port.PortfolioPhoto = await _fileManager.SaveImage(vm.PortfolioPhoto);
            }

                    if (port.Id > 0)
                        _repo.UpdatePortfolio(port);
                    else
                        _repo.AddPortfolio(port);

                    if (await _repo.SaveChangesAsync())
                        return RedirectToAction("Index");
                    else
                        return View(port);
 
        }


        [HttpGet("/PortfolioPhoto/{portfolioPhoto}")]
        [ResponseCache(CacheProfileName = "Monthly")]
        public IActionResult PortfolioPhoto(string portfolioPhoto)
        {
            var mine = portfolioPhoto.Substring(portfolioPhoto.LastIndexOf('.') + 1);
            return new FileStreamResult(_fileManager.ImageStream(portfolioPhoto), $"portfolioPhoto/{mine}");
        }





    }
}
