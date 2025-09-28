using JobJuggler.Common;
using JobJuggler.Domain.MetaModels.Enums;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure;

namespace JobJuggler.Persistence.Extensions;

public static class DbContextExtensions
{
    public static void MapEnums(this NpgsqlDbContextOptionsBuilder builder)
    {
        // builder.MapEnum<PriceType>("price_type");
        builder.MapEnum<ProductType>(nameof(ProductType).ToSnakeCase(), "job_juggler");
        builder.MapEnum<BillingPeriod>(nameof(BillingPeriod).ToSnakeCase(), "job_juggler");
        
        // return builder;
    }
}