using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Migrations.History;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Migrations;
internal class CrystalCleanHistoryContext : HistoryContext
{
    public CrystalCleanHistoryContext(DbConnection existingConnection, string defaultSchema) : base(existingConnection, defaultSchema)
    {
    }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<HistoryRow>().ToTable(tableName: "migration_history", schemaName: "crystal_clean");
    }
}


internal class ModelConfiguration : DbConfiguration
{
    public ModelConfiguration()
    {
        this.SetHistoryContext("System.Data.SqlClient", (connection, defaultSchema) => new CrystalCleanHistoryContext(connection, defaultSchema));
    }
}