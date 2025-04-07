using FluentAssertions;
using Newtonsoft.Json;
using Prorent24.Enum.General;
using Prorent24.Test.ApiTest.BaseTest;
using Prorent24.WebApp.Models.General.Filter;
using System;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.Test.ApiTests.General
{
    public class FilterApiTest : BaseTest<SavedFilterViewModel>
    {
        public FilterApiTest(TestServerFixture fixture, string crewMemberId) : base(fixture, "generals/filters")
        {
            model = new SavedFilterViewModel()
            {
                FilterGroupType = FilterGroupTypeEnum.FixedValueFor,
                FilterType = FilterTypeEnum.CrewMember,
                ModuleType = ModuleTypeEnum.CrewMembers,
                Text ="Filter",
                Value = new SavedFilterValueViewModel()
                {
                    CrewMemberId = crewMemberId,
                    Date = DateTime.Now,
                    Property = Enum.Directory.PropertyEnum.EqualTo
                }
            };
        }

        public async Task GetListFilters()
        {
            HttpResponseMessage response = await _fixture.Client.GetAsync(String.Concat(_endPoint,"/", ModuleTypeEnum.CrewMembers));
            response.EnsureSuccessStatusCode();
        }

        public async Task SaveFilter()
        {
            HttpResponseMessage response = await _fixture.Client.PostAsync(_endPoint, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            string responseStrong = await response.Content.ReadAsStringAsync();
            model = JsonConvert.DeserializeObject<SavedFilterViewModel>(responseStrong);
            model.Id.Should().BeGreaterThan(0);
            id = model.Id.ToString();
        }

        public async Task DeleteSavedFilter()
        {
            id.Should().NotBeNullOrEmpty();
            var endPoint = string.Concat(_endPoint, "/", id, "/delete/");
            HttpResponseMessage response = await _fixture.Client.PostAsync(endPoint, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            string responseStrong = await response.Content.ReadAsStringAsync();
            bool result = JsonConvert.DeserializeObject<bool>(responseStrong);
            result.Should().Be(true);
        }
    }
}
