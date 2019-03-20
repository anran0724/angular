using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Authorization.Roles;
using Abp.Authorization.Users;
using Abp.Configuration;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.Notifications;
using Abp.Organizations;
using Abp.Runtime.Session;
using Abp.UI;
using Abp.Zero.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Sinodom.ElevatorCloud.Authorization.Permissions;
using Sinodom.ElevatorCloud.Authorization.Permissions.Dto;
using Sinodom.ElevatorCloud.Authorization.Roles;
using Sinodom.ElevatorCloud.Authorization.Users.Dto;
using Sinodom.ElevatorCloud.Authorization.Users.Exporting;
using Sinodom.ElevatorCloud.Dto;
using Sinodom.ElevatorCloud.MultiTenancy.UserExtensions;
using Sinodom.ElevatorCloud.Notifications;
using Sinodom.ElevatorCloud.Organizations.Dto;
using Sinodom.ElevatorCloud.Storage;
using Sinodom.ElevatorCloud.Url;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Sinodom.ElevatorCloud.EccpBaseElevators;
using Sinodom.ElevatorCloud.MultiTenancy.EccpUserExtensions;

namespace Sinodom.ElevatorCloud.Authorization.Users
{
    using Sinodom.ElevatorCloud.Editions;
    using Sinodom.ElevatorCloud.MultiTenancy;

    [AbpAuthorize(AppPermissions.Pages_Administration_Users)]
    public class UserAppService : ElevatorCloudAppServiceBase, IUserAppService
    {
        private const int MaxProfilPictureBytes = 1048576; //1MB
        public IAppUrlService AppUrlService { get; set; }

        private readonly RoleManager _roleManager;
        private readonly IUserEmailer _userEmailer;
        private readonly IUserListExcelExporter _userListExcelExporter;
        private readonly INotificationSubscriptionManager _notificationSubscriptionManager;
        private readonly IAppNotifier _appNotifier;
        private readonly IRepository<RolePermissionSetting, long> _rolePermissionRepository;
        private readonly IRepository<UserPermissionSetting, long> _userPermissionRepository;
        private readonly IRepository<UserRole, long> _userRoleRepository;

        private readonly IUserPolicy _userPolicy;
        private readonly IEnumerable<IPasswordValidator<User>> _passwordValidators;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IRepository<OrganizationUnit, long> _organizationUnitRepository;
        private readonly IRepository<EccpCompanyUserExtension, int> _eccpCompanyUserExtension;
        private readonly IBinaryObjectManager _binaryObjectManager;
        private readonly ITempFileCacheManager _tempFileCacheManager;
        private readonly TenantManager _tenantManager;
        private readonly IRepository<ECCPEditionPermission> _eccpEditionPermissionRepository;
        private readonly IRepository<EccpCompanyUserChangeLog, int> _eccpCompanyUserChangeLogRepository;
        public UserAppService(
            RoleManager roleManager,
            IUserEmailer userEmailer,
            IUserListExcelExporter userListExcelExporter,
            INotificationSubscriptionManager notificationSubscriptionManager,
            IAppNotifier appNotifier,
            IRepository<RolePermissionSetting, long> rolePermissionRepository,
            IRepository<UserPermissionSetting, long> userPermissionRepository,
            IRepository<UserRole, long> userRoleRepository,
            IUserPolicy userPolicy,
            IEnumerable<IPasswordValidator<User>> passwordValidators,
            IPasswordHasher<User> passwordHasher,
            IRepository<OrganizationUnit, long> organizationUnitRepository,
            IRepository<EccpCompanyUserExtension, int> eccpCompanyUserExtension,
            IBinaryObjectManager binaryObjectManager, ITempFileCacheManager tempFileCacheManager,
            TenantManager tenantManager,
            IRepository<ECCPEditionPermission> eccpEditionPermissionRepository,
            IRepository<EccpCompanyUserChangeLog, int> eccpCompanyUserChangeLogRepository)
        {
            _roleManager = roleManager;
            _userEmailer = userEmailer;
            _userListExcelExporter = userListExcelExporter;
            _notificationSubscriptionManager = notificationSubscriptionManager;
            _appNotifier = appNotifier;
            _rolePermissionRepository = rolePermissionRepository;
            _userPermissionRepository = userPermissionRepository;
            _userRoleRepository = userRoleRepository;
            _userPolicy = userPolicy;
            _passwordValidators = passwordValidators;
            _passwordHasher = passwordHasher;
            _organizationUnitRepository = organizationUnitRepository;
            _eccpCompanyUserExtension = eccpCompanyUserExtension;
            AppUrlService = NullAppUrlService.Instance;
            _binaryObjectManager = binaryObjectManager;
            _tempFileCacheManager = tempFileCacheManager;
            _tenantManager = tenantManager;
            _eccpEditionPermissionRepository = eccpEditionPermissionRepository;
            _eccpCompanyUserChangeLogRepository = eccpCompanyUserChangeLogRepository;
        }

