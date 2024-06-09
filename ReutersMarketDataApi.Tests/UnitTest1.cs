using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using ReutersMarketDataApi.Interface;
using ReutersMarketDataApi.Model;
using ReutersMarketDataApi;
using System.Net;

public class AssetsControllerTests
{
    private AssetsController _sut;
    private IAssetRepository _assetRepository;
    private Asset _expectedAsset;

    [SetUp]
    public void Setup()
    {
        _assetRepository = Substitute.For<IAssetRepository>();
        _sut = new AssetsController(_assetRepository);

        _expectedAsset = new Asset
        {
            Id = 123,
            Name = "Microsoft Corporation",
            Symbol = "MSFT",
            ISIN = "US5949181045",
            Prices = new List<Price>()
        };
    }


    [Test]
    public async Task GetAssets_ShouldReturnAllAssets()
    {
        // Arrange
        var expectedAssets = new List<Asset> { _expectedAsset };
        _assetRepository.GetAllAssetsAsync().Returns(expectedAssets);

        // Act
        var result = await _sut.GetAssets();

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result);
        var okResult = result as OkObjectResult;
        var assets = okResult!.Value as IEnumerable<Asset>;

        Assert.NotNull(okResult);
        Assert.NotNull(assets);
        Assert.AreEqual(expectedAssets, assets);
    }

    [Test]
    public async Task GetAssetById_ShouldReturnAssetExpected()
    {
        //Arrange
        _assetRepository.GetAssetById(Arg.Any<int>()).Returns(_expectedAsset);

        //Act
        var result = await _sut.GetAsset(123);

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result);
        var okResult = result as OkObjectResult;
        Assert.NotNull(okResult);

        var asset = okResult.Value as Asset;
        Assert.NotNull(asset);
        Assert.AreEqual(_expectedAsset, asset);

    }

    [Test]
    public async Task GetFilterAsset_ShouldReturnAssetsExpected()
    {
        //Arrange
        var expectedAssets = new List<Asset> { _expectedAsset };
        _assetRepository.GetFilterAssetsAsync(Arg.Any<DateTime>(), Arg.Any<string?>()).Returns(expectedAssets);

        //Act
        var result = await _sut.GetAssetByFilter(DateTime.Today);

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result);
        var okResult = result as OkObjectResult;
        var assets = okResult!.Value as IEnumerable<Asset>;

        Assert.NotNull(okResult);
        Assert.NotNull(assets);
        Assert.AreEqual(expectedAssets, assets);

    }

    [Test]
    public async Task CreateAsset_ShouldReturnTaskComplete()
    {
        //Arrange
        _assetRepository.CreateAsset(Arg.Any<Asset>()).Returns(Task.CompletedTask);

        //Act
        var result = await _sut.CreateAsset(_expectedAsset);

        // Assert
        var createdResult = result.Result as CreatedAtActionResult;

        Assert.NotNull(createdResult);
        Assert.AreEqual((int)HttpStatusCode.Created, createdResult.StatusCode);
    }

    [Test]
    public async Task UpdateAsset_ShouldReturnTaskComplete()
    {
        //Arrange
        _assetRepository.UpdateAsset(Arg.Any<int>(), Arg.Any<Asset>()).Returns(Task.CompletedTask);

        //Act
        var result = await _sut.UpdateAsset(123, _expectedAsset);

        // Assert
        var okResult = result as NoContentResult;
        
        Assert.NotNull(okResult);
        Assert.AreEqual((int)HttpStatusCode.NoContent, okResult.StatusCode);
    }
}
