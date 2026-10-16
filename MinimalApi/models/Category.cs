using System.Collections.ObjectModel;

public class Category
{
    public int Id {get; set;}
    public string Name {get; set;}= string.Empty;
    public bool IsDeleted {get; set;}
    public DateTime CreateAt {get; set;}
    public DateTime UpdateAt {get; set;}
    public DateTime DeleteAt {get; set;}
    public ICollection<Funko> Funkos {get; set;} = new List<Funko>();
}