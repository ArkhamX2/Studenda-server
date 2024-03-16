using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Studenda.Server.Configuration.Static;
using Studenda.Server.Data;
using Studenda.Server.Model.Common;
using ConfigurationManager = Studenda.Server.Configuration.ConfigurationManager;

namespace Studenda.Server.Service.Security;

/// <summary>
///     Сервис работы с безопасностью.
/// </summary>
/// <param name="configurationManager">Менеджер конфигурации.</param>
/// <param name="identityContext">Сессия работы с базой данных безопасности.</param>
/// <param name="accountService">Сервис работы с аккаунтами.</param>
/// <param name="userManager">Менеджер работы с пользователями.</param>
/// <param name="roleManager">Менеджер работы с ролями.</param>
public class SecurityService(
    ConfigurationManager configurationManager,
    IdentityContext identityContext,
    AccountService accountService,
    UserManager<IdentityUser> userManager,
    RoleManager<IdentityRole> roleManager
)
{
    private ConfigurationManager ConfigurationManager { get; } = configurationManager;
    private IdentityContext IdentityContext { get; } = identityContext;
    private AccountService AccountService { get; } = accountService;
    private UserManager<IdentityUser> UserManager { get; } = userManager;
    private RoleManager<IdentityRole> RoleManager { get; } = roleManager;

    /// <summary>
    ///     Получить список аккаунтов по названию роли.
    /// </summary>
    /// <param name="roleName">Название роли.</param>
    /// <returns>Список аккаунтов.</returns>
    public async Task<List<Account>> GetAccountsByRole(string roleName)
    {
        var userIds = (await GetRoleUsers(roleName))
            .Select(user => user.Id)
            .ToList();

        return await AccountService.DataContext.Accounts
            .Where(account => userIds.Contains(account.IdentityId!))
            .ToListAsync();
    }

    /// <summary>
    ///     Получить список аккаунтов по пользователям.
    /// </summary>
    /// <param name="users">Пользователи.</param>
    /// <returns>Список аккаунтов.</returns>
    public async Task<List<Account>> GetAccountsByUser(List<IdentityUser> users)
    {
        var identityIds = users.Select(identity => identity.Id).ToList();

        return await AccountService.GetByIdentityId(identityIds);
    }

    /// <summary>
    ///     Выдать роль пользователю.
    /// </summary>
    /// <param name="userName">Имена пользователей.</param>
    /// <param name="roleName">Названия ролей.</param>
    /// <returns>Статус операции.</returns>
    public async Task<bool> GrantRoleToUser(IEnumerable<string> userNames, IEnumerable<string> roleNames)
    {
        return await GrantRoleToUser(userNames, MakeRolesFromNames(roleNames));
    }

    /// <summary>
    ///     Выдать роль пользователю.
    /// </summary>
    /// <param name="user">Пользователи.</param>
    /// <param name="roleName">Названия ролей.</param>
    /// <returns>Статус операции.</returns>
    public async Task<bool> GrantRoleToUser(IEnumerable<IdentityUser> users, IEnumerable<string> roleNames)
    {
        return await GrantRoleToUser(users, MakeRolesFromNames(roleNames));
    }

    /// <summary>
    ///     Выдать роль пользователю.
    /// </summary>
    /// <param name="userName">Имена пользователей.</param>
    /// <param name="role">Роли.</param>
    /// <returns>Статус операции.</returns>
    /// <exception cref="ArgumentNullException">При отсутствии имен пользователей.</exception>
    /// <exception cref="Exception">При недопустимом имени пользователя.</exception>
    public async Task<bool> GrantRoleToUser(IEnumerable<string> userNames, IEnumerable<IdentityRole> roles)
    {
        if (!userNames.Any())
        {
            throw new ArgumentNullException(nameof(userNames));
        }

        var users = await IdentityContext.Users.Where(user => userNames.Contains(user.UserName)).ToListAsync();

        if (users.Count != userNames.Count())
        {
            throw new Exception("Invalid user name!");
        }

        return await GrantRoleToUser(users, roles);
    }

    /// <summary>
    ///     Выдать роль пользователю.
    /// </summary>
    /// <param name="user">Пользователи.</param>
    /// <param name="role">Роли.</param>
    /// <returns>Статус операции.</returns>
    /// <exception cref="ArgumentNullException">При отсутствии ролей.</exception>
    /// <exception cref="Exception">При недопустимом названии роли.</exception>
    public async Task<bool> GrantRoleToUser(IEnumerable<IdentityUser> users, IEnumerable<IdentityRole> roles)
    {
        if (!roles.Any())
        {
            throw new ArgumentNullException(nameof(roles));
        }

        foreach (var role in roles)
        {
            if (string.IsNullOrEmpty(role.Name))
            {
                throw new Exception("Invalid role name!");
            }

            await RoleManager.CreateAsync(role);
        }

        var status = false;

        foreach (var user in users)
        {
            var result = await UserManager.AddToRolesAsync(user, roles.Select(role => role.Name!));

            status = status || result.Succeeded;
        }

        return status;
    }

    /// <summary>
    ///     Удалить роль у пользователя.
    /// </summary>
    /// <param name="userNames">Имена пользователей.</param>
    /// <param name="roleNames">Названия ролей.</param>
    /// <returns>Статус операции.</returns>
    public async Task<bool> TakeRoleFromUser(IEnumerable<string> userNames, IEnumerable<string> roleNames)
    {
        return await TakeRoleFromUser(userNames, MakeRolesFromNames(roleNames));
    }

    /// <summary>
    ///     Удалить роль у пользователя.
    /// </summary>
    /// <param name="users">Пользователи.</param>
    /// <param name="roleNames">Названия ролей.</param>
    /// <returns>Статус операции.</returns>
    public async Task<bool> TakeRoleFromUser(IEnumerable<IdentityUser> users, IEnumerable<string> roleNames)
    {
        return await TakeRoleFromUser(users, MakeRolesFromNames(roleNames));
    }

    /// <summary>
    ///     Удалить роль у пользователя.
    /// </summary>
    /// <param name="userNames">Имена пользователей.</param>
    /// <param name="roles">Роли.</param>
    /// <returns>Статус операции.</returns>
    /// <exception cref="ArgumentNullException">При отсутствии имен пользователей.</exception>
    /// <exception cref="Exception">При недопустимом имени пользователя.</exception>
    public async Task<bool> TakeRoleFromUser(IEnumerable<string> userNames, IEnumerable<IdentityRole> roles)
    {
        if (!userNames.Any())
        {
            throw new ArgumentNullException(nameof(userNames));
        }

        var users = await IdentityContext.Users.Where(user => userNames.Contains(user.UserName)).ToListAsync();

        if (users.Count != userNames.Count())
        {
            throw new Exception("Invalid user name!");
        }

        return await TakeRoleFromUser(users, roles);
    }

    /// <summary>
    ///     Удалить роль у пользователя.
    /// </summary>
    /// <param name="users">Пользователи.</param>
    /// <param name="roles">Роли.</param>
    /// <returns>Статус операции.</returns>
    /// <exception cref="ArgumentNullException">При отсутствии ролей.</exception>
    /// <exception cref="Exception">При недопустимом названии роли.</exception>
    public async Task<bool> TakeRoleFromUser(IEnumerable<IdentityUser> users, IEnumerable<IdentityRole> roles)
    {
        if (!roles.Any())
        {
            throw new ArgumentNullException(nameof(roles));
        }

        if (roles.Any(role => string.IsNullOrEmpty(role.Name)))
        {
            throw new Exception("Invalid role name!");
        }

        var status = false;

        foreach (var user in users)
        {
            var result = await UserManager.RemoveFromRolesAsync(user, roles.Select(role => role.Name!));

            status = status || result.Succeeded;
        }

        return status;
    }

    /// <summary>
    ///     Получить роль пользователя.
    /// </summary>
    /// <param name="users">Пользователь.</param>
    /// <returns>Список ролей.</returns>
    public async Task<List<IdentityRole>> GetUserRoles(IdentityUser user)
    {
        return await GetUserRoles(user.Id);
    }

    /// <summary>
    ///     Получить роли пользователя.
    /// </summary>
    /// <param name="users">Идентификатор пользователя.</param>
    /// <returns>Список ролей.</returns>
    public async Task<List<IdentityRole>> GetUserRoles(string userId)
    {
        return await IdentityContext.UserRoles
            .Where(userRole => userRole.UserId == userId)
            .Join(IdentityContext.Roles,
                userRole => userRole.RoleId,
                innerRole => innerRole.Id,
                (userRole, innerRole) => innerRole)
            .ToListAsync();
    }

    /// <summary>
    ///     Получить пользователей с указанной ролью.
    /// </summary>
    /// <param name="users">Роль.</param>
    /// <returns>Список пользователей.</returns>
    /// <exception cref="Exception">При недопустимом названии роли.</exception>
    public async Task<IList<IdentityUser>> GetRoleUsers(IdentityRole role)
    {
        if (string.IsNullOrEmpty(role.Name))
        {
            throw new Exception("Invalid role name!");
        }

        return await UserManager.GetUsersInRoleAsync(role.Name);
    }

    /// <summary>
    ///     Получить пользователей с указанной ролью.
    /// </summary>
    /// <param name="users">Идентификатор роли.</param>
    /// <returns>Список пользователей.</returns>
    public async Task<IList<IdentityUser>> GetRoleUsers(string roleName)
    {
        return await UserManager.GetUsersInRoleAsync(roleName);
    }

    /// <summary>
    ///     Проверить возможность регистрации с указанными названиями ролей.
    /// </summary>
    /// <param name="roleNames">Имена ролей.</param>
    /// <returns>Статус проверки.</returns>
    public bool CanRegisterWithRoles(List<string> roleNames)
    {
        foreach (var roleName in roleNames)
        {
            if (!IdentityRoleConfiguration.GetRoleNames().Contains(roleName))
            {
                return false;
            }

            if (!ConfigurationManager.IdentityConfiguration.GetRoleCanRegister(roleName))
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    ///     Инициализировать роли из списка названий.
    /// </summary>
    /// <param name="roleNames">Названия ролей.</param>
    /// <returns>Список ролей.</returns>
    private static List<IdentityRole> MakeRolesFromNames(IEnumerable<string> roleNames)
    {
        var roles = new List<IdentityRole>();

        foreach (var roleName in roleNames)
        {
            roles.Add(new IdentityRole
            {
                Name = roleName
            });
        }

        return roles;
    }
}