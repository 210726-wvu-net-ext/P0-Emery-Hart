using System;

namespace Models
{
    public class User
    {
        public User(){}

        public User(String name, String pass, int accesslvl)
        {
            this.Name = name;
            this.Pass = pass;
            this.AccessLvl = accesslvl;
        }
        public User(int id, String name, String pass, int accesslvl)
        {
            this.Name = name;
            this.Pass = pass;
            this.Id = id;
            this.AccessLvl = accesslvl;
        }

        public int Id {get; set;}
        public string Name {get; set;}
        public string Pass {get; set;}
        public int AccessLvl {get; set;}
    }
}
