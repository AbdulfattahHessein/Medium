//using EntityFrameworkCore.EncryptColumn.Interfaces;
//using EntityFrameworkCore.EncryptColumn.Util;
using EntityFrameworkCore.EncryptColumn.Interfaces;
using Medium.BL.Interfaces.Services;
using Medium.BL.ResponseHandler;
using Medium.Core.Entities;
using Medium.Core.Interfaces.Bases;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using MimeKit;
using static Medium.BL.ResponseHandler.ApiResponseHandler;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;



namespace Medium.BL.AppServices
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration configuration;
        private readonly IUnitOfWork unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEncryptionProvider _encryptionProvider;

        public EmailService(IConfiguration configuration, IUnitOfWork unitOfWork,
                                     UserManager<ApplicationUser> userManager)
        {
            this.configuration = configuration;
            this.unitOfWork = unitOfWork;
            _userManager = userManager;
            //_encryptionProvider = new GenerateEncryptionProvider("8a4dcaaec64d412380fe4b02193cd26f");
        }


        public async Task<ApiResponse<bool>> SendEmail(EmailSendRequest request)
        {
            try
            {
                //sending the Message of passwordResetLink
                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync(configuration["Email:host"], 465, true);
                    client.Authenticate(configuration["Email:fromEmail"], configuration["Email:password"]);
                    var bodybuilder = new BodyBuilder
                    {
                        HtmlBody = $"{request.Message}",
                        TextBody = "wellcome",
                    };
                    var message = new MimeMessage
                    {
                        Body = bodybuilder.ToMessageBody()
                    };
                    message.From.Add(new MailboxAddress("Future Team", "nadasaeed566@gmail.com"));
                    message.To.Add(new MailboxAddress("testing", request.Email));
                    message.Subject = request.Reason == null ? "No Submitted" : request.Reason;
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                }
                //end of sending email
                return Success<bool>("True");

            }
            catch (Exception ex)

            {
                return BadRequest<bool>("False");

            }

        }

        public async Task<ApiResponse<string>> SendResetPasswordCode(SendResetPasswordRequest request)
        {
            // var trans = await _applicationDBContext.Database.BeginTransactionAsync();
            // try
            //{
            //user

            var user = await _userManager.FindByEmailAsync(request.Email);
            //user not Exist => not found
            if (user == null)
            {
                return NotFound<string>("User Not Found");

            }
            //Generate Random Number

            //Random generator = new Random();
            //string randomNumber = generator.Next(0, 1000000).ToString("D6");
            var chars = "0123456789";
            var random = new Random();
            var randomNumber = new string(Enumerable.Repeat(chars, 6).Select(s => s[random.Next(s.Length)]).ToArray());

            //update User In Database Code
            user.Code = randomNumber;
            var updateResult = await _userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
            {
                return BadRequest<string>("Error In Update User");
            }
            var message = "Code To Reset Passsword : " + user.Code;
            //Send Code To  Email 
            var Request = new EmailSendRequest(user.Email, message, "Reset Password");
            await SendEmail(Request);

            // await trans.CommitAsync();
            return Success<string>("Successed");
        }
        public async Task<ApiResponse<string>> ConfirmResetPassword(ConfirmResetPasswordRequest request)
        {
            //Get User
            //user
            var user = await _userManager.FindByEmailAsync(request.Email);
            //user not Exist => not found
            if (user == null)
            {
                return NotFound<string>("User Not Found");

            }
            //Decrept Code From Database User Code
            var userCode = user.Code;
            //Equal With Code
            if (userCode == request.Code)
            {
                return Success<string>("Successed");
            }
            return BadRequest<string>("Operation is failed");
        }

        public async Task<ApiResponse<string>> ResetPassword(ResetPasswordRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            //user not Exist => not found
            if (user == null)
            {
                return NotFound<string>("User Not Found");

            }

            await _userManager.RemovePasswordAsync(user);
            if (!await _userManager.HasPasswordAsync(user))
            {
                await _userManager.AddPasswordAsync(user, request.Password);
            }

            return Success<string>("Successed");
        }


    }
}

