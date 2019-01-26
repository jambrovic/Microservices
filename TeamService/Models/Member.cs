using System;

namespace TeamService.Models
{
    public class Member
    {
        public Guid ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Member()
        {

        }

        public Member(Guid id)
        {
            this.ID = id;
        }

        public Member(Guid id, string firstName, string lastName) : this(id)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
        }

        public override string ToString()
        {
            return this.LastName;
        }
    }
}