using System.Collections.Generic;

namespace Models
{
    public class Resturant
    {
        public Resturant(){}

        public Resturant(string name)
        {
            this.Name = name;
        }

        public Resturant(int id, string name, int style, string description, int zip) : this (name)
        {
            this.Id = id;
            this.Style = style;
            this.Zip = zip;
            this.Desc = description;
        }

        public int Id {get; set;}
        public string Name {get; set;}
        public int Style {get; set;}
        public string Desc {get; set;}
        public int Zip {get; set;}
        public List<Review> Reviews {get; set;}
    }
}