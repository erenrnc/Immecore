using Core.Db;

namespace PeopleApi.Repositories.Impl
{
    public class DepartmentRepo : IDepartmentRepo
    {
        private readonly PostgreSqlContext _context;
        private readonly ILogger<PeopleRepo> _logger;

        public DepartmentRepo(PostgreSqlContext context, ILogger<PeopleRepo> logger)
        {
            _context = context;
            _logger = logger;
        }

        public bool Update(Department info)
        {
            try
            {
                var result = _context.Department.Update(info);
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
                var result = _context.Department.First(x => x.Id == Id);
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

        public List<Department> List()
        {
            try
            {
                var res = _context.Department.ToList();
                return res;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public Department Get(int Id)
        {
            try
            {
                return _context.Department.FirstOrDefault(x => x.Id == Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public bool Add(Department info)
        {
            try
            {
                var result = _context.Department.Add(info);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }
    }
}
