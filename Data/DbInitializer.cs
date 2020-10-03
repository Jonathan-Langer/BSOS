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
                new Customer{FirstName="Shay",LastName="Horovitz",Gender="Man",PhoneNumber="0543918224",Email="Shay.horo@gmail.com",Password="1234",Country="Israel",City="Rishon Letziyon",ZipCode="1234567",Address="Rotchild 15",Birthday=DateTime.Parse("1977-04-09")},
                new Customer{FirstName="Moshe",LastName="Dovav",Gender="Man",PhoneNumber="0506522131",Email="moshedov5@gmail.com",Password="1234",Country="United States",City="La la land",ZipCode="4567891",Address="Avenue Bd 3491",Birthday=DateTime.Parse("1966-03-14")},
                new Customer{FirstName="Dror",LastName="Cohen",Gender="Man",PhoneNumber="0523798117",Email="drorcn105@gmail.com",Password="1234",Country="Israel",City="Tel Aviv",ZipCode="1234567",Address="Alanbi 15",Birthday=DateTime.Parse("2003-07-25")}
            };
            foreach (Customer c in customers)
            {
                context.Customers.Add(c);
            }
            context.SaveChanges();
            var products = new Product[]
            {
                new Product{ProductName="Vans Old Skool",Price=120,Size="39",Brand="Vans",Color="Black&White",Discription="Vans the best for you",Category="Shoes Men Women Unisex"},
                new Product{ProductName="Toms Classic",Price=140,Size="40",Brand="Toms",Color="Blue",Discription="Toms since 1972",Category="Men Shoes"},
                new Product{ProductName="Lee Copper Classic",Price=70,Size="M",Brand="Lee Copper",Color="Black",Discription="Lee Copper since 1980",Category="Men Jeans"},
                new Product{ProductName="Blanston Boots",Price=270,Size="41",Brand="Blanston",Color="Brown",Discription="Blanston the best for you",Category="Shoes Unisex Men Women "},
                new Product{ProductName="Prada Dress",Price=3200,Size="M",Brand="Prada",Color="Black",Discription="Very Uniq",Category="Dress Women Prada Uniq"},
                new Product{ProductName="Rolex watch",Price=1200,Size="No Size",Brand="Rolex",Color="Silver",Discription="Most Uniq",Category="Accesories"},
                new Product{ProductName="Timberland Boots",Price=350,Size="42",Brand="Timberland",Color="Brown",Discription="Come To Us",Category="Shoes"},
                new Product{ProductName="Adidas Super Star",Price=250,Size="43",Brand="Adidas",Color="Any",Discription="Be a Star",Category="Shoes"},
                new Product{ProductName="Nike Air",Price=220,Size="41",Brand="Nike",Color="White",Discription="The Best For You",Category="Shoes"},
                new Product{ProductName="T-Shirt",Price=40,Size="S",Brand="No Brand",Color="Black",Discription="With wolf paint",Category="T-Shirt Shirt Men"},
                new Product{ProductName="Coat",Price=100,Size="M",Brand="No Brand",Color="Many",Discription="Ladies coat",Category="Coat Women winter"},
                new Product{ProductName="Shirt",Price=60,Size="M",Brand="No Brand",Color="Colorfull",Discription="",Category="Shirt Men Children"},
                new Product{ProductName="Silver Ring",Price=100,Size="No Size",Brand="No Brand",Color="Silver",Discription="Silver Ladies Ring",Category="Accesories"},
                new Product{ProductName="Silver neckless",Price=120,Size="No Size",Brand="No Brand",Color="Silver",Discription="Silver Ladies Neckless",Category="Accesories"},
                new Product{ProductName="Shirt",Price=60,Size="S",Brand="No Brand",Color="White",Discription="",Category="Shirt Men Children"},
                new Product{ProductName="Shirt",Price=60,Size="L",Brand="No Brand",Color="Blue",Discription="",Category="Shirt Women"},
                new Product{ProductName="Dress",Price=60,Size="M",Brand="No Brand",Color="Red",Discription="",Category="Dress Women"},
                new Product{ProductName="Shirt",Price=60,Size="M",Brand="No Brand",Color="green",Discription="",Category="Shirt Women"},
                new Product{ProductName="Dress",Price=60,Size="L",Brand="No Brand",Color="Pink",Discription="",Category="Dress Women"},
                new Product{ProductName="Shirt",Price=60,Size="M",Brand="No Brand",Color="Orange",Discription="",Category="Shirt Men"},
                new Product{ProductName="Coat",Price=80,Size="S",Brand="No Brand",Color="Colorfull",Discription="",Category="Shirt Men Children Women Unisex"}
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

