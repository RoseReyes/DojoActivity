using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace dojoactivity.Models
{
    public class User {
        
        public int UserId { get; set; }
        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Password { get; set; }
 
        public List<Planner> activities { get; set;}
        
        public List<Participant> participants { get; set;}
        public DateTime Created_at { get; set; }

        public DateTime Updated_at { get; set; }


        public User () {
            activities = new List<Planner>();
            participants = new List<Participant>();
            this.Created_at = DateTime.Now;
            this.Updated_at = DateTime.Now;
        }

    }
}