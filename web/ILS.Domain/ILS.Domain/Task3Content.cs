using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ILS.Domain
{
	public class Task3Content : ThemeContent
	{
        public int OrderNumber { get; set; }
	}
}
