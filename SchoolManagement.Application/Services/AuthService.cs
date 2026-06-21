using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SchoolManagement.Application.Common.Exceptions;
using SchoolManagement.Application.Common.Models;
using SchoolManagement.Application.Constants;
using SchoolManagement.Application.DTOs.Auth;
using SchoolManagement.Application.DTOs.Menu;
using SchoolManagement.Application.DTOs.Module;
using SchoolManagement.Application.DTOs.Permission;
using SchoolManagement.Application.DTOs.RolePermission;
using SchoolManagement.Application.Interfaces;
using SchoolManagement.Domain.Entities.SecurityModule;
using SchoolManagement.Infrastructure.Persistence.Repositories.Interfaces;
using SchoolManagement.Infrastructure.Security;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SchoolManagement.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly JwtSettings _jwtSettings;

        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;

        public AuthService(IUserRepository userRepository, IRoleRepository roleRepository, IOptions<JwtSettings> settings)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _jwtSettings = settings.Value;
        }

        public async Task<ApiResponse<LoginResponse>> LoginAsync(LoginRequest request)
        {
            var user = await _userRepository.GetUserByUsernameAsync(request.Username);

            if (user is null)
            {
                throw new UnauthorizedException("Invalid username or password");
            }

            if (!user.IsActive)
            {
                throw new UnauthorizedException("Invalid username or password");
            }

            if (user.IsLocked)
            {
                throw new UnauthorizedException("Invalid username or password");
            }

            var isValidPassword = PasswordHasher.Verify(request.Password, user.Password);

            if (!isValidPassword)
            {
                throw new UnauthorizedException("Invalid username or password");
            }

            var roles = user.UserRoles?
                    .Select(x => x.Role?.Name ?? "")
                    .ToList()
                ?? [];

            var permissions = user.UserRoles!
                .SelectMany(x =>
                    x.Role!.RolePermissions!)
                .Select(x =>
                    x.Permission!.Code!)
                .Distinct()
                .ToList();

            var token = GenerateToken(user, roles, permissions);

            user.LastLoginAt = DateTime.UtcNow;

            _userRepository.Update(user);

            await _userRepository.SaveChangesAsync();

            return new ApiResponse<LoginResponse>
            {
                Data = new LoginResponse
                {
                    Token = token,
                    Username = user.Username,
                    Roles = roles
                }
            };
        }

        public async Task RegisterUserAsync(RegisterRequest request)
        {
            var exists =
                await _userRepository.ExistsAsync(
                    x => x.Username == request.Username);

            if (exists)
            {
                throw new Exception(
                    "Username already exists.");
            }

            var user = new UserEntity
            {
                Id = Guid.NewGuid(),

                Username = request.Username,

                Email = request.Email,

                Password = PasswordHasher.Hash(
                    request.Password),

                FirstName = request.FirstName,

                LastName = request.LastName,

                MobileNumber = request.MobileNumber,

                CreatedAt = DateTime.UtcNow,

                IsActive = true
            };

            var roles =
                await _roleRepository
                    .GetRolesByIdsAsync(
                        request.RoleIds);

            foreach (var role in roles)
            {
                user.UserRoles ??= [];

                user.UserRoles.Add(
                    new UserRoleEntity
                    {
                        Id = Guid.NewGuid(),

                        UserId = user.Id,

                        RoleId = role.Id,

                        CreatedAt = DateTime.UtcNow,

                        IsActive = true
                    });
            }

            await _userRepository.AddAsync(user);

            await _userRepository.SaveChangesAsync();
        }

        #region Private Methods
        private string GenerateToken(UserEntity user, List<string> roles, List<string> permissions)
        {
            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new(JwtRegisteredClaimNames.UniqueName, user.Username),

                new(ClaimConstants.UserId, user.Id.ToString()),
                new(ClaimConstants.Username, user.Username),
                new(ClaimConstants.Email, user.Email)
            };

            // Roles
            foreach (var role in roles.Distinct())
            {
                claims.Add(
                    new Claim(
                        ClaimTypes.Role,
                        role));
            }

            // Permissions
            foreach (var permission in permissions.Distinct())
            {
                claims.Add(
                    new Claim(
                        ClaimConstants.Permission,
                        permission));
            }

            var key =
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(
                        _jwtSettings.SecretKey));

            var credentials =
                new SigningCredentials(
                    key,
                    SecurityAlgorithms.HmacSha256);

            var token =
                new JwtSecurityToken(
                    issuer: _jwtSettings.Issuer,
                    audience: _jwtSettings.Audience,
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(
                        _jwtSettings.ExpiryMinutes),
                    signingCredentials: credentials);

            return new JwtSecurityTokenHandler()
                .WriteToken(token);
        }
        #endregion
    }
}
