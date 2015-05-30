using System.Collections.Generic;

namespace ILS.Domain
{
    public class UserGroup : EntityBase
    {
        public string Name { get; set; }
        public virtual ICollection<User> Users { get; set; }

        public UserGroup()
        {
            Users = new List<User>();
        }
    }
}