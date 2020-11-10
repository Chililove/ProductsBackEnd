using Core.Entity;
using Microsoft.EntityFrameworkCore.Internal;
using ProductsProject.Infrastructure.Data.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProductsProject.Infrastructure.Data
{
    public class DBInitializer : IDBInitializer
    {
        private IAuthenticationHelper authenticationHelper;
        
        public DBInitializer(IAuthenticationHelper authenticationHelp)
        {
            authenticationHelper = authenticationHelp; 
            
        }

        public void Initialize(Context context)
        {
           context.Database.EnsureDeleted();
           // context.Database.EnsureCreated();

            context.Database.EnsureCreated();
            if (context.Products.Any()) 
            {
                return;
            }
            //outcomment to work without auth
             List<Product> items = new List<Product>
             {
                 new Product { IsComplete=true, Name="I did it!!"},
                 new Product { IsComplete=false, Name="Failed...again"},
                 new Product { IsComplete=false, Name="<h3>Message from a Black Hat! Ha, ha, ha...<h3>"}
             };


            List<Product> products = new List<Product>
               {
                   new Product{
                       Name = "Computer Screen",
                       Color = "White",
                       Type = "Apple",
                       Price = 12000,
                       CreatedDate =  DateTime.Now
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

            string password = "1234";
            byte[] passwordHashNadia, passwordSaltNadia, passwordHashMartin, passwordSaltMartin;
            authenticationHelper.CreatePasswordHash(password, out passwordHashNadia, out passwordSaltNadia);
            authenticationHelper.CreatePasswordHash(password, out passwordHashMartin, out passwordSaltMartin);

            List<User> users = new List<User>
                {
                new User{
                    Username = "UserNadia",
                    PasswordHash = passwordHashNadia,
                    PasswordSalt = passwordSaltNadia,
                    IsAdmin = false
                         },
                new User {
                    Username = "AdminMartin",
                    PasswordHash = passwordHashMartin,
                    PasswordSalt = passwordSaltMartin,
                    IsAdmin = true
                        }
                };



            context.Products.AddRange(items);
            context.Products.AddRange(products);
            context.Users.AddRange(users);
            
            context.SaveChanges();
        }
    }
}
