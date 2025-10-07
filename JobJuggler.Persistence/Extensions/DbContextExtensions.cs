using JobJuggler.Common;
using JobJuggler.Domain.MetaModels.Enums;
using JobJuggler.Persistence.EntityConfigurations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure;

namespace JobJuggler.Persistence.Extensions;

public static class DbContextExtensions
{
    public static void MapEnums(this NpgsqlDbContextOptionsBuilder builder)
    {
        // builder.MapEnum<PriceType>("price_type");
        builder.MapEnum<ProductType>(nameof(ProductType).ToSnakeCase(), nameof(DbSchemas.JobJuggler).ToSnakeCase());
        builder.MapEnum<BillingPeriod>(nameof(BillingPeriod).ToSnakeCase(), nameof(DbSchemas.JobJuggler).ToSnakeCase());
        builder.MapEnum<SubscriptionStatus>(nameof(SubscriptionStatus).ToSnakeCase(), nameof(DbSchemas.JobJuggler).ToSnakeCase());
        
        // return builder;
    }
}