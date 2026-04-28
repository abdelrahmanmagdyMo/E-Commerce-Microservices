namespace Basket.Core.Entities
{
    public class ShoppingCart
    {
        public string UserName { get; set; }
        public List<ShoppingCartItem> Items { get; set; } = new();
#pragma warning disable CS8618
        public ShoppingCart() { }
        public ShoppingCart(string userName)
        {
            UserName = userName;
        }
    }
}
