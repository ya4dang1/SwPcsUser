using Application.Models;
using Application.Services;
using AutoMapper;
using Core.Libraries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.User
{
    public class UpdateUserProfileCommand: IRequest<UpdateUserProfileCommandResponse>
    {
        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public DateTime Birthday { get; set; }

        public string AddressType { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string Region { get; set; }

        public string Zip { get; set; }

        public string Mobile { get; set; }

        public string IDValue { get; set; }
    }

    public class UpdateUserProfileCommandResponse: BaseResponse
    {
    }

    public class UpdateUserProfileCommandHandler : IRequestHandler<UpdateUserProfileCommand, UpdateUserProfileCommandResponse>
    {
        private readonly LoginUser loginUser;
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public UpdateUserProfileCommandHandler(LoginUser loginUser, ApplicationDbContext dbContext)
        {
            this.loginUser = loginUser;
            this.dbContext = dbContext;
            var mapperConfig = new MapperConfiguration(config => {
                config.CreateMap<UpdateUserProfileCommand,UserProfile>();
            });

            mapper = mapperConfig.CreateMapper();
        }

        public async Task<UpdateUserProfileCommandResponse> Handle(UpdateUserProfileCommand request, CancellationToken cancellationToken)
        {
            var response = new UpdateUserProfileCommandResponse();
            var user = await loginUser.GetAsync();
            var userProfile = await dbContext.UserProfiles.FirstOrDefaultAsync(fd => fd.User == user);

            if (userProfile == null)
                userProfile = new UserProfile { User = user};

            userProfile = mapper.Map<UpdateUserProfileCommand, UserProfile>(request, userProfile);
            dbContext.Update(userProfile);

            await dbContext.SaveChangesAsync();
            return response;
            
        }
    }
}
