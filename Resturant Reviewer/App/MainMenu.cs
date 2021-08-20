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

        private void GetResturants()
        {

        }

        private void SingleSearch()
        {

        }

        private void ListSearch()
        {

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