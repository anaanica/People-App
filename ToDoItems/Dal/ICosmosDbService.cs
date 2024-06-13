using ToDoItems.Models;

namespace ToDoItems.Dal
{
	public interface ICosmosDbService
	{
		Task<IEnumerable<Item>> GetItemsAsync(string queryString);
		Task<Item?> GetItemAsync(string id);
		Task AddItemAsync(Item item);
		Task UpdateItemAsync(Item item);
		Task DeleteItemAsync(Item item);
    }
}
