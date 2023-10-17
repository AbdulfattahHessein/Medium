using AutoMapper;
using Medium.Core.Interfaces.Bases;

namespace Medium.BL.AppServices
{
    public abstract class AppService
    {
        public IUnitOfWork UnitOfWork { get; set; }
        public IMapper Mapper { get; set; }
        public AppService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            UnitOfWork = unitOfWork;
            Mapper = mapper;
        }

        public void Dispose()
        {
            UnitOfWork.Dispose();
        }

    }
}
