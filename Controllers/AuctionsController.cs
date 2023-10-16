using DistLab2.Core;
using DistLab2.Core.Interfaces;
using DistLab2.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DistLab2.Controllers
{
    public class AuctionsController : Controller
    {
        private readonly IAuctionService _auctionService;
        public AuctionsController(IAuctionService auctionService)
        {
            _auctionService = auctionService;
        }


        // GET: AuctionsController
        //public ActionResult Index()
        //{
        //    Console.WriteLine("hello from Aucton controller");
        //    //List<Auction> auctions = _auctionService.GetAll();
        //    //return View(auctions);

        //    List<Auction> auctions = _auctionService.GetAll();
        //    List<AuctionVM> auctionVMs = new();
        //    foreach (var auction in auctions)
        //    {
        //        auctionVMs.Add(AuctionVM.FromAuction(auction));
        //    }
        //    return View(auctionVMs);
        //}

        // GET: AuctionsController/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //// GET: AuctionsController/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: AuctionsController/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: AuctionsController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: AuctionsController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: AuctionsController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: AuctionsController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
