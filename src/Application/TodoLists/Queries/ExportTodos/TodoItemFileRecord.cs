using Chatty.Application.Common.Mappings;
using Chatty.Domain.Entities;

namespace Chatty.Application.TodoLists.Queries.ExportTodos
{
    public class TodoItemRecord : IMapFrom<TodoItem>
    {
        public string Title { get; set; }

        public bool Done { get; set; }
    }
}
