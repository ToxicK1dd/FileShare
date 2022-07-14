using ImageApi.DataAccess.Base.Repository;
using ImageApi.DataAccess.Models.Primary;
using ImageApi.DataAccess.Repository.Primary.Login.Interface;
using Microsoft.EntityFrameworkCore;
using Model = ImageApi.DataAccess.Models.Primary.Login.Login;

namespace ImageApi.DataAccess.Repository.Primary.Login
{
    public class LoginRepository : RepositoryBase<Model, PrimaryContext>, ILoginRepository
    {
        public LoginRepository(PrimaryContext context) : base(context) { }


        public async Task<Model> GetFromUsernameAsync(string username, CancellationToken cancellationToken = default)
        {
            return await context.Set<Model>().FirstOrDefaultAsync(x => x.Username == username, cancellationToken);
        }

        public async Task<bool> ExistsFromUsernameAsync(string username, CancellationToken cancellationToken = default)
        {
            return await context.Set<Model>().Where(x => x.Username == username).Select(x => x.Id).AnyAsync(cancellationToken);
        }
    }
}