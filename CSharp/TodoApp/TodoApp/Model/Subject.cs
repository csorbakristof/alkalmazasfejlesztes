using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TodoApp.Model
{
    public class Subject
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<TodoItem> Todos { get; set; }

        public override string ToString()
        {
            return $"SUBJECT Id: {Id}\tName: {Name}";
        }
    }
}
