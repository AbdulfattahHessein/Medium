using AutoMapper;
using Medium.BL.Interfaces.Services;
using Medium.BL.ResponseHandler;
using Medium.Core.Entities;
using Medium.Core.Interfaces.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Medium.BL.ResponseHandler.ApiResponseHandler;
namespace Medium.BL.AppServices
{
    public class TopicsService : AppService, ITopicsService
    {
        public TopicsService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
        public async Task<ApiResponse<CreateTopicResponse>> CreateAsync(CreateTopicRequest requset)
        {
            Topic Topic = new Topic() { Name = requset.Name };
            await UnitOfWork.Topics.InsertAsync(Topic);
            await UnitOfWork.CommitAsync();

            var response = Mapper.Map<CreateTopicResponse>(Topic);

            return Success(response);

        }

        public async Task<ApiResponse<DeleteTopicResponse>> DeleteAsync(DeleteTopicRequest requset)
        {
            var Topic = await UnitOfWork.Topics.GetByIdAsync(requset.Id);
            if (Topic == null)
            {
                return NotFound<DeleteTopicResponse>();

            }

            UnitOfWork.Topics.Delete(Topic);
            await UnitOfWork.CommitAsync();

            var response = Mapper.Map<DeleteTopicResponse>(Topic);
            return Success(response);

        }

        public async Task<ApiResponse<GetTopicByIdResponse>> GetById(GetTopicByIdRequest requset)
        {
            var Topic = await UnitOfWork.Topics.GetByIdAsync(requset.Id);
            if (Topic == null)
            {
                return NotFound<GetTopicByIdResponse>();
            }

            var response = Mapper.Map<GetTopicByIdResponse>(Topic);
            return Success(response);
        }

        public async Task<ApiResponse<UpdateTopicResponse>> UpdateAsync(UpdateTopicRequest requset)
        {
            var Topic = await UnitOfWork.Topics.GetByIdAsync(requset.Id);
            if (Topic == null)
            {
                return NotFound<UpdateTopicResponse>();
            }
            Mapper.Map(requset, Topic);
            UnitOfWork.Topics.Update(Topic);
            await UnitOfWork.CommitAsync();
            var response = Mapper.Map<UpdateTopicResponse>(Topic);
            return Success(response);
        }
    }
}
