using Prorent24.Test.ApiTests.ConfigurationsTest.SettingsTest;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Prorent24.Test.ApiTests.ConfigurationsTest
{
    public class SettingApiTest
    {
        private TestServerFixture _fixture = new TestServerFixture();

        [Fact]
        public async Task ProjectType_Test()
        {
            ProjectTypeApiTest apiTest = new ProjectTypeApiTest(_fixture);
            await apiTest.Create();
            await apiTest.Update();
            await apiTest.GetById();
            await apiTest.GetAll();
            await apiTest.Delete();
        }
    }
}
