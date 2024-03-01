internal class StatusDAL: IStatusDAL
{
    public int? getStatusID(string status)
    {
        using (Context context = new())
        {
            return context.Status.Where(s => s.name == status).Select(s => s.id).FirstOrDefault();
        }
    }
}
