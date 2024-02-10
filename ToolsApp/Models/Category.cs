using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolsApp.Models
{
    public class Category
    {
        public string Name { get; set; }

        public string MaterialIcon { get; set; }

        public string Path { get; set; }

        public Category(string name, string icon, string path)
        {
            this.Name = name;
            this.MaterialIcon = icon;
            this.Path = path;
        }

        public Category(string name, string icon)
        {
            this.Name = name;
            this.MaterialIcon = icon;
            this.Path = string.Empty;
        }
    }
}
