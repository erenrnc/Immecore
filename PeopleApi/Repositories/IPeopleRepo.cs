using Core.Db;

namespace PeopleApi.Repositories
{
    public interface IPeopleRepo
    {
        bool Update(People people);
        bool Delete(int Id);
        List<People> List();
        People Get(int Id);
        bool Add(People people);
    }
}
