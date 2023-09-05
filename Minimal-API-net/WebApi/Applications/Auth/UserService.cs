using Domain.Auth.Interfaces.Repository;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebApi.Domain.Auth.Interfaces.Service;
using WebApi.Domain.Auth.Models;
using WebApi.Domain.Request;
using WebApi.Domain.Response;

namespace WebApi.Applications.Auth
{
    public class UserService : IUserService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly ITokenService _tokenService;

        public UserService(IUsersRepository usersRepository, ITokenService tokenService)
        {
            _usersRepository = usersRepository;
            _tokenService = tokenService;
        }


        public async Task<UserResponse> CreateUser(UserRequest request)
        {
            // Lógica para criar um usuário, por exemplo
            Users newUser = new Users(Guid.NewGuid(), request.Username, request.Password, request.Image,request.Email, request.Roles);
            
            //Entitie será usado para persistir os dados no banco

            UserEntitie userEntitie = new UserEntitie(newUser.Id, newUser.Username, newUser.Password, newUser.Image,newUser.Email);

            var token = _tokenService.Generate(newUser);

           var create =  await _usersRepository.Add(userEntitie);

            // Montar a resposta
            UserResponse response = new UserResponse { 
                UserId = newUser.Id,
                Username = newUser.Username,
                Password = newUser.Password,
                Email = newUser.Email,
                Image  = newUser.Image,
                Roles = null,
                Message = token,

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
                    Username = user.Username,
                    Password = user.Password,
                    Email = user.Email,
                    Image = user.Image,
                    Roles = null,
                }).ToList();

                return new ActionResult<IEnumerable<UserResponse>>(userResponses);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao obter usuários. Detalhes: " + ex.Message, ex);
            }
        }

        public async Task<ActionResult<UserResponse>> GetUser(UserRequest request)
        {

            var user = await _usersRepository.GetUser(request.Username);
            
            Users newUser = new Users(Guid.NewGuid(), user.Username, user.Password, user.Image, user.Email, request.Roles);
            var token = _tokenService.Generate(newUser);

            if (request.Password == user.Password)
            {
                var userResponse = new UserResponse
                {
                    UserId = user.Id,
                    Username = user.Username,
                    Password = user.Password,
                    Email = user.Email,
                    Image = user.Image,
                    Roles = null,
                    Message = token
                };

                return userResponse;
            }
            else
            {
                throw new Exception("Senha ou Usuário não encontrado");
            }
        }

        public async Task<ActionResult<UserResponse>> PutUserPassword(string id, string NewPassword, UserRequest request)
        {
            //Serch the user in BD
            var user = await _usersRepository.GetUser(request.Username);

            //Verify if the id is the same that come from the BD
            if (user.Id.ToString() == id)
            {
                //Call the Bd passing the new password
                if(await _usersRepository.PutUserPassword(id, NewPassword) == true)
                {
                    user = await _usersRepository.GetUser(request.Username);
                }

                var userResponse = new UserResponse
                {
                    UserId = user.Id,
                    Username = user.Username,
                    Password = user.Password,
                    Email = user.Email,
                    Image = user.Image,
                    Roles = null,
                    Message = "Usuario Atualizado"
                };

                return userResponse;
            }
            else
            {
                throw new Exception("Senha ou Usuário não encontrado");
            }

        }

        public async Task<ActionResult<UserResponse>> DeleteUser(string id, UserRequest request)
        {
            //Serch the user in BD
            var user = await _usersRepository.GetUser(request.Username);
            
            if (user != null && user.Id.ToString() == id)
            {
                //Call the Bd passing the id 
                if (await _usersRepository.DeleteUser(id))
                {
                    return new UserResponse
                    {
                        UserId = user.Id,
                        Username = user.Username,
                        Password = user.Password,
                        Email = user.Email,
                        Image = user.Image,
                        Roles = null,
                        Message = "Usuario deletado"
                    };

                }
                else
                {
                    return(new UserResponse
                    {
                        UserId = user.Id,
                        Email = user.Email,
                        Username = user.Username,
                        Message = "Senha ou Usuário não encontrado."
                    });
                }
            }
            else
            {
                throw new Exception("Senha ou Usuário não encontrado");
            }

        }

    }

}
