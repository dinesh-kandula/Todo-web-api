namespace TodoModels.Services
{
    public interface IUnitofWork
    {
        IUserRepository UserRepository { get; }

        ICredentials Credentials { get; }

        ITodoRepository TodoRepository { get; }

        Task CompleteAsync();
    }
}
