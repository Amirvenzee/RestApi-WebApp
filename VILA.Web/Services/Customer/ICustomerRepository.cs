using VILA.Web.Models;
using VILA.Web.Models.Customer;

namespace VILA.Web.Services.Customer
{
    public interface ICustomerRepository
    {
        Task<OperationResult> Register(RegisterModel model);
        Task<LoginResultModel> Login(RegisterModel model);
    }
}
