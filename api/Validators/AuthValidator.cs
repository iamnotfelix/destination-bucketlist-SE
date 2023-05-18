using System.Text.RegularExpressions;
using api.Models;

namespace api.Validators;

public class AuthValidator
{
    public string ValidateUser(User user)
    {
        var usernameErrors = this.ValidateUserName(user.Username);

        return usernameErrors;
    }
    
    private string ValidateUserName(string username)
    {
        var errors = string.Empty;
        
        if (username.Length < 3)
        {
            errors += "Username too short\n";
        }
        else if (username.Length > 20)
        {
            errors += "Username too long\n";
        }

        if (!Regex.IsMatch(username, "^[a-zA-Z0-9_]+$"))
        {
            errors += "Username can only contain alphanumeric characters and '_'\n";
        }
        
        return errors;
    }
}