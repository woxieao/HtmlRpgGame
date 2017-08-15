namespace RpgGame.Model.Item
{
    public interface IItem
    {
        double GetPrice();
        double GetSellPrice();
        bool IsSellAble();
    }
}