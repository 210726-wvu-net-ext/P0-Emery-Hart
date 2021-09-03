using Models;
using DL.Entities; 
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Pulls entities from the database and transforms them into usable forms for C#
/// Uses Linq to accomplish this
/// </summary>

namespace DL
{
    public class ReviewRepo : IReviewRepo
    {
        private PojectzeroContext _context;
        
        public ReviewRepo(PojectzeroContext context)
        {
            _context = context;
        }
        #region Getting
        // Gets a list of all resturants 
        public List<Models.Resturant> GetAllResturaunts()
        {
            return _context.Resturants.Select(
                rest => new Models.Resturant(rest.ResturantId, rest.Name, rest.Style, rest.Description, rest.Zip)
            ).ToList();
        }
        #endregion

        #region Searching
        /// <summary>
        /// Searches Resturants by ID or Name

        /// </summary>
        /// <param name="ID">ID the user wishes to search for</param>
        /// <param name="Name">Name of the resturant being searched for</param>
        /// <returns>Resturant Model, Empty if none matching is found</returns>
        public Models.Resturant SearchResturants(int ID)
        {
            Entities.Resturant foundRest = _context.Resturants
                .FirstOrDefault(rest => rest.ResturantId == ID);

            if(foundRest != null)
            {
                return new Models.Resturant(foundRest.ResturantId, foundRest.Name, foundRest.Style, foundRest.Description, foundRest.Zip);
            }
            return new Models.Resturant();
        }
        //Overload for name search
        public Models.Resturant SearchResturants(string Name) 
        {
            Entities.Resturant foundRest = _context.Resturants
                .FirstOrDefault(rest => rest.Name == Name);

            if(foundRest != null)
            {
                return new Models.Resturant(foundRest.ResturantId, foundRest.Name, foundRest.Style, foundRest.Description, foundRest.Zip);
            }
            return new Models.Resturant();
        }
        /// <summary>
        /// Search for a list of resturants by Zip or Style
        /// Method to search by average review in BL
        /// </summary>
        /// <returns>List of matching resturants</returns>
        public List<Models.Resturant> SearchResturantList(int Zip) 
        {
            List<Models.Resturant> restList = GetAllResturaunts();
            List<Models.Resturant> foundList = new List<Models.Resturant>();
            foreach (Models.Resturant rest in restList)
            {
                if (rest.Zip == Zip)
                {
                    foundList.Add(rest);
                }
            }

            return foundList;
        }

        /// <summary>
        /// Searches resturants by style, more complex than by zip
        /// Reaches into styles to match string with style ID to match to resturants
        /// </summary>
        /// <returns>List of matching resturants</returns>       
        public List<Models.Resturant> SearchResturantList(string Style)
        {
            List<Models.Resturant> restList = GetAllResturaunts();
            List<Models.Resturant> foundList = new List<Models.Resturant>();
            //Match the style to style ID
            Entities.Style foundStyle = _context.Styles
                .FirstOrDefault(style => style.Style1 == Style);

            if (foundStyle != null)
            {
                foreach (Models.Resturant rest in restList)
                {
                    if (foundStyle.StyleId == rest.Style)
                    {
                        foundList.Add(rest);
                    }
                }
                return foundList;
            }

            return foundList;
        }


        public List<Models.User> GetUsers() //This fucntion is restricted to only admin (lvl 2) users
        {
            return _context.Users.Select(
                user => new Models.User(user.UserId, user.Uname, user.Upass, user.AccessLvl)
            ).ToList();
        }

        /// <summary>
        /// Searches reviews by resturaunt ID, may potentially expand to use name
        /// </summary>
        /// <param name="id">Resturaunt ID to find reviews for</param>
        /// <returns>list of review objects</returns>
        public List<Models.Review> GetReviews(int id)
        {
            List<Models.Review> foundList = new List<Models.Review>();
            List<Models.Review> reviewList = _context.Reviews.Select(
                rev => new Models.Review(rev.ReviewId, rev.UserId, rev.ResturantId, rev.Rating, rev.Thoughts)
            ).ToList();

            foreach (Models.Review rev in reviewList)
            {
                if (rev.RID == id)
                {
                    foundList.Add(rev);                
                }
            }
            return foundList;
        }
        #endregion
        #region Adding
        /// <summary>
        /// Adds a new review 
        /// </summary>
        /// <param name="nReview">The review object to parse</param>
        /// <returns></returns>
        public Models.Review AddReview(Models.Review nReview)
        {
            _context.Reviews.Add(
                new Entities.Review{
                    UserId = nReview.UID,
                    ResturantId = nReview.RID,
                    Rating = nReview.Rating,
                    Thoughts = nReview.Thoughts
                }
            );
            _context.SaveChanges();

            return nReview;
        }

        /// <summary>
        /// Adds a new user 
        /// This function is locked behind admin level and not viewable or accessable by normal users
        /// </summary>
        /// <param name="nUser">The user object to parse</param>
        /// <returns></returns>
        public Models.User AddUser(Models.User nUser)
        {
            _context.Users.Add(
                new Entities.User{
                    UserId = nUser.Id,
                    Uname = nUser.Name,
                    Upass = nUser.Pass,
                    AccessLvl = nUser.AccessLvl
                }
            );
            _context.SaveChanges();

            return nUser;
        }

        #endregion
    }
}