        public async Task<PagedResultDto<UserListDto>> GetUsers(GetUsersInput input)
        {
            var query = UserManager.Users
                .WhereIf(input.Role.HasValue, u => u.Roles.Any(r => r.RoleId == input.Role.Value))
                .WhereIf(input.OnlyLockedUsers, u => u.LockoutEndDateUtc.HasValue && u.LockoutEndDateUtc.Value > DateTime.UtcNow)
                .WhereIf(
                    !input.Filter.IsNullOrWhiteSpace(),
                    u =>
                        u.Name.Contains(input.Filter) ||
                        u.Surname.Contains(input.Filter) ||
                        u.UserName.Contains(input.Filter) ||
                        u.EmailAddress.Contains(input.Filter)
                );

            if (!input.Permission.IsNullOrWhiteSpace())
            {
                query = from user in query
                        join ur in _userRoleRepository.GetAll() on user.Id equals ur.UserId into urJoined
                        from ur in urJoined.DefaultIfEmpty()
                        join up in _userPermissionRepository.GetAll() on new { UserId = user.Id, Name = input.Permission } equals new { up.UserId, up.Name } into upJoined
                        from up in upJoined.DefaultIfEmpty()
                        join rp in _rolePermissionRepository.GetAll() on new { RoleId = ur == null ? 0 : ur.RoleId, Name = input.Permission } equals new { rp.RoleId, rp.Name } into rpJoined
                        from rp in rpJoined.DefaultIfEmpty()
                        where up != null && up.IsGranted || up == null && rp != null
                        group user by user
                        into userGrouped
                        select userGrouped.Key;
            }

            var userCount = await query.CountAsync();

            var users = await query
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();

            var userListDtos = ObjectMapper.Map<List<UserListDto>>(users);
            await FillRoleNames(userListDtos);

            return new PagedResultDto<UserListDto>(
                userCount,
                userListDtos
                );
        }

