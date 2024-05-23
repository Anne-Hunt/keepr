
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
        throw new NotImplementedException();
    }

    internal Keep GetKeepById(int keepId)
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