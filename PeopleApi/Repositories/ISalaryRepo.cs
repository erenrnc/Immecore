using Core.Db;

namespace PeopleApi.Repositories
{
    public interface ISalaryRepo
    {
        bool Update(Salary salary);
        bool Delete(int Id);
        List<Salary> List(int peopleId);
        Salary Get(int Id);
        bool Add(Salary salary);
    }
}
