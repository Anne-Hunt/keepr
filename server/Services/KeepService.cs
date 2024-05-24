
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

    internal Keep CreateKeep(Keep keepData, string userId)
    {
        Keep keep = _repository.CreateKeep(keepData, userId);
        return keep;
    }

    internal Keep UpdateKeep(Keep keepdata, int keepId, string userId)
    {
        Keep keepToUpdate = GetKeepById(keepId);
        if (keepToUpdate.CreatorId != userId)
        {
            throw new Exception("You cannot update what is not yours!");
        }
        Keep keep = _repository.UpdateKeep(keepdata, keepId);
        return keep;
    }

    internal string TrashKeep(int keepId, string userId)
    {
        _repository.TrashKeep(keepId);
        return "Deleted Keep";
    }
}