using Core.Db;
using Dapper;
using System.Data;

namespace PeopleApi.Repositories.Impl
{
    public class PeopleRepo : IPeopleRepo
    {
        private readonly PostgreSqlContext _context;
        private readonly ILogger<PeopleRepo> _logger;

        public PeopleRepo(PostgreSqlContext context, ILogger<PeopleRepo> logger)
        {
            _context = context;
            _logger = logger;
        }

        public bool Update(People info)
        {
            try
            {
                var result = _context.People.Update(info);
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
                var result = _context.People.First(x => x.Id == Id);
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

        public List<People> List()
        {
            try
            {
                var result = _context.People.ToList();
                return result.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public People Get(int Id)
        {
            try
            {
                var result = _context.People.FirstOrDefault(x => x.Id == Id);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public bool Add(People people)
        {
            try
            {
                var result = _context.People.Add(people);
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
