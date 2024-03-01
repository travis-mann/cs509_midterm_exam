public interface IStatusDAL
{
    public int? GetStatusID(string status);
    public string GetStatusFromID(int id);
}
