using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ILS.Domain
{
    public class LinkEditorCoordinates : EntityBase
    {
        public int X { get; set; }
        public int Y { get; set; }
        [ForeignKey("User")] public Guid User_Id { get; set; }

        public virtual User User { get; set; }
    }
}
