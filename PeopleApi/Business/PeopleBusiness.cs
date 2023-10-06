using AutoMapper;
using Core.Db;
using Core.Models;
using Core.Result;
using PeopleApi.Repositories;

namespace PeopleApi.Business
{
    public class PeopleBusiness
    {
        private readonly ILogger<PeopleBusiness> _logger;
        private readonly IPeopleRepo _repo;
        private readonly IMapper _mapper;
        private readonly IDepartmentRepo _repoDepartment;

        public PeopleBusiness(ILogger<PeopleBusiness> logger, IPeopleRepo repo, IMapper mapper, IDepartmentRepo repoDepartment)
        {
            _logger = logger;
            _repo = repo;
            _mapper = mapper;
            _repoDepartment = repoDepartment;
        }

        public async Task<Result<object>> Insert(PeopleDto dto)
        {
            try
            {
                var res = _repo.Add(_mapper.Map<PeopleDto, People>(dto));
                return res;
            }
            catch (Exception ex)
            {
                return new Error($"Method UnKnown Error Code Insert", ex);
            }
        }

        public async Task<Result<PeopleDto>> GetById(int Id)
        {
            try
            {
                var res = _repo.Get(Id);
                return _mapper.Map<People, PeopleDto>(res);
            }
            catch (Exception ex)
            {
                return new Error($"Method UnKnown Error Code GetById", ex);
            }
        }

        public async Task<Result<List<PeopleDto>>> GetList()
        {
            try
            {
                var res = _mapper.Map<List<People>, List<PeopleDto>>(_repo.List());
                foreach (var x in res)
                {
                    x.Department = new DepartmentDto { Id = x.DepartmentId, Name = _repoDepartment.Get(x.DepartmentId).Name };
                }
                return res;
            }
            catch (Exception ex)
            {
                return new Error($"Method UnKnown Error Code List", ex);
            }
        }

        public async Task<Result<bool>> Delete(int Id)
        {
            try
            {
                var res = _repo.Delete(Id);
                return res;
            }
            catch (Exception ex)
            {
                return new Error($"Method UnKnown Error Code Delete", ex);
            }
        }

        public async Task<Result<bool>> Update(PeopleDto dto)
        {
            try
            {
                var res = _repo.Update(_mapper.Map<PeopleDto, People>(dto));
                return res;
            }
            catch (Exception ex)
            {
                return new Error($"Method UnKnown Error Code Update", ex);
            }
        }
    }
}
