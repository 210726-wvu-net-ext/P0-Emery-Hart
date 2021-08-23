using Models;
using System.Collections.Generic;

namespace BL
{
    public interface IReviewBL
    {
        List<Resturant> GetAllResturants();
        Models.Resturant SearchResturants(int ID);
        Models.Resturant SearchResturants(string Name); 
        List<Models.Resturant> SearchResturantList(int zip);
        List<Models.Resturant> SearchResturantList(string style);
        List<User> GetUsers(); //Admin restricted function
        List<Models.Review> GetReviews(int ID);
        Models.Review AddReview(Models.Review nReview);
        Models.User AddUser(Models.User nUser);
        double AverageReviews(Models.Resturant rest);
        // string TranslateStyle(Models.Resturant rest);
    }
}