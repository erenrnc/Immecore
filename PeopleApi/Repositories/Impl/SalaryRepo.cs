using Core.Conn;
using Core.Db;
using Core.Models;
using Dapper;
using System.Data;

namespace PeopleApi.Repositories.Impl
{
    public class SalaryRepo : ISalaryRepo
    {
        private readonly PostgreSqlContext _context;
        private readonly ILogger<PeopleRepo> _logger;

        public SalaryRepo(PostgreSqlContext context, ILogger<PeopleRepo> logger)
        {
            _context = context;
            _logger = logger;
        }

        public bool Update(Salary info)
        {
            try
            {
                var result = _context.Salary.Update(info);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        public bool Delete(int Id)
        {
            try
            {
                var result = _context.Salary.First(x => x.Id == Id);
                _context.Remove(result);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        public List<Salary> List(int peopleId)
        {
            try
            {
                var res = _context.Salary.Where(x => x.PeopleId == peopleId).ToList();
                return res;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public Salary Get(int Id)
        {
            try
            {
                return _context.Salary.FirstOrDefault(x => x.Id == Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public bool Add(Salary info)
        {
            try
            {
                var result = _context.Salary.Add(info);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
