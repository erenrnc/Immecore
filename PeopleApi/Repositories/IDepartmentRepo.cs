using Core.Db;

namespace PeopleApi.Repositories
{
    public interface IDepartmentRepo
    {
        bool Update(Department department);
        bool Delete(int Id);
        List<Department> List();
        Department Get(int Id);
        bool Add(Department department);
    }
}
