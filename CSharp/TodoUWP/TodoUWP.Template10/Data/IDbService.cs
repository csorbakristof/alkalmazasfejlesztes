using System;
using System.Collections.Generic;
using TodoUWP.Template10.Model;

namespace TodoUWP.Template10.Data
{
    public interface IDbService
    {
        void AddTodo(string name, DateTime deadline, string description);

        List<TodoItem> GetTodos();

        void DeleteTodo(TodoItem todo);

    }
}
