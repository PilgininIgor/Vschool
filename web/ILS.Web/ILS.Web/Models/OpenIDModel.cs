public class OpenIDModel
{
    public string Login { get; set; }
    public string Hash { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Key { get; set; }

    public OpenIDModel(string login, string hash, string firstName, string lastName, string email, string key)
    {
        this.Login = login;
        this.Hash = hash;
        this.FirstName = firstName;
        this.LastName = lastName;
        this.Email = email;
        this.Key = key;
    }

    public OpenIDModel() { }
}