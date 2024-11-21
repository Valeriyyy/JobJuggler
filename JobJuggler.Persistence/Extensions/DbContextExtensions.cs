using JobJuggler.Domain.Enums;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure;

namespace JobJuggler.Persistence.Extensions;

public static class DbContextExtensions
{
    public static void MapEnums(this NpgsqlDbContextOptionsBuilder builder)
    {
        builder.MapEnum<PriceType>("price_type");
        
        // return builder;
    }
}