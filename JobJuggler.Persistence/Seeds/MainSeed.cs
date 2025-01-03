﻿using JobJuggler.Domain.Models;
using JobJuggler.Domain.Enums;
using JobJuggler.Domain.IdentityModels;
using Microsoft.EntityFrameworkCore;

namespace JobJuggler.Persistence.Seeds;
public class MainSeed {
    public static async Task SeedData(DataContext context)
    {
        var user = await context.Users.SingleOrDefaultAsync(u => u.UserName == "valeriykutsar18@gmail.com");
        await Task.WhenAll(
            PaymentMethodSeed(context),
            LineItemSeed(context),
            ClientSeed(context, user),
            LocationSeed(context)
        );

        await context.SaveChangesAsync();
    }

    /// <summary>
    /// Seeds into the database some predetermined payment methods i.e cash, credit card, debit card, zelle...
    /// </summary>
    /// <param name="context">The postgres database context</param>
    /// <returns></returns>
    public static Task PaymentMethodSeed(DataContext context) {
        if (!context.PaymentMethods.Any()) {
            var paymentMethods = new List<PaymentMethod>
            {
                new ()
                {
                    Name = "Cash",
                    IsActive = true,
                },
                new ()
                {
                    Name = "Check",
                    IsActive = true,
                },
                new ()
                {
                    Name = "Credit Card",
                    IsActive = true,
                },
                new ()
                {
                    Name = "Debit Card",
                    IsActive = true,
                },
                new ()
                {
                    Name = "Zelle",
                    IsActive = true,
                },
                new ()
                {
                    Name = "Paypal",
                    IsActive = true,
                },
                new ()
                {
                    Name = "Venmo",
                    IsActive = true,
                },
                new ()
                {
                    Name = "Cash App",
                    IsActive = true,
                }
            };

            return context.PaymentMethods.AddRangeAsync(paymentMethods);
        }
        return Task.CompletedTask;
    }

    public static Task LineItemSeed(DataContext context) {
        if (!context.LineItems.Any()) {
            var lineItems = new List<LineItem> {
                new ()
                {
                    Name = "Living Room",
                    BasePrice = 45,
                    PriceType = PriceType.FlatRate
                },
                new () {
                    Name = "Room",
                    BasePrice = 25,
                    PriceType= PriceType.PerUnit
                },
                new () {
                    Name = "Hallway",
                    BasePrice = 0,
                    PriceType = PriceType.PerUnit
                },
                new ()
                {
                    Name = "Staircase",
                    BasePrice = 25,
                    PriceType = PriceType.PerUnit
                }
            };

            return context.LineItems.AddRangeAsync(lineItems);
        }
        return Task.CompletedTask;
    }

    public static Task ClientSeed(DataContext context, AppUser user) {
        if (!context.Clients.Any()) {
            var clients = new List<Client>
            {
                new ()
                {
                    Name = "Valeriy Kutsar",
                    Phone = "916-519-8858",
                    Email = "valeriykutsar@gmail.com",
                    DateCreated = DateTime.UtcNow,
                    CreatedById = user.Id
                },
                new ()
                {
                    Name = "Vick Kuzmenko",
                    Phone = "951-514-5236",
                    Email = "vkuzmenko@rcglogistics.com",
                    DateCreated = DateTime.UtcNow,
                    CreatedById = user.Id
                },
                new ()
                {
                    Name = "Barack Obama",
                    Phone = "652-123-1523",
                    DateCreated = DateTime.UtcNow,
                    CreatedById = user.Id
                },
            };

            return context.Clients.AddRangeAsync(clients);
        }
        return Task.CompletedTask;
    }

    public static Task LocationSeed(DataContext context) {
        if (!context.Locations.Any()) {
            var locations = new List<Location>
            {
                new ()
                {
                    Name = "My House",
                    LocationType = "Residence",
                    Street1 = "3316 Tualatin Way",
                    City = "Rancho Cordova",
                    State = "CA",
                    PostalCode = "95670",
                    Country = "USA",
                    Latitude = (decimal)38.582989,
                    Longitude = (decimal)-121.268051,
                    Notes = "My House"
                },
                new ()
                {
                    Name = "RCG Office",
                    LocationType = "Office",
                    Street1 = "9300 Tech Center Dr",
                    City = "Sacramento",
                    State = "CA",
                    PostalCode = "95826",
                    Country = "USA",
                    Latitude = (decimal)38.562080,
                    Longitude = (decimal)-121.356071,
                    Notes = "My House"
                },
                new ()
                {
                    LocationType = "Residence",
                    Street1 = "9587 Harbour Bay Pl",
                    City = "Elk Grove",
                    State = "CA",
                    PostalCode = "95758",
                    Country = "USA",
                    Latitude = (decimal)38.409409,
                    Longitude = (decimal)-121.471184,
                    Notes = "My House"
                }
            };

            return context.Locations.AddRangeAsync(locations);
        }
        return Task.CompletedTask;
    }
}
