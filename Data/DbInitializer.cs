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
            context.Database.EnsureCreated();
            if (context.Customers.Any())
            {
                return; //DB is full already
            }
            var customers = new Customer[]
            {
                new Customer{FirstName="Shay",LastName="Horovitz",Gender="Male",PhoneNumber="0543918224",Email="Shay.horo@gmail.com",Password="1234",Country="Israel",City="Rishon Letziyon",ZipCode="1234567",Address="Rotchild 15",Birthday=DateTime.Parse("1977-04-09"),Orders=new List<Order>(),Roles=Role.Customer},
                new Customer{FirstName="Moshe",LastName="Dovav",Gender="Male",PhoneNumber="0506522131",Email="moshedov5@gmail.com",Password="1234",Country="United States",City="La la land",ZipCode="4567891",Address="Avenue Bd 3491",Birthday=DateTime.Parse("1966-03-14"),Orders=new List<Order>(),Roles=Role.Customer},
                new Customer{FirstName="Dror",LastName="Cohen",Gender="Male",PhoneNumber="0523798117",Email="drorcn105@gmail.com",Password="1234",Country="Israel",City="Tel Aviv",ZipCode="1234567",Address="Alanbi 15",Birthday=DateTime.Parse("2003-07-25"),Orders=new List<Order>(),Roles=Role.Admin},
                new Customer{FirstName="Efrat",LastName="Man-Tzur:)",Gender="Women",PhoneNumber="0536678944",Email="efratmath12@gmail.com",Password="1234",Country="Israel",City="Holon",ZipCode="1234567",Address="HaDagan 13",Birthday=DateTime.Parse("1968-05-18"),Orders=new List<Order>(),Roles=Role.Admin},
                new Customer{FirstName="Yafa",LastName="Benin",Gender="Women",PhoneNumber="0584975511",Email="YafaIsTheQueen@gmail.com",Password="1234",Country="Israel",City="Ramat Gan",ZipCode="1234567",Address="Kinor 17",Birthday=DateTime.Parse("1965-11-29"),Orders=new List<Order>(),Roles=Role.Customer}
            };
           
            if (context.Products.Any())
            {
                return; //DB is full already
            }
            var products = new Product[]
            {
                new Product{ProductName="Vans Old Skool",Price=120,Size="39",Brand="Vans",Color="Black&White",Discription="Tommy Hilfiger men's T-shirt. Look and great and feel good in this more sustainably-made tee. It contains independently certified organic cotton, meaning it was grown without chemical pesticides, chemical fertilizers and genetically modified seeds. Part of our Tommy Jeans collection.",Urlimage="https://images.asos-media.com/products/vans-classic-old-skool-black-trainers/11911199-1-black?$XXL$&wid=513&fit=constrain",Category="Shoes Men Women Unisex",Comments=new List<Comment>(),ProductOrders=new List<ProductOrder>()},
                new Product{ProductName="Toms Classic",Price=140,Size="40",Brand="Toms",Color="Blue",Discription="Tommy Hilfiger men's bracelet. Add a relaxed accent to your more formal ensembles--this bracelet accomplishes the task effortlessly.",Urlimage="https://images.asos-media.com/products/toms-classic-espadrilles-in-navy/20592170-1-navy?$n_320w$&wid=317&fit=constrain",Category="Men Shoes",Comments=new List<Comment>(),ProductOrders=new List<ProductOrder>()},
                new Product{ProductName="Lee Copper Classic",Price=70,Size="M",Brand="Lee Copper",Color="Black",Discription="Our deadstock vintage BSOS cat-eye sunglasses offer full UV protection. Recently unearthed and never before worn or sold, we've brushed away the years of dust and brought you the originals!",Urlimage="https://images.asos-media.com/products/lee-jeans-overhead-hoodie-with-chest-logo-in-navy/13706623-1-blueprint?$n_320w$&wid=317&fit=constrain",Category="Men Jeans",Comments=new List<Comment>(),ProductOrders=new List<ProductOrder>()},
                new Product{ProductName="Jeans Demin",Price=270,Size="41",Brand="Calvin Klein",Color="Black",Discription="Cami NYC's 'Aaliyah' gown is minimal and elegant. Cut from white silk-blend charmeuse in a figure-skimming silhouette, it has pin-thin straps and lustrous pearls along the front that lead to a split at the center. Wear it with sandals and a clutch",Urlimage="https://images.asos-media.com/products/jack-jones-intelligence-skinny-fit-stretch-jeans-in-mid-blue/13586448-1-bluedenim?$n_320w$&wid=317&fit=constrain",Category="Men Jeans",Comments=new List<Comment>(),ProductOrders=new List<ProductOrder>()},
                new Product{ProductName="Men T-Shirt Tommy Classic",Price=50,Size="M",Brand="Tommy Hilfiger",Color="Blue",Discription="Central Saint Martins alumnus Stella McCartney launched her own label in 2001, after completing an apprenticeship on Savile Row. A highlight of Paris Fashion Week, McCartney has developed a signature brand of sportswear-inspired clothes, remixed tailoring, and streamlined evening dresses. Technical fabrics, rich colours and precision cuts are integral to her aesthetic, while her ethical accessories always create a buzz.",Urlimage="https://images.asos-media.com/products/tommy-hilfiger-embroidered-flag-logo-t-shirt-in-navy/12591348-1-skycaptain?$n_320w$&wid=317&fit=constrain",Category="Men T-Shirts",Comments=new List<Comment>(),ProductOrders=new List<ProductOrder>()},
                new Product{ProductName="Men T-Shirt BSOS",Price=35,Size="S,M",Brand="BSOS Design",Color="Black",Discription="Say goodbye to waistband gapping & sizing up! Now in our highest-waisted fit.",Urlimage="https://images.asos-media.com/products/calvin-klein-logo-t-shirt-in-navy/12121744-1-navy?$n_320w$&wid=317&fit=constrain",Category="Men T-Shirts",Comments=new List<Comment>(),ProductOrders=new List<ProductOrder>()},
                new Product{ProductName="Skinny wool tuxido suit",Price=500,Size="L",Brand="BSOS Design",Color="Blue naivy",Discription="Vintage 90's Calvin Klein Standard Tan Khaki Pants",Urlimage="https://images.asos-media.com/groups/asos-design-skinny-wool-tuxedo-suit-in-navy/12068-group-1?$n_320w$&wid=317&fit=constrain",Category="Suits Men",Comments=new List<Comment>(),ProductOrders=new List<ProductOrder>()},
                new Product{ProductName="Wedding Skinny Suit In Dark Blue",Price=550,Size="M",Brand="BSOS Design",Color="Black&Blue",Discription="Boots really are a girls best friend. Our Claudia Black Ruched Knee High Heeled Boots are a stunning knee high PU boot with ruched detailing and a heel, perfect for brunchin', nights out and dressing up your day-to-day fits.",Urlimage="https://images.asos-media.com/groups/asos-design-wedding-skinny-suit-in-dark-blue-crosshatch/25356-group-1?$n_320w$&wid=317&fit=constrain",Category="Men Suits",Comments=new List<Comment>(),ProductOrders=new List<ProductOrder>()},
                new Product{ProductName="Tommy Hilfiger Slim Fit Blazer",Price=710,Size="M",Brand="Tommy Hilfiger",Color="Black&Blue",Discription="there heeled sandals in beige",Urlimage="https://images.asos-media.com/products/tommy-hilfiger-structured-slim-fit-blazer/20675304-1-black?$n_320w$&wid=317&fit=constrain",Category="Men Suits",Comments=new List<Comment>(),ProductOrders=new List<ProductOrder>()},
                new Product{ProductName="Siksilk Swim Short In Navy White Stripe",Price=60,Size="M",Brand="BSOS Design",Color="Navy&White",Discription="Tiffany HardWear is elegantly subversive and captures the spirit of the women of New York City. This bold necklace is both refined and rebellious.",Urlimage="https://images.asos-media.com/products/siksilk-swim-short-in-navy-white-stripe/14905276-1-stripe?$n_320w$&wid=317&fit=constrain",Category="Men Swim Wear",Comments=new List<Comment>(),ProductOrders=new List<ProductOrder>()},
                new Product{ProductName="French Connection Small Print Swim Short",Price=50,Size="M",Brand="French Connection",Color="White&Blue",Discription="French Connection small print white and blue geo swim shorts",Urlimage="https://images.asos-media.com/products/french-connection-small-print-white-and-blue-geo-swim-shorts/13624741-1-marine?$n_320w$&wid=317&fit=constrain",Category="Men Swwim Wear",Comments=new List<Comment>(),ProductOrders=new List<ProductOrder>()},
                new Product{ProductName="BSOS Design Nova",Price=100,Size="S",Brand="BSOS Design",Color="Wight",Discription="Tommy Hilfiger men's blazer. Designed for the man on the move, our TH Flex unlined jersey blazer is infused with 4-way stretch for extra comfort and motion in an easy, trim fit. Part of our Tailored collection.",Urlimage="https://images.asos-media.com/products/asos-design-nova-barely-there-heeled-sandals-in-beige-patent/20562609-1-beigepatent?$n_320w$&wid=317&fit=constrain",Category="Women Shoes",Comments=new List<Comment>(),ProductOrders=new List<ProductOrder>()},
                new Product{ProductName="BSOS Design Claudia Knee High Boots",Price=100,Size="38",Brand="BSOS Design",Color="Black",Discription="BSOS DESIGN wedding skinny suit in dark blue crosshatch",Urlimage="https://images.asos-media.com/products/asos-design-claudia-knee-high-boots-in-black/20545911-1-black?$n_320w$&wid=317&fit=constrain",Category="Women Shoes",Comments=new List<Comment>(),ProductOrders=new List<ProductOrder>()},
                new Product{ProductName="Calvin Klein Khaki",Price=60,Size="S",Brand="Calvin Klein",Color="Khaki Green",Discription="BSOS DESIGN skinny wool tuxedo suit jacket in navy",Urlimage="https://images.asos-media.com/products/missguided-co-ord-riot-jeans-in-khaki/20399776-1-khaki?$n_320w$&wid=317&fit=constrain",Category="Women Jeans",Comments=new List<Comment>(),ProductOrders=new List<ProductOrder>()},
                new Product{ProductName="Curvey Mom Jeans",Price=50,Size="S",Brand="American Eagle",Color="Black",Discription="Monogram Logo Crewneck T-Shirt",Urlimage="https://images.asos-media.com/products/american-eagle-curvy-mom-jeans-in-black/14739097-1-rockerblack080?$n_320w$&wid=317&fit=constrain",Category="Women Jeans",Comments=new List<Comment>(),ProductOrders=new List<ProductOrder>()},
                new Product{ProductName="BSOS Edition Satin Drape Maxi Dress In Lemon Bloom Print",Price=180,Size="S",Brand="BSOS Design",Color="Colorfull",Discription="The Old Skool, Vans classic skate shoe and the first to bear the iconic side stripe, has a low-top lace-up silhouette with a durable suede and canvas upper with padded tongue and lining and Vans signature Waffle Outsole.",Urlimage="https://images.asos-media.com/products/asos-edition-satin-drape-maxi-dress-in-lemon-bloom-print/14722289-1-multi?$n_320w$&wid=317&fit=constrain",Category="Women Dress",Comments=new List<Comment>(),ProductOrders=new List<ProductOrder>()},
                new Product{ProductName="BSOS Edition Embellished Cami Midi Dress",Price=160,Size="s",Brand="BSOS Design",Color="Gold",Discription="Toms classic espadrilles in BLUE canvas",Urlimage="https://images.asos-media.com/products/asos-edition-embellished-cami-midi-dress/14788662-1-blush?$n_320w$&wid=317&fit=constrain",Category="Women Dress",Comments=new List<Comment>(),ProductOrders=new List<ProductOrder>()},
                new Product{ProductName="Retro Sun Glasses",Price=80,Size="No Size",Brand="BSOS Design",Color="Gold&Brown",Discription="2 Flap pockets & 2 side pockets",Urlimage="https://images.asos-media.com/products/asos-design-retro-sunglasses-in-gold-with-brown-tort-detail-and-smoked-lens/14329920-1-tort?$n_320w$&wid=317&fit=constrain",Category="Acssesories",Comments=new List<Comment>(),ProductOrders=new List<ProductOrder>()},
                new Product{ProductName="Tommy Hilfiger Braclet",Price=60,Size="No Size",Brand="Tommy Hilfiger",Color="Blue", Discription = "ATHLETIC TAPER FIT CALDER 4-WAY STRETCH JEANS",Urlimage="https://images.asos-media.com/products/tommy-hilfiger-woven-bracelet-in-navy/11559677-1-blue?$n_320w$&wid=317&fit=constrain",Category="Accessories",Comments=new List<Comment>(),ProductOrders=new List<ProductOrder>()},
                new Product{ProductName="Silver neckless",Price=120,Size="No Size",Brand="No Brand",Color="Silver",Discription = "SikSilk swim short in navy & white stripe",Urlimage="https://images.asos-media.com/products/rains-msn-large-backpack-in-black/11649179-1-black?$n_320w$&wid=317&fit=constrain",Category="Accessories",Comments=new List<Comment>(),ProductOrders=new List<ProductOrder>()},
                new Product{ProductName="Design London Exclusive Pearl Choker Necklace In Gold",Price=60,Size="",Brand="Design London",Color="Gold",Discription="DesignB London Exclusive pearl choker necklace in gold",Urlimage="https://images.asos-media.com/products/designb-london-exclusive-pearl-choker-necklace-in-gold/14174163-1-gold?$n_320w$&wid=317&fit=constrain",Category="Accessories",Comments=new List<Comment>(),ProductOrders=new List<ProductOrder>()},
            };
            

            //****************************
            var orders = new Order[]
            {
                new Order{OrderDate=DateTime.Parse("20/09/2020"),TotalPrice=60,CustomerId=customers[4].Id,Customer=customers[4],ProductOrders=new List<ProductOrder>() },
                new Order{OrderDate=DateTime.Parse("25/08/2020"),TotalPrice=140,CustomerId=customers[1].Id,Customer=customers[1],ProductOrders=new List<ProductOrder>()},
                new Order{OrderDate=DateTime.Parse("06/09/2020"),TotalPrice=85,CustomerId=customers[0].Id,Customer=customers[0],ProductOrders=new List<ProductOrder>()},
            };
            var productOrders = new ProductOrder[]
            {
                new ProductOrder{ProductId=products[17].ProductId,OrderId=orders[0].OrderID,Product=products[17],Order=orders[0],Amount=1},
                new ProductOrder{ProductId=products[1].ProductId,OrderId=orders[1].OrderID,Product=products[1],Order=orders[1]},
                new ProductOrder{ProductId=products[4].ProductId,OrderId=orders[2].OrderID,Product=products[4],Order=orders[2]},
                new ProductOrder{ProductId=products[5].ProductId,OrderId=orders[2].OrderID,Product=products[5],Order=orders[2]},
            };
            orders[0].ProductOrders.Add(productOrders[0]);
            orders[1].ProductOrders.Add(productOrders[1]);
            orders[2].ProductOrders.Add(productOrders[2]);
            orders[2].ProductOrders.Add(productOrders[3]);

            products[17].ProductOrders.Add(productOrders[0]);
            products[1].ProductOrders.Add(productOrders[1]);
            products[4].ProductOrders.Add(productOrders[2]);
            products[5].ProductOrders.Add(productOrders[3]);

            var comments = new Comment[]
           {
                new Comment{Title="Awesome",Body="Very good item thanks a lot. I highly recommend this item, and will buy more",SentBy="Moshe",Posted=DateTime.Parse("09-08-2020"),IP="",ProductId=products[1].ProductId,Product=products[1]},
                new Comment{Title="Nice",Body="Very nice item thanks a lot. I highly recommend this item, and will buy more",SentBy="Anonimus",Posted=DateTime.Parse("09-09-2020"),IP="",ProductId=products[0].ProductId,Product=products[0]},
                new Comment{Title="Amazing",Body="The Best item thanks a lot. I highly recommend this item, and will buy more",SentBy="Queen Of Nothing",Posted=DateTime.Parse("09-10-2020"),IP="",ProductId=products[0].ProductId,Product=products[0] }
           };
            products[1].Comments.Add(comments[0]);
            products[0].Comments.Add(comments[1]);
            products[0].Comments.Add(comments[2]);

            foreach (Customer c in customers)
            {
                context.Customers.Add(c);
            }
            context.SaveChanges();
            /*****************************/
            foreach (Product p in products)
            {
                context.Products.Add(p);
            }
            context.SaveChanges();
            //*****************************
            foreach (Order o in orders)
            {
                context.Orders.Add(o);
                context.SaveChanges();
            }
            context.SaveChanges();
            //*****************************

            foreach (ProductOrder po in productOrders)
            {
                context.ProductOrder.Add(po);
            }
            context.SaveChanges();

            foreach (Comment c in comments)
            {
                context.Comment.Add(c);
            }
            context.SaveChanges();
        }
    }
}

