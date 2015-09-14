using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ILS.Domain
{
	public class Task1Content : ThemeContent
	{
        public int OrderNumber { get; set; }

        public string Type { get; set; }
        public string Operation { get; set; }
        public int Number1 { get; set; }
        public int Number2 { get; set; }
        public int Scale1 { get; set; }
        public int Scale2 { get; set; }
	}
}
