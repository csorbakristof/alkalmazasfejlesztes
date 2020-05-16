using System;
using System.Collections.ObjectModel;
using Template10.Mvvm;
using TodoUWP.Template10.Data;
using TodoUWP.Template10.Model;

namespace TodoUWP.Template10.ViewModel
{
    public class TodoPageViewModel : ViewModelBase
    {
        private string _newTodoName;
        public string NewTodoName
        {
            get { return _newTodoName; }
            set {Set(ref _newTodoName, value, nameof(NewTodoName));}
        }

        private string _newTodoDescription;
        public string NewTodoDescription
        {
            get { return _newTodoDescription; }
            set { Set(ref _newTodoDescription, value, nameof(NewTodoDescription)); }
        }

        private DateTimeOffset _newTodoDeadline;
        public DateTimeOffset NewTodoDeadline
        {
            get { return _newTodoDeadline; }
            set { Set(ref _newTodoDeadline, value, nameof(NewTodoDeadline)); }
        }

        public ObservableCollection<TodoItem> Todos { get; set; }

        private TodoItem _selectedTodo;
        public TodoItem SelectedTodo
        {
            get { return _selectedTodo; }
            set { Set(ref _selectedTodo, value, nameof(SelectedTodo)); }
        }

        private IDbService dbService;
        public TodoPageViewModel(IDbService dbService)
        {
            this.dbService = dbService;

            Todos = new ObservableCollection<TodoItem>();
            AddTodoCommand = new DelegateCommand(AddTodoCmd);
            GetTodosCommand = new DelegateCommand(GetTodosCmd);
            DeleteTodo = new DelegateCommand(DeleteTodoCmd);

            NewTodoDeadline = DateTime.Now;
            SelectedTodo = null;
        }

        public DelegateCommand AddTodoCommand { get; }
        private void AddTodoCmd()
        {
            dbService.AddTodo(NewTodoName, new DateTime(NewTodoDeadline.Year, NewTodoDeadline.Month, NewTodoDeadline.Day), NewTodoDescription);
            NewTodoName = "";
            NewTodoDeadline = DateTimeOffset.Now;
            NewTodoDescription = "";
        }
        public DelegateCommand GetTodosCommand { get; }
        private void GetTodosCmd()
        {
            Todos.Clear();
            var todos = dbService.GetTodos();
            foreach (var item in todos)
            {
                Todos.Add(item);
            }
        }

        public DelegateCommand DeleteTodo { get; }
        private void DeleteTodoCmd()
        {
            dbService.DeleteTodo(SelectedTodo);
            SelectedTodo = null;
        }   
    }
}
