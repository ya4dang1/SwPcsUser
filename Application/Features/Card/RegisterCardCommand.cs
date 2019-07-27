using Application.Infrastructures;
using Application.Models;
using Application.Services;
using Core.Libraries;
using EmyralSystems;
using EmyralSystems.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Card
{
    public class RegisterCardCommand: IRequest<RegisterCardCommandResponse>
    {
        public string CardNumber { get; set; }
        public int CVV { get; set; }
        public int PIN { get; set; }
        public DateTime ExpiredDate { get; set; }
    }

    public class RegisterCardCommandResponse: BaseResponse
    {
    }

    public class RegisterCardCommandHandler : IRequestHandler<RegisterCardCommand, RegisterCardCommandResponse>
    {
        private readonly ResellerConfig resellerConfig;
        private readonly ApplicationDbContext dbContext;
        private readonly PcsDbContext pcsDbContext;
        private readonly LoginUser loginUser;

        public RegisterCardCommandHandler(IOptions<ResellerConfig> resellerConfig, ApplicationDbContext dbContext, PcsDbContext pcsDbContext, LoginUser loginUser)
        {
            this.resellerConfig = resellerConfig.Value;
            this.dbContext = dbContext;
            this.pcsDbContext = pcsDbContext;
            this.loginUser = loginUser;
        }

        public async Task<RegisterCardCommandResponse> Handle(RegisterCardCommand request, CancellationToken cancellationToken)
        {
            var response = new RegisterCardCommandResponse();
            var user = await loginUser.GetAsync();
            if(user != null)
            {
                if (!await dbContext.UserCards.AnyAsync(a => a.CardNumber == request.CardNumber))
                {
                    var card = await pcsDbContext.Card.FirstOrDefaultAsync(fd => fd.CardNumber == request.CardNumber);
                    if (card != null && card.ExpiryDate == request.ExpiredDate)
                    {
                        pcsDbContext.Entry(card).Reference(r => r.Reseller).Load();

                        if (resellerConfig.HostNames.Contains(card.Reseller.HostName))
                        {

                            //Add to UserCard Model
                            var userCard = new UserCard
                            {
                                User = user,
                                CardNumber = request.CardNumber,
                                ExpiredDate = request.ExpiredDate,
                                CVV = request.CVV,
                                PIN = request.PIN
                            };

                            dbContext.Add(userCard);
                            await dbContext.SaveChangesAsync();

                            //Add to CardUser Model
                            pcsDbContext.Entry(card).Reference(r => r.CardUser).Load();
                            if (card.CardUser == null)
                            {
                                var userProfile = await dbContext.UserProfiles.FirstOrDefaultAsync(fd => fd.User == user);
                                var cardUser = await pcsDbContext.CardUser.FirstOrDefaultAsync(fd => fd.Reseller == card.Reseller && fd.UserIdentification == userProfile.IDValue);

                                if(cardUser == null)
                                {
                                    cardUser = await pcsDbContext.CardUser.FirstOrDefaultAsync(fd => fd.Reseller == card.Reseller && fd.Email == user.Email);
                                }

                                if (cardUser == null)
                                {
                                    cardUser = new CardUser
                                    {
                                         Reseller = card.Reseller,
                                         Email = user.Email,
                                         UserIdentification = userProfile.IDValue,
                                         FullName = $"{userProfile.LastName} {userProfile.MiddleName} {userProfile.FirstName}"
                                    };
                                }

                                card.CardUser = cardUser;
                                                                
                                card.Status = 100;
                                pcsDbContext.Update(card);
                                await pcsDbContext.SaveChangesAsync();
                            }
                        }
                        else
                            response.AddError($"Invalid {resellerConfig.Name} card");
                    }
                    else
                        response.AddError("Invalid card details");
                }
                else
                    response.AddError("Card already register");
            }
            else            
                response.AddError("Invalid user");            

            return response;
        }
    }
}
