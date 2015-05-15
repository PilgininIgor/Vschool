using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ILS.Domain.TestGenerator.Settings;

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

        /// <summary>
        /// Настройки генерируемого теста
        /// </summary>
        [ForeignKey("TestSetting")]
        public Guid TestSetting_Id { get; set; }

        public virtual TGTestSetting TestSetting { get; set; }
        public virtual ICollection<TGTaskTemplate> TaskTemplates { get; set; }

        public TGTest()
        {
            TaskTemplates = new List<TGTaskTemplate>();
        }
    }
}