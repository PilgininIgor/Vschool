using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace ILS.Domain
{
    public class Achievement : EntityBase
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public int Priority { get; set; }

        public Achievement()
        {

        }
    }
}
