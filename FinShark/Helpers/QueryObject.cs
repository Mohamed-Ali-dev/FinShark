namespace FinShark.Helpers
{
    public class QueryObject
    {
        public string? Symbol { get; set; } = null;
        public string? CompanyName { get; set; } = null;
        public string? orderBy { get; set; } = string.Empty;
       public bool? isDescending { get; set; } = false;
        public int pageNumber { get; set; } = 1;
        public int pageSize { get; set; } = 20;

    }
}
