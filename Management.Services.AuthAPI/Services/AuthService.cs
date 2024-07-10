using Management.Services.AuthAPI.Data;
using Management.Services.AuthAPI.Model;
using Management.Services.AuthAPI.Model.DTO;
using Management.Services.AuthAPI.Services.IService;
using Microsoft.AspNetCore.Identity;

namespace Management.Services.AuthAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        public AuthService(AppDbContext db,UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager,IJwtTokenGenerator jwtTokenGenerator) 
        {
            _db = db;
            _jwtTokenGenerator = jwtTokenGenerator;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<bool> AssignRole(string email, string roleName)
        {
            var user = _db.ApplicationUsers.FirstOrDefault(db => db.UserName.ToLower() == email.ToLower());
            if (user != null)
            {
                if (!_roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult())
                {
                    _roleManager.CreateAsync(new IdentityRole(roleName)).GetAwaiter().GetResult();
                }
                await _userManager.AddToRoleAsync(user, roleName);
                return true;
            }
            return false;
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto requestDto)
        {
            var user =_db.ApplicationUsers.FirstOrDefault(db=>db.UserName.ToLower()== requestDto.UserName.ToLower());
            bool isValid = await _userManager.CheckPasswordAsync(user, requestDto.Password);

            if(user==null || isValid==false)
            {
                return new LoginResponseDto()
                {
                    User = null,
                    Token = ""
                };
            }
            //if user is found, generate Token

            var token = _jwtTokenGenerator.GenerateToken(user);

            UserDto userDto = new()
            {
                Email = user.Email,
                ID = user.Id,
                Name = user.Name,
                PhoneNumber = user.PhoneNumber
            };
            LoginResponseDto loginResponseDto = new LoginResponseDto()
            {
                User = userDto,
                Token = token
            };
            return loginResponseDto;

        }

        public async Task<string> Register(RegisterationRequestDto requestDto)
        {
            ApplicationUser user = new()
            {
                UserName = requestDto.Email,
                Email = requestDto.Email,
                NormalizedEmail = requestDto.Email.ToUpper(),
                Name = requestDto.Name,
                PhoneNumber = requestDto.PhoneNumber
            };
            try
            {
                var result = await _userManager.CreateAsync(user,requestDto.Password);
                if (result.Succeeded)
                {
                    var userToReturn = _db.ApplicationUsers.First(u=>u.UserName == requestDto.Email);
                    UserDto userDto = new() 
                    { 
                        Email=userToReturn.Email,
                        ID = userToReturn.Id,
                        Name = userToReturn.Name,
                        PhoneNumber= userToReturn.PhoneNumber
                    };
                    return "";

                }
                else
                {
                    return result.Errors.FirstOrDefault().Description;
                }
            }
            catch (Exception ex)
            {
            }
            return "Error Encountered";
        }
    }
}
