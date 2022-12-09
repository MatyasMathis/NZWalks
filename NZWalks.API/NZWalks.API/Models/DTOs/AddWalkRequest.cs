﻿using NZWalks.API.Models.Domain;

namespace NZWalks.API.Models.DTOs
{
    public class AddWalkRequest
    {
       

        public string Name { get; set; }

        public double Lenght { get; set; }

        public Guid RegionId { get; set; }
        public Guid WalkDifficultyId { get; set; }


    }
}
