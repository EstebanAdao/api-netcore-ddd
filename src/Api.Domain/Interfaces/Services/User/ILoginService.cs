using Api.Domain.Dtos;
using Api.Domain.Entities;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services.User
{
    public interface ILoginService
    {
        Task<object> FindByLogin(LoginDto loginDto);
    }
}
