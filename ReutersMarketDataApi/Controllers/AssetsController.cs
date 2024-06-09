using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReutersMarketDataApi.Interface;
using ReutersMarketDataApi.Model;
using Swashbuckle.AspNetCore.Annotations;

namespace ReutersMarketDataApi;

[Route("api/[controller]")]
[ApiController]
public class AssetsController : ControllerBase
{
    private readonly IAssetRepository _assetRepository;

    public AssetsController(IAssetRepository assetRepository)
    {
        _assetRepository = assetRepository;
    }

    /// <summary>
    /// Retrieve all the Assets and Prices
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [SwaggerOperation("GetAssets")]
    [ProducesResponseType(typeof(IEnumerable<Asset>), 200)]
    public async Task<IActionResult> GetAssets() => Ok(await _assetRepository.GetAllAssetsAsync());

    /// <summary>
    /// Retrieve all assets and prices filtering by date and optional source
    /// </summary>
    /// <param name="date"></param>
    /// <param name="source"></param>
    /// <returns></returns>
    [HttpGet("GetAssetByDate")]
    [SwaggerOperation("GetAssetsByFilter")]
    [ProducesResponseType(typeof(IEnumerable<Asset>), 200)]
    public async Task<IActionResult> GetAssetByFilter([FromQuery] DateTime date, [FromQuery] string? source = null)
    {
        var assets = await _assetRepository.GetFilterAssetsAsync(date, source);

        if (assets == null || !assets.Any())
        {
            return NotFound();
        }

        return Ok(assets);
    }

    /// <summary>
    /// Retrieve the specific data from specific id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    [SwaggerOperation("GetAssetById")]
    [ProducesResponseType(typeof(Asset), 200)]
    public async Task<IActionResult> GetAsset(int id)
    {
        var asset = await _assetRepository.GetAssetById(id);

        if (asset == null)
        {
            return NotFound();
        }

        return Ok(asset);
    }

    /// <summary>
    /// Create new asset from the Asset/Price model
    /// </summary>
    /// <param name="asset"></param>
    /// <returns></returns>
    [HttpPost]
    [SwaggerOperation("CreateAsset")]
    [ProducesResponseType(typeof(Asset), 201)]
    public async Task<ActionResult<Asset>> CreateAsset(Asset asset)
    {
        await _assetRepository.CreateAsset(asset);

        return CreatedAtAction(nameof(GetAsset), new { id = asset.Id }, asset);
    }

    /// <summary>
    /// Update Asset when id and the whole model are provided
    /// </summary>
    /// <param name="id"></param>
    /// <param name="asset"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    [SwaggerOperation("UpdateAsset")]
    [ProducesResponseType(typeof(Asset), 201)]
    public async Task<IActionResult> UpdateAsset(int id, Asset asset)
    {
        if (id != asset.Id)
        {
            return BadRequest("Asset ID mismatch");
        }

        try
        {

            await _assetRepository.UpdateAsset(id, asset);

            return NoContent();
        }
        catch(DbUpdateConcurrencyException ex )
        {
            return BadRequest(ex.Message);
        }

    }
}