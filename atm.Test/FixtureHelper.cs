namespace Atm.Test;
using System.Globalization;

internal class FixtureHelper
{
    public static int CreateIntInRange(int min, int max) => new Fixture().Create<int>() % (max - min + 1) + min;

    public static string CreatePinInput() => CreateIntInRange(10000, 99999).ToString(new CultureInfo("en-US"));
}
