// <auto-generated />
namespace DataflowICB.Database.Migrations
{
    using System.CodeDom.Compiler;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Migrations.Infrastructure;
    using System.Resources;
    
    [GeneratedCode("EntityFramework.Migrations", "6.1.0-30225")]
    public sealed partial class AddedIsDeletedFlag : IMigrationMetadata
    {
        private readonly ResourceManager Resources = new ResourceManager(typeof(AddedIsDeletedFlag));
        
        string IMigrationMetadata.Id
        {
            get { return "201711081537083_Added IsDeleted Flag"; }
        }
        
        string IMigrationMetadata.Source
        {
            get { return null; }
        }
        
        string IMigrationMetadata.Target
        {
            get { return Resources.GetString("Target"); }
        }
    }
}
