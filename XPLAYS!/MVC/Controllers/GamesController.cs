using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using BLL.Controllers.Bases;
using BLL.Services;
using BLL.Models;
using Newtonsoft.Json;
using BLL.DAL;

// Generated from Custom Template.

namespace MVC.Controllers
{
    public class GamesController : MvcController
    {
        // Service injections:
        private readonly IGameService _gameService;
        private readonly IPublisherService _publisherService;

        /* Can be uncommented and used for many to many relationships. ManyToManyRecord may be replaced with the related entiy name in the controller and views. */
        private readonly IManyToManyRecordService _ManyToManyRecordService;
        //private IPublisherService publisherService;

        internal IManyToManyRecordService ManyToManyRecordService { get; }

        public GamesController(
			IGameService gameService  , IPublisherService publisherService

            /* Can be uncommented and used for many to many relationships. ManyToManyRecord may be replaced with the related entiy name in the controller and views. */
            // IManyToManyRecordService ManyToManyRecordService
        )
        {
            _gameService = gameService;
            _publisherService = publisherService;

            /* Can be uncommented and used for many to many relationships. ManyToManyRecord may be replaced with the related entiy name in the controller and views. */
           // _ManyToManyRecordService = ManyToManyRecordService;
        }

        // GET: Games
        public IActionResult Index()
        {
            // Get collection service logic:
            var list = _gameService.Query().ToList();

            SetViewData();

            /*var publisherList = _publisherService.Query().ToList().ToList();

            foreach (var item in list)
            {
                if (item.Record.Publisher == null)
                {

                }
            } */

            return View(list);
        }

        // GET: Games/Details/5
        public IActionResult Details(int id)
        {
            // Get item service logic:
            var item = _gameService.Query().SingleOrDefault(q => q.Record.Id == id);
            return View(item);
        }

        protected void SetViewData()
        {
            // Related items service logic to set ViewData (Record.Id and Name parameters may need to be changed in the SelectList constructor according to the model):
            
             ViewData["PublisherId"] = new SelectList(_publisherService.Query().ToList(), "Record.Id", "Name");

            var publisherlistesi = _publisherService.Query().ToList();

            var settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore // Prevent self-referencing loops
            };

            String publisherJsonString = JsonConvert.SerializeObject(publisherlistesi, settings);


            TempData["PublisherList"] = publisherJsonString;

            /* Can be uncommented and used for many to many relationships. ManyToManyRecord may be replaced with the related entiy name in the controller and views. */
            //ViewBag.ManyToManyRecordIds = new MultiSelectList(_ManyToManyRecordService.Query().ToList(), "Record.Id", "Name");
        }

        // GET: Games/Create
        public IActionResult Create()
        {



            SetViewData();
            return View();
        }

        // POST: Games/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(GameModel game)
        {
            if (ModelState.IsValid)
            {

                // Insert item service logic:
                var result = _gameService.Create(game.Record);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id = game.Record.Id });
                }
                ModelState.AddModelError("", result.Message);
            }
            ///// ???
            
            /////
            SetViewData();
            return View(game);
        }

        // GET: Games/Edit/5
        public IActionResult Edit(int id)
        {
            // Get item to edit service logic:
            var item = _gameService.Query().SingleOrDefault(q => q.Record.Id == id);
            SetViewData();
            return View(item);
        }

        // POST: Games/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(GameModel game)
        {
            if (ModelState.IsValid)
            {
                // Update item service logic:
                var result = _gameService.Update(game.Record);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id = game.Record.Id });
                }
                ModelState.AddModelError("", result.Message);
            }
            SetViewData();
            return View(game);
        }

        // GET: Games/Delete/5
        public IActionResult Delete(int id)
        {
            // Get item to delete service logic:
            var item = _gameService.Query().SingleOrDefault(q => q.Record.Id == id);
            return View(item);
        }

        // POST: Games/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            // Delete item service logic:
            var result = _gameService.Delete(id);
            TempData["Message"] = result.Message;
            return RedirectToAction(nameof(Index));
        }
	}

    public interface IManyToManyRecordService //internal 
    {
        object Query();
    }
}
