using SchoolManagement.Application.Common.Models;
using SchoolManagement.Application.DTOs.Master;
using SchoolManagement.Application.DTOs.Menu;
using SchoolManagement.Application.DTOs.Module;
using SchoolManagement.Application.DTOs.Permission;
using SchoolManagement.Application.DTOs.RolePermission;
using SchoolManagement.Application.Interfaces;
using SchoolManagement.Domain.Entities.AcademicModule;
using SchoolManagement.Domain.Entities.SecurityModule;
using SchoolManagement.Infrastructure.Persistence.Repositories.Interfaces;

namespace SchoolManagement.Application.Services
{
    public class SettingService : ISettingService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IAcademicSessionRepository _academicSessionRepository;
        private readonly IClassRepository _classRepository;
        private readonly ISectionRepository _sectionRepository;
        private readonly IModuleRepository _moduleRepository;
        private readonly IPermissionRepository _permissionRepository;
        private readonly IRolePermissionRepository _rolePermissionRepository;
        public SettingService(IUserRepository userRepository, IRoleRepository roleRepository, IAcademicSessionRepository academicSessionRepository, IClassRepository classRepository, ISectionRepository sectionRepository, IModuleRepository moduleRepository, IPermissionRepository permissionRepository, IRolePermissionRepository rolePermissionRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _academicSessionRepository = academicSessionRepository;
            _classRepository = classRepository;
            _sectionRepository = sectionRepository;
            _moduleRepository = moduleRepository;
            _permissionRepository = permissionRepository;
            _rolePermissionRepository = rolePermissionRepository;
        }

        public async Task CreateRoleAsync(CreateRoleRequest request)
        {
            var role = new RoleEntity()
            {
                Name = request.RoleName,
                Description = request.Description,
                CreatedAt = DateTime.UtcNow
            };
            await _roleRepository.AddAsync(role);

            await _roleRepository.SaveChangesAsync();
        }

        public async Task CreateAcademicSessionAsync(CreateAcademicSessionRequest request)
        {
            var entity = new AcademicSessionEntity
            {
                Id = Guid.NewGuid(),

                SessionName = request.SessionName,

                StartDate = request.StartDate,

                EndDate = request.EndDate,

                IsCurrent = request.IsCurrent,

                CreatedAt = DateTime.UtcNow
            };

            await _academicSessionRepository.AddAsync(entity);

            await _academicSessionRepository.SaveChangesAsync();
        }

        public async Task CreateClassAsync(CreateClassRequest request)
        {
            var entity = new ClassEntity
            {
                Id = Guid.NewGuid(),

                ClassName = request.ClassName,

                DisplayOrder = request.DisplayOrder,

                CreatedAt = DateTime.UtcNow
            };

            await _classRepository.AddAsync(entity);

            await _classRepository.SaveChangesAsync();
        }

        public async Task CreateSectionAsync(CreateSectionRequest request)
        {
            var entity = new SectionEntity
            {
                Id = Guid.NewGuid(),

                SectionName = request.SectionName,

                CreatedAt = DateTime.UtcNow
            };

            await _sectionRepository.AddAsync(entity);

            await _sectionRepository.SaveChangesAsync();
        }

        public async Task CreateModuleAsync(CreateModuleRequest request)
        {
            var module = new ModuleEntity
            {
                Id = Guid.NewGuid(),

                Name = request.Name,

                Code = request.Code,

                Route = request.Route,

                Icon = request.Icon,

                DisplayOrder =
                    request.DisplayOrder,

                ParentModuleId =
                    request.ParentModuleId,

                CreatedAt = DateTime.UtcNow
            };

            await _moduleRepository
                .AddAsync(module);

            await _moduleRepository
                .SaveChangesAsync();
        }

        public async Task CreatePermissionAsync(CreatePermissionRequest request)
        {
            var permission =
                new PermissionEntity
                {
                    Id = Guid.NewGuid(),

                    ModuleId =
                        request.ModuleId,

                    Name = request.Name,

                    Code = request.Code,

                    CreatedAt =
                        DateTime.UtcNow
                };

            await _permissionRepository
                .AddAsync(permission);

            await _permissionRepository
                .SaveChangesAsync();
        }

