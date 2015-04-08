using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ILS.Domain
{
	public abstract class ThemeContent : EntityBase
	{
        public int OrderNumber { get; set; }
        public string Name { get; set; }

        [ForeignKey("Theme")] public Guid Theme_Id { get; set; }

        public virtual Theme Theme { get; set; }

        public virtual ICollection<ThemeContentLink> OutputThemeContentLinks { get; set; }
        public virtual ICollection<LinkEditorCoordinates> LinkEditorCoordinates { get; set; }

        public ThemeContent()
        {
            OutputThemeContentLinks = new List<ThemeContentLink>();
            LinkEditorCoordinates = new List<LinkEditorCoordinates>();
        }
	}
}
