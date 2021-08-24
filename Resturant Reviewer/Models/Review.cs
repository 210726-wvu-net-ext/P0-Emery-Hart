namespace Models
{
    public class Review
    {
        public Review(){}

        public Review( int uID, int restID, int rating, string thoughts)
        {
            this.UID = uID;
            this.RID = restID;
            this.Rating = rating;
            this.Thoughts = thoughts;
        }

        public Review(int id, int uID, int restID, int rating, string thoughts)
        {
            this.Id = id;
            this.UID = uID;
            this.RID = restID;
            this.Rating = rating;
            this.Thoughts = thoughts;
        }

        public int Id {get; set;}
        public int UID {get; set;}
        public int RID {get; set;}
        public int Rating {get; set;}
        public string Thoughts {get; set;}

    }
}