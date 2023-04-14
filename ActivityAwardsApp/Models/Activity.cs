﻿using System.ComponentModel.DataAnnotations;

namespace ActivityAwardsApp.Models
{
    public class Activity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int Points { get; set; }

        public List<Award> Awards { get; set; }
    }
}
