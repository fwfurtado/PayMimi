using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Bogus;
using Bogus.Extensions.Brazil;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using NUnit.Framework;
using PayMimi.Infra.Http.Clients;
using Refit;

namespace PayMimi.Test.Infra.Http.Clients;

public class SocialNumberClient
{
    private readonly IContainer _mockoonContainer = new ContainerBuilder()
        .WithImage("mockoon/cli")
        .WithPortBinding(3000)
        .WithResourceMapping(new FileInfo("./Mockoon/social-number.json"), "/data")
        .WithCommand("-d", "/data/social-number.json", "-p", "3000")
        .WithWaitStrategy(Wait.ForUnixContainer().UntilHttpRequestIsSucceeded(r => r.ForPath("/api/health").ForPort(3000)))
        .Build();

    [OneTimeSetUp]
    public async Task BeforeAll()
    {
        await _mockoonContainer.StartAsync();
    }

    [OneTimeTearDown]
    public async Task AfterAll()
    {
        await _mockoonContainer.StopAsync();
    }

    [Test]
    public async Task ShouldCallClient()
    {
        var faker = new Faker();
        var endpoint = $"http://{_mockoonContainer.Hostname}:{_mockoonContainer.GetMappedPublicPort(3000)}/api";
        var client = RestService.For<ISocialNumberClient>(endpoint);

        var socialNumber = faker.Person.Cpf();
        var response = await client.IsValid(socialNumber);

        Assert.AreEqual(socialNumber, response.SocialNumber);
    }
}