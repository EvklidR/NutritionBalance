using Grpc.Net.Client;
using AuthorisationService.Grpc;
using UserProfileService.Application.Interfaces;

namespace UserProfileService.Infrastructure.Grpc
{
    public class UserAuthClient : IUserService
    {
        private readonly UserAuthService.UserAuthServiceClient _client;

        public UserAuthClient(string address)
        {
            var channel = GrpcChannel.ForAddress(address);
            _client = new UserAuthService.UserAuthServiceClient(channel);
        }

        public async Task<bool> CheckUserByIdAsync(int userId)
        {
            var request = new CheckUserByIdRequest { Id = userId };

            var response = await _client.CheckUserByIdAsync(request);
            return response.Exists;
        }
    }
}