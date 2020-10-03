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
                new Customer{Id=11,FirstName="Shay",LastName="Horovitz",Gender="Man",PhoneNumber="0543918224",Email="Shay.horo@gmail.com",Password="1234",Country="Israel",City="Rishon Letziyon",ZipCode="1234567",Address="Rotchild 15",Birthday=DateTime.Parse("1977-04-09")},
                new Customer{Id=12,FirstName="Moshe",LastName="Dovav",Gender="Man",PhoneNumber="0506522131",Email="moshedov5@gmail.com",Password="1234",Country="United States",City="La la land",ZipCode="4567891",Address="Avenue Bd 3491",Birthday=DateTime.Parse("1966-03-14")},
                new Customer{Id=13,FirstName="Dror",LastName="Cohen",Gender="Man",PhoneNumber="0523798117",Email="drorcn105@gmail.com",Password="1234",Country="Israel",City="Tel Aviv",ZipCode="1234567",Address="Alanbi 15",Birthday=DateTime.Parse("2003-07-25")}
            };
            foreach (Customer c in customers)
            {
                context.Customers.Add(c);
            }
            context.SaveChanges();
            //****************************
            var products = new Product[]
            {
                new Product{ProductId=8,ProductName="Vans Old Skool",Price=120,Size="39",Brand="Vans",Color="Black&White",Discription="Vans the best for you",Category="Shoes"},
                new Product{ProductId=9,ProductName="Toms Classic",Price=140,Size="40",Brand="Toms",Color="Blue",Discription="Toms since 1972",Category="Men Shoes"},
                new Product{ProductId=10,ProductName="Lee Copper Classic",Price=70,Size="M",Brand="Lee Copper",Color="Black",Discription="Lee Copper since 1980",Category="Men Jeans"}
            };
            foreach (Product p in products)
            {
                context.Products.Add(p);
            }
            context.SaveChanges();
            //*****************************
            var comments = new Comment[]
            {
                new Comment{Id=1,Title="Comment1",Body="stam",SentBy="Moshe",Posted=DateTime.Parse("09-09-2020"),IP="",ProductId=8},
                new Comment{Id=2,Title="Comment2",Body="stam",SentBy="Dror",Posted=DateTime.Parse("09-09-2020"),IP="",ProductId=8},
                new Comment{Id=3,Title="Comment3",Body="stam",SentBy="Dror",Posted=DateTime.Parse("09-09-2020"),IP="",ProductId=9 }
            };
            foreach (Comment c in comments)
            {
                context.Comment.Add(c);
            }
            context.SaveChanges();
            //****************************
            var orders = new Order[]
            {
                new Order{OrderID=10,OrderDate=DateTime.Now,TotalPrice=260,CustomerId=11},
                new Order{OrderID=11,OrderDate=DateTime.Now,TotalPrice=140,CustomerId=12},
                new Order{OrderID=12,OrderDate=DateTime.Now,TotalPrice=70,CustomerId=13},
                //new Order{OrderID=13,OrderDate=DateTime.Now,TotalPrice=,CustomerId=},
                //new Order{OrderID=14,OrderDate=DateTime.Now,TotalPrice=,CustomerId=},
                //new Order{OrderID=15,OrderDate=DateTime.Now,TotalPrice=,CustomerId=}
            };
            foreach (Order o in orders)
            {
                context.Orders.Add(o);
            }
            context.SaveChanges();
            //*****************************
            var productOrders = new ProductOrder[]
            {
                new ProductOrder{ProductId=8,OrderId=10},
                new ProductOrder{ProductId=9,OrderId=10},
                new ProductOrder{ProductId=9,OrderId=11},
                new ProductOrder{ProductId=10,OrderId=12},
                //new ProductOrder{ProductId=9,OrderId=13},
                //new ProductOrder{ProductId=10,OrderId=13}
            };
            foreach (ProductOrder po in productOrders)
            {
                context.ProductOrder.Add(po);
            }
            context.SaveChanges();
        }
    }
}

