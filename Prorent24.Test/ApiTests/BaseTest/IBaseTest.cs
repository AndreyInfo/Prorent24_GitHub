using System.Threading.Tasks;

namespace Prorent24.Test.ApiTest.BaseTest
{
    public interface IBaseTest<T>
    {
        Task GetAll();
        Task GetById();
        Task Create();
        Task Update();
        Task Delete();
    }
}
