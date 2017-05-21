using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MealPicker.Model
{
    public class Relation
    {
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        public Guid MealId { get; set; }
    }
}
