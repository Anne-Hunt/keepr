
namespace keepr.Repositories;

public class KeepRepository
{
    private readonly IDbConnection _db;
    public KeepRepository(IDbConnection db)
    {
        _db = db;
    }

    internal Keep GetKeepById(int keepId)
    {
        string sql = @"
        SELECT 
        keeps.*,
        accounts.*
        FROM keeps
        JOIN accounts ON accounts.Id = keeps.CreatorId
        WHERE keeps.Id = @keepId;
        ";

        Keep keep = _db.Query<Keep, Profile, Keep>(sql, (keep, profile) =>
        {
            keep.Creator = profile;
            return keep;
        }, new { keepId }).FirstOrDefault();
        return keep;
    }

    internal List<Keep> GetKeeps()
    {
        string sql = @"
        SELECT
        keeps.*,
        COUNT(vaultkeeps.id) AS kept,
        accounts.*
        FROM keeps
        JOIN accounts ON accounts.Id = keeps.CreatorId
        LEFT JOIN vaultkeeps on vaultkeeps.keepId = keeps.Id
        GROUP BY (keeps.Id)
        ORDER BY keeps.createdAt;";

        List<Keep> keeps = _db.Query<Keep, Profile, Keep>(sql, (keep, profile) =>
        {
            keep.Creator = profile;
            return keep;
        }).ToList();
        return keeps;
    }

    internal Keep CreateKeep(Keep keepData)
    {
        string sql = @"
        INSERT INTO keeps(
            name,
            description,
            img,
            views,
            creatorId)
                VALUES(
                    @Name,
                    @Description,
                    @Img,
                    @Views,
                    @CreatorId
                );
        SELECT
        keeps.*,
        accounts.*
        FROM keeps
        JOIN accounts ON accounts.Id = keeps.CreatorId
        WHERE keeps.Id = LAST_INSERT_ID();";

        Keep keep = _db.Query<Keep, Profile, Keep>(sql, (keep, profile) =>
        {
            keep.Creator = profile;
            return keep;
        }, keepData).FirstOrDefault();
        return keep;
    }

    internal Keep UpdateKeep(Keep keepdata)
    {
        string sql = @"
        UPDATE keeps
        SET
        name = @Name,
        description = @Description,
        img = @Img,
        views = @Views
        WHERE keeps.Id = @Id;

        SELECT
        keeps.*,
        accounts.*
        FROM keeps
        JOIN accounts ON accounts.Id = keeps.CreatorId
        WHERE keeps.Id = @Id;";

        Keep keep = _db.Query<Keep, Profile, Keep>(sql, (keep, profile) =>
        {
            keep.Creator = profile;
            return keep;
        }, keepdata).FirstOrDefault();
        return keep;
    }

    internal void TrashKeep(int keepId)
    {
        string sql = "DELETE FROM keeps WHERE keeps.Id = @keepId;";
        _db.Execute(sql, new { keepId });
    }

    internal List<KeptVaultKeep> GetKeepsByVaultId(int vaultId)
    {
        string sql = @"
        SELECT
        keeps.*,
        accounts.*,
        vaultkeeps.*
        FROM keeps
        JOIN accounts ON keeps.CreatorId = accounts.Id
        JOIN vaultkeeps ON keeps.Id = vaultkeeps.KeepId
        WHERE vaultkeeps.VaultId = @vaultId;";

        List<KeptVaultKeep> kept = _db.Query<KeptVaultKeep, Profile, VaultKeep, KeptVaultKeep>(sql, (keptvault, profile, vaultkeep) =>
        {
            keptvault.Creator = profile;
            keptvault.VaultKeepId = vaultkeep.Id;
            keptvault.CreatorId = vaultkeep.CreatorId;
            keptvault.VaultKeep = vaultkeep;
            return keptvault;
        }, new { vaultId }).ToList();
        return kept;
    }

    internal List<Keep> GetKeepsByProfile(string profileId)
    {
        string sql = @"
        SELECT
        keeps.*,
        accounts.*
        FROM keeps
        JOIN accounts ON keeps.CreatorId = accounts.Id
        WHERE keeps.CreatorId = @profileId;";

        List<Keep> keeps = _db.Query<Keep, Profile, Keep>(sql, (keep, profile) =>
        {
            keep.Creator = profile;
            return keep;
        }, new { profileId }).ToList();
        return keeps;
    }

    internal List<Keep> GetAccountKeeps(string userId)
    {
        string sql = @"
        SELECT
        keeps.*,
        accounts.*
        FROM keeps
        JOIN accounts ON keeps.CreatorId = accounts.Id
        WHERE keeps.CreatorId = @userId;";

        List<Keep> keeps = _db.Query<Keep, Profile, Keep>(sql, (keep, profile) =>
        {
            keep.Creator = profile;
            return keep;
        }, new { userId }).ToList();
        return keeps;
    }

    internal void IncrementViews(int keepId)
    {
        string sql = @"
      UPDATE keeps
      SET views = views + 1
      WHERE id = @keepId
      LIMIT 1;";

        _db.Execute(sql, new { keepId });
    }
}