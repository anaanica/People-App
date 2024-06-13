using Newtonsoft.Json;
using static NuGet.Client.ManagedCodeConventions;

namespace ToDoItems.ViewModels
{
    public class VMItem
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool Completed { get; set; }
        public string? Person { get; set; }
        public List<string>? People { get; set; }
    }
}
