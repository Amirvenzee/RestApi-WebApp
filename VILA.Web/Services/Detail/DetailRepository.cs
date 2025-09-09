using VILA.Web.Models.Detail;
using VILA.Web.Services.Generic;

namespace VILA.Web.Services.Detail
{
    public class DetailRepository:Repository<DetailModel>,IDetailRepository
    {
        private readonly IHttpClientFactory _Client;

        public DetailRepository(IHttpClientFactory client):base(client)
        {
            _Client = client;
        }

        
    }
}
