namespace Comabit.BL.Shared
{
    using AutoMapper;

    public interface IAutoMapperManager
    {
        IMapper Mapper { get; }
    }

    public abstract class AutoMapperManager : IAutoMapperManager
    {
        public IMapper Mapper
        {
            get { return ObjectMapper.Mapper; }
        }
    }
}
