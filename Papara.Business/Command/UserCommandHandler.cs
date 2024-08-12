using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Papara.Base.Response;
using Papara.Business.Cqrs.CqrsCommand;
using Papara.Business.Services;
using Papara.Data.Entities;
using Papara.Data.UnitOfWork;
using Papara.Schema.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Papara.Business.Command
{
    public class UserCommandHandler :
            IRequestHandler<SignupUserCommand, BaseResponse<UserSignupResponse>>,
            IRequestHandler<LoginUserCommand, BaseResponse<UserResponse>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly TokenService tokenService;

        public UserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, TokenService tokenService)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.tokenService = tokenService;
        }

        public async Task<BaseResponse<UserSignupResponse>> Handle(SignupUserCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await unitOfWork.UserRepository.FindByEmailAsync(request.Request.Email);
            if (existingUser != null)
            {
                return new BaseResponse<UserSignupResponse>("Email already exists.");
            }

            var newUser = mapper.Map<User>(request.Request);
            newUser.PasswordHash = HashPassword(request.Request.Password);

            await unitOfWork.UserRepository.Insert(newUser);
            await unitOfWork.SaveDatabase();

            var response = new UserSignupResponse { Email = newUser.Email };
            return new BaseResponse<UserSignupResponse>(response);
        }

        public async Task<BaseResponse<UserResponse>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await unitOfWork.UserRepository.FindByEmailAsync(request.Request.Email);
            if (user == null || !VerifyPasswordHash(request.Request.Password, user.PasswordHash))
            {
                return new BaseResponse<UserResponse>("Invalid email or password.");
            }

            var userResponse = mapper.Map<UserResponse>(user);
            userResponse.Token = tokenService.GenerateToken(user);

            return new BaseResponse<UserResponse>(userResponse);
        }

        private string HashPassword(string password)
        {
            return new PasswordHasher<User>().HashPassword(null, password);
        }

        private bool VerifyPasswordHash(string password, string storedHash)
        {
            return new PasswordHasher<User>().VerifyHashedPassword(null, storedHash, password) != PasswordVerificationResult.Failed;
        }
    }
}
