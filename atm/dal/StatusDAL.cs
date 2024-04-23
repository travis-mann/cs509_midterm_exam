internal sealed class StatusDAL: IStatusDAL
{
    public int? GetStatusID(string status)
    {
        using (Context context = new())
        {
            return context.Status.Where(s => s.name == status).Select(s => s.id).FirstOrDefault();
        }
    }

    public string GetStatusFromID(int id)
    {
        using (Context context = new())
        {
            return context.Status.Where(s => s.id == id).Select(s => s.name).First();
        }
    }
}
