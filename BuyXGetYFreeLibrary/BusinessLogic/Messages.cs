
using BuyXGetYFreeLibrary.Models;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace BuyXGetYFreeLibrary.BusinessLogic


{
    public class Messages : IMessages
    {
        private readonly ILogger<Messages> _log;

        public Messages(ILogger<Messages> log)
        {
            _log = log;
        }

        public string GetText(string keyText,string language)
        {
            string output = LookUpCustomText(keyText, language);
            return output;
        }

        private string LookUpCustomText(string key, string language)
        {
            JsonSerializerOptions options = new()
            {
                PropertyNameCaseInsensitive = true
            };



            try
            {
                List<CustomText>? messageSets = JsonSerializer
                .Deserialize<List<CustomText>>
                (
                    File.ReadAllText("CustomText.json"), options
                );

                CustomText? messages = messageSets?.Where(x => x.Language == language).First();


                if (messages is null)
                {
                    throw new NullReferenceException("The specified language was not found in the json");
                }

                return messages.Translations[key];

            }
            catch (Exception ex)
            {
                _log.LogError("Error looking up the custom text", ex);
                throw;
            }
        }

        public int GetXFromUser(string language)
        {
            try
            {
                bool validInput = false;
                int x = 0;
                while (!validInput)
                {
                    string enterX = GetText("enterX", language);
                    Console.WriteLine(enterX);
                    
                    try
                    {
                        x = Convert.ToInt32(Console.ReadLine());
                    }
                    catch (OverflowException)
                    {
                        Console.Error.WriteLine("{0} is outside the range of the Int32 type.", x);
                    }
                    catch (FormatException)
                    {
                        Console.Error.WriteLine("The {0} value '{1}' is not in a recognizable format.",
                                          x.GetType().Name, x);
                    }
                    if (x < 1)
                        Console.Error.WriteLine("Payable quantity cannot be less then 1");
                    else
                        validInput = true;
                }
                return x;

            }
            catch (Exception ex)
            {
                _log.LogError("Error getting payable quantity from the user", ex);
                throw;
            }
        }

        public int GetYFromUser(string language)
        {
            try
            {
                bool validInput = false;
                int boughtQuantity = 0;
                while (!validInput)
                {
                    string enterY = GetText("enterY", language);
                    Console.WriteLine(enterY);

                    try
                    {
                        boughtQuantity = Convert.ToInt32(Console.ReadLine());
                    }
                    catch (OverflowException)
                    {
                        Console.Error.WriteLine("{0} is outside the range of the Int32 type.", boughtQuantity);
                    }
                    catch (FormatException)
                    {
                        Console.Error.WriteLine("The {0} value '{1}' is not in a recognizable format.",
                                          boughtQuantity.GetType().Name, boughtQuantity);
                    }
                    if (boughtQuantity < 0)
                        Console.Error.WriteLine("Free quantity cannot be less then 0");
                    else
                        validInput = true;
                }
                return boughtQuantity;

            }
            catch (Exception ex)
            {
                _log.LogError("Error getting free quantity from the user", ex);
                throw;
            }
        }

        public int GetBoughtQuantityFromUser(string language)
        {
            try
            {
                bool validInput = false;
                int boughtQuantity = 0;
                while (!validInput)
                {
                    string enterY = GetText("enterBoughtQuantity", language);
                    Console.WriteLine(enterY);

                    try
                    {
                        boughtQuantity = Convert.ToInt32(Console.ReadLine());
                    }
                    catch (OverflowException)
                    {
                        Console.Error.WriteLine("{0} is outside the range of the Int32 type.", boughtQuantity);
                    }
                    catch (FormatException)
                    {
                        Console.Error.WriteLine("The {0} value '{1}' is not in a recognizable format.",
                                          boughtQuantity.GetType().Name, boughtQuantity);
                    }
                    if (boughtQuantity < 0)
                        Console.Error.WriteLine("Bought quantity cannot be less then 0");
                    else
                        validInput = true;
                }
                return boughtQuantity;

            }
            catch (Exception ex)
            {
                _log.LogError("Error getting bought quantity from the user", ex);
                throw;
            }
        }

        public (int freeQuantity, int payableQuantity) CalculateBuyXGetYFree(int boughtQuantity, int x, int y)
        {
            try
            {
                if (boughtQuantity < x)
                    return (0, boughtQuantity);
                else if (boughtQuantity == x)
                    return (0, x);
                else if(x < boughtQuantity && boughtQuantity  < x+y)
                    return (boughtQuantity-x, x);

                int remainingQuantity = boughtQuantity;
                int payableQuantity = 0;
                int freeQuantity = 0;
                bool addToFreeQuantity = false;
                while(remainingQuantity >= x)
                {
                    payableQuantity += x;
                    remainingQuantity -= x;
                    if (remainingQuantity > y)
                    {
                        freeQuantity += y;
                        remainingQuantity -= y;
                    }
                    else
                    {
                        addToFreeQuantity= true;
                        break;
                    }
                        
                }
                if (addToFreeQuantity)
                    freeQuantity += remainingQuantity;
                else
                    payableQuantity += remainingQuantity;
                
                return (freeQuantity, payableQuantity);
                

            }
            catch (Exception ex)
            {
                _log.LogError("Error calculating the free and payable quantity", ex);
                throw;
            }
            
        }

        public int HandleAdditionalQuantity(int boughtQuantity, int x, int y, string language)
        {
            try
            {
                int additionalFreeQuantity = CalculateAdditionalFreeQuantity(boughtQuantity, x, y);
                
                bool validInput = false;
                char userInput = '\0';
                while (!validInput)
                {
                    Console.WriteLine(GetText("additionalQuantity", language), x, y, additionalFreeQuantity);
                    userInput = char.ToLower(Console.ReadKey().KeyChar);
                    if (userInput != 'n' && userInput != 'y')
                        Console.WriteLine(GetText("incorrectInput", language));
                    else
                        validInput = true;
                }
                if (userInput == 'y')
                    return additionalFreeQuantity;
                else
                    return 0;

            }
            catch (Exception ex)
            {
                _log.LogError("Error handling additional free quantity", ex);
                throw;
            }
            

        }

        private int CalculateAdditionalFreeQuantity(int boughtQuantity, int x, int y)
        {
            try
            {
                return (x+y) - boughtQuantity;
            }
            catch (Exception ex)
            {
                _log.LogError("Error calculating the additional free quantity", ex);
                throw;
            }
            
        }
    }
}
