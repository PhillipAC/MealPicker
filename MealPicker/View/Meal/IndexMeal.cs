using System;
using MealPicker.Data;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MealPicker.View.Meal;

namespace MealPicker.View
{
    public partial class IndexMeal : Form
    {
        private MainApp mainApp;

        public IndexMeal(MainApp mainApp)
        {
            InitializeComponent();
            
            this.mainApp = mainApp;
            foreach(var meal in Context.Meals)
            {
                listBox1.Items.Add(meal.Name);
            }
        }

        public void ReloadMeals()
        {
            listBox1.Items.Clear();
            foreach (var meal in Context.Meals)
            {
                listBox1.Items.Add(meal.Name);
            }
        }

        private void IndexMeal_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var meal = listBox1.SelectedItem;

            var selectedMeal = Context.Meals.FirstOrDefault(m => m.Name == meal.ToString());

            Form editMeal = new EditMeal(this, selectedMeal);
            editMeal.Focus();
            editMeal.ShowDialog();
        }
    }
}
