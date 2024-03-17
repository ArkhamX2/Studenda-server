namespace Studenda.Server.Configuration.Repository;

public class SecurityConfiguration(IConfiguration configuration) : ConfigurationRepository(configuration)
{
    private const string SectionName = "Security";

    public string GetDefaultUserEmail()
    {
        var result = Configuration
            .GetSection(SectionName)
            .GetSection("Default")
            .GetSection("User")
            .GetValue<string>("Email");

        return HandleStringValue(result, "Default user email is null or empty!");
    }

    public string GetDefaultUserPassword()
    {
        var result = Configuration
            .GetSection(SectionName)
            .GetSection("Default")
            .GetSection("User")
            .GetValue<string>("Password");

        return HandleStringValue(result, "Default user password is null or empty!");
    }

    public string GetDefaultAccountName()
    {
        var result = Configuration
            .GetSection(SectionName)
            .GetSection("Default")
            .GetSection("Account")
            .GetValue<string>("Name");

        return HandleStringValue(result, "Default account name is null or empty!");
    }

    public string GetDefaultAccountSurname()
    {
        var result = Configuration
            .GetSection(SectionName)
            .GetSection("Default")
            .GetSection("Account")
            .GetValue<string>("Surname");

        return HandleStringValue(result, "Default account surname is null or empty!");
    }

    public string GetDefaultAccountPatronymic()
    {
        var result = Configuration
            .GetSection(SectionName)
            .GetSection("Default")
            .GetSection("Account")
            .GetValue<string>("Patronymic");

        return HandleStringValue(result, "Default account patronymic is null or empty!");
    }

    public string GetDefaultRoleName()
    {
        var result = Configuration
            .GetSection(SectionName)
            .GetSection("Default")
            .GetSection("Role")
            .GetValue<string>("Name");

        return HandleStringValue(result, "Default role name is null or empty!");
    }

    public string GetDefaultRolePermission()
    {
        var result = Configuration
            .GetSection(SectionName)
            .GetSection("Default")
            .GetSection("Role")
            .GetValue<string>("Permission");

        return HandleStringValue(result, "Default role permission is incorrect!");
    }
}