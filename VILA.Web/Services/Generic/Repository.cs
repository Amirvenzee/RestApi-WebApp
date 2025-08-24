namespace VILA.Web.Services.Generic
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly IHttpClientFactory _client;
        public Repository(IHttpClientFactory client)
        {
            _client = client;
        }

        public Task<bool> Create(string url, string token, T model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(string url, string token)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> GetAll(string url, string token)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetById(string url, string token)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(string url, string token, T model)
        {
            throw new NotImplementedException();
        }
    }
}
