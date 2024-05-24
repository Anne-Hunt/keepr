

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
        accounts.*
        FROM keeps
        JOIN accounts ON accounts.Id = keeps.CreatorId;";

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
}