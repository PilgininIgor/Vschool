using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ILS.Domain.TestGenerator.Settings
{
    /// <summary>
    /// Настройки генерируемого теста
    /// </summary>
    public class TGTestSetting : EntityBase
    {
        /// <summary>
        /// Количество вопросов в тесте
        /// </summary>
        [ForeignKey("CountOfTaskMode")]
        public Guid? CountOfTaskMode_Id { get; set; }

        public virtual TGCountOfTaskMode CountOfTaskMode { get; set; }

        /// <summary>
        /// Количество вопросов в режиме "Случайные N вопросов"
        /// </summary>
        public int CountOfTasks { get; set; }

        /// <summary>
        /// Способ оценивания прохождения теста
        /// </summary>
        [ForeignKey("RatingCalculationMode")]
        public Guid? RatingCalculationMode_Id { get; set; }

        public virtual TGRatingCalculationMode RatingCalculationMode { get; set; }

        /// <summary>
        /// Режим ограничения по времени выполнения теста
        /// </summary>
        public bool IsTimeLimitMode { get; set; }

        /// <summary>
        /// Время ограничения, минуты
        /// </summary>
        public int TimeLimitMinutes { get; set; }

        /// <summary>
        /// Время ограничения, секунды
        /// </summary>
        public int TimeLimitSeconds { get; set; }

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
