using Marques.EFCore.SnakeCase;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Magicord.Extensions
{
  public static class ModelBuilderExtension
  {
    /// <summary>
    /// Remove pluralizing table name convention to create table name in singular form.
    /// </summary>       
    public static void RemovePluralizingTableNameConvention(this ModelBuilder modelBuilder)
    {
      foreach (IMutableEntityType entityType in modelBuilder.Model.GetEntityTypes())
      {
        entityType.SetTableName(entityType.DisplayName().ToSnakeCase());
      }
    }
  }
}