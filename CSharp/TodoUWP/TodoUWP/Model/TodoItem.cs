using System;

namespace TodoUWP.Model
{
    public class TodoItem
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }

        public override string ToString()
        {
            return $"Name: {Name}\tDeadline: {Deadline}\tDescription: {Description}";
        }
    }
}
