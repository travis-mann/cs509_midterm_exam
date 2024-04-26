namespace Atm.Test;
using System.Globalization;

internal class FixtureHelper
{
    public static int CreateIntInRange(int min, int max) => (new Fixture().Create<int>() % (max - min + 1)) + min;

    public static string CreatePinInput() => CreateIntInRange(10000, 99999).ToString(new CultureInfo("en-US"));

    public static string CreateStatusInput()
    {
        var selector = CreateIntInRange(1, 2);
        if (selector == 1)
        {
            return "active";
        }
        else
        {
            return "disabled";
        }
    }

    public static string CreateRoleInput()
    {
        var selector = CreateIntInRange(1, 2);
        if (selector == 1)
        {
            return "admin";
        }
        else
        {
            return "customer";
        }
    }

    public static string CreateNameInput() => new(new Fixture().Create<string>().Where(char.IsLetter).ToArray());

    public static string CreateLoginInput()
    {
        var randomString = new Fixture().Create<string>();
        var randomStringLetters = new Fixture().Create<string>().Where(char.IsLetter).ToArray();
        var randomStringNumbers = new Fixture().Create<string>().Where(char.IsLetter).ToArray();
        return new string(randomStringLetters.Concat(randomStringNumbers).ToArray());
    }
}
