using System.Drawing;
using ZoniSMLibrary.Models.Base;

namespace ZoniSMLibrary.Models;

public class Category : EntityObjectModifedBy
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string BsonId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Colour { get; set; }

    public override bool Equals(object? o)
    {
        var other = o as Category;
        return other?.Name == Name;
    }

    public override int GetHashCode() => Name?.GetHashCode() ?? 0;

    public override string ToString() => Name;
}
