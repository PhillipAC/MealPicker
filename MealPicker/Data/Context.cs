using MealPicker.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MealPicker.Data
{
    public static class Context
    {
        public static string XFile = "meal.xml";
        public static XmlDocument _xDoc;
        public static List<Meal> Meals = new List<Meal>();
        public static List<Category> Categories = new List<Category>();
        public static List<Relation> Relations = new List<Relation>();

        public static void Save(string xmlFile)
        {
            if (!File.Exists(xmlFile))
            {
                var fs = File.Create(xmlFile);
                fs.Dispose();
            }
            if (File.Exists(xmlFile)) {
                //try { 
                //File.SetAttributes(xmlFile, FileAttributes.Normal);
                //FileIOPermission filePermission =
                //         new FileIOPermission(FileIOPermissionAccess.AllAccess, xmlFile);
                //}
                //catch
                //{

                //}
                using (FileStream fs = new FileStream(xmlFile, FileMode.OpenOrCreate))
                {
                    fs.SetLength(0);
                    using (XmlWriter w = XmlWriter.Create(fs))
                    {
                        w.WriteStartElement("MealPicker");
                        foreach(var meal in Context.Meals)
                        {
                            w.WriteStartElement("Meal");
                            w.WriteElementString("Name", meal.Name);
                            var categories = (from r in Context.Relations
                                              join c in Context.Categories on r.CategoryId equals c.Id
                                              where r.MealId == meal.Id select c);
                            foreach(var category in categories)
                            {
                                w.WriteElementString("Category", category.Name);
                            }
                            w.WriteElementString("LastUsed", meal.LastUsed.ToString());
                            w.WriteEndElement();

                        };
                        w.WriteEndElement();
                        w.Flush();
                    }
                }
            }
        }

        public static void Load(string xmlFile)
        {
            if (!File.Exists(xmlFile))
            {
                var fs = File.Create(xmlFile);
                fs.Dispose();
            }
            else
            {
                _xDoc = new XmlDocument();
                _xDoc.Load(xmlFile);
                Meals = new List<Meal>();
                Categories = new List<Category>();
                Relations = new List<Relation>();

                XmlNodeList categories = _xDoc.DocumentElement.SelectNodes("//MealPicker/Meal/Category");
                List<string> categoryNames = categories.Cast<XmlNode>().Select(node => node.InnerText).Distinct().ToList();
                foreach (string category in categoryNames)
                {
                    Categories.Add(new Category
                    {
                        Name = category,
                        Id = Guid.NewGuid()
                    });
                }

                XmlNodeList meals = _xDoc.DocumentElement.SelectNodes("//MealPicker/Meal");
                foreach (XmlNode meal in meals)
                {
                    Guid mealId = Guid.NewGuid();
                    Meals.Add(new Meal
                    {
                        Name = meal.SelectSingleNode("Name").InnerText,
                        Id = mealId,
                        LastUsed = DateTime.Parse(meal.SelectSingleNode("LastUsed").InnerText)
                    });

                    foreach (XmlNode category in meal.SelectNodes("Category"))
                    {
                        var categoryId = Categories.FirstOrDefault(c => c.Name == category.InnerText).Id;
                        Relations.Add(new Relation()
                        {
                            Id = Guid.NewGuid(),
                            CategoryId = categoryId,
                            MealId = mealId
                        });
                    }
                }
            }
        }


    }
}
