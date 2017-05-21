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
    public partial class EditMeal : Form
    {
        private IndexMeal indexMeal;
        private Model.Meal _meal;


        public EditMeal(IndexMeal indexMeal, Model.Meal meal)
        {
            this.indexMeal = indexMeal;
            _meal = meal;
            InitializeComponent();
            label1.Text = _meal.Name;
            foreach (var category in Context.Categories)
            {
                checkedListBox1.Items.Add(category.Name);
            }
            var categoriesForMeal = (from r in Context.Relations
                                   join c in Context.Categories on r.CategoryId equals c.Id
                                   where r.MealId == _meal.Id select c);
            foreach(var category in categoriesForMeal)
            {
                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                {
                    if ((string)checkedListBox1.Items[i] == category.Name)
                    {
                        checkedListBox1.SetItemChecked(i, true);
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var mealCategories = (from r in Context.Relations
                                  join c in Context.Categories on r.CategoryId equals c.Id
                                  where r.MealId == _meal.Id select c);
            var checkedItems = checkedListBox1.CheckedItems;
            var items = checkedListBox1.Items;
            foreach (object item in items)
            {
                var category = Context.Categories.FirstOrDefault(c => c.Name == item.ToString());
                if (!checkedItems.Contains(item) && mealCategories.Contains(category))
                {
                    Context.Relations.Remove(Context.Relations.FirstOrDefault(r => r.CategoryId == category.Id && r.MealId == _meal.Id));
                }
                else if(checkedItems.Contains(item) && !mealCategories.Contains(category))
                {
                    Context.Relations.Add(new Relation()
                    {
                        CategoryId = category.Id,
                        MealId = _meal.Id,
                        Id = Guid.NewGuid()
                    });
                }
            }
            Context.Save(Context.XFile);
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Are you sure to delete this item ?",
                                     "Confirm Delete",
                                     MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                var deleteList = Context.Relations.Where(r => r.MealId == _meal.Id).ToList();
                foreach (var relation in deleteList)
                {
                    Context.Relations.Remove(relation);
                }
                Context.Meals.Remove(_meal);
                Context.Save(Context.XFile);
                indexMeal.ReloadMeals();
                this.Close();
            }
            
        }
    }
}
