namespace E_commerceTask.Application.DTOs.Response
{
    public record GetAllResponse
    {
        public required int TotalRecords { get; init; }
        public int totalPages
        {
            get
            {
                return (int)Math.Ceiling((double)TotalRecords / (PageSize == 0 ? 10 : PageSize));
            }
        }
        public required dynamic Items { get; init; }
        public required int CurrentPage { get; init; }
        public required int PageSize { get; init; }
        public required int PageNumber { get; init; }
        public required int page { get; init; }
        public required string host { get; init; }
        public string FirstPageUrl
        {
            get
            {
                return host + $"?PageSize={PageSize}&PageNumber=1";
            }
        }
        public int From
        {
            get
            {
                return (page - 1) * PageSize + 1;
            }
        }
        public int To
        {
            get
            {
                return Math.Min(page * PageSize, TotalRecords);
            }
        }
        public int LastPage
        {
            get
            {
                return totalPages;
            }
        }
        public string LastPageUrl
        {
            get
            {
                return host + $"?PageSize={PageSize}&PageNumber={totalPages}";
            }
        }
        public string PreviousPage
        {
            get
            {
                return page > 1 ? host + $"?PageSize={PageSize}&PageNumber={page - 1}" : null;
            }
        }
        public string NextPageUrl
        {
            get
            {
                return page < totalPages ? host + $"?PageSize={PageSize}&PageNumber={page + 1}" : null;
            }
        }
        public string Path
        {
            get
            {
                return host;
            }
        }
        public int PerPage { get { return PageSize; } }
        public List<Link> Links
        {
            get
            {
                return Enumerable.Range(1, totalPages)
                           .Select(p => new Link() { label = p.ToString(),
                            url = host + $"?PageSize={PageSize}&PageNumber={p}", active = p == PageNumber }).ToList();
            }
        }
    }
    public record GetAllResponseForWebSite
    {
        public required int TotalRecords { get; init; }
        public int totalPages
        {
            get
            {
                return (int)Math.Ceiling((double)TotalRecords / (PageSize == 0 ? 10 : PageSize));
            }
        }
        public required dynamic Items { get; init; }
        public required int CurrentPage { get; init; }
        public required int PageSize { get; init; }
        public required int PageNumber { get; init; }
        public required int page { get; init; }
        public required string host { get; init; }
        public string FirstPageUrl
        {
            get
            {
                return host + $"?PageSize={PageSize}&PageNumber=1";
            }
        }
        public int From
        {
            get
            {
                return (page - 1) * PageSize + 1;
            }
        }
        public int To
        {
            get
            {
                return Math.Min(page * PageSize, TotalRecords);
            }
        }
        public int LastPage
        {
            get
            {
                return totalPages;
            }
        }
        public string LastPageUrl
        {
            get
            {
                return host + $"?PageSize={PageSize}&PageNumber={totalPages}";
            }
        }
        public string PreviousPage
        {
            get
            {
                return page > 1 ? host + $"?PageSize={PageSize}&PageNumber={page - 1}" : null;
            }
        }
        public string NextPageUrl
        {
            get
            {
                return page < totalPages ? host + $"?PageSize={PageSize}&PageNumber={page + 1}" : null;
            }
        }
        public string Path
        {
            get
            {
                return host;
            }
        }
        public int PerPage { get { return PageSize; } }
        public List<Link> Links
        {
            get
            {
                return Enumerable.Range(1, totalPages)
                           .Select(p => new Link()
                           {
                               label = p.ToString(),
                               url = host + $"?PageSize={PageSize}&PageNumber={p}",
                               active = p == PageNumber
                           }).ToList();
            }
        }
    }
    public class Link
    {
        public string url { get; set; }
        public string label { get; set; }
        public bool active { get; set; }
    }
}
