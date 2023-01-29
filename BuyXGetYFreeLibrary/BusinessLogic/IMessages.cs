namespace BuyXGetYFreeLibrary.BusinessLogic
{
    public interface IMessages
    {
        
        string GetText(string keyText, string language);
        int GetXFromUser(string language);
        int GetYFromUser(string language);
        int GetBoughtQuantityFromUser(string language);
        (int freeQuantity, int payableQuantity) CalculateBuyXGetYFree(int boughtQuantity, int x, int y);
        int HandleAdditionalQuantity(int boughtQuantity, int x, int y, string language);
    }
}