namespace BlepItLibrary.Models.Base;

public class EntityObjectModifedBy
{
    public DateTimeOffset? ModifiedDateTime { get; set; }
    public string ModifiedBy { get; set; }

    public void SetModifiedByAndDateTime(string modifiedBy)
    {
        if (modifiedBy != null)
        {
            ModifiedBy = modifiedBy;
            ModifiedDateTime = DateTimeOffset.Now;
        }
    }
}
