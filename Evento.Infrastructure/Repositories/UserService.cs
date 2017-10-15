using System;
using System.Threading.Tasks;
using AutoMapper;
using Evento.Core.Domain;
using Evento.Core.Repositories;
using Evento.Infrastructure.DTO;
using Evento.Infrastructure.Extensions;
using Evento.Infrastructure.Services;

namespace Evento.Infrastructure.Repositories
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtHandler _jwtHandler;
        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepository, IJwtHandler jwtHandler, IMapper mapper)
        {
            _mapper = mapper;
            _jwtHandler = jwtHandler;
            _userRepository = userRepository;
        }

        public async Task RegisterAsync(Guid userId, string email, string name, string password, string role = "user")
        {
            var user = await _userRepository.GetAsync(email);
            if (user != null)
                throw new Exception($"User with Id: {email} exists!");

            user = new User(userId, role, name, email, password);
            await _userRepository.AddAsync(user);

        }

        public async Task<TokenDto> LoginAsyc(string email, string password)
        {
            var user = await _userRepository.GetAsync(email);
            if (user == null)
                throw new Exception($"Invalid credentials");
            if (user.Password != password)
                throw new Exception($"Invalid credentials");

            var jwt = _jwtHandler.CreateToken(user.Id, user.Role);
            return new TokenDto()
            {
                Token = jwt.Token,
                Expires = jwt.Expires,
                Role = user.Role

            };
        }

        public async Task<AccountDto> GetAccountAsync(Guid userId)
        {
            var user = await _userRepository.GetOrFailAnsyc(userId);
            return _mapper.Map<AccountDto>(user);
          
        }
    }
}