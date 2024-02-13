namespace TodoApi.Repositories
{
    public interface IUnitofWork
    {
        IUserRepository UserRepository { get; }

        Task CompleteAsync();
    }
}
