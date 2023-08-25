using Domain.Auth;
using Domain.Auth.Interfaces.Repository;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Domain.Auth.Interfaces.Service;
using WebApi.Domain.Request;
using WebApi.Domain.Response;

namespace WebApi.Applications.Auth
{
    public class UserService : IUserService
    {
        private readonly IUsersRepository _usersRepository;

        public UserService(IUsersRepository usersRepository) {
            _usersRepository = usersRepository;
        }
      
        public async Task<UserResponse> CreateUser(UserRequest request)
        {
            // Lógica para criar um usuário, por exemplo
            Users newUser = new Users(Guid.NewGuid(), request.Username, request.Password);
            
            _usersRepository.Add(newUser);

            // Montar a resposta
            UserResponse response = new UserResponse
            {
                UserId = newUser.Id,
                Username = newUser.Username,
                Password = newUser.Password
                // Preencha outros campos da resposta conforme necessário
            };

            return response;
        }

        public async Task<ActionResult<IEnumerable<UserResponse>>> GetAll()
        {
            try
            {
                var repo = await _usersRepository.GetAll();

                var userResponses = repo.Select(user => new UserResponse
                {
                    UserId = user.Id,
                    Username = user.Username
                }).ToList();

                return new ActionResult<IEnumerable<UserResponse>>(userResponses);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao obter usuários. Detalhes: " + ex.Message, ex);
            }
        }
    }

}
