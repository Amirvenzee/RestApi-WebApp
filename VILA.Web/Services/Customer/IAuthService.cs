namespace VILA.Web.Services.Customer
{
    public interface IAuthService
    {

        string GetJwtToken();
        void Logout();
    }
}
