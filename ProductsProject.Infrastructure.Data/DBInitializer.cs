using Core.Entity;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProductsProject.Infrastructure.Data
{
    public class DBInitializer : IDBInitializer
    {
        public void Initialize(Context context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            /*  List<Product> products = new List<Product>
             {
              new Product {Name="Deploy ProductWebApi"}
              };*/

            List<Product> products = new List<Product>
               {
                   new Product{
                       Name = "Computer Screen",
                       Color = "White",
                       Type = "Apple",
                       Price = 12000,
                       CreatedDate =  DateTime.Now.Date.AddYears(-5)
                   },
                    new Product{
                       Name = "Geforce gtx 3080",
                       Color = "Green",
                       Type = "Graphic Card",
                       Price = 13000,
                       CreatedDate =  DateTime.Now.Date.AddYears(-1)
                   },
                     new Product{
                       Name = "Milk",
                       Color = "White",
                       Type = "Almond",
                       Price = 15000,
                       CreatedDate =  DateTime.Now.Date.AddYears(-2)
                   }, 
                        new Product{
                       Name = "Face Mask",
                       Color = "Yellow",
                       Type = "Pokemon",
                       Price = 500,
                       CreatedDate =  DateTime.Now.Date.AddYears(-3)
                   }
               };

            context.Products.AddRange(products);
            context.SaveChanges();
        }
    }
}
