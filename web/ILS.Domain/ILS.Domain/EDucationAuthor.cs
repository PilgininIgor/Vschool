﻿namespace ILS.Domain
{
    public class EDucationAuthor : EntityBase
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public int Priority { get; set; }

        public EDucationAuthor()
        {

        }
    }
}