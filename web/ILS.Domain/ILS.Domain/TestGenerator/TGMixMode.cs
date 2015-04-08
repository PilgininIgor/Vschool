using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ILS.Domain.TestGenerator
{
    /// <summary>
    /// Справочник: режим перемешивания
    /// </summary>
    public class TGMixMode : EntityBase
    {
        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; }
    }
}
