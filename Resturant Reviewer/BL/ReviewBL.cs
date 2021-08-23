using System;
using DL;
using Models;
using System.Collections.Generic;

namespace BL
{
    public class ReviewBL : IReviewBL
    {
        /// <summary>
        /// Establishes repo connection and passes through functions between DL and BL
        /// Additional function to calculate the average reviews for a resturant
        /// </summary>
        private IReviewRepo _repo;

        public ReviewBL(IReviewRepo repo)
        {
            _repo = repo;
        }
        public List<Resturant> GetAllResturants()
        {
            return _repo.GetAllResturaunts();
        }
        public Models.Resturant SearchResturants(int ID)
        {
            return _repo.SearchResturants(ID);
        }
        public Models.Resturant SearchResturants(string Name)
        {
            return _repo.SearchResturants(Name);
        }
        public List<Models.Resturant> SearchResturantList(int zip)
        {
            return _repo.SearchResturantList(zip);
        }
        public List<Models.Resturant> SearchResturantList(string style)
        {
            return _repo.SearchResturantList(style);
        }
        public List<User> GetUsers() //Admin restricted function
        {
            return _repo.GetUsers();
        }
        public List<Models.Review> GetReviews(int ID)
        {
            return _repo.GetReviews(ID);
        }
        public Models.Review AddReview(Models.Review nReview)
        {
            return _repo.AddReview(nReview);
        }
        public Models.User AddUser(Models.User nUser)
        {
            return _repo.AddUser(nUser);
        }

        /// <summary>
        /// Calculates the average reviews for a given resturant
        /// </summary>
        /// <param name="rest">The resturaunt to find the average score for</param>
        /// <returns>The average score for the resturant</returns>

        public double AverageReviews(Models.Resturant rest)
        {
            double avg = 0;
            List<Models.Review> reviews = new List<Review>(GetReviews(rest.Id));
        
            foreach (Models.Review rev in reviews)
            {
                avg += rev.Rating;
            }

            avg = (avg / reviews.Count);

            return avg;
        }


    }
}
