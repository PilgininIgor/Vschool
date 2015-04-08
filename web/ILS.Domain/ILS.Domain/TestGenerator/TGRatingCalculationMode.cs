using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ILS.Domain.TestGenerator
{
    /// <summary>
    /// Справочник: способ оценивания прохождения теста
    /// </summary>
    public class TGRatingCalculationMode : EntityBase
    {
        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Описание
        /// </summary>
        public string Desciption { get; set; }
    }
}
