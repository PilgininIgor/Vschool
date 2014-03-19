using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ILS.Domain
{
	public class Theme : EntityBase
	{
        public int OrderNumber { get; set; }
        public string Name { get; set; }
        [ForeignKey("Course")] public Guid Course_Id { get; set; }

        public virtual Course Course { get; set; }
        public virtual ICollection<ThemeContent> ThemeContents { get; set; }
        public virtual ICollection<ThemeLink> OutputThemeLinks { get; set; }
        public virtual ICollection<LinkEditorCoordinates> LinkEditorCoordinates { get; set; }
        
		public Theme()
		{
			ThemeContents = new List<ThemeContent>();
            OutputThemeLinks = new List<ThemeLink>();
            LinkEditorCoordinates = new List<LinkEditorCoordinates>();
		}
	}
}