        public async Task AssignPermissionsAsync(AssignRolePermissionRequest request)
        {
            foreach (var permissionId
                     in request.PermissionIds)
            {
                await _rolePermissionRepository
                    .AddAsync(
                        new RolePermissionEntity
                        {
                            Id = Guid.NewGuid(),

                            RoleId = request.RoleId,

                            PermissionId =
                                permissionId,

                            CreatedAt =
                                DateTime.UtcNow
                        });
            }

            await _rolePermissionRepository
                .SaveChangesAsync();
        }

        public async Task<ApiResponse<List<PermissionMatrixResponse>>> GetPermissionMatrixAsync()
        {
            var roles =
                await _roleRepository
                    .GetRolesWithPermissionsAsync();

            var permissions =
                await _permissionRepository
                    .GetAllAsync();

            var result = roles.Select(role =>
                new PermissionMatrixResponse
                {
                    RoleId = role.Id,

                    RoleName = role.Name,

                    Permissions =
                        permissions
                            .Select(permission =>
                                new PermissionMatrixItemResponse
                                {
                                    PermissionId =
                                        permission.Id,

                                    PermissionName =
                                        permission.Name,

                                    PermissionCode =
                                        permission.Code!,

                                    Assigned =
                                        role.RolePermissions!
                                            .Any(x =>
                                                x.PermissionId ==
                                                permission.Id)
                                })
                            .ToList()
                })
                .ToList();

            return ApiResponse<List<PermissionMatrixResponse>>.SuccessResponse(result);
        }

        public async Task<ApiResponse<List<MenuTreeResponse>>> GetMenuTreeAsync()
        {
            var modules =
                await _moduleRepository
                    .GetMenuModulesAsync();

            var result = modules

                .Where(x =>
                    x.ParentModuleId == null)

                .Select(BuildMenuTree)

                .ToList();

            return ApiResponse<List<MenuTreeResponse>>.SuccessResponse(result);
        }

        public async Task SavePermissionMatrixAsync(SavePermissionMatrixRequest request)
        {
            var existingPermissions =
                await _rolePermissionRepository
                    .GetByRoleIdAsync(
                        request.RoleId);

            if (existingPermissions.Any())
            {
                await _rolePermissionRepository
                    .DeleteRangeAsync(
                        existingPermissions);
            }

            foreach (var permissionId
                     in request.PermissionIds)
            {
                await _rolePermissionRepository
                    .AddAsync(
                        new RolePermissionEntity
                        {
                            Id = Guid.NewGuid(),

                            RoleId = request.RoleId,

                            PermissionId = permissionId,

                            CreatedAt =
                                DateTime.UtcNow
                        });
            }

            await _rolePermissionRepository
                .SaveChangesAsync();
        }

        public async Task<ApiResponse<List<UserMenuResponse>>> GetUserMenuAsync(Guid userId)
        {
            var user =
                await _userRepository
                    .GetUserWithCompletePermissionsAsync(
                        userId);

            if (user is null)
                return ApiResponse<List<UserMenuResponse>>.FailureResponse("User not found.");

            var modules = user.UserRoles!

                .SelectMany(x =>
                    x.Role!.RolePermissions!)

                .Select(x =>
                    x.Permission!.Module!)

                .DistinctBy(x =>
                    x.Id)

                .ToList();

            var rootModules =
                modules.Where(x =>
                    x.ParentModuleId == null)

                .OrderBy(x =>
                    x.DisplayOrder)

                .ToList();

            var result = rootModules
                .Select(x =>
                    BuildUserMenuTree(
                        x,
                        modules))
                .ToList();

            return ApiResponse<List<UserMenuResponse>>.SuccessResponse(result);
        }

        #region Private Methods
        private MenuTreeResponse BuildMenuTree(ModuleEntity module)
        {
            return new MenuTreeResponse
            {
                Id = module.Id,

                Name = module.Name,

                Route = module.Route,

                Icon = module.Icon,

                Children =
                    module.ChildModules

                        .OrderBy(x =>
                            x.DisplayOrder)

                        .Select(BuildMenuTree)

                        .ToList()
            };
        }

        private UserMenuResponse BuildUserMenuTree(ModuleEntity module, List<ModuleEntity> allModules)
        {
            return new UserMenuResponse
            {
                Id = module.Id,

                Name = module.Name,

                Route = module.Route,

                Icon = module.Icon,

                Children =
                    allModules

                    .Where(x =>
                        x.ParentModuleId ==
                        module.Id)

                    .OrderBy(x =>
                        x.DisplayOrder)

                    .Select(x =>
                        BuildUserMenuTree(
                            x,
                            allModules))

                    .ToList()
            };
        }
        #endregion
    }
}
