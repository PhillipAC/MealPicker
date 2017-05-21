using MealPicker.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MealPicker.View
{
    public partial class NewCategories : Form
    {
        private MainApp _parentForm;

        public NewCategories(MainApp parent)
        {
            _parentForm = parent;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string input = textBox1.Text;
            string[] categories= input.Split(',');
            foreach(string category in categories)
            {
                Context.Categories.Add(new Model.Category()
                {
                    Id = Guid.NewGuid(),
                    Name = category.Trim(' ')
                });
            }
            _parentForm.updateCheckList();
            this.Close();
        }
    }
}
