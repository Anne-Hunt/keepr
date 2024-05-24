
namespace keepr.Repositories;

public class VaultRepository
{
    private readonly IDbConnection _db;
    public VaultRepository(IDbConnection db)
    {
        _db = db;
    }

    internal Vault GetVaultById(int vaultId)
    {
        string sql = @"
        SELECT
        vaults.*,
        accounts.*
        FROM vaults
        JOIN accounts ON accounts.Id = vaults.CreatorId
        WHERE vault.Id = @vaultId;";

        Vault vault = _db.Query<Vault, Profile, Vault>(sql, (vault, profile) =>
        {
            vault.Creator = profile;
            return vault;
        }, new { vaultId }).FirstOrDefault();
        return vault;
    }

    internal List<Vault> GetVaults()
    {
        string sql = @"
        SELECT
        vaults.*,
        accounts.*
        FROM vaults
        JOIN accounts ON accounts.Id = vaults.CreatorId;";

        List<Vault> vaults = _db.Query<Vault, Profile, Vault>(sql, (vault, profile) =>
        {
            vault.Creator = profile;
            return vault;
        }).ToList();
        return vaults;
    }

    internal Vault CreateVault(Vault vaultData, string userId)
    {
        string sql = @"
        INSERT INTO
        vaults(
            name,
            description,
            img,
            isPrivate,
            creatorId
        )VALUES(
            @Name,
            @Description,
            @Img,
            @IsPrivate,
            @CreatorId
        );

        SELECT
        vaults.*,
        accounts.*
        FROM vaults
        JOIN accounts ON accounts.Id = vaults.CreatorId
        WHERE 
        vaults.Id = LAST_INSERT_ID()
        ;";

        Vault vault = _db.Query<Vault, Profile, Vault>(sql, (vault, profile) =>
        {
            vault.Creator = profile;
            return vault;
        }, vaultData).FirstOrDefault();
        return vault;
    }

    internal Vault UpdateVault(Vault vaultData, int vaultId)
    {
        string sql = @"
        UPDATE vaults
        SET
        name = @Name,
        description = @Description,
        img = @Img,
        isPrivate = @IsPrivate;
        
        SELECT
        vaults.*,
        accounts.*
        FROM vaults
        JOIN accounts ON accounts.Id = vaults.CreatorId
        WHERE vaults.Id = vaultId;";

        Vault vault = _db.Query<Vault, Profile, Vault>(sql, (vault, profile) =>
        {
            vault.Creator = profile;
            return vault;
        }, vaultData).FirstOrDefault();
        return vault;
    }

    internal void TrashVault(int vaultId)
    {
        string sql = "DELETE * FROM vaults WHERE vaults.Id = @vaultId;";
        _db.Execute(sql, new { vaultId });
    }
}