        public async Task<FileDto> GetUsersToExcel()
        {
            var users = await UserManager.Users.ToListAsync();
            var userListDtos = ObjectMapper.Map<List<UserListDto>>(users);
            await FillRoleNames(userListDtos);

            return _userListExcelExporter.ExportToFile(userListDtos);
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Users_Create, AppPermissions.Pages_Administration_Users_Edit)]
        public async Task<GetUserForEditOutput> GetUserForEdit(NullableIdDto<long> input)
        {
            //Getting all available roles
            var userRoleDtos = await _roleManager.Roles
                .OrderBy(r => r.DisplayName)
                .Select(r => new UserRoleDto
                {
                    RoleId = r.Id,
                    RoleName = r.Name,
                    RoleDisplayName = r.DisplayName
                })
                .ToArrayAsync();

            var allOrganizationUnits = await _organizationUnitRepository.GetAllListAsync();

            var output = new GetUserForEditOutput
            {
                Roles = userRoleDtos,
                AllOrganizationUnits = ObjectMapper.Map<List<OrganizationUnitDto>>(allOrganizationUnits),
                MemberedOrganizationUnits = new List<string>()
            };

            if (!input.Id.HasValue)
            {
                //Creating a new user
                output.User = new UserEditDto
                {
                    IsActive = true,
                    ShouldChangePasswordOnNextLogin = true,
                    IsTwoFactorEnabled = await SettingManager.GetSettingValueAsync<bool>(AbpZeroSettingNames.UserManagement.TwoFactorLogin.IsEnabled),
                    IsLockoutEnabled = await SettingManager.GetSettingValueAsync<bool>(AbpZeroSettingNames.UserManagement.UserLockOut.IsEnabled)
                };

                foreach (var defaultRole in await _roleManager.Roles.Where(r => r.IsDefault).ToListAsync())
                {
                    var defaultUserRole = userRoleDtos.FirstOrDefault(ur => ur.RoleName == defaultRole.Name);
                    if (defaultUserRole != null)
                    {
                        defaultUserRole.IsAssigned = true;
                    }
                }
            }
            else
            {
                //Editing an existing user
                User user;
                using (UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
                {
                    user = await UserManager.GetUserByIdAsync(input.Id.Value);
                    EccpCompanyUserExtension companyUser =
                        await _eccpCompanyUserExtension.FirstOrDefaultAsync(m => m.UserId == input.Id.Value);

                    output.User = ObjectMapper.Map<UserEditDto>(user);
                    output.CompanyUser = ObjectMapper.Map<EccpCompanyUserExtensionEditDto>(companyUser);
                    output.ProfilePictureId = user.ProfilePictureId;
                }


                foreach (UserRoleDto userRoleDto in userRoleDtos)
                {
                    userRoleDto.IsAssigned = await UserManager.IsInRoleAsync(user, userRoleDto.RoleName);
                }

                var organizationUnits = await UserManager.GetOrganizationUnitsAsync(user);
                output.MemberedOrganizationUnits = organizationUnits.Select(ou => ou.Code).ToList();
            }

            return output;
        }

        [AbpAllowAnonymous]
        public async Task<GetUserForEditOutput> GetUserForAppView(NullableIdDto<long> input)
        {
            var output = new GetUserForEditOutput();

            if (input.Id.HasValue)
            {
                var user = await this.UserManager.GetUserByIdAsync(input.Id.Value);
                EccpCompanyUserExtension companyUser =
                    await _eccpCompanyUserExtension.FirstOrDefaultAsync(m => m.UserId == input.Id.Value);

                output.User = ObjectMapper.Map<UserEditDto>(user);
                output.CompanyUser = ObjectMapper.Map<EccpCompanyUserExtensionEditDto>(companyUser);
                output.ProfilePictureId = user.ProfilePictureId;
            }

            return output;
        }

        [AbpAllowAnonymous]
        public async Task<PagedResultDto<UserListDto>> GetUsersForAppView(GetUsersInput input)
        {
            var query = this.UserManager.Users.WhereIf(
                !input.Filter.IsNullOrWhiteSpace(),
                u => u.Name.Contains(input.Filter) || u.Surname.Contains(input.Filter)
                                                   || u.UserName.Contains(input.Filter)
                                                   || u.EmailAddress.Contains(input.Filter));

            var userCount = await query.CountAsync();

            var users = await query
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();

            var userListDtos = this.ObjectMapper.Map<List<UserListDto>>(users);

            return new PagedResultDto<UserListDto>(userCount, userListDtos);
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Users_ChangePermissions)]
        public async Task<GetUserPermissionsForEditOutput> GetUserPermissionsForEdit(EntityDto<long> input)
        {
            List<FlatPermissionDto> flatPermissionDtos = new List<FlatPermissionDto>();

            if (!this.AbpSession.TenantId.HasValue)
            {
                var permissions = this.PermissionManager.GetAllPermissions();
                flatPermissionDtos = this.ObjectMapper.Map<List<FlatPermissionDto>>(permissions).OrderBy(p => p.DisplayName).ToList();
            }
            else
            {
                var tenant = await _tenantManager.GetByIdAsync(AbpSession.TenantId.Value);
                if (tenant.EditionId.HasValue)
                {
                    IReadOnlyList<Permission> permissions = PermissionManager.GetAllPermissions();
                    var editionGrantedPermissions = await this._eccpEditionPermissionRepository.GetAllListAsync(e => e.EditionId == tenant.EditionId.Value && e.IsGranted);
                    var editionPermissions = new List<Permission>();
                    foreach (Permission permission in permissions)
                    {
                        if (editionGrantedPermissions.Any(e => e.Name == permission.Name))
                        {
                            editionPermissions.Add(permission);
                        }
                    }

                    flatPermissionDtos = this.ObjectMapper.Map<List<FlatPermissionDto>>(editionPermissions).OrderBy(p => p.DisplayName).ToList();
                }
            }

            User user = await UserManager.GetUserByIdAsync(input.Id);

            IReadOnlyList<Permission> grantedPermissions = await UserManager.GetGrantedPermissionsAsync(user);

            return new GetUserPermissionsForEditOutput
            {
                Permissions = flatPermissionDtos,
                GrantedPermissionNames = grantedPermissions.Select(p => p.Name).ToList()
            };
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Users_ChangePermissions)]
        public async Task ResetUserSpecificPermissions(EntityDto<long> input)
        {
            var user = await UserManager.GetUserByIdAsync(input.Id);
            await UserManager.ResetAllPermissionsAsync(user);
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Users_ChangePermissions)]
        public async Task UpdateUserPermissions(UpdateUserPermissionsInput input)
        {
            var user = await UserManager.GetUserByIdAsync(input.Id);
            var grantedPermissions = PermissionManager.GetPermissionsFromNamesByValidating(input.GrantedPermissionNames);
            await UserManager.SetGrantedPermissionsAsync(user, grantedPermissions);
        }

        public async Task CreateOrUpdateUser(CreateOrUpdateUserInput input)
        {
            if (input.User.Id.HasValue)
            {
                await UpdateUserAsync(input);
            }
            else
            {
                await CreateUserAsync(input);
            }
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Users_Delete)]
        public async Task DeleteUser(EntityDto<long> input)
        {
            if (input.Id == AbpSession.GetUserId())
            {
                throw new UserFriendlyException(L("YouCanNotDeleteOwnAccount"));
            }

            var user = await UserManager.GetUserByIdAsync(input.Id);
            CheckErrors(await UserManager.DeleteAsync(user));
        }

        public async Task UnlockUser(EntityDto<long> input)
        {
            var user = await UserManager.GetUserByIdAsync(input.Id);
            user.Unlock();
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Users_Edit)]
        protected virtual async Task UpdateUserAsync(CreateOrUpdateUserInput input)
        {
            Debug.Assert(input.User.Id != null, "input.User.Id should be set.");

            User user = await UserManager.FindByIdAsync(input.User.Id.Value.ToString());
            EccpCompanyUserExtension companyUser = _eccpCompanyUserExtension.FirstOrDefault(m => m.UserId == input.User.Id.Value);

            if (user.Name != input.User.Name)
            {
                await this._eccpCompanyUserChangeLogRepository.InsertAsync(
                    new EccpCompanyUserChangeLog
                    {
                        FieldName = "Name",
                        OldValue = user.Name ?? string.Empty,
                        NewValue = input.User.Name,
                        UserId = input.User.Id.Value
                    });
            }

            if (user.Surname != input.User.Surname)
            {
                await this._eccpCompanyUserChangeLogRepository.InsertAsync(
                    new EccpCompanyUserChangeLog
                    {
                        FieldName = "Surname",
                        OldValue = user.Surname ?? string.Empty,
                        NewValue = input.User.Surname,
                        UserId = input.User.Id.Value
                    });
            }

            if (user.EmailAddress != input.User.EmailAddress)
            {
                await this._eccpCompanyUserChangeLogRepository.InsertAsync(
                    new EccpCompanyUserChangeLog
                    {
                        FieldName = "EmailAddress",
                        OldValue = user.EmailAddress ?? string.Empty,
                        NewValue = input.User.EmailAddress,
                        UserId = input.User.Id.Value
                    });
            }

            if (user.UserName != input.User.UserName)
            {
                await this._eccpCompanyUserChangeLogRepository.InsertAsync(
                    new EccpCompanyUserChangeLog
                    {
                        FieldName = "UserName",
                        OldValue = user.UserName ?? string.Empty,
                        NewValue = input.User.UserName,
                        UserId = input.User.Id.Value
                    });
            }

            if (user.PhoneNumber != input.User.PhoneNumber)
            {
                await this._eccpCompanyUserChangeLogRepository.InsertAsync(
                    new EccpCompanyUserChangeLog
                    {
                        FieldName = "PhoneNumber",
                        OldValue = user.PhoneNumber ?? string.Empty,
                        NewValue = input.User.PhoneNumber,
                        UserId = input.User.Id.Value
                    });
            }

            if (user.IsActive != input.User.IsActive)
            {
                await this._eccpCompanyUserChangeLogRepository.InsertAsync(
                    new EccpCompanyUserChangeLog
                    {
                        FieldName = "IsActive",
                        OldValue = user.IsActive.ToString(),
                        NewValue = input.User.IsActive.ToString(),
                        UserId = input.User.Id.Value
                    });
            }

            if (user.ShouldChangePasswordOnNextLogin != input.User.ShouldChangePasswordOnNextLogin)
            {
                await this._eccpCompanyUserChangeLogRepository.InsertAsync(
                    new EccpCompanyUserChangeLog
                    {
                        FieldName = "ShouldChangePasswordOnNextLogin",
                        OldValue = user.ShouldChangePasswordOnNextLogin.ToString(),
                        NewValue = input.User.ShouldChangePasswordOnNextLogin.ToString(),
                        UserId = input.User.Id.Value
                    });
            }

            if (user.IsTwoFactorEnabled != input.User.IsTwoFactorEnabled)
            {
                await this._eccpCompanyUserChangeLogRepository.InsertAsync(
                    new EccpCompanyUserChangeLog
                    {
                        FieldName = "IsTwoFactorEnabled",
                        OldValue = user.IsTwoFactorEnabled.ToString(),
                        NewValue = input.User.IsTwoFactorEnabled.ToString(),
                        UserId = input.User.Id.Value
                    });
            }

            if (user.IsLockoutEnabled != input.User.IsLockoutEnabled)
            {
                await this._eccpCompanyUserChangeLogRepository.InsertAsync(
                    new EccpCompanyUserChangeLog
                    {
                        FieldName = "IsLockoutEnabled",
                        OldValue = user.IsLockoutEnabled.ToString(),
                        NewValue = input.User.IsLockoutEnabled.ToString(),
                        UserId = input.User.Id.Value
                    });
            }

            if (companyUser.IdCard != input.CompanyUser.IdCard)
            {
                await this._eccpCompanyUserChangeLogRepository.InsertAsync(
                    new EccpCompanyUserChangeLog
                    {
                        FieldName = "IdCard",
                        OldValue = companyUser.IdCard ?? string.Empty,
                        NewValue = input.CompanyUser.IdCard,
                        UserId = input.User.Id.Value
                    });
            }

            if (companyUser.Mobile != input.CompanyUser.Mobile)
            {
                await this._eccpCompanyUserChangeLogRepository.InsertAsync(
                    new EccpCompanyUserChangeLog
                    {
                        FieldName = "Mobile",
                        OldValue = companyUser.Mobile ?? string.Empty,
                        NewValue = input.CompanyUser.Mobile,
                        UserId = input.User.Id.Value
                    });
            }

            var roles = this._roleManager.Roles;
            var userRoles = this._userRoleRepository.GetAll().Where(e => e.UserId == input.User.Id.Value);
            var roleName = from role in roles
                           join userRole in userRoles
                               on role.Id equals userRole.RoleId
                           select role.Name;
            if (!roleName.All(input.AssignedRoleNames.ToList().Contains) || roleName.ToList().Count != input.AssignedRoleNames.Length)
            {
                await this._eccpCompanyUserChangeLogRepository.InsertAsync(
                    new EccpCompanyUserChangeLog
                    {
                        FieldName = "RoleNames",
                        OldValue = string.Join(",", roleName) ?? string.Empty,
                        NewValue = string.Join(",", input.AssignedRoleNames),
                        UserId = input.User.Id.Value
                    });
            }

            #region  用户详细信息修改
            if (companyUser != null)
            {
                ////-------signPicture
                if (input.SignPictureToken != "ceb4a760-a3d9-dd62-8b1b-39ea28a2d423")
                {
                    if (companyUser.SignPictureId != input.CompanyUser.SignPictureId && input.CompanyUser.SignPictureId != null)
                    {
                        byte[] byteArray;
                        byte[] imageBytes = _tempFileCacheManager.GetFile(input.SignPictureToken);
                        if (imageBytes == null)
                        {
                            throw new UserFriendlyException("There is no such image file with the token: " + input.CompanyUser.SignPictureId);
                        }
                        using (MemoryStream stream = new MemoryStream(imageBytes))
                        {
                            byteArray = stream.ToArray();
                        }

                        if (byteArray.Length > MaxProfilPictureBytes)
                        {
                            throw new UserFriendlyException(L("ResizedProfilePicture_Warn_SizeLimit", AppConsts.ResizedMaxProfilPictureBytesUserFriendlyValue));
                        }
                        BinaryObject storedFile = new BinaryObject(AbpSession.TenantId, byteArray);
                        await _binaryObjectManager.SaveAsync(storedFile);
                        input.CompanyUser.SignPictureId = storedFile.Id;
                    }
                }
                ////-------certificateBackPicture
                if (input.CertificateBackPictureToken != "ceb4a760-a3d9-dd62-8b1b-39ea28a2d423")
                {
                    if (companyUser.CertificateBackPictureId != input.CompanyUser.CertificateBackPictureId && input.CompanyUser.CertificateBackPictureId != null)
                    {
                        byte[] certificateBackPictureByteArray;
                        byte[] certificateBackPictureImageBytes = _tempFileCacheManager.GetFile(input.CertificateBackPictureToken);
                        if (certificateBackPictureImageBytes == null)
                        {
                            throw new UserFriendlyException("There is no such image file with the token: " + input.CompanyUser.CertificateBackPictureId);
                        }
                        using (MemoryStream stream = new MemoryStream(certificateBackPictureImageBytes))
                        {
                            certificateBackPictureByteArray = stream.ToArray();
                        }

                        if (certificateBackPictureByteArray.Length > MaxProfilPictureBytes)
                        {
                            throw new UserFriendlyException(L("ResizedProfilePicture_Warn_SizeLimit", AppConsts.ResizedMaxProfilPictureBytesUserFriendlyValue));
                        }
                        BinaryObject certificateBackPictureStoredFile = new BinaryObject(AbpSession.TenantId, certificateBackPictureByteArray);
                        await _binaryObjectManager.SaveAsync(certificateBackPictureStoredFile);
                        input.CompanyUser.CertificateBackPictureId = certificateBackPictureStoredFile.Id;
                    }
                }
                ////-------certificateFrontPicture
                if (input.CertificateFrontPictureToken != "ceb4a760-a3d9-dd62-8b1b-39ea28a2d423")
                {
                    if (companyUser.CertificateFrontPictureId != input.CompanyUser.CertificateFrontPictureId && input.CompanyUser.CertificateFrontPictureId != null)
                    {
                        byte[] certificateFrontPictureByteArray;
                        byte[] certificateFrontPictureImageBytes = _tempFileCacheManager.GetFile(input.CertificateFrontPictureToken);
                        if (certificateFrontPictureImageBytes == null)
                        {
                            throw new UserFriendlyException("There is no such image file with the token: " + input.CompanyUser.CertificateFrontPictureId);
                        }
                        using (MemoryStream stream = new MemoryStream(certificateFrontPictureImageBytes))
                        {
                            certificateFrontPictureByteArray = stream.ToArray();
                        }

                        if (certificateFrontPictureByteArray.Length > MaxProfilPictureBytes)
                        {
                            throw new UserFriendlyException(L("ResizedProfilePicture_Warn_SizeLimit", AppConsts.ResizedMaxProfilPictureBytesUserFriendlyValue));
                        }
                        BinaryObject certificateFrontPictureStoredFile = new BinaryObject(AbpSession.TenantId, certificateFrontPictureByteArray);
                        await _binaryObjectManager.SaveAsync(certificateFrontPictureStoredFile);
                        input.CompanyUser.CertificateFrontPictureId = certificateFrontPictureStoredFile.Id;
                    }
                }

                ObjectMapper.Map(input.CompanyUser, companyUser);

                companyUser.UserId = user.Id;

                await _eccpCompanyUserExtension.UpdateAsync(companyUser);
            }
            #endregion
            //Update user properties
            ObjectMapper.Map(input.User, user); //Passwords is not mapped (see mapping configuration)            

            if (input.SetRandomPassword)
            {
                var randomPassword = User.CreateRandomPassword();
                user.Password = _passwordHasher.HashPassword(user, randomPassword);
                input.User.Password = randomPassword;
            }
            else if (!input.User.Password.IsNullOrEmpty())
            {
                await UserManager.InitializeOptionsAsync(AbpSession.TenantId);
                CheckErrors(await UserManager.ChangePasswordAsync(user, input.User.Password));
            }

            CheckErrors(await UserManager.UpdateAsync(user));

            //Update roles
            CheckErrors(await UserManager.SetRoles(user, input.AssignedRoleNames));

            //update organization units
            await UserManager.SetOrganizationUnitsAsync(user, input.OrganizationUnits.ToArray());

            if (input.SendActivationEmail)
            {
                user.SetNewEmailConfirmationCode();
                await _userEmailer.SendEmailActivationLinkAsync(
                    user,
                    AppUrlService.CreateEmailActivationUrlFormat(AbpSession.TenantId),
                    input.User.Password
                );
            }
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Users_Create)]
        protected virtual async Task CreateUserAsync(CreateOrUpdateUserInput input)
        {
            if (AbpSession.TenantId.HasValue)
            {
                await _userPolicy.CheckMaxUserCountAsync(AbpSession.GetTenantId());
            }

            User user = ObjectMapper.Map<User>(input.User); //Passwords is not mapped (see mapping configuration)
            if (AbpSession.TenantId == null && user.UserName == "test")
            {
                user.TenantId = 1;
            }
            else
            {
                user.TenantId = AbpSession.TenantId;
            }
            EccpCompanyUserExtension companyUse = ObjectMapper.Map<EccpCompanyUserExtension>(input.CompanyUser);

            //Set password
            if (input.SetRandomPassword)
            {
                var randomPassword = User.CreateRandomPassword();
                user.Password = _passwordHasher.HashPassword(user, randomPassword);
                input.User.Password = randomPassword;
            }
            else if (!input.User.Password.IsNullOrEmpty())
            {
                await UserManager.InitializeOptionsAsync(AbpSession.TenantId);
                foreach (var validator in _passwordValidators)
                {
                    CheckErrors(await validator.ValidateAsync(UserManager, user, input.User.Password));
                }
                user.Password = _passwordHasher.HashPassword(user, input.User.Password);
            }

            user.ShouldChangePasswordOnNextLogin = input.User.ShouldChangePasswordOnNextLogin;

            //Assign roles
            user.Roles = new Collection<UserRole>();
            foreach (var roleName in input.AssignedRoleNames)
            {
                var role = await _roleManager.GetRoleByNameAsync(roleName);
                user.Roles.Add(new UserRole(AbpSession.TenantId, user.Id, role.Id));
            }
            ////-------signPicture
            if (input.SignPictureToken != "ceb4a760-a3d9-dd62-8b1b-39ea28a2d423")
            {
                if (input.SignPictureToken != "")
                {
                    byte[] byteArray;
                    byte[] imageBytes = _tempFileCacheManager.GetFile(input.SignPictureToken);
                    if (imageBytes == null)
                    {
                        throw new UserFriendlyException("There is no such image file with the token: " + companyUse.SignPictureId);
                    }
                    using (MemoryStream stream = new MemoryStream(imageBytes))
                    {
                        byteArray = stream.ToArray();
                    }

                    if (byteArray.Length > MaxProfilPictureBytes)
                    {
                        throw new UserFriendlyException(L("ResizedProfilePicture_Warn_SizeLimit", AppConsts.ResizedMaxProfilPictureBytesUserFriendlyValue));
                    }
                    BinaryObject storedFile = new BinaryObject(AbpSession.TenantId, byteArray);
                    await _binaryObjectManager.SaveAsync(storedFile);
                    companyUse.SignPictureId = storedFile.Id;
                }
            }
            else
            {
                companyUse.SignPictureId = new Guid("ceb4a760-a3d9-dd62-8b1b-39ea28a2d423");
            }
            ////-------certificateBackPicture
            if (input.CertificateBackPictureToken != "ceb4a760-a3d9-dd62-8b1b-39ea28a2d423")
            {
                if (input.CertificateBackPictureToken != "")
                {
                    byte[] certificateBackPictureByteArray;
                    byte[] certificateBackPictureImageBytes = _tempFileCacheManager.GetFile(input.CertificateBackPictureToken);
                    if (certificateBackPictureImageBytes == null)
                    {
                        throw new UserFriendlyException("There is no such image file with the token: " + companyUse.CertificateBackPictureId);
                    }
                    using (MemoryStream stream = new MemoryStream(certificateBackPictureImageBytes))
                    {
                        certificateBackPictureByteArray = stream.ToArray();
                    }

                    if (certificateBackPictureByteArray.Length > MaxProfilPictureBytes)
                    {
                        throw new UserFriendlyException(L("ResizedProfilePicture_Warn_SizeLimit", AppConsts.ResizedMaxProfilPictureBytesUserFriendlyValue));
                    }
                    BinaryObject certificateBackPictureStoredFile = new BinaryObject(AbpSession.TenantId, certificateBackPictureByteArray);
                    await _binaryObjectManager.SaveAsync(certificateBackPictureStoredFile);
                    companyUse.CertificateBackPictureId = certificateBackPictureStoredFile.Id;
                }
            }
            else
            {
                companyUse.CertificateBackPictureId = new Guid("ceb4a760-a3d9-dd62-8b1b-39ea28a2d423");
            }
            ////-------certificateFrontPicture
            if (input.CertificateFrontPictureToken != "ceb4a760-a3d9-dd62-8b1b-39ea28a2d423")
            {
                if (input.CertificateFrontPictureToken != "")
                {
                    byte[] certificateFrontPictureByteArray;
                    byte[] certificateFrontPictureImageBytes = _tempFileCacheManager.GetFile(input.CertificateFrontPictureToken);
                    if (certificateFrontPictureImageBytes == null)
                    {
                        throw new UserFriendlyException("There is no such image file with the token: " + companyUse.CertificateFrontPictureId);
                    }
                    using (MemoryStream stream = new MemoryStream(certificateFrontPictureImageBytes))
                    {
                        certificateFrontPictureByteArray = stream.ToArray();
                    }

                    if (certificateFrontPictureByteArray.Length > MaxProfilPictureBytes)
                    {
                        throw new UserFriendlyException(L("ResizedProfilePicture_Warn_SizeLimit", AppConsts.ResizedMaxProfilPictureBytesUserFriendlyValue));
                    }
                    BinaryObject certificateFrontPictureStoredFile = new BinaryObject(AbpSession.TenantId, certificateFrontPictureByteArray);
                    await _binaryObjectManager.SaveAsync(certificateFrontPictureStoredFile);
                    companyUse.CertificateFrontPictureId = certificateFrontPictureStoredFile.Id;
                }
            }
            else
            {
                companyUse.CertificateFrontPictureId = new Guid("ceb4a760-a3d9-dd62-8b1b-39ea28a2d423");
            }

            CheckErrors(await UserManager.CreateAsync(user));
            await CurrentUnitOfWork.SaveChangesAsync(); //To get new user's Id.


            companyUse.UserId = user.Id;
            await _eccpCompanyUserExtension.InsertAsync(companyUse);

            //Notifications
            await _notificationSubscriptionManager.SubscribeToAllAvailableNotificationsAsync(user.ToUserIdentifier());
            await _appNotifier.WelcomeToTheApplicationAsync(user);

            //Organization Units
            await UserManager.SetOrganizationUnitsAsync(user, input.OrganizationUnits.ToArray());

            input.SendActivationEmail = false;
            //Send activation email
            if (input.SendActivationEmail)
            {
                user.SetNewEmailConfirmationCode();
                await _userEmailer.SendEmailActivationLinkAsync(
                    user,
                    AppUrlService.CreateEmailActivationUrlFormat(AbpSession.TenantId),
                    input.User.Password
                );
            }
        }

        private async Task FillRoleNames(List<UserListDto> userListDtos)
        {
            /* This method is optimized to fill role names to given list. */

            var userRoles = await _userRoleRepository.GetAll()
                .Where(userRole => userListDtos.Any(user => user.Id == userRole.UserId))
                .Select(userRole => userRole).ToListAsync();

            var distinctRoleIds = userRoles.Select(userRole => userRole.RoleId).Distinct();

            foreach (var user in userListDtos)
            {
                var rolesOfUser = userRoles.Where(userRole => userRole.UserId == user.Id).ToList();
                user.Roles = ObjectMapper.Map<List<UserListRoleDto>>(rolesOfUser);
            }

            var roleNames = new Dictionary<int, string>();
            foreach (var roleId in distinctRoleIds)
            {
                roleNames[roleId] = (await _roleManager.GetRoleByIdAsync(roleId)).DisplayName;
            }

            foreach (var userListDto in userListDtos)
            {
                foreach (var userListRoleDto in userListDto.Roles)
                {
                    userListRoleDto.RoleName = roleNames[userListRoleDto.RoleId];
                }

                userListDto.Roles = userListDto.Roles.OrderBy(r => r.RoleName).ToList();
            }
        }
    }
}
