namespace BlepItLibrary.Models;
public class SimpleUser
{
    public string Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string DisplayName { get => FirstName + " " + LastName; }

    public override bool Equals(object? o)
    {
        var other = o as User;
        return other?.DisplayName == DisplayName;
    }

    public override int GetHashCode() => DisplayName?.GetHashCode() ?? 0;

    public override string ToString() => DisplayName;
}