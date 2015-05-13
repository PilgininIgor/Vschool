using System.Collections.Generic;

public class ProfileModel
{
    public string Name { get; set; }
    public string Email { get; set; }
    public int Money { get; set; }
    public int Progress { get; set; }
    public int Rating { get; set; }
    public Dictionary<string, string> Achievements { get; set; }
    public int AchievementsCount { get; set; }
    public ProfileModel() { }
}