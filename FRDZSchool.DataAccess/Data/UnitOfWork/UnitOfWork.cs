namespace FRDZSchool.DataAccess.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork.IUnitOfWork
    {
        public ApplicationContext Context { get; set; }

        public UnitOfWork(ApplicationContext context)
        {
            Context = context;
        }

        public async Task SaveAsync(CancellationToken token = default)
        {
            await Context.SaveChangesAsync(token);
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
