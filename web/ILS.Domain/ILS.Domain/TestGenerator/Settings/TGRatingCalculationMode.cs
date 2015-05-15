using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ILS.Domain.TestGenerator.Settings
{
    /// <summary>
    /// Справочник: способ оценивания прохождения теста
    /// </summary>
    public class TGRatingCalculationMode : EntityBase
    {
        /// <summary>
        /// 5-балльная шкала
        /// </summary>
        public static readonly Guid IN_5_POINT_SCALE = new Guid("1d856e6e-e4f1-4c05-a5f9-41dcb2cbfa1f");

        /// <summary>
        /// 100-балльная шкала
        /// </summary>
        public static readonly Guid IN_100_POINT_SCALE = new Guid("435f1ae9-8fc6-488b-8dc3-66a4ca5ff69f");

        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Порядковый номер
        /// </summary>
        public int OrderNumber { get; set; }
    }
}
