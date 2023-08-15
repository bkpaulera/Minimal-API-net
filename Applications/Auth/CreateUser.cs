using MediatR;

namespace Applications.Auth
{
    public record CreateUser( string username, string password) : IRequest;
    
}