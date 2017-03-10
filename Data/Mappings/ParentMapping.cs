using Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Mappings
{
    public class ParentMapping : EntityTypeConfiguration<Parent>
    {
        //TODO: add mappings for other classes
        public ParentMapping()
        {
            ToTable("Parents");
            HasKey(x => x.Id);
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).HasColumnName("Id");
            Property(x => x.LastName).IsRequired().HasMaxLength(250);
        }
    }
}
