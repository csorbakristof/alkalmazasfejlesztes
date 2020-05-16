using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoUWP.Model;

namespace TodoUWP.Data
{
    public interface IDbService
    {
        void AddTodo(string name, DateTime deadline, string description);

        List<TodoItem> GetTodos();

        void DeleteTodo(TodoItem todo);

    }
}
