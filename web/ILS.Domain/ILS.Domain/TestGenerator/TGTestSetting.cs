using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ILS.Domain.TestGenerator
{
    /// <summary>
    /// Настройки генерируемого теста
    /// </summary>
    public class TGTestSetting : EntityBase
    {
        /// <summary>
        /// Количество вопросов в тесте
        /// </summary>
        [ForeignKey("TGCountOfTaskMode")]
        public Guid? TGCountOfTaskMode_Id { get; set; }

        public virtual TGCountOfTaskMode TGCountOfTaskMode { get; set; }

        /// <summary>
        /// Количество вопросов в режиме "Случайные N вопросов"
        /// </summary>
        public int CountOfTasks { get; set; }

        /// <summary>
        /// Способ оценивания прохождения теста
        /// </summary>
        [ForeignKey("TGRatingCalculationMode")]
        public Guid? TGRatingCalculationMode_Id { get; set; }

        public virtual TGRatingCalculationMode TGRatingCalculationMode { get; set; }

        /// <summary>
        /// Режим ограничения по времени выполнения теста
        /// </summary>
        public bool IsTimeLimitMode { get; set; }

        /// <summary>
        /// Время ограничения, минуты
        /// </summary>
        public bool TimeLimitMinutes { get; set; }

        /// <summary>
        /// Время ограничения, секунды
        /// </summary>
        public bool TimeLimitSeconds { get; set; }

        /// <summary>
        /// Режим перемешивания вариантов ответов
        /// </summary>
        [ForeignKey("AnswersMixMode")]
        public Guid? AnswersMixMode_Id { get; set; }

        public virtual TGMixMode AnswersMixMode { get; set; }

        /// <summary>
        /// Режим перемешивания тестовых заданий
        /// </summary>
        [ForeignKey("TasksMixMode")]
        public Guid? TasksMixMode_Id { get; set; }

        public virtual TGMixMode TasksMixMode { get; set; }

        public virtual ICollection<TGTest> GenTests { get; set; }

        public TGTestSetting()
        {
            GenTests = new List<TGTest>();
        }
    }
}
