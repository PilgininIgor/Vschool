using System.Collections.Generic;

namespace ILS.Domain
{
	public class Role : EntityBase
	{
		public string Name { get; set; }

        public ICollection<User> Users { get; set; }

        public Role()
        {
            Users = new HashSet<User>();
        }
	}
}
