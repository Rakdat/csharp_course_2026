namespace Marketplace.Core;

public class MarketingService
{
    public void ApplySecretDiscount(ShoppingCart cart, decimal amount)
    {
        ArgumentNullException.ThrowIfNull(cart);
        cart.SetDiscount(amount);
    }
}