using Domain.Models;
using Domain.Models.Enums;

namespace Persistence.Seeds;
public class MainSeed
{
    public static async Task SeedData(DataContext context)
    {
        await Task.WhenAll(PaymentMethodSeed(context), LineItemSeed(context));
    }

    /// <summary>
    /// Seeds into the database some predetermined payment methods i.e cash, credit card, debit card, zelle...
    /// </summary>
    /// <param name="context">The postgres database context</param>
    /// <returns></returns>
    public static async Task PaymentMethodSeed(DataContext context)
    {
        if(!context.PaymentMethods.Any())
        {
            var paymentMethods = new List<PaymentMethod>
            {
                new PaymentMethod
                {
                    Name = "Cash",
                    IsActive = true,
                },
                new PaymentMethod
                {
                    Name = "Check",
                    IsActive = true,
                },
                new PaymentMethod
                {
                    Name = "Credit Card",
                    IsActive = true,
                },
                new PaymentMethod
                {
                    Name = "Debit Card",
                    IsActive = true,
                },
                new PaymentMethod
                {
                    Name = "Zelle",
                    IsActive = true,
                },
                new PaymentMethod
                {
                    Name = "Paypal",
                    IsActive = true,
                },
                new PaymentMethod
                {
                    Name = "Venmo",
                    IsActive = true,
                },
                new PaymentMethod
                {
                    Name = "Cash App",
                    IsActive = true,
                }
            };

            await context.PaymentMethods.AddRangeAsync(paymentMethods);
            await context.SaveChangesAsync();
        }
    }

    public static async Task LineItemSeed(DataContext context)
    {
        if(!context.LineItems.Any())
        {
            var lineItems = new List<LineItem> { 
                new LineItem
                {
                    Name = "Living Room",
                    BasePrice = 45,
                    PriceType = PriceType.FlatRate
                },
                new LineItem { 
                    Name = "Room",
                    BasePrice = 25,
                    PriceType= PriceType.PerUnit
                },
                new LineItem {
                    Name = "Hallway",
                    BasePrice = 0,
                    PriceType = PriceType.PerUnit
                },
                new LineItem
                {
                    Name = "Staircase",
                    BasePrice = 25,
                    PriceType = PriceType.PerUnit
                }
            };

            await context.LineItems.AddRangeAsync(lineItems);
            await context.SaveChangesAsync();
        }
    }
}
