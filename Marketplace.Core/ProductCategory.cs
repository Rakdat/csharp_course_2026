namespace Marketplace.Core;

[Flags]
public enum ProductCategory
{
    None = 0,
    Electronics = 1 << 0,
    Clothing = 1 << 1, 
    Food = 1 << 2,    
    Books = 1 << 3
}