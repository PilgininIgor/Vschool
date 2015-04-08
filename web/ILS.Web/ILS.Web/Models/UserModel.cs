using System.Collections.Generic;

public class UserModel
{
    public bool IsApproved { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int EXP { get; set; }
    public bool IsAdmin { get; set; }
    public bool IsTeacher { get; set; }
    public bool IsStudent { get; set; }

    public UserModel(string pName, bool pIsApproved, string pEmail)
    {
        this.IsApproved = pIsApproved;
        this.Name = pName;
        this.Email = pEmail;
    }

    public UserModel() { }
}