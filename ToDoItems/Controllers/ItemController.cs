using Microsoft.AspNetCore.Mvc;
using ToDoItems.Dal;
using ToDoItems.Models;
using ToDoItems.ViewModels;

namespace ToDoItems.Controllers
{
    public class ItemController : Controller
    {

		private static readonly ICosmosDbService service = CosmosDbServiceProvider.Service!;
        private static readonly ICosmosDbPeopleService peopleService = CosmosDbServiceProvider.PeopleService!;

        public async Task<ActionResult> Index()
		{
            var items = await service.GetItemsAsync("SELECT * FROM Item");

            foreach (var item in items)
            {
                item.Person = await peopleService.GetPersonAsync(item.PersonId);
            }

            return View(items);
        }

		public async Task<ActionResult> Create()
		{
			var people = await peopleService.GetPeopleAsync("SELECT * FROM Person");
			var vmPeople = people.Select(x => x.FirstName).ToList();
            var vmItem = new VMItem();
            vmItem.People = vmPeople;

            return View(vmItem);
		}

		[HttpPost]
		public async Task<ActionResult> Create(VMItem vmItem)
		{
			if (ModelState.IsValid)
			{
				Item item = new Item();
				item.Name = vmItem.Name;
				item.Description = vmItem.Description;
				item.Completed = vmItem.Completed;
                var people = await peopleService.GetPeopleAsync("SELECT * FROM Person");
                var personId = people.FirstOrDefault(x => x.FirstName.Equals(vmItem.Person)).Id;
				item.PersonId = personId;
                item.Id = Guid.NewGuid().ToString();
				await service.AddItemAsync(item);
				return RedirectToAction(nameof(Index));
			}

			return View(vmItem);
		}

		public async Task<ActionResult> Edit(string id) => await ShowItem(id);

		public async Task<ActionResult> Delete(string id) => await ShowItem(id);

		public async Task<ActionResult> Details(string id) => await ShowItem(id);

		private async Task<ActionResult> ShowItem(string id)
		{
			if (id == null)
			{
				return BadRequest();
			}
			var item = await service.GetItemAsync(id);
			if (item == null)
			{
				return NotFound();
			}
			return View(item);
		}

		// copy from Create
		[HttpPost]
		public async Task<ActionResult> Edit(Item item)
		{
			if (ModelState.IsValid)
			{
				await service.UpdateItemAsync(item);
				return RedirectToAction(nameof(Index));
			}
			return View(item);
		}

		// copy from Edit post
		[HttpPost]
		public async Task<ActionResult> Delete(Item item)
		{
			await service.DeleteItemAsync(item);
			return RedirectToAction(nameof(Index));

		}

	}
}
