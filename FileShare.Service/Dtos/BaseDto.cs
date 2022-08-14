using FileShare.DataAccess.Base.Model.Entity;
using Mapster;

namespace FileShare.Service.Dtos
{
    // https://medium.com/@M-S-2/enjoy-using-mapster-in-net-6-2d3f287a0989
    public abstract class BaseDto<TDto, TModel> : IRegister
           where TDto : class, new()
           where TModel : BaseEntity
    {
        public TModel ToEntity()
        {
            return this.Adapt<TModel>();
        }

        public TModel ToEntity(TModel entity)
        {
            return (this as TDto).Adapt(entity);
        }

        public static TDto FromEntity(TModel entity)
        {
            return entity.Adapt<TDto>();
        }


        private TypeAdapterConfig Config { get; set; }

        public virtual void AddCustomMappings() { }


        protected TypeAdapterSetter<TDto, TModel> SetCustomMappings()
            => Config.ForType<TDto, TModel>();

        protected TypeAdapterSetter<TModel, TDto> SetCustomMappingsInverse()
            => Config.ForType<TModel, TDto>();

        public void Register(TypeAdapterConfig config)
        {
            Config = config;
            AddCustomMappings();
        }
    }
}