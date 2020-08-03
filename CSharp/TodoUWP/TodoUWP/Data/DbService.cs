using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoUWP.Model;

namespace TodoUWP.Data
{
    public class DbService : IDbService
    {
        private List<TodoItem> todos;

        public DbService()
        {
            todos = new List<TodoItem>();
        }

        public void AddTodo(string name, DateTime deadline, string description)
        {
            foreach (var item in todos)
            {
                //don't add if there is an item with same name
                if (item.Name == name) return;
            }
            TodoItem todo = new TodoItem()
            {
                Name = name,
                Deadline = deadline,
                Description = description
            };

            todos.Add(todo);
        }

        public List<TodoItem> GetTodos()
        {
            return todos;
        }

        public void DeleteTodo(TodoItem todo)
        {
            if(todo!=null) todos.Remove(todo);
        }

    }
}
