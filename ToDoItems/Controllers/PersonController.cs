using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoItems.Dal;
using ToDoItems.Models;

namespace ToDoItems.Controllers
{
    public class PersonController : Controller
    {
        private static readonly ICosmosDbPeopleService peopleService 
            = CosmosDbServiceProvider.PeopleService!;
        // GET: PersonController
        public async Task<ActionResult> Index()
        {
            return View(await peopleService.GetPeopleAsync("SELECT * FROM Person"));
        }

        // GET: PersonController/Details/5
        public async Task<ActionResult> Details(string id) => await ShowPerson(id);

        private async Task<ActionResult> ShowPerson(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var person = await peopleService.GetPersonAsync(id);
            if (person == null)
            {
                return NotFound();
            }
            return View(person);
        }

        // GET: PersonController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PersonController/Create
        [HttpPost]
        public async Task<ActionResult> Create(Person person)
        {
            if (ModelState.IsValid)
            {
                person.Id = Guid.NewGuid().ToString();
                await peopleService.AddPersonAsync(person);
                return RedirectToAction(nameof(Index));
            }

            return View(person);
        }

        // GET: PersonController/Edit/5
        public async Task<ActionResult> Edit(string id) => await ShowPerson(id);

        // POST: PersonController/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(Person person)
        {
            if (ModelState.IsValid)
            {
                await peopleService.UpdatePersonAsync(person);
                return RedirectToAction(nameof(Index));
            }
            return View(person);
        }

        // GET: PersonController/Delete/5
        public async Task<ActionResult> Delete(string id) => await ShowPerson(id);

        // POST: PersonController/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(Person person)
        {
            await peopleService.DeletePersonAsync(person);
            return RedirectToAction(nameof(Index));
        }
    }
}
