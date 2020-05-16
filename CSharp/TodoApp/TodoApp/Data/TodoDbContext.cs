using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TodoApp.Model;

namespace TodoApp.Data
{
    public class TodoDbContext : DbContext
    {        
        public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options)
        {
            
        }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<TodoItem> TodoItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TodoItem>()
                .HasOne<Subject>(t => t.Subject)
                .WithMany(s => s.Todos);
        }
    }
}
