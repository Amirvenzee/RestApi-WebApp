using Vila.WebApi.Dtos;
using VILA.Api.Models;

namespace VILA.Api.Mappings
{
    public static class CustomerMapper
    {
        public static LoginResultDto ToDto(this Customer customer)
        {
            return new LoginResultDto()
            {
                CustomerId = customer.CustomerId,
                JwtSecret = customer.JwtSecret,
                Mobile = customer.Mobile,
                Role = customer.Role,
            };
        }
    }
}
