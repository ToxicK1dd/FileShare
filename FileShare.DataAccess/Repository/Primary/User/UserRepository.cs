using FileShare.DataAccess.Models.Primary;
using FileShare.DataAccess.Repository.Primary.Account.Interface;
using Microsoft.EntityFrameworkCore;
using Model = FileShare.DataAccess.Models.Primary.User.User;

namespace FileShare.DataAccess.Repository.Primary.Account
{
    public class UserRepository : IUserRepository
    {
        protected DbSet<Model> dbSet;

        public UserRepository(PrimaryContext context)
        {
            dbSet = context.Set<Model>();
        }


        // Create
        public virtual async Task AddAsync(Model model, CancellationToken cancellationToken = default)
        {
            await dbSet.AddAsync(model, cancellationToken);
        }

        // Read
        public virtual async Task<Model> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await dbSet.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        // Update
        public virtual void Update(Model model)
        {
            dbSet.Update(model);
        }

        // Delete
        public virtual void Remove(Model model)
        {
            dbSet.Remove(model);
        }

        // Delete
        public virtual void RemoveById(Guid id)
        {
            var model = dbSet.Where(x => x.Id == id).FirstOrDefault();
            dbSet.Remove(model);
        }

        // Exists
        public virtual async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await dbSet.Where(x => x.Id == id).AsNoTracking().Select(x => x.Id).AnyAsync(cancellationToken);
        }

        public async Task<bool> IsEnabledByUsernameAsync(string username, CancellationToken cancellationToken = default)
        {
            return await dbSet.Where(x => x.UserName == username).Select(x => x.IsEnabled).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<bool> IsVerifiedByUsernameAsync(string username, CancellationToken cancellationToken = default)
        {
            return await dbSet.Where(x => x.UserName == username).Select(x => x.IsVerified).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<Model> GetByUsernameAsync(string username, CancellationToken cancellationToken = default)
        {
            return await dbSet.Where(x => x.UserName == username).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<Guid> GetIdByUsernameAsync(string username, CancellationToken cancellationToken = default)
        {
            return await dbSet.Where(x => x.UserName == username).Select(x => x.Id).FirstOrDefaultAsync(cancellationToken);
        }
    }
}