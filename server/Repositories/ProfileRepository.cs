

namespace keepr.Repositories;

public class ProfileRepository
{
    private readonly IDbConnection _db;

    public ProfileRepository(IDbConnection db)
    {
        _db = db;
    }

    internal Profile GetProfileById(string profileId)
    {
        string sql = "SELECT * FROM accounts WHERE accounts.Id = @profileId;";
        Profile profile = _db.Query<Profile>(sql, new { profileId }).FirstOrDefault();
        return profile;
    }
}