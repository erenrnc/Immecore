using AutoMapper;
using Core.Db;
using Core.Models;
using Core.Result;
using PeopleApi.Repositories;

namespace PeopleApi.Business
{
    public class DepartmentBusiness
    {
        private readonly ILogger<DepartmentBusiness> _logger;
        private readonly IDepartmentRepo _repo;
        private readonly IMapper _mapper;

        public DepartmentBusiness(ILogger<DepartmentBusiness> logger, IDepartmentRepo repo, IMapper mapper)
        {
            _logger = logger;
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<Result<object>> Insert(DepartmentDto dto)
        {
            try
            {
                var res = _repo.Add(_mapper.Map<DepartmentDto, Department>(dto));
                return res;
            }
            catch (Exception ex)
            {
                return new Error($"Method UnKnown Error Code Insert", ex);
            }
        }

        public async Task<Result<DepartmentDto>> GetById(int Id)
        {
            try
            {
                var res = _repo.Get(Id);
                return _mapper.Map<Department, DepartmentDto>(res);
            }
            catch (Exception ex)
            {
                return new Error($"Method UnKnown Error Code GetById", ex);
            }
        }

        public async Task<Result<List<DepartmentDto>>> GetList()
        {
            try
            {
                var res = _repo.List();
                return _mapper.Map<List<Department>, List<DepartmentDto>>(res);

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

        public async Task<Result<bool>> Update(DepartmentDto dto)
        {
            try
            {
                var res = _repo.Update(_mapper.Map<DepartmentDto, Department>(dto));
                return res;
            }
            catch (Exception ex)
            {
                return new Error($"Method UnKnown Error Code Update", ex);
            }
        }
    }
}
