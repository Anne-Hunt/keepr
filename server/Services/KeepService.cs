
namespace keepr.Services;

public class KeepService
{
    private readonly KeepRepository _repository;

    public KeepService(KeepRepository repository)
    {
        _repository = repository;
    }

    internal List<Keep> GetKeeps()
    {
        List<Keep> keeps = _repository.GetKeeps();
        return keeps;
    }

    internal Keep GetKeepById(int keepId)
    {
        Keep keep = _repository.GetKeepById(keepId);
        if (keep == null)
        {
            throw new Exception("Unable to find keep!");
        }
        return keep;
    }

    internal Keep CreateKeep(Keep keepData, string id)
    {
        throw new NotImplementedException();
    }

    internal Keep UpdateKeep(int keepId, string userId)
    {
        throw new NotImplementedException();
    }

    internal string TrashKeep(int keepId, string userId)
    {
        _repository.TrashKeep(keepId);
        return "Deleted Keep";
    }
}