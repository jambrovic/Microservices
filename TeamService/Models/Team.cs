using System;
using System.Collections.Generic;

namespace TeamService.Models
{
    public class Team
    {
        public string Name { get; set; }
        public Guid ID { get; set; }
        public ICollection<Member> Members { get; set; }
        
        public Team()
        {
            this.Members=new List<Member>();
        }
        
        public Team(string name) : this()
        {
            this.Name=name;
        }
        public Team(string name, Guid iD) : this(name)
        {
            this.ID = iD;
        }

        public override string ToString()
        {
            return this.Name;
        }

    }
}