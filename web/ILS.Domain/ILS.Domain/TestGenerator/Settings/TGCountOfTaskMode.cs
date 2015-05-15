using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ILS.Domain.TestGenerator.Settings
{
    /// <summary>
    /// Справочник: количество задаваемых вопросов в тесте
    /// </summary>
    public class TGCountOfTaskMode : EntityBase
    {
        /// <summary>
        /// все вопросы
        /// </summary>
        public static readonly Guid ALL_TASKS = new Guid("3959da31-e101-4a1e-b576-fa8948ee5587");
        
        /// <summary>
        /// Случайные N вопросов
        /// </summary>
        public static readonly Guid RANDOM_N_TASKS = new Guid("1fa32ada-2eec-4858-834a-85a902734077");

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
