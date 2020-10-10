using Microsoft.AspNetCore.Mvc.Formatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSOS.Models;

namespace BSOS.Data
{
    public static class DBInitializer
    {
        public static void Initialize(BSOSContext context)
        {
            //context.Database.EnsureCreated();
            if (context.Customers.Any())
            {
                return; //DB is full already
            }
            var customers = new Customer[]
            {
                new Customer{FirstName="Shay",LastName="Horovitz",Gender="Male",PhoneNumber="0543918224",Email="Shay.horo@gmail.com",Password="1234",Country="Israel",City="Rishon Letziyon",ZipCode="1234567",Address="Rotchild 15",Birthday=DateTime.Parse("1977-04-09"),Orders=new List<Order>()},
                new Customer{FirstName="Moshe",LastName="Dovav",Gender="Male",PhoneNumber="0506522131",Email="moshedov5@gmail.com",Password="1234",Country="United States",City="La la land",ZipCode="4567891",Address="Avenue Bd 3491",Birthday=DateTime.Parse("1966-03-14"),Orders=new List<Order>()},
                new Customer{FirstName="Dror",LastName="Cohen",Gender="Male",PhoneNumber="0523798117",Email="drorcn105@gmail.com",Password="1234",Country="Israel",City="Tel Aviv",ZipCode="1234567",Address="Alanbi 15",Birthday=DateTime.Parse("2003-07-25"),Orders=new List<Order>()},
                new Customer{FirstName="Efrat",LastName="Man-Tzur:)",Gender="Women",PhoneNumber="0536678944",Email="efratmath12@gmail.com",Password="1234",Country="Israel",City="Holon",ZipCode="1234567",Address="HaDagan 13",Birthday=DateTime.Parse("1968-05-18"),Orders=new List<Order>()},
                new Customer{FirstName="Yafa",LastName="Benin",Gender="Women",PhoneNumber="0584975511",Email="YafaIsTheQueen@gmail.com",Password="1234",Country="Israel",City="Ramat Gan",ZipCode="1234567",Address="Kinor 17",Birthday=DateTime.Parse("1965-11-29"),Orders=new List<Order>()}
            };
            foreach (Customer c in customers)
            {
                context.Customers.Add(c);
            }
            context.SaveChanges();
            if (context.Products.Any())
            {
                return; //DB is full already
            }
            var products = new Product[]
            {
                new Product{ProductName="Vans Old Skool",Price=120,Size="39",Brand="Vans",Color="Black&White",Discription="Vans the best for you",Urlimage="https://images.asos-media.com/products/vans-classic-old-skool-black-trainers/11911199-1-black?$XXL$&wid=513&fit=constrain",Category="Shoes Men Women Unisex"},
                new Product{ProductName="Toms Classic",Price=140,Size="40",Brand="Toms",Color="Blue",Discription="Toms since 1972",Urlimage="https://images.asos-media.com/products/toms-classic-espadrilles-in-navy/20592170-1-navy?$n_320w$&wid=317&fit=constrain",Category="Men Shoes"},
                new Product{ProductName="Lee Copper Classic",Price=70,Size="M",Brand="Lee Copper",Color="Black",Discription="Lee Copper since 1980",Urlimage="https://images.asos-media.com/products/lee-jeans-overhead-hoodie-with-chest-logo-in-navy/13706623-1-blueprint?$n_320w$&wid=317&fit=constrain",Category="Men Jeans"},
                new Product{ProductName="Jeans Demin",Price=270,Size="41",Brand="Calvin Klein",Color="Black",Discription="Blanston the best for you",Urlimage="https://images.asos-media.com/products/jack-jones-intelligence-skinny-fit-stretch-jeans-in-mid-blue/13586448-1-bluedenim?$n_320w$&wid=317&fit=constrain",Category="Men Jeans"},
                new Product{ProductName="Men T-Shirt Tommy Classic",Price=50,Size="M",Brand="Tommy Hilfiger",Color="Blue",Discription="Very Uniq",Urlimage="https://images.asos-media.com/products/tommy-hilfiger-embroidered-flag-logo-t-shirt-in-navy/12591348-1-skycaptain?$n_320w$&wid=317&fit=constrain",Category="Men T-Shirts"},
                new Product{ProductName="Men T-Shirt BSOS",Price=35,Size="S,M",Brand="BSOS Design",Color="Black",Discription="Most Uniq",Urlimage="https://images.asos-media.com/products/calvin-klein-logo-t-shirt-in-navy/12121744-1-navy?$n_320w$&wid=317&fit=constrain",Category="Men T-Shirts"},
                new Product{ProductName="Skinny wool tuxido suit",Price=500,Size="L",Brand="BSOS Design",Color="Blue naivy",Discription="",Urlimage="https://images.asos-media.com/groups/asos-design-skinny-wool-tuxedo-suit-in-navy/12068-group-1?$n_320w$&wid=317&fit=constrain",Category="Suits Men"},
                new Product{ProductName="Wedding Skinny Suit In Dark Blue",Price=550,Size="M",Brand="BSOS Design",Color="Black&Blue",Discription="",Urlimage="https://images.asos-media.com/groups/asos-design-wedding-skinny-suit-in-dark-blue-crosshatch/25356-group-1?$n_320w$&wid=317&fit=constrain",Category="Men Suits"},
                new Product{ProductName="Tommy Hilfiger Slim Fit Blazer",Price=710,Size="M",Brand="Tommy Hilfiger",Color="Black&Blue",Discription="",Urlimage="https://images.asos-media.com/products/tommy-hilfiger-structured-slim-fit-blazer/20675304-1-black?$n_320w$&wid=317&fit=constrain",Category="Men Suits"},
                new Product{ProductName="Siksilk Swim Short In Navy White Stripe",Price=60,Size="M",Brand="BSOS Design",Color="Navy&White",Discription="",Urlimage="https://images.asos-media.com/products/siksilk-swim-short-in-navy-white-stripe/14905276-1-stripe?$n_320w$&wid=317&fit=constrain",Category="Men Swim Wear"},
                new Product{ProductName="French Connection Small Print Swim Short",Price=50,Size="M",Brand="French Connection",Color="White&Blue",Discription="",Urlimage="https://images.asos-media.com/products/french-connection-small-print-white-and-blue-geo-swim-shorts/13624741-1-marine?$n_320w$&wid=317&fit=constrain",Category="Men Swwim Wear"},
                new Product{ProductName="BSOS Design Nova",Price=100,Size="S",Brand="BSOS Design",Color="Wight",Discription="",Urlimage="https://images.asos-media.com/products/asos-design-nova-barely-there-heeled-sandals-in-beige-patent/20562609-1-beigepatent?$n_320w$&wid=317&fit=constrain",Category="Women Shoes"},
                new Product{ProductName="BSOS Design Claudia Knee High Boots",Price=100,Size="38",Brand="BSOS Design",Color="Black",Discription="Come To Us",Urlimage="https://images.asos-media.com/products/asos-design-claudia-knee-high-boots-in-black/20545911-1-black?$n_320w$&wid=317&fit=constrain",Category="Women Shoes"},
                new Product{ProductName="Cakvin Klein Khaki",Price=60,Size="S",Brand="Calvin Klein",Color="Khaki Green",Discription="Be a Star",Urlimage="https://images.asos-media.com/products/missguided-co-ord-riot-jeans-in-khaki/20399776-1-khaki?$n_320w$&wid=317&fit=constrain",Category="Women Jeans"},
                new Product{ProductName="Curvey Mom Jeans",Price=50,Size="S",Brand="American Eagle",Color="Black",Discription="The Best For You",Urlimage="https://images.asos-media.com/products/american-eagle-curvy-mom-jeans-in-black/14739097-1-rockerblack080?$n_320w$&wid=317&fit=constrain",Category="Women Jeans"},
                new Product{ProductName="BSOS Edition Satin Drape Maxi Dress In Lemon Bloom Print",Price=180,Size="S",Brand="BSOS Design",Color="Colorfull",Discription="With wolf paint",Urlimage="https://images.asos-media.com/products/asos-edition-satin-drape-maxi-dress-in-lemon-bloom-print/14722289-1-multi?$n_320w$&wid=317&fit=constrain",Category="Women Dress"},
                new Product{ProductName="BSOS Edition Embellished Cami Midi Dress",Price=160,Size="s",Brand="BSOS Design",Color="Gold",Discription="Ladies coat",Urlimage="https://images.asos-media.com/products/asos-edition-embellished-cami-midi-dress/14788662-1-blush?$n_320w$&wid=317&fit=constrain",Category="Women Dress"},
                new Product{ProductName="Retro Sun Glasses",Price=80,Size="No Size",Brand="BSOS Design",Color="Gold&Brown",Discription="",Urlimage="https://images.asos-media.com/products/asos-design-retro-sunglasses-in-gold-with-brown-tort-detail-and-smoked-lens/14329920-1-tort?$n_320w$&wid=317&fit=constrain",Category="Acssesories"},
                new Product{ProductName="Tommy Hilfiger Braclet",Price=60,Size="No Size",Brand="Tommy Hilfiger",Color="Blue",Urlimage="https://images.asos-media.com/products/tommy-hilfiger-woven-bracelet-in-navy/11559677-1-blue?$n_320w$&wid=317&fit=constrain",Discription="Silver Ladies Ring",Category="Accessories"},
                new Product{ProductName="Silver neckless",Price=120,Size="No Size",Brand="No Brand",Color="Silver",Urlimage="https://images.asos-media.com/products/rains-msn-large-backpack-in-black/11649179-1-black?$n_320w$&wid=317&fit=constrain",Discription="Silver Ladies Neckless",Category="Accessories"},
                new Product{ProductName="Design London Exclusive Pearl Choker Necklace In Gold",Price=60,Size="",Brand="Design London",Color="Gold",Discription="",Urlimage="https://images.asos-media.com/products/designb-london-exclusive-pearl-choker-necklace-in-gold/14174163-1-gold?$n_320w$&wid=317&fit=constrain",Category="Accessories"},
            };
            foreach (Product p in products)
            {
                context.Products.Add(p);
            }
            context.SaveChanges();
            //*****************************

            //****************************
            //var orders = new Order[]
            //{
            //    new Order{OrderID=1,OrderDate=DateTime.Now,TotalPrice=260,CustomerId=11},
            //    new Order{OrderID=1,OrderDate=DateTime.Now,TotalPrice=140,CustomerId=12},
            //    new Order{OrderID=3,OrderDate=DateTime.Now,TotalPrice=70,CustomerId=13},
            //    //new Order{OrderID=13,OrderDate=DateTime.Now,TotalPrice=,CustomerId=},
            //    //new Order{OrderID=14,OrderDate=DateTime.Now,TotalPrice=,CustomerId=},
            //    //new Order{OrderID=15,OrderDate=DateTime.Now,TotalPrice=,CustomerId=}
            //};
            //foreach (Order o in orders)
            //{
            //    context.Orders.Add(o);
            //}
            //context.SaveChanges();
            ////*****************************
            //var productOrders = new ProductOrder[]
            //{
            //    new ProductOrder{ProductId=8,OrderId=1},
            //    new ProductOrder{ProductId=9,OrderId=1},
            //    new ProductOrder{ProductId=9,OrderId=2},
            //    new ProductOrder{ProductId=10,OrderId=3},
            //    //new ProductOrder{ProductId=9,OrderId=13},
            //    //new ProductOrder{ProductId=10,OrderId=13}
            //};
            //foreach (ProductOrder po in productOrders)
            //{
            //    context.ProductOrder.Add(po);
            //}
            //context.SaveChanges();
            //var comments = new Comment[]
            //{
            //    new Comment{Id=1,Title="Comment1",Body="stam",SentBy="Moshe",Posted=DateTime.Parse("09-09-2020"),IP="",ProductId=8},
            //    new Comment{Id=2,Title="Comment2",Body="stam",SentBy="Dror",Posted=DateTime.Parse("09-09-2020"),IP="",ProductId=8},
            //    new Comment{Id=3,Title="Comment3",Body="stam",SentBy="Dror",Posted=DateTime.Parse("09-09-2020"),IP="",ProductId=9 }
            //};
            //foreach (Comment c in comments)
            //{
            //    context.Comment.Add(c);
            //}
            //context.SaveChanges();
        }
    }
}

