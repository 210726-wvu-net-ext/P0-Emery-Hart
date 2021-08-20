using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using BL;
using DL;
using DL.Entities;


// Connection for our DB
var config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

string connectionStr = config.GetConnectionString("projDB");

DbContextOptions<PojectzeroContext> option = new DbContextOptionsBuilder<PojectzeroContext>()
    .UseSqlServer(connectionStr)
    .Options;

var context = new PojectzeroContext(option);    
namespace App
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
