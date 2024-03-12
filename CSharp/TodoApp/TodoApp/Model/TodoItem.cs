using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TodoApp.Model
{
    public class TodoItem
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Deadline { get; set; }

        public Subject Subject { get; set; }

        public override string ToString()
        {
            return $"TODO Id: {Id}\tName: {Name}\tDeadline: {Deadline}\tSubject name: {Subject.Name}";
        }
    }
}
