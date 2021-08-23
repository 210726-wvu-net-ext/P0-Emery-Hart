using Models;
using BL;
using System;
using System.Collections.Generic;
using Serilog;


/// <summary>
/// Creates Menu functionality
/// </summary>


namespace App
{
    public class MainMenu : IMenu
    {
        //establish and store user or just prompt for admin when needed?

        //Logging here
        private IReviewBL _reviewbl;
        public  MainMenu(IReviewBL bl)
        {
            _reviewbl = bl;
            Log.Logger=new LoggerConfiguration()
                            .MinimumLevel.Debug()
                            .WriteTo.File("../logs/logs.txt", rollingInterval:RollingInterval.Day)
                            .CreateLogger();
            Log.Information("UI Starting");
        }
        public void Start()
        {
            bool done = false;
            do
            {
                Console.WriteLine("---Welcome to Resturant Reviewer!---");
                Console.WriteLine("[0] Exit");
                Console.WriteLine("[1] View All Resturants");
                Console.WriteLine("[2] Find a specific Resturant by name or ID");
                Console.WriteLine("[3] Find Resturants by Zip or Style");
                Console.WriteLine("[4] Get reviews by Resturant ID");
                Console.WriteLine("[5] Add a review");
                Console.WriteLine("");
                Console.WriteLine("---   Administrative Functions  ---");
                Console.WriteLine("[20] View all Users");
                Console.WriteLine("[21] Add a User");

                switch(Console.ReadLine())
                {
                    case "0":
                        Console.WriteLine("Goodbye!");
                        done = true;
                        break;
                    case "1":
                        GetResturants();
                        break;
                    case "2":
                        SingleSearch();
                        break;
                    case "3":
                        ListSearch();
                        break;
                    case "4":
                        GetReviews();
                        break;
                    case "5":
                        AddReview();
                        break;
                    case "20":
                        GetUsers();
                        break;
                    case "21":
                        AddUser();
                        break;
                    default:
                        Console.WriteLine("Invalid Selection, try again");
                        break;
                }
            }while(!done);
        }

        public void GetDetails(Resturant rest)
        {
            double avg = _reviewbl.AverageReviews(rest);
            //Needs functionality to translate the SID to its actual string
            Console.WriteLine($"Resturant#: {rest.Id} -- {rest.Name}");
            Console.WriteLine($"Zip Code - {rest.Zip}");
            Console.WriteLine($"Style - {rest.Style}");
            Console.WriteLine($"Average User Rating - {avg}");
            Console.WriteLine($"Desc - {rest.Desc}");
            Console.WriteLine("----------------------------------------------------------------------");
        }
        private void GetResturants()
        {
            List<Resturant> resturants = _reviewbl.GetAllResturants();
            foreach(Resturant rest in resturants)
            {
                GetDetails(rest);
            }
            Log.Debug("Printed all resturant details");

        }

        private void SingleSearch()
        {
            Resturant foundRest;
            string input;
            int goodIn;
            Console.WriteLine("Enter either the Name or ID# you wish to search for: ");
            // May want to switch this for a switch if its easier, or another kind of t-check?
            input = Console.ReadLine();

            if (int.TryParse(input, out goodIn))
            {
                foundRest = _reviewbl.SearchResturants(goodIn);
                Console.WriteLine("**Search Results**");
                Console.WriteLine("----------------------------------------------------------------------");
                GetDetails(foundRest);
                Log.Information("We found this one by ID");
            }
            else if (input != null)
            {
                foundRest = _reviewbl.SearchResturants(input);
                Console.WriteLine("**Search Results**");
                Console.WriteLine("----------------------------------------------------------------------");
                GetDetails(foundRest);
                Log.Information("We found this one by name");
            }
            else
            {
                Console.WriteLine("Bad entry, please try again");
                foundRest = null;
            }

        }

        private void ListSearch()
        {
            List<Resturant> foundList = new List<Resturant>();
            int goodIn;
            Console.WriteLine("Please enter a Zip code or Style of resturant to search for:");
            var input = Console.ReadLine();
            
            if (int.TryParse(input, out goodIn))
            {
                //Search here
                foundList = _reviewbl.SearchResturantList(goodIn);
                Console.WriteLine("**Search Results**");
                Console.WriteLine("----------------------------------------------------------------------");
                foreach (Resturant rest in foundList)
                {
                    GetDetails(rest);
                }
            }
            else if (input != null)
            {
                //Search here
                foundList = _reviewbl.SearchResturantList(input);
                Console.WriteLine("**Search Results**");
                Console.WriteLine("----------------------------------------------------------------------");
                foreach (Resturant rest in foundList)
                {
                    GetDetails(rest);
                }
            }
            else
            {
                Console.WriteLine("Bad entry, please try again");
                foundList = null;
            }


        }

        private void GetReviews()
        {

        }

        private void AddReview()
        {

        }

        private void GetUsers()
        {

        }
        private void AddUser()
        {

        }
    }
}