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
            // bool logedIN = false;
            // //Logon functionality here
            // do
            // {
                
            // }while(!logedIN);
            
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

        /// <summary>
        /// This method formats and displays details of a resturant
        /// </summary>
        /// <param name="rest">resturant to display details of</param>
        public void PrintDetails(Resturant rest)
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

        /// <summary>
        /// This method formats and displays review contents
        /// </summary>
        /// <param name="rev">the review to display</param>
        public void PrintReviews(Review rev)
        {
            Console.WriteLine($"Review# - {rev.Id} ");
            Console.WriteLine($"Written for (RID) - {rev.RID}, By (UID) - {rev.UID} ");
            Console.WriteLine($"Score - {rev.Rating}/10");
            Console.WriteLine($"Reviewers Comments: {rev.Thoughts}");
            Console.WriteLine("----------------------------------------------------------------------");
        }
        
        /// <summary>
        /// Gets a list of every resturant in the system
        /// </summary>
        private void GetResturants()
        {
            List<Resturant> resturants = _reviewbl.GetAllResturants();
            foreach(Resturant rest in resturants)
            {
                PrintDetails(rest);
            }
            Log.Debug("Printed all resturant details");

        }

        /// <summary>
        /// Searches by ID or Name by passing certain types
        /// </summary>
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
                PrintDetails(foundRest);
                Log.Information("Found by ID");
            }
            else if (input != null)
            {
                foundRest = _reviewbl.SearchResturants(input);
                Console.WriteLine("**Search Results**");
                Console.WriteLine("----------------------------------------------------------------------");
                PrintDetails(foundRest);
                Log.Information("Found by name");
            }
            else
            {
                Console.WriteLine("Bad entry, please try again");
                foundRest = null;
                Log.Debug("SS - Bad input, or type mismatch");
            }

        }

        /// <summary>
        /// Uses a similar input parsing style as single search,
        /// main difference is this methong returns a list of matching
        /// objects
        /// </summary>
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
                    PrintDetails(rest);
                }
                Log.Information("Found list by zip");
            }
            else if (input != null)
            {
                //Search here
                foundList = _reviewbl.SearchResturantList(input);
                Console.WriteLine("**Search Results**");
                Console.WriteLine("----------------------------------------------------------------------");
                foreach (Resturant rest in foundList)
                {
                    PrintDetails(rest);
                }
                Log.Information("Found list by style");
            }
            else
            {
                Console.WriteLine("Bad entry, please try again");
                foundList = null;
                Log.Debug("LS - Bad input, or type mismatch");
            }


        }

        /// <summary>
        /// Returns all reviews based on a resturant ID#
        /// </summary>
        private void GetReviews()
        {
            int goodIn;
            List<Review> foundList = new List<Review>();
            Console.WriteLine("Please enter the ID# of the resturant you wish to view reviews for: ");
            var input = Console.ReadLine();

            //Validate
            if (int.TryParse(input, out goodIn))
            {
                foundList = _reviewbl.GetReviews(goodIn);
                Console.WriteLine("**Search Results**");
                Console.WriteLine("----------------------------------------------------------------------");
                foreach (Review rev in foundList)
                {
                    PrintReviews(rev);
                }
                Log.Information($"Found reviews for {goodIn}");
            }
            else
            {
                Console.WriteLine("Bad entry, please try again");
                foundList = null;
                Log.Debug("REV - Bad input, or type mismatch");
            }
        }

        /// <summary>
        /// Adds a review to the DB, uses nested loops to ensure data is entered
        /// </summary>
        private void AddReview()
        {
            string input;
            int nRest;
            int nScore;
            string nThoughts = "";
            Review newRev;
            bool next = false;

            Console.WriteLine("----------------------------------------------------------------------");
            Console.WriteLine("Please enter the details of your review as prompted: ");

            do //Start with resturant target
            {
                Console.WriteLine("Enter the ID of the resturant you wish to review");
                input = Console.ReadLine();
                if (int.TryParse(input, out nRest))
                {
                    next = true;
                    Log.Debug("NR - Accepted RID");
                }
                else
                {
                    Console.WriteLine("Bad entry, please try again");
                    Log.Error("NR - (RID) Bad input, or type mismatch");
                }

            }while(!next);

            next = false;
            do //Next, Rating
            {
                Console.WriteLine("Enter a rating between 1-10: ");
                input = Console.ReadLine();
                if (int.TryParse(input, out nScore))
                {
                    if (nScore <= 10 && nScore >= 1)
                    {
                    next = true;
                    Log.Debug("NR - Accepted Score");
                    }
                    else
                    {
                        Console.WriteLine("Out of bounds!, please try again");
                        Log.Error("NR - (score) out of bounds");
                    }
                }
                else
                {
                    Console.WriteLine("Bad entry, please try again");
                    Log.Error("NR - (score) Bad input, or type mismatch");
                }

            }while(!next);

            next = false;

            do //Finally, thoughts
            {
                Console.WriteLine("Enter any remarks/thoughts: ");
                input = Console.ReadLine();
                if(!String.IsNullOrWhiteSpace(input))
                {
                    next = true;
                    nThoughts = input;
                    Log.Debug("NR - Accepted Thoughts");
                }
                else
                {
                    Console.WriteLine("Bad entry, please try again");
                    Log.Error("NR - (thoughts) String Empty");
                }
            }while(!next);

            //Create and commit our new review
            //Needs login to work to get UID, passes dummy (admin) for now
            newRev = new Review(2, nRest, nScore, nThoughts);
            newRev = _reviewbl.AddReview(newRev);
            Console.WriteLine("Review added!");
            Log.Debug("NR - Accepted Review");
        }

        /// <summary>
        /// Checks the current logins access level, returns list of users auth passes
        /// ommits passwords
        /// </summary>
        private void GetUsers()
        {
            // if (currAccess >= 2) //admin check, needs login
            List<User> users = _reviewbl.GetUsers();
            foreach (User use in users)
            {
                Console.WriteLine($"User ID: {use.Id}");
                Console.WriteLine($"Username: {use.Name}");
                Console.WriteLine($"Access Level: {use.AccessLvl}");
                Console.WriteLine("----------------------------------------------------------------------");
            }
            Log.Debug("Printed and fetched all users");
        }
        
        /// <summary>
        /// Checks the current logins access level, allows to add user if auth passes
        /// password entered on creation
        /// </summary>
        private void AddUser()
        {
            string input;
            string nUname = "";
            string nPass = "";
            int nAccess;
            bool next = false;

            Console.WriteLine("----------------------------------------------------------------------");
            Console.WriteLine("Please enter the new users details when promtped");

            do //Enter Username, allows duplicates
            {
                Console.WriteLine("Enter username: ");
                input = Console.ReadLine();
                if(!String.IsNullOrWhiteSpace(input))
                {
                    next = true;
                    nUname = input;
                    Log.Debug("NU - Accepted Username");
                }
                else
                {
                    Console.WriteLine("Bad entry, please try again");
                    Log.Error("NU - (Name) String Empty");
                }

            }while(!next);

            next = false;

            do //Enter Password
            {
                Console.WriteLine("Enter temp password: ");
                input = Console.ReadLine();
                if(!String.IsNullOrWhiteSpace(input))
                {
                    next = true;
                    nPass = input;
                    Log.Debug("NU - Accepted Pass");
                }
                else
                {
                    Console.WriteLine("Bad entry, please try again");
                    Log.Error("NU - (Pass) String Empty");
                }

            }while(!next);

            next = false;

            do //Access Level
            {
                Console.WriteLine("Enter an access level: ");
                input = Console.ReadLine();
                if (int.TryParse(input, out nAccess))
                {
                    if (nAccess >= 1)
                    {
                        next = true;
                        Log.Debug("Nu - Accepted Access lvl");
                    }
                    else
                    {
                        Console.WriteLine("Out of bounds!, please try again");
                        Log.Error("NU - (Access) out of bounds");
                    }
                }
                else
                {
                    Console.WriteLine("Bad entry, please try again");
                    Log.Error("NU - (Access) Bad input, or type mismatch");
                }

            }while(!next);
            
            User nUser = new User(nUname, nPass, nAccess);
            nUser = _reviewbl.AddUser(nUser);

        }
    }
}