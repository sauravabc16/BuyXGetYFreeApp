using BuyXGetYFreeLibrary.BusinessLogic;

namespace BuyXGetYFree;

public class App
{
    private readonly IMessages _messages;

    public App(IMessages messages)
    {
        _messages = messages;
    }

    public void Run(string[] args)
    {
        //setting the default language as "en" if user does not enter it
        string language = "en";

        for (int i = 0; i < args.Length; i++)
        {
            if (args[i].ToLower().StartsWith("lang="))
            {
                language = args[i].Substring(5);
                break;
            }
        }
        string greeting = _messages.GetText("greeting", language);
        
        Console.WriteLine(greeting);
        
        int x = _messages.GetXFromUser(language);
        int y = _messages.GetYFromUser(language);
        int boughtQuantity = _messages.GetBoughtQuantityFromUser(language);
        if(boughtQuantity >= x && boughtQuantity < x + y) 
        {
            boughtQuantity += _messages.HandleAdditionalQuantity(boughtQuantity, x, y, language); 
        }
        (int freeQuantity, int payableQuantity) = _messages.CalculateBuyXGetYFree(boughtQuantity, x, y);

        Console.WriteLine(
            _messages.GetText("outputMessage",language),
            boughtQuantity,
            payableQuantity,
            freeQuantity);
        Console.WriteLine(_messages.GetText("exitMessage", language));
        Console.ReadKey();


    }
}
