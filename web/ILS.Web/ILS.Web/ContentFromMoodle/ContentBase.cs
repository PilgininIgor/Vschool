using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ILS.Web.ContentFromMoodle
{
    /// <summary>
    /// Базовый DTO контента.
    /// </summary>
    public class ContentBase
    {
        public ContentBase()
        {
            
        }

        public ContentBase(long id, string name, ContentType contentType)
        {
            Id = id;
            Name = name;
            Type = contentType;
        }

        public long Id { get; set; }

        public string Name { get; set; }

        public ContentType Type { get; set; }
    }

    /// <summary>
    /// Тип контента.
    /// </summary>
    public enum ContentType
    {
        Lecture = 0,
        Test = 1
    }
}
