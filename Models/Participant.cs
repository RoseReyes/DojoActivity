using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace dojoactivity.Models
{
    public class Participant {
        
        public int ParticipantId { get; set;}
        public int UserId { get; set; }
        public User User { get; set; }
        public int PlannerId { get; set; }
        public Planner Planners { get; set;}

        public DateTime Created_at { get; set; }

        public DateTime Updated_at { get; set; }


        public Participant () {
            this.Created_at = DateTime.Now;
            this.Updated_at = DateTime.Now;
        }

    }
}