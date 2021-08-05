using System.Threading.Tasks;
using Api.Domain.Dtos;


namespace Api.Domain.Interfaces.Services.User
{
    public interface IloginService
    {
        Task<object> FindByLogin(LoginDto user);
    }
}
