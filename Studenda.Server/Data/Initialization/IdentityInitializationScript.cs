using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Studenda.Server.Configuration.Repository;
using Studenda.Server.Model.Common;
using ConfigurationManager = Studenda.Server.Configuration.ConfigurationManager;

namespace Studenda.Server.Data.Initialization;

/// <summary>
///     Скрипт инициализации контекста данных.
/// </summary>
/// <param name="configurationManager">Менеджер конфигурации.</param>
/// <param name="identityContext">Контекст данных.</param>
/// <param name="identityContext">Контекст данных идентификации.</param>
/// <param name="userManager">Менеджер работы с пользователями.</param>
/// <param name="roleManager">Менеджер работы с ролями.</param>
class IdentityInitializationScript(
    ConfigurationManager configurationManager,
    DataContext dataContext,
    IdentityContext identityContext,
    UserManager<IdentityUser> userManager,
    RoleManager<IdentityRole> roleManager
) : IInitializationScript
{
    private IdentityConfiguration IdentityConfiguration { get; } = configurationManager.IdentityConfiguration;
    private DataContext DataContext { get; } = dataContext;
    private IdentityContext IdentityContext { get; } = identityContext;
    private UserManager<IdentityUser> UserManager { get; } = userManager;
    private RoleManager<IdentityRole> RoleManager { get; } = roleManager;

    /// <summary>
    ///     Запустить инициализацию контекста данных.
    /// </summary>
    /// <returns>Операция.</returns>
    /// <exception cref="Exception">При ошибке инициализации.</exception>
    public async Task Run()
    {
        if (!await IdentityContext.TryInitializeAsync())
        {
            throw new Exception("Data initialization failed!");
        }

        await CreateDefaultUser();
    }

    /// <summary>
    ///     Создать пользователя по-умолчанию.
    /// </summary>
    /// <returns>Операция.</returns>
    /// <exception cref="Exception">При ошибке создания.</exception>
    private async Task CreateDefaultUser()
    {
        // TODO: Использовать сервис безопасности.

        var defaultUserEmail = IdentityConfiguration.GetDefaultUserEmail();
        var defaultUserPassword = IdentityConfiguration.GetDefaultUserPassword();
        var defaultUserRoles = IdentityConfiguration.GetDefaultUserRoles();

        var identityUser = await IdentityContext.Users
            .FirstOrDefaultAsync(identityUser => identityUser.Email == defaultUserEmail);

        if (identityUser is null)
        {
            var result = await UserManager.CreateAsync(
                new IdentityUser
                {
                    UserName = defaultUserEmail,
                    Email = defaultUserEmail
                },
                defaultUserPassword);

            if (!result.Succeeded)
            {
                throw new Exception("Data initialization failed while creating default user!");
            }

            identityUser = await IdentityContext.Users
                .FirstOrDefaultAsync(identityUser => identityUser.Email == defaultUserEmail);
        }

        if (identityUser is null)
        {
            throw new Exception("Data initialization failed while creating default user!");
        }

        foreach (var roleName in defaultUserRoles)
        {
            await RoleManager.CreateAsync(new IdentityRole
            {
                Name = roleName
            });
        }

        await UserManager.AddToRolesAsync(identityUser, defaultUserRoles);

        if (!await DataContext.Accounts.AnyAsync(account => account.IdentityId == identityUser.Id))
        {
            await DataContext.Accounts.AddAsync(new Account
            {
                IdentityId = identityUser.Id
            });

            await DataContext.SaveChangesAsync();
        }
    }
}