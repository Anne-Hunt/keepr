


namespace keepr.Repositories;

public class VaultKeepRepository
{
    private readonly IDbConnection _db;
    public VaultKeepRepository(IDbConnection db)
    {
        _db = db;
    }

    internal VaultKeep GetVaultKeepById(int vaultkeepId)
    {
        string sql = @"
        SELECT
        *
        FROM vaultkeeps
        WHERE vaultkeeps.Id = @vaultkeepId;";

        VaultKeep vaultkeep = _db.Query<VaultKeep>(sql, new { vaultkeepId }).FirstOrDefault();
        return vaultkeep;
    }

    internal List<VaultKeep> GetVaultKeeps(string userId)
    {
        string sql = @"
        SELECT
        * 
        FROM vaultkeeps
        WHERE vaultkeeps.CreatorId = @userId;";

        List<VaultKeep> vaultkeeps = _db.Query<VaultKeep>(sql, new { userId }).ToList();
        return vaultkeeps;
    }

    internal VaultKeep CreateVaultKeep(VaultKeep vaultkeepData)
    {
        string sql = @"
        INSERT INTO
        vaultkeeps(
            keepId,
            vaultId,
            creatorId
            )VALUES(
                @KeepId,
                @VaultId,
                @CreatorId
        );
        SELECT
        *
        FROM vaultkeeps
        WHERE vaultkeeps.Id = LAST_INSERT_ID();";

        VaultKeep vaultkeep = _db.Query<VaultKeep>(sql, (vaultkeepData)).FirstOrDefault();
        return vaultkeep;
    }

    internal void TrashVaultKeep(int vaultkeepId)
    {
        string sql = "DELETE FROM vaultkeeps WHERE vaultkeeps.Id = @vaultkeepId;";
        _db.Execute(sql, new { vaultkeepId });
    }

    internal List<VaultKeep> GetVaultKeepsByVault(int vaultId)
    {
        string sql = @"
        SELECT *
        FROM vaultkeeps
        WHERE vaultkeeps.VaultId = @vaultId;";

        List<VaultKeep> vaultkeeps = _db.Query<VaultKeep>(sql, new { vaultId }).ToList();
        return vaultkeeps;
    }
}