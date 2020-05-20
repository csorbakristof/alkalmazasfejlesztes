using System;
using System.Collections.Generic;
using TodoUWP.Template10.Model;

namespace TodoUWP.Template10.Data

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
                //don't add if theres an item with same name
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
