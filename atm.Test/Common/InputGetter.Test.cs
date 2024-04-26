namespace Atm.Test.Common;
using Atm.Common;

public class InputGetterTest
{
    [Fact]
    public void GetInputReturnsValidInput()
    {
        var input = new Fixture().Create<string>();
        var fieldName = new Fixture().Create<string>();
        var errorMessage = new Fixture().Create<string>();

        var inputBuffer = new StringReader(input);
        Console.SetIn(inputBuffer);

        _ = new InputGetter().GetInput((i) => i == input, fieldName, errorMessage).Should().Be(input);
    }

    [Fact]
    public void GetInputDoesNotReturnInvalidInput()
    {
        var input = new Fixture().Create<string>();
        var fieldName = new Fixture().Create<string>();
        var errorMessage = new Fixture().Create<string>();

        var inputBuffer = new StringReader(input);
        Console.SetIn(inputBuffer);

        _ = InputGetter.GetInputSingleLoop((i) => i != input, errorMessage).Should().Be(null);
    }
}
