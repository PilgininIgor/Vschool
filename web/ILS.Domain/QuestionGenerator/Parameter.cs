using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ILS.Domain.QuestionGenerator
{
    /// <summary>
    /// Параметр настройки генерируемого задания
    /// </summary>
    public class Parameter
    {
        /// <summary>
        /// Идентификатор параметра (строковая константа)
        /// </summary>
        [Key, Column(Order = 0)]
        public string ParameterIdentifier { get; set; }

        /// <summary>
        /// Описание параметра (его назначние)
        /// </summary>
        public string Description { get; set; }

        [ForeignKey("QuestionTemplate")]
        public Guid QuestionTemplate_Id { get; set; }

        public virtual QuestionTemplate QuestionTemplate { get; set; }
    }
}
