using ImageApi.DataAccess.Base.Repository;
using ImageApi.DataAccess.Models.Primary;
using ImageApi.DataAccess.Repository.Primary.PersonalDetail.Interface;
using Model = ImageApi.DataAccess.Models.Primary.PersonalDetail.PersonalDetail;

namespace ImageApi.DataAccess.Repository.Primary.PersonalDetail
{
    public class PersonalDetailRepository : RepositoryBase<Model, PrimaryContext>, IPersonalDetailRepository
    {
        public PersonalDetailRepository(PrimaryContext context) : base(context) { }
    }
}