using System.Collections.Generic;

public class EDucationAuthorModel
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Image { get; set; }
    public int Priority { get; set; }

    public EDucationAuthorModel(string Name, string Description, string Image)
    {
        this.Name = Name;
        this.Description = Description;
        this.Image = Image;
    }

    public EDucationAuthorModel() { }
}