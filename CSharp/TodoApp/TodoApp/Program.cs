using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using TodoApp.Data;
using TodoApp.Model;

namespace TodoApp
{
    class Program
    {
        public static void Example1()   // Egyszerű adatbeszúrás
        {
            DbContextOptionsBuilder<TodoDbContext> optionBuilder =
                new DbContextOptionsBuilder<TodoDbContext>().UseInMemoryDatabase(databaseName: "TodoDB");

            TodoDbContext context = new TodoDbContext(optionBuilder.Options);

            DbService DbService = new DbService(context);

            DbService.AddSubject("Alkalmazásfejlesztés");
            DbService.AddTodo("HF", new DateTime(2020, 12, 10), "Alkalmazásfejlesztés");

            List<Subject> subjects = DbService.GetSubjects();
            foreach (var item in subjects)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();

            List<TodoItem> todos = DbService.GetTodos();
            foreach (var item in todos)
            {
                Console.WriteLine(item);
            }
        }
        public static void Example2()   // Id használata
        {
            DbContextOptionsBuilder<TodoDbContext> optionBuilder =
                new DbContextOptionsBuilder<TodoDbContext>().UseInMemoryDatabase(databaseName: "TodoDB");

            TodoDbContext context = new TodoDbContext(optionBuilder.Options);

            DbService DbService = new DbService(context);

            DbService.AddSubject("Random GTK-s tárgy");
            int subjectId = DbService.GetSubjectId("Random GTK-s tárgy");
            DbService.AddTodo2("ZH", new DateTime(2020, 11, 23), subjectId);

            List<Subject> subjects = DbService.GetSubjects();
            foreach (var item in subjects)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();

            List<TodoItem> todos = DbService.GetTodos();
            foreach (var item in todos)
            {
                Console.WriteLine(item);
            }
        }
        public static void Example3()   // Több tantárgy, több todo
        {
            DbContextOptionsBuilder<TodoDbContext> optionBuilder =
                new DbContextOptionsBuilder<TodoDbContext>().UseInMemoryDatabase(databaseName: "TodoDB");

            TodoDbContext context = new TodoDbContext(optionBuilder.Options);

            DbService DbService = new DbService(context);

            DbService.AddSubject("Alkalmazásfejlesztés");
            DbService.AddTodo("HF", new DateTime(2020, 12, 10), "Alkalmazásfejlesztés");
            DbService.AddTodo("Vizsga", new DateTime(2020, 12, 23), "Alkalmazásfejlesztés");

            DbService.AddSubject("Random GTK-s tárgy");
            int gtkId = DbService.GetSubjectId("Random GTK-s tárgy");
            DbService.AddTodo2("ZH1", new DateTime(2020, 10, 10), gtkId);
            DbService.AddTodo2("ZH2", new DateTime(2020, 12, 15), gtkId);

            int alkfejlId = DbService.GetSubjectId("Alkalmazásfejlesztés");
            List<TodoItem> alkfejlTodos = DbService.GetTodosForSubject(alkfejlId);
            foreach (var item in alkfejlTodos)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();

            
            List<TodoItem> gtkTodos = DbService.GetTodosForSubject2(gtkId);
            foreach (var item in gtkTodos)
            {
                Console.WriteLine(item);
            }

        }
        public static void Example4()   // Módosítás
        {
            DbContextOptionsBuilder<TodoDbContext> optionBuilder =
                new DbContextOptionsBuilder<TodoDbContext>().UseInMemoryDatabase(databaseName: "TodoDB");

            TodoDbContext context = new TodoDbContext(optionBuilder.Options);

            DbService DbService = new DbService(context);

            DbService.AddSubject("Alkalmazásfejlesztés");
            DbService.AddTodo("HF", new DateTime(2020, 12, 10), "Alkalmazásfejlesztés");
            DbService.AddTodo("Vizsga", new DateTime(2020, 12, 23), "Alkalmazásfejlesztés");

            int alkfejlId = DbService.GetSubjectId("Alkalmazásfejlesztés");
            List<TodoItem> alkfejlTodos = DbService.GetTodosForSubject(alkfejlId);
            foreach (var item in alkfejlTodos)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();

            DbService.EditTodo(alkfejlId, "HF", "Megajánlott jegy", new DateTime(2020, 12, 10), new DateTime(2020, 12, 15));

            alkfejlTodos = DbService.GetTodosForSubject(alkfejlId);
            foreach (var item in alkfejlTodos)
            {
                Console.WriteLine(item);
            }
        }
        public static void Example5()   // EditTodo más paraméterezéssel (polymorfizmus)
        {
            DbContextOptionsBuilder<TodoDbContext> optionBuilder =
                new DbContextOptionsBuilder<TodoDbContext>().UseInMemoryDatabase(databaseName: "TodoDB");

            TodoDbContext context = new TodoDbContext(optionBuilder.Options);

            DbService DbService = new DbService(context);

            DbService.AddSubject("Alkalmazásfejlesztés");
            DbService.AddTodo("HF", new DateTime(2020, 12, 10), "Alkalmazásfejlesztés");
            DbService.AddTodo("Vizsga", new DateTime(2020, 12, 23), "Alkalmazásfejlesztés");

            int alkfejlId = DbService.GetSubjectId("Alkalmazásfejlesztés");
            List<TodoItem> alkfejlTodos = DbService.GetTodosForSubject(alkfejlId);
            foreach (var item in alkfejlTodos)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();

            DbService.EditTodo(alkfejlId, "HF", "Megajánlott jegy");


            alkfejlTodos = DbService.GetTodosForSubject(alkfejlId);
            foreach (var item in alkfejlTodos)
            {
                Console.WriteLine(item);
            }
        }
        public static void Example6()   // Törlés
        {
            DbContextOptionsBuilder<TodoDbContext> optionBuilder =
                new DbContextOptionsBuilder<TodoDbContext>().UseInMemoryDatabase(databaseName: "TodoDB");

            TodoDbContext context = new TodoDbContext(optionBuilder.Options);

            DbService DbService = new DbService(context);

            DbService.AddSubject("Alkalmazásfejlesztés");
            DbService.AddTodo("Megajánlott jegy", new DateTime(2020, 12, 10), "Alkalmazásfejlesztés");
            DbService.AddTodo("Vizsga", new DateTime(2020, 12, 23), "Alkalmazásfejlesztés");

            int alkfejlId = DbService.GetSubjectId("Alkalmazásfejlesztés");
            List<TodoItem> alkfejlTodos = DbService.GetTodosForSubject(alkfejlId);
            foreach (var item in alkfejlTodos)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();

            DbService.DeleteTodo(alkfejlId, "Vizsga");

            alkfejlTodos = DbService.GetTodosForSubject(alkfejlId);
            foreach (var item in alkfejlTodos)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine();

            var todos = DbService.GetTodos();
            foreach (var item in todos)
            {
                Console.WriteLine(item);
            }
        }
        public static void Example7()   // Törlés - nem sikerül
        {
            DbContextOptionsBuilder<TodoDbContext> optionBuilder =
                new DbContextOptionsBuilder<TodoDbContext>().UseInMemoryDatabase(databaseName: "TodoDB");

            TodoDbContext context = new TodoDbContext(optionBuilder.Options);

            DbService DbService = new DbService(context);

            DbService.AddSubject("Alkalmazásfejlesztés");
            DbService.AddTodo("Megajánlott jegy", new DateTime(2020, 12, 10), "Alkalmazásfejlesztés");
            DbService.AddTodo("Vizsga", new DateTime(2020, 12, 23), "Alkalmazásfejlesztés");

            DbService.AddSubject("Random GTK-s tárgy");
            int gtkId = DbService.GetSubjectId("Random GTK-s tárgy");
            DbService.AddTodo2("ZH1", new DateTime(2020, 10, 10), gtkId);
            DbService.AddTodo2("ZH2", new DateTime(2020, 12, 15), gtkId);

            int alkfejlId = DbService.GetSubjectId("Alkalmazásfejlesztés");
            List<TodoItem> alkfejlTodos = DbService.GetTodosForSubject(alkfejlId);
            foreach (var item in alkfejlTodos)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();

            List<TodoItem> gtkTodos = DbService.GetTodosForSubject(gtkId);
            foreach (var item in gtkTodos)
            {
                Console.WriteLine(item);
            }

            DbService.DeleteSubject(gtkId);

            var subjects = DbService.GetSubjects();
            foreach (var item in subjects)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();

            var todos = DbService.GetTodos();
            foreach (var item in todos)
            {
                Console.WriteLine(item);
            }
        }
        public static void Example8()   // Függő rekord törlése
        {
            DbContextOptionsBuilder<TodoDbContext> optionBuilder =
                new DbContextOptionsBuilder<TodoDbContext>().UseInMemoryDatabase(databaseName: "TodoDB");

            TodoDbContext context = new TodoDbContext(optionBuilder.Options);

            DbService DbService = new DbService(context);

            DbService.AddSubject("Alkalmazásfejlesztés");
            DbService.AddTodo("Megajánlott jegy", new DateTime(2020, 12, 10), "Alkalmazásfejlesztés");
            DbService.AddTodo("Vizsga", new DateTime(2020, 12, 23), "Alkalmazásfejlesztés");

            DbService.AddSubject("Random GTK-s tárgy");
            int gtkId = DbService.GetSubjectId("Random GTK-s tárgy");
            DbService.AddTodo2("ZH1", new DateTime(2020, 10, 10), gtkId);
            DbService.AddTodo2("ZH2", new DateTime(2020, 12, 15), gtkId);

            int alkfejlId = DbService.GetSubjectId("Alkalmazásfejlesztés");
            List<TodoItem> alkfejlTodos = DbService.GetTodosForSubject(alkfejlId);
            foreach (var item in alkfejlTodos)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();

            List<TodoItem> gtkTodos = DbService.GetTodosForSubject(gtkId);
            foreach (var item in gtkTodos)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();

            DbService.DeleteSubjectWithTodos(gtkId);

            var subjects = DbService.GetSubjects();
            foreach (var item in subjects)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();

            var todos = DbService.GetTodos();
            foreach (var item in todos)
            {
                Console.WriteLine(item);
            }
        }
        public static void Example9()   // SqlServer használata
        {
            DbContextOptionsBuilder<TodoDbContext> optionBuilder =
                new DbContextOptionsBuilder<TodoDbContext>()
                .UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=TodoDB;Trusted_Connection=True;");
            
            TodoDbContext context = new TodoDbContext(optionBuilder.Options);
            
            DbService DbService = new DbService(context);

            DbService.AddSubject("Alkalmazásfejlesztés");
            DbService.AddTodo("Megajánlott jegy", new DateTime(2020, 12, 10), "Alkalmazásfejlesztés");
            DbService.AddTodo("Vizsga", new DateTime(2020, 12, 23), "Alkalmazásfejlesztés");

            DbService.AddSubject("Random GTK-s tárgy");
            int gtkId = DbService.GetSubjectId("Random GTK-s tárgy");
            DbService.AddTodo2("ZH1", new DateTime(2020, 10, 10), gtkId);
            DbService.AddTodo2("ZH2", new DateTime(2020, 12, 15), gtkId);

            int alkfejlId = DbService.GetSubjectId("Alkalmazásfejlesztés");
            List<TodoItem> alkfejlTodos = DbService.GetTodosForSubject(alkfejlId);
            foreach (var item in alkfejlTodos)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();

            List<TodoItem> gtkTodos = DbService.GetTodosForSubject(gtkId);
            foreach (var item in gtkTodos)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();

            DbService.DeleteSubjectWithTodos(gtkId);

            var subjects = DbService.GetSubjects();
            foreach (var item in subjects)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();

            var todos = DbService.GetTodos();
            foreach (var item in todos)
            {
                Console.WriteLine(item);
            }

            //cleanup after use
            //DbService.DeleteSubjectWithTodos(alkfejlId);
        }
        static void Main(string[] args)
        {
            Example1();

            //Example2();

            //Example3();

            //Example4();

            //Example5();

            //Example6();

            //Example7();

            //Example8();

            //Example9();

            Console.ReadLine();
        }
    }
}
