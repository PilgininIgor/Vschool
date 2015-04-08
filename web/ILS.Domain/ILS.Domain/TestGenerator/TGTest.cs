using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ILS.Domain.TestGenerator
{
    /// <summary>
    /// Генерируемый тест
    /// </summary>
    public class TGTest : ThemeContent
    {
        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; }

        /*
        /// <summary>
        /// Пользователь-владелец теста, имеющий право его редактировать
        /// </summary>
        [ForeignKey("Owner")]
        public Guid User_Id { get; set; }

        public virtual User Owner { get; set; }
        */

        /// <summary>
        /// Настройки генерируемого теста
        /// </summary>
        [ForeignKey("TGTestSetting")]
        public Guid TGTestSetting_Id { get; set; }

        public virtual TGTestSetting TGTestSetting { get; set; }
    }
}