using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TodoApp.Model;

namespace TodoApp.Data
{
    public class DbService
    {
        private TodoDbContext dbContext;

        public DbService(TodoDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void AddSubject(string name)
        {
            Subject s = new Subject() { Name = name };
            dbContext.Subjects.Add(s);
            dbContext.SaveChanges();
        }

        public List<Subject> GetSubjects()
        {
            var subjects = dbContext.Subjects.ToList();
            return subjects;
        }

        public int GetSubjectId(string subjectName)
        {
            var subject = dbContext.Subjects.Where(s => s.Name == subjectName).FirstOrDefault();

            return subject.Id;
        }

        public void AddTodo(string name, DateTime deadline, string subjectName)
        {
            var subject = dbContext.Subjects.Include(s=>s.Todos).Where(s => s.Name == subjectName).FirstOrDefault();

            TodoItem todo = new TodoItem()
            {
                Name = name,
                Deadline = deadline
            };
            
            subject.Todos.Add(todo);
            dbContext.SaveChanges();
        }

        public void AddTodo2(string name, DateTime deadline, int subjectid)
        {
            var subject = dbContext.Subjects.Include(s => s.Todos).SingleOrDefault(s => s.Id == subjectid);

            TodoItem todo = new TodoItem()
            {
                Name = name,
                Deadline = deadline
            };

            subject.Todos.Add(todo);
            dbContext.SaveChanges();
        }

        public List<TodoItem> GetTodos()
        {
            var todos = dbContext.TodoItems.ToList();
            return todos;
        }

        public List<TodoItem> GetTodosForSubject(int subjectId)
        {
            var subject = dbContext.Subjects.Include(s => s.Todos).SingleOrDefault(s => s.Id == subjectId);

            var todos = dbContext.TodoItems.Where(t => t.Subject == subject).ToList();

            return todos;
        }

        public List<TodoItem> GetTodosForSubject2(int subjectId)
        {
            var todos = dbContext.TodoItems.Where(t => t.Subject.Id == subjectId).ToList();

            return todos;
        }

        public void EditTodo(int subjectId, string oldName, string newName, DateTime oldDeadline, DateTime newDeadline)
        {
            var todo = dbContext.TodoItems.Where(t => t.Subject.Id == subjectId)
                .Where(t => t.Name == oldName)
                .Where(t=>t.Deadline==oldDeadline)
                .FirstOrDefault();

            todo.Name = newName;
            todo.Deadline = newDeadline;

            dbContext.SaveChanges();
        }

        public void EditTodo(int subjectId, string oldName, string newName)
        {
            var todo = dbContext.TodoItems.Where(t => t.Subject.Id == subjectId)
                .Where(t => t.Name == oldName)
                .FirstOrDefault();

            todo.Name = newName;
            
            dbContext.SaveChanges();
        }

        public void DeleteTodo(int subjectId, string name)
        {
            var todo = dbContext.TodoItems.Where(t => t.Subject.Id == subjectId)
                .Where(t => t.Name == name)
                .FirstOrDefault();

            dbContext.TodoItems.Remove(todo);

            dbContext.SaveChanges();
        }

        public void DeleteSubject(int subjectId)
        {
            var subject = dbContext.Subjects.Find(subjectId);

            dbContext.Subjects.Remove(subject);
            dbContext.SaveChanges();
        }

        public void DeleteSubjectWithTodos(int subjectId)
        {
            var subject = dbContext.Subjects.Find(subjectId);

            var todos = dbContext.TodoItems.Where(t => t.Subject == subject).ToList();

            dbContext.TodoItems.RemoveRange(todos);

            dbContext.Subjects.Remove(subject);
            dbContext.SaveChanges();
        }
    }
}
