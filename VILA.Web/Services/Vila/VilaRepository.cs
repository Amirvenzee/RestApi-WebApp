using Microsoft.Extensions.Options;
using VILA.Web.Models.Vila;
using VILA.Web.Utility;

namespace VILA.Web.Services.Vila
{
    public class VilaRepository:IVilaRepository
    {
        private readonly ApiUrls _urls;

        //برای فرستادن ریکویست استفاده میشه 
        private readonly IHttpClientFactory _client;

        public VilaRepository(IOptions<ApiUrls> apiUrls, IHttpClientFactory client)
        {
            _urls = apiUrls.Value;
            _client = client;
        }

        public VilaPaging Search(int pageId, string filter, int take)
        {
            throw new NotImplementedException();
        }
    }
}
