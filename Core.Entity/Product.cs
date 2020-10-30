using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entity
{
   public class Product
    {
        public int id { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }

        public string Color { get; set; }

        public string Type { get; set; }

        public DateTime CreatedDate { get; set; }

    }
}
