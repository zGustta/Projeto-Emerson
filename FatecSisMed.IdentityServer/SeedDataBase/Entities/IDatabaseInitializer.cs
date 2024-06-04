namespace FatecSisMed.IdentityServer.SeedDataBase.Entities
{
    public interface IDatabaseInitializer
    {
        void InitializeSeedRoles();
        void InitializeSeedUsers();
    }
}