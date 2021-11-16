using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace App.EFCore
{
    public static class ModelBuilderExtensions
    {
        public static void RemovePluralizingTableNameConvention(this ModelBuilder modelBuilder)
        {
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                if (entity.BaseType == null && entity.ClrType.CustomAttributes.All(a => a.AttributeType != typeof(TableAttribute)))
                {
                    entity.SetTableName(entity.GetDefaultTableName());
                }
            }

        }
    }
}