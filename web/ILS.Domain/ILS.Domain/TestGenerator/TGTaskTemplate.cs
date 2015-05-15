using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ILS.Domain.TestGenerator
{
	public class TGTaskTemplate : EntityBase
	{
        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; }

		public int OrderNumber { get; set; }
        
        [ForeignKey("Test")] public Guid Test_Id { get; set; }

        public virtual TGTest Test { get; set; }
        
        public TGTaskTemplate() { }
    }
}