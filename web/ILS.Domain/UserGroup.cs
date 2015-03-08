namespace ILS.Domain
{
    using System.Collections.Generic;

	public class UserGroup : EntityBase
	{
		public string Name { get; set; }
        public ICollection<User> Users {get; set;}

        public UserGroup()
        {
            Users = new List<User>();
        }
	}
}