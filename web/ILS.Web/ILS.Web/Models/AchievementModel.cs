using System.Collections.Generic;

public class AchievementModel
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Image { get; set; }
    public int Priority { get; set; }

    public AchievementModel(string Name, string Description, string Image)
    {
        this.Name = Name;
        this.Description = Description;
        this.Image = Image;
    }

    public AchievementModel() { }
}