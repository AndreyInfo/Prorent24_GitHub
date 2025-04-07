using System.Threading.Tasks;

namespace Prorent24.Test.ApiTest.BaseJsonTest
{
    public interface IBaseJsonTest<T>
    {
        Task Get();
        Task Update();
       
    }
}
