using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ILS.Domain
{
	public class Task2Content : ThemeContent
	{
        public int OrderNumber { get; set; }

        public string TaskString { get; set; }
	}
}
