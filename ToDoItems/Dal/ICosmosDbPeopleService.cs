using ToDoItems.Models;

namespace ToDoItems.Dal
{
    public interface ICosmosDbPeopleService
    {
        Task<IEnumerable<Person>> GetPeopleAsync(string queryString);
        Task<Person?> GetPersonAsync(string id);
        Task AddPersonAsync(Person person);
        Task UpdatePersonAsync(Person person);
        Task DeletePersonAsync(Person person);
    }
}
