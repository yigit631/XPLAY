using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using BLL.Controllers.Bases;
using BLL.Services;
using BLL.Models;

// Generated from Custom Template.

namespace MVC.Controllers
{
    public class PublishersController : MvcController
    {
        // Service injections:
        private readonly IPublisherService _publisherService;

        /* Can be uncommented and used for many to many relationships. ManyToManyGameInfo may be replaced with the related entiy name in the controller and views. */
        //private readonly IManyToManyGameInfoService _ManyToManyGameInfoService;

        public PublishersController(
			IPublisherService publisherService

            /* Can be uncommented and used for many to many relationships. ManyToManyGameInfo may be replaced with the related entiy name in the controller and views. */
           // , IManyToManyGameInfoService ManyToManyGameInfoService
        )
        {
            _publisherService = publisherService;

            /* Can be uncommented and used for many to many relationships. ManyToManyGameInfo may be replaced with the related entiy name in the controller and views. */
            //_ManyToManyGameInfoService = ManyToManyGameInfoService;
        }

        // GET: Publishers
        public IActionResult Index()
        {
            // Get collection service logic:
            var list = _publisherService.Query().ToList();
            return View(list);
        }

        // GET: Publishers/Details/5
        public IActionResult Details(int id)
        {
            // Get item service logic:
            var item = _publisherService.Query().SingleOrDefault(q => q.GameInfo.Id == id);
            return View(item);
        }

        protected void SetViewData()
        {
            // Related items service logic to set ViewData (GameInfo.Id and Name parameters may need to be changed in the SelectList constructor according to the model):
            
            /* Can be uncommented and used for many to many relationships. ManyToManyGameInfo may be replaced with the related entiy name in the controller and views. */
            //ViewBag.ManyToManyGameInfoIds = new MultiSelectList(_ManyToManyGameInfoService.Query().ToList(), "GameInfo.Id", "Name");
        }

        // GET: Publishers/Create
        public IActionResult Create()
        {
            SetViewData();
            return View();
        }

        // POST: Publishers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PublisherModel publisher)
        {
            if (ModelState.IsValid)
            {
                // Insert item service logic:
                var result = _publisherService.Create(publisher.GameInfo);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id = publisher.GameInfo.Id });
                }
                ModelState.AddModelError("", result.Message);
            }
            SetViewData();
            return View(publisher);
        }

        // GET: Publishers/Edit/5
        public IActionResult Edit(int id)
        {
            // Get item to edit service logic:
            var item = _publisherService.Query().SingleOrDefault(q => q.GameInfo.Id == id);
            SetViewData();
            return View(item);
        }

        // POST: Publishers/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(PublisherModel publisher)
        {
            if (ModelState.IsValid)
            {
                // Update item service logic:
                var result = _publisherService.Update(publisher.GameInfo);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id = publisher.GameInfo.Id });
                }
                ModelState.AddModelError("", result.Message);
            }
            SetViewData();
            return View(publisher);
        }

        // GET: Publishers/Delete/5
        public IActionResult Delete(int id)
        {
            // Get item to delete service logic:
            var item = _publisherService.Query().SingleOrDefault(q => q.GameInfo.Id == id);
            return View(item);
        }

        // POST: Publishers/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            // Delete item service logic:
            var result = _publisherService.Delete(id);
            TempData["Message"] = result.Message;
            return RedirectToAction(nameof(Index));
        }
	}
}
