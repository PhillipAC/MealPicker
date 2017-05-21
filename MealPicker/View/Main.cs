using MealPicker.Model;
using MealPicker.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using MealPicker.Data;
using MealPicker.View.Meal;

namespace MealPicker
{
    public partial class MainApp : Form
    {
        public MainApp()
        {
            InitializeComponent();

            Context.Load(Context.XFile);

            updateCheckList();
        }

        public void updateCheckList()
        {
            checkedListBox1.Items.Clear();
            foreach(Category category in Context.Categories)
            {
                checkedListBox1.Items.Add(category.Name);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Random rand = new Random();
            var items = checkedListBox1.CheckedItems;
            var selectCategoriesId = Context.Categories.Where(c => items.Contains(c.Name)).Select(c => c.Id);
            var relations = Context.Relations.Where(c => selectCategoriesId.Contains(c.CategoryId))
                .Select(c => c.MealId).Distinct();
            var mealChoices = Context.Meals.Where(m => relations.Contains(m.Id)).ToList();
            if(mealChoices.Count > 0) { 
                string meal = mealChoices[rand.Next(mealChoices.Count())]?.Name;
                Choice.Text = meal;
            }
            else
            {
                MessageBox.Show("There are no meals that are in those categories");
            }


        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void MealDisplay_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form newCategories = new NewCategories(this);
            newCategories.Focus();
            newCategories.ShowDialog();
        }

        private void indexToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form indexMeals = new IndexMeal(this);
            indexMeals.Focus();
            indexMeals.ShowDialog();
        }

        private void newToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form newMeal = new NewMeal(this);
            newMeal.Focus();
            newMeal.ShowDialog();
        }
    }
}
