namespace TrollMarket.Persentation.Web.Models
{
    public class PaginationViewModel
    {
        public int TotalItems { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public int TotalPages
        {
            get
            {
                if (TotalItems == 0)
                {
                    return 1;
                }
                else
                {
                    return (int)Math.Ceiling((double)TotalItems / PageSize);
                }
            }
        }
    }
}
