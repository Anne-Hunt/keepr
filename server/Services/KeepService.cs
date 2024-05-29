
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
        keepData.CreatorId = userId;
        Keep keep = _repository.CreateKeep(keepData);
        return keep;
    }

    internal Keep UpdateKeep(Keep keepdata, int keepId, string userId)
    {
        Keep keepToUpdate = GetKeepById(keepId);
        if (keepToUpdate.CreatorId != userId)
        {
            throw new Exception("You cannot update what is not yours!");
        }
        keepToUpdate.Id = keepId;
        keepToUpdate.Name = keepdata.Name ?? keepToUpdate.Name;
        keepToUpdate.Description = keepdata.Description ?? keepToUpdate.Description;
        keepToUpdate.Img = keepdata.Img ?? keepToUpdate.Img;

        Keep keep = _repository.UpdateKeep(keepToUpdate);
        return keep;
    }

    internal string TrashKeep(int keepId, string userId)
    {
        Keep keep = GetKeepById(keepId);
        if (keep.CreatorId != userId)
        {
            throw new Exception("You cannot delete what is not yours!");
        }
        _repository.TrashKeep(keepId);
        return "Deleted Keep";
    }

    internal List<KeptVaultKeep> GetKeepsByVaultId(int keepId)
    {
        List<KeptVaultKeep> keeps = _repository.GetKeepsByVaultId(keepId);
        if (keeps == null)
        {
            throw new Exception("Unable to find keeps!");
        }
        return keeps;
    }

    internal Keep GetKeepByVaultKeep(int keepId)
    {
        Keep keep = _repository.GetKeepById(keepId);
        return keep;
    }

    internal List<Keep> GetKeepsByProfile(string profileId)
    {
        List<Keep> keeps = _repository.GetKeepsByProfile(profileId);
        return keeps;
    }

    internal List<Keep> GetMyKeeps(string userId)
    {
        List<Keep> keeps = _repository.GetAccountKeeps(userId);
        return keeps;
    }


    internal Keep IncrementViews(int keepId)
    {
        _repository.IncrementViews(keepId);

        Keep keep = GetKeepById(keepId);

        return keep;
    }
}