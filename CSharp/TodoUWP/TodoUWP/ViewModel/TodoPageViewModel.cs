using System;
using System.Collections.ObjectModel;
using TodoUWP.Data;
using TodoUWP.Model;

namespace TodoUWP.ViewModel
{
    public class TodoPageViewModel : BindableBase
    {
        private string _newTodoName;
        public string NewTodoName
        {
            get { return _newTodoName; }
            set {SetProperty(ref _newTodoName, value, nameof(NewTodoName));}
        }

        private string _newTodoDescription;
        public string NewTodoDescription
        {
            get { return _newTodoDescription; }
            set { SetProperty(ref _newTodoDescription, value, nameof(NewTodoDescription)); }
        }

        private DateTimeOffset _newTodoDeadline;
        public DateTimeOffset NewTodoDeadline
        {
            get { return _newTodoDeadline; }
            set { SetProperty(ref _newTodoDeadline, value, nameof(NewTodoDeadline)); }
        }

        public ObservableCollection<TodoItem> Todos { get; set; }

        private TodoItem _selectedTodo;
        public TodoItem SelectedTodo
        {
            get { return _selectedTodo; }
            set { SetProperty(ref _selectedTodo, value, nameof(SelectedTodo)); }
        }

        private IDbService dbService;
        public TodoPageViewModel()
        {
            dbService = new DbService();

            Todos = new ObservableCollection<TodoItem>();
            AddTodoCommand = new CommandBase(AddTodoCmd);
            GetTodosCommand = new CommandBase(GetTodosCmd);
            DeleteTodo = new CommandBase(DeleteTodoCmd);

            NewTodoDeadline = DateTime.Now;
            SelectedTodo = null;
        }

        public CommandBase AddTodoCommand { get; }
        private void AddTodoCmd()
        {
            dbService.AddTodo(NewTodoName, new DateTime(NewTodoDeadline.Year, NewTodoDeadline.Month, NewTodoDeadline.Day), NewTodoDescription);
            NewTodoName = "";
            NewTodoDeadline = DateTimeOffset.Now;
            NewTodoDescription = "";
        }
        public CommandBase GetTodosCommand { get; }
        private void GetTodosCmd()
        {
            Todos.Clear();
            var todos = dbService.GetTodos();
            foreach (var item in todos)
            {
                Todos.Add(item);
            }
        }

        public CommandBase DeleteTodo { get; }
        private void DeleteTodoCmd()
        {
            dbService.DeleteTodo(SelectedTodo);
            SelectedTodo = null;
        }
    }
}
