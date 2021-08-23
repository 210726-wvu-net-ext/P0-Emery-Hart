namespace Models
{
    public class Style
    {
        public Style(){}

        public Style(int id, string style)
        {
            this.Id = id;
            this.Style1 = style;
        }
    
        public int Id {get; set;}
        public string Style1 {get; set;}

    
    }
}