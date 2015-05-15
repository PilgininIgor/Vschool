using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ILS.Domain.TestGenerator.Settings
{
    /// <summary>
    /// Справочник: режим перемешивания
    /// </summary>
    public class TGMixMode : EntityBase
    {
        /// <summary>
        /// По порядку
        /// </summary>
        public static readonly Guid IN_ORDER = new Guid("1d856e6e-e4f1-4c05-a5f9-41dcb2cbfa1f");

        /// <summary>
        /// В случайном порядке
        /// </summary>
        public static readonly Guid IN_RANDOM = new Guid("435f1ae9-8fc6-488b-8dc3-66a4ca5ff69f");

        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Порядковый номер
        /// </summary>
        public int OrderNumber { get; set; }
    }
}
