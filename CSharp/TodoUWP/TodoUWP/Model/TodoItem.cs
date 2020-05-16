using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
