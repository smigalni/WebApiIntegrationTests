using API.Controllers;
using DepositSettlements.IntegrationTests.Extensions;
using DepositSettlements.IntegrationTests.Setup;
using Shouldly;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace MyIntegrationTests.Controllers
{
    [Collection(Constants.TEST_COLLECTION)]
    public sealed class WeatherForecastControllerTests : IDisposable
    {
        public TestServerFixture TestServerFixture { get; private set; }
        public ITestOutputHelper Output { get { return TestServerFixture.Output; } }

        public WeatherForecastControllerTests(ITestOutputHelper output)
        {
            TestServerFixture = new TestServerFixture(output);
        }

        [Fact]
        public async Task Get_Entities()
        {
            var request = "/WeatherForecast";
            var response = await TestServerFixture
                .Client
                .GetAsync(TestServerFixture.Client.BuildPath(request, Output));

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Post_Entity()
        {
            var request = "/WeatherForecast";

            var dto = new CreateCommand() { Id = 1, Name = "My Name" };

            var response = await TestServerFixture
                .Client
                .PostAsync(TestServerFixture.Client.BuildPath(request, Output),
                dto.ToStringContent());

            response.EnsureSuccessStatusCode();

            var entity = await response.ConvertToAsync<CreateCommand>(Output);
            entity.ShouldNotBe(null);
            entity.Id.ShouldBe(1);
            entity.Name.ShouldBe("My Name");
        }     

      
        [Fact]
        public async Task TryingPostWithFile()
        {
            var path = "TestFiles\\collection.json";

            using var form = new MultipartFormDataContent();
            using var fileContent = new ByteArrayContent(await File.ReadAllBytesAsync(path));

            fileContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var request = "/WeatherForecast/ReadFileTest";

            var response = await TestServerFixture
               .Client
               .PostAsync(TestServerFixture.Client.BuildPath(request, Output), form);

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task TryingPostWithFile2()
        {
            var path = "TestFiles\\collection.json";

            var form = new MultipartFormDataContent();           

            using var content = new ByteArrayContent(await File.ReadAllBytesAsync(path));

            content.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
            {
                Name = "\"file\"",
                FileName = "\"collection.json\""
            };

            form.Add(content, "uploadedFile1", "collection.json");

            content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

            var request = "/WeatherForecast/ReadFileTest";

            TestServerFixture
               .Client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br");
            TestServerFixture.Client.DefaultRequestHeaders.Add("Accept", "*/*");

            var response = await TestServerFixture
               .Client
               .PostAsync(TestServerFixture.Client.BuildPath(request, Output), form);

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task TryingPostWithFile3()
        {
            var path = "TestFiles\\collection.json";

            var form = new MultipartFormDataContent();

            using var content = new ByteArrayContent(await File.ReadAllBytesAsync(path));

            content.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
            {
                Name = "\"file\"",
                FileName = "\"collection.json\""
            };

            form.Add(content, "uploadedFile1", "collection.json");

            content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

            var request = "/WeatherForecast/ReadFileTest2/collection.json";

            TestServerFixture
               .Client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br");
            TestServerFixture.Client.DefaultRequestHeaders.Add("Accept", "*/*");

            var response = await TestServerFixture
               .Client
               .PostAsync(TestServerFixture.Client.BuildPath(request, Output), form);

            response.EnsureSuccessStatusCode();
        }

        public void Dispose()
        {
            TestServerFixture.Dispose();
        }
    }
}
