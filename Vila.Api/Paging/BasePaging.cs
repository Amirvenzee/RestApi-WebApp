namespace VILA.Api.Paging
{
    public class BasePaging
    {
        public int PageCount { get; set; }
        //صفحه چندم
        public int PageId { get; set; }
        public int TotalData { get; set; }
        public int Take { get; set; }
        public int StartPage { get; set; }
        public int EndPage { get; set; }

        public void Generate(IQueryable<object> data,int pageId, int take )
        {
            TotalData = data.Count();
            PageId = pageId;
            Take = take;
            PageCount = TotalData / Take;

            if(data.Count() % take > 0)
            {
                PageCount++;
            }

            StartPage = (pageId - 2 <= 0) ? 1 : pageId - 2;
            EndPage = (pageId + 2 > PageCount) ? PageCount : pageId + 2;


        }
    }
}
