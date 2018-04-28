using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace dojoactivity.Models
{
    public class Planner {
        
        public int PlannerId { get; set; }
        public string Title { get; set; }
        public TimeSpan Time { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
        public string Hours { get; set; }
        public int UserId { get; set; }
        public User User { get; set;}
        public List<Participant> participants { get; set; }
        public DateTime Updated_at { get; set; }

        public Planner () {
            participants = new List<Participant>();
            this.Date = DateTime.Now;
            this.Updated_at = DateTime.Now;
        }

    }
}