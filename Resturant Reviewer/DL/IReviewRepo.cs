using Models;
using System.Collections.Generic;

/// <summary>
/// Interface to hold methods and function bones
/// </summary>

namespace DL
{
    public interface IReviewRepo
    {
        List<Resturant> GetAllResturaunts();
        //For single return searches (by Name or ID)
        Models.Resturant SearchResturants(int ID); 
        Models.Resturant SearchResturants(string Name); 

        //For list searches (by ZIP or Style)
        List<Models.Resturant> SearchResturantList(int zip);
        List<Models.Resturant> SearchResturantList(string style);  

        List<User> GetUsers(); //Admin restricted function

        List<Models.Review> GetReviews(int ID); 

        Models.Review AddReview(Models.Review nReview);

        Models.User AddUser(Models.User nUser);

    }
}
