using FluentAssertions;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Prorent24.Test.ApiTest.BaseTest
{
    public abstract class BaseTest<T>: IBaseTest<T>, IClassFixture<TestServerFixture>
    {
        protected readonly TestServerFixture _fixture;
        public string _endPoint = "/api/";

        #region Params

        public string id { get; set; }
        public T model { get; set; }

        #endregion

        public BaseTest(TestServerFixture fixture, string controllerName)
        {
            _endPoint = string.Concat(_endPoint, controllerName);

            _fixture = fixture;
            _fixture.Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        }

        public async Task GetAll()
        {
            HttpResponseMessage response = await _fixture.Client.GetAsync(_endPoint);
            response.EnsureSuccessStatusCode();
        }
        public async Task GetDetails()
        {
            id.Should().NotBeNullOrEmpty();

            HttpResponseMessage response = await _fixture.Client.GetAsync(string.Concat(_endPoint, "/", id, "/details"));
            response.EnsureSuccessStatusCode();
            string responseStrong = await response.Content.ReadAsStringAsync();
            responseStrong.Should().NotBeNull();
        }

        public async Task GetById()
        {
            id.Should().NotBeNullOrEmpty();

            HttpResponseMessage response = await _fixture.Client.GetAsync(string.Concat(_endPoint, "/", id));
            response.EnsureSuccessStatusCode();
            string responseStrong = await response.Content.ReadAsStringAsync();
            responseStrong.Should().NotBeNull();
        }

        public async Task Create()
        {
            model.Should().NotBeNull();

            HttpResponseMessage response = await _fixture.Client.PostAsync(_endPoint, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            string responseStrong = await response.Content.ReadAsStringAsync();
            model = JsonConvert.DeserializeObject<T>(responseStrong);
            PropertyInfo property = model.GetType().GetProperty("Id");
            if (property != null)
            {
                var value = property.GetValue(model);
                id = value.ToString();

                if (property.PropertyType == typeof(string))
                {
                   
                    id.Should().NotBe(Guid.Empty.ToString());
                }
                else
                {
                   Convert.ToInt32(id).Should().BeGreaterThan(0);
                } 
            }
        }

        public async Task Update()
        {
            id.Should().NotBeNullOrEmpty();
            model.Should().NotBeNull();
            var endPoint = string.Concat(_endPoint, "/", id);
            HttpResponseMessage response = await _fixture.Client.PostAsync(endPoint, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            string responseStrong = await response.Content.ReadAsStringAsync();
            T returnModel = JsonConvert.DeserializeObject<T>(responseStrong);

            PropertyInfo property = returnModel.GetType().GetProperty("Id");
            if (property != null)
            {
                var value = property.GetValue(returnModel);
                id = value.ToString();

                if (property.PropertyType == typeof(string))
                {

                    id.Should().NotBe(Guid.Empty.ToString());
                }
                else
                {
                    Convert.ToUInt32(id).Should().BeGreaterThan(0);
                }
            }
        }

        public async Task Delete()
        {
            id.Should().NotBeNullOrEmpty();

            HttpResponseMessage response = await _fixture.Client.PostAsync(string.Concat(_endPoint, "/", id, "/delete"), null);
            response.EnsureSuccessStatusCode();
            string responseStrong = await response.Content.ReadAsStringAsync();
            bool result = JsonConvert.DeserializeObject<bool>(responseStrong);
            result.Should().Be(true);
        }
    }
}
