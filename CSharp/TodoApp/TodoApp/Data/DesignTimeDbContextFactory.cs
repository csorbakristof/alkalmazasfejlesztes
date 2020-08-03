using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TodoApp.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<TodoDbContext>
    {
        public TodoDbContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<TodoDbContext> optionBuilder =
                new DbContextOptionsBuilder<TodoDbContext>()
                .UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=TodoDB;Trusted_Connection=True;");

            return new TodoDbContext(optionBuilder.Options);
        }
    }
}
