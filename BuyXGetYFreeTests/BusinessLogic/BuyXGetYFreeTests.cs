
using BuyXGetYFreeLibrary.BusinessLogic;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace BuyXGetYFreeTests.BusinessLogic;

public class BuyXGetYFreeTests
{

    [Fact]
    public void Test_BoughtQuantityLessThanX()
    {
        ILogger<Messages> logger = new NullLogger<Messages>();
        Messages messages = new(logger);
        int boughtQuantity = 2;
        int x = 3;
        int y = 2;
        var result = messages.CalculateBuyXGetYFree(boughtQuantity, x, y);
        Assert.Equal(0, result.freeQuantity);
        Assert.Equal(2, result.payableQuantity);
    }

    [Fact]
    public void Test_BoughtQuantityEqualToX()
    {
        ILogger<Messages> logger = new NullLogger<Messages>();
        Messages messages = new(logger);
        int boughtQuantity = 3;
        int x = 3;
        int y = 2;
        var result = messages.CalculateBuyXGetYFree(boughtQuantity, x, y);
        Assert.Equal(0, result.freeQuantity);
        Assert.Equal(3, result.payableQuantity);
    }

    [Fact]
    public void Test_BoughtQuantityGreaterThanXAndLessThanXPlusY()
    {
        ILogger<Messages> logger = new NullLogger<Messages>();
        Messages messages = new(logger);
        int boughtQuantity = 4;
        int x = 3;
        int y = 2;
        var result = messages.CalculateBuyXGetYFree(boughtQuantity, x, y);
        Assert.Equal(1, result.freeQuantity);
        Assert.Equal(3, result.payableQuantity);
    }

    [Fact]
    public void Test_BoughtQuantityMultipleOfX()
    {
        ILogger<Messages> logger = new NullLogger<Messages>();
        Messages messages = new(logger);
        int boughtQuantity = 6;
        int x = 3;
        int y = 2;
        var result = messages.CalculateBuyXGetYFree(boughtQuantity, x, y);
        Assert.Equal(2, result.freeQuantity);
        Assert.Equal(4, result.payableQuantity);
    }

    [Fact]
    public void Test_BoughtQuantityNotMultipleOfX()
    {
        ILogger<Messages> logger = new NullLogger<Messages>();
        Messages messages = new(logger);
        int boughtQuantity = 7;
        int x = 3;
        int y = 2;
        var result = messages.CalculateBuyXGetYFree(boughtQuantity, x, y);
        Assert.Equal(2, result.freeQuantity);
        Assert.Equal(5, result.payableQuantity);
    }

    [Fact]
    public void Test_XGreaterThanY()
    {
        ILogger<Messages> logger = new NullLogger<Messages>();
        Messages messages = new(logger);
        int boughtQuantity = 7;
        int x = 4;
        int y = 2;
        var result = messages.CalculateBuyXGetYFree(boughtQuantity, x, y);
        Assert.Equal(2, result.freeQuantity);
        Assert.Equal(5, result.payableQuantity);
    }

    [Fact]
    public void CalculateBuyXGetYFree_XEqualToY_Test()
    {
        ILogger<Messages> logger = new NullLogger<Messages>();
        Messages messages = new(logger);
        int boughtQuantity = 6;
        int x = 3;
        int y = 3;

        (int freeQuantity, int payableQuantity) result = messages.CalculateBuyXGetYFree(boughtQuantity, x, y);

        Assert.Equal(result.freeQuantity, 3);
        Assert.Equal(result.payableQuantity, 3);
    }

    [Fact]
    public void CalculateBuyXGetYFree_YGreaterThanX_Test()
    {
        ILogger<Messages> logger = new NullLogger<Messages>();
        Messages messages = new(logger);
        int boughtQuantity = 8;
        int x = 3;
        int y = 5;

        (int freeQuantity, int payableQuantity) result = messages.CalculateBuyXGetYFree(boughtQuantity, x, y);

        Assert.Equal(result.freeQuantity, 5);
        Assert.Equal(result.payableQuantity, 3);
    }

    [Fact]
    public void Greeting_InEnglish()
    {
        ILogger<Messages> logger = new NullLogger<Messages>();
        Messages messages = new(logger);

        string expected = "Hello, lets build BuyXGetYFree offer!";
        string actual = messages.GetText("greeting", "en");
        Assert.Equal(expected, actual);

    }

    [Fact]
    public void Greeting_InSpanish()
    {
        ILogger<Messages> logger = new NullLogger<Messages>();
        Messages messages = new(logger);

        string expected = "Hola, ¡construyamos la oferta BuyXGetYFree!,";
        string actual = messages.GetText("greeting", "es");
        Assert.Equal(expected, actual);

    }

    [Fact]
    public void Greeting_Invalid()
    {
        ILogger<Messages> logger = new NullLogger<Messages>();
        Messages messages = new(logger);
        Assert.Throws<InvalidOperationException>(
            () => messages.GetText("greeting", "fr")
            );
    }

   
}
