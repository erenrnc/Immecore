using AutoMapper;
using Core.Db;
using Core.Models;
using Core.Result;
using PeopleApi.Repositories;

namespace PeopleApi.Business
{
    public class SalaryBusiness
    {
        private readonly ILogger<SalaryBusiness> _logger;
        private readonly ISalaryRepo _repo;
        private readonly IMapper _mapper;

        public SalaryBusiness(ILogger<SalaryBusiness> logger, ISalaryRepo repo, IMapper mapper)
        {
            _logger = logger;
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<Result<object>> Insert(SalaryDto dto)
        {
            try
            {
                var res =  _repo.Add(_mapper.Map<SalaryDto, Salary>(dto));
                return res;
            }
            catch (Exception ex)
            {
                return new Error($"Method UnKnown Error Code Insert", ex);
            }
        }

        public async Task<Result<SalaryDto>> GetById(int Id)
        {
            try
            {
                var res =  _repo.Get(Id);
                return _mapper.Map<Salary, SalaryDto>(res);
            }
            catch (Exception ex)
            {
                return new Error($"Method UnKnown Error Code GetById", ex);
            }
        }

        public async Task<Result<List<SalaryDto>>> GetList(int peopleId)
        {
            try
            {
                var res =  _repo.List(peopleId);
                return _mapper.Map<List<Salary>, List<SalaryDto>>(res);

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
                var res =  _repo.Delete(Id);
                return res;
            }
            catch (Exception ex)
            {
                return new Error($"Method UnKnown Error Code Delete", ex);
            }
        }

        public async Task<Result<bool>> Update(SalaryDto dto)
        {
            try
            {
                var res =  _repo.Update(_mapper.Map<SalaryDto, Salary>(dto));
                return res;
            }
            catch (Exception ex)
            {
                return new Error($"Method UnKnown Error Code Update", ex);
            }
        }
    }
}
