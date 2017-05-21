using MealPicker.Data;
using MealPicker.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MealPicker.View.Meal
{
    public partial class NewMeal : Form
    {
        private MainApp mainApp;


        public NewMeal(MainApp indexMeal)
        {
            this.mainApp = indexMeal;
            InitializeComponent();
            foreach (var category in Context.Categories)
            {
                checkedListBox1.Items.Add(category.Name);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var mealName = textBox1.Text;
            if(Context.Meals.Any(m => m.Name == mealName))
            {
                MessageBox.Show("There is already a meal with that name. Try a new name.");
            }
            else
            {
                var newMeal = new Model.Meal()
                {
                    Id = Guid.NewGuid(),
                    LastUsed = DateTime.Now,
                    Name = mealName
                };
                Context.Meals.Add(newMeal);
                var checkedItems = checkedListBox1.CheckedItems;
                var items = checkedListBox1.Items;
                foreach (object item in items)
                {
                    var category = Context.Categories.FirstOrDefault(c => c.Name == item.ToString());
                    if (checkedItems.Contains(item))
                    {
                        Context.Relations.Add(new Relation()
                        {
                            CategoryId = category.Id,
                            MealId = newMeal.Id,
                            Id = Guid.NewGuid()
                        });
                    }
                }
                Context.Save(Context.XFile);
                this.Close();
            }
        }
    }
}
