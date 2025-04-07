using FluentAssertions;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Prorent24.Test.ApiTest.BaseJsonTest
{
    public abstract class BaseJsonTest<T>: IBaseJsonTest<T>, IClassFixture<TestServerFixture>
    {
        protected readonly TestServerFixture _fixture;
        public string _endPoint = "/api/";

        #region Params

        public string id { get; set; }
        public T model { get; set; }

        #endregion

        public BaseJsonTest(TestServerFixture fixture, string controllerName)
        {
            _endPoint = string.Concat(_endPoint, controllerName);

            _fixture = fixture;
            _fixture.Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        }

        public async Task Get()
        {
            HttpResponseMessage response = await _fixture.Client.GetAsync(_endPoint);
            response.EnsureSuccessStatusCode();
        }
        

        
        public async Task Update()
        {
            model.Should().NotBeNull();

            HttpResponseMessage response = await _fixture.Client.PostAsync(string.Concat(_endPoint, "/", id), new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            string responseStrong = await response.Content.ReadAsStringAsync();
            T returnModel = JsonConvert.DeserializeObject<T>(responseStrong);
            
        }

       
    }
}
