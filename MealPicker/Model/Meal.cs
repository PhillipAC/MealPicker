using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MealPicker.Model
{
    public class Meal
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime LastUsed { get; set; }
    }
}
