using Marketplace.Core;
using Marketplace.Core.Users;

namespace Marketplace.Core.Tests.Users;

public class UserHierarchyTests
{
    [Test]
    public void Employee_AddOrderAndGetOrders_ReturnsDefensiveCopy()
    {
        var employee = new Employee { Login = "emp", CompanyInn = "7707083893" };
        var order = new Order();
        employee.AddOrder(order);

        var copy = employee.GetOrders();
        copy[0] = new Order();

        Assert.Multiple(() =>
        {
            Assert.That(employee, Is.InstanceOf<User>());
            Assert.That(employee.CompanyInn, Is.EqualTo("7707083893"));
            Assert.That(employee.GetOrders()[0], Is.SameAs(order));
        });
    }

    [Test]
    public void Customer_HasCartAndStoresOrders()
    {
        var customer = new Customer { Login = "cust" };
        var order = new Order();
        customer.AddOrder(order);

        Assert.Multiple(() =>
        {
            Assert.That(customer, Is.InstanceOf<User>());
            Assert.That(customer.Cart, Is.Not.Null);
            Assert.That(customer.GetOrders(), Has.Length.EqualTo(1));
        });
    }

    [Test]
    public void Admin_AddEmployeeAndGetEmployees_StoresEmployees()
    {
        var admin = new Admin { Login = "admin" };
        var employee = new Employee { Login = "emp" };
        admin.AddEmployee(employee);

        Assert.Multiple(() =>
        {
            Assert.That(admin, Is.InstanceOf<User>());
            Assert.That(admin.GetEmployees()[0], Is.SameAs(employee));
        });
    }

    [Test]
    public void Seller_StoresProductsOrdersAndCompanyInn()
    {
        var seller = new Seller { Login = "seller", CompanyInn = "5408123456" };
        var product = new Product("Laptop", 80000m, ProductCategory.Electronics);
        var order = new Order();
        seller.AddProduct(product);
        seller.AddOrder(order);

        Assert.Multiple(() =>
        {
            Assert.That(seller, Is.InstanceOf<User>());
            Assert.That(seller.CompanyInn, Is.EqualTo("5408123456"));
            Assert.That(seller.GetProducts()[0], Is.SameAs(product));
            Assert.That(seller.GetOrders()[0], Is.SameAs(order));
        });
    }
}