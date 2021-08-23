using System;
using Xunit;
using Microsoft.EntityFrameworkCore;
using DL;

namespace Test
{
    public class UnitTest1
    {
        private readonly DbContextOptions<Entity.reviewdbContext> options;

        public TestRepo()
        {
            options = new DbContextOptionsBuilder<Entity.reviewdbContext>().UseSqlite("Filename=test.db").Options;
            Seed();
        }
        [Fact]
        public void AddAUserShouldAddAUser()
        {
           //Arrange
           using(var testcontext = new Entity.reviewdbContext(options))
           {
               IReviewRepo _repo = new ReviewRepo(testcontext);

               //Act
               _repo.AddUser(
                   new Models.User{
                        Id = 5,
                        Name = "Bob Smith",
                        AcessLvl = 1

                    }
               );
           } 

           //Assert
           using(var assertContext = new Entity.reviewdbContext(options))
           {
               Entity.User user = assertContext.users.FirstOrDefault();
           }

        }

        private void Seed()
        {
            using(var context = new Entity.reviewdbContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated(); // wipe the tables

                context.Users.AddRange(
                    new Models.User{
                        Id = 1,
                        Name = "Frank Stevens",
                        AcessLvl = 1

                    }
                );
            }

            context.SaveChanges; //Does the actual commit
        }

        //Given
        //When
        //Then
    }
}
