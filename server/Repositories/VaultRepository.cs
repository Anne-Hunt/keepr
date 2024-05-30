

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
        WHERE vaults.Id = @vaultId;";

        Vault vault = _db.Query<Vault, Profile, Vault>(sql, (vault, profile) =>
        {
            vault.Creator = profile;
            return vault;
        }, new { vaultId }).FirstOrDefault();
        return vault;
    }


    internal Vault GetPrivateVault(int vaultId)
    {
        string sql = @"
        SELECT
        vaults.*,
        accounts.*
        FROM vaults
        JOIN accounts ON accounts.Id = vaults.CreatorId
        WHERE vaults.Id = @vaultId;";

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

    internal Vault CreateVault(Vault vaultData)
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

    internal Vault UpdateVault(Vault vaultData)
    {
        string sql = @"
        UPDATE vaults
        SET
        name = @Name,
        description = @Description,
        img = @Img,
        isPrivate = @IsPrivate
        WHERE id = @Id;
        
        SELECT
        vaults.*,
        accounts.*
        FROM vaults
        JOIN accounts ON accounts.Id = vaults.CreatorId
        WHERE vaults.Id = @Id;";

        Vault vault = _db.Query<Vault, Profile, Vault>(sql, (vault, profile) =>
        {
            vault.Creator = profile;
            return vault;
        }, vaultData).FirstOrDefault();
        return vault;
    }

    internal void TrashVault(int vaultId)
    {
        string sql = "DELETE FROM vaults WHERE vaults.Id = @vaultId;";
        _db.Execute(sql, new { vaultId });
    }

    internal List<Vault> GetMyVaults(string userId)
    {
        string sql = @"
        SELECT 
        vaults.*,
        accounts.*
        FROM vaults
        JOIN accounts ON accounts.Id = vaults.CreatorId
        WHERE vaults.CreatorId = @userId
        ;";

        List<Vault> vaults = _db.Query<Vault, Profile, Vault>(sql, (vault, profile) =>
        {
            vault.Creator = profile;
            return vault;
        }, new { userId }).ToList();
        return vaults;
    }

    internal List<Vault> GetPublicVaults(string profileId)
    {
        string sql = @"
        SELECT 
        vaults.*,
        accounts.*
        FROM vaults
        JOIN accounts ON accounts.Id = vaults.CreatorId
        WHERE vault.CreatorId = @profileId AND NOT vaults.IsPrivate = true;";

        List<Vault> vaults = _db.Query<Vault, Profile, Vault>(sql, (vault, profile) =>
        {
            vault.Creator = profile;
            return vault;
        }, new { profileId }).ToList();
        return vaults;
    }
}