using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using BL;
using DL;
using DL.Entities;
using App;
using Serilog;



/// <summary>
/// Pulls our connection string to establish a DB context
/// </summary>
/// <returns></returns>
var config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

string connectionStr = config.GetConnectionString("projDB");

DbContextOptions<PojectzeroContext> option = new DbContextOptionsBuilder<PojectzeroContext>()
    .UseSqlServer(connectionStr)
    .Options;

var context = new PojectzeroContext(option);    

/// <summary>
/// Attempts to start the menu, will throw a fatal error to log if it fails to start for any reason
/// </summary>
try
{
    IMenu menu = new MainMenu(new ReviewBL(new ReviewRepo(context)));
    Log.Debug("Program started");
}
catch (Exception e)
{
    Log.Fatal(e, "Program must exit :'(");
}