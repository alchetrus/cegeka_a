using System.Collections.Immutable;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PetShelter.Api.Resources;
using PetShelter.Api.Resources.Extensions;
using PetShelter.Domain.Services;

namespace PetShelter.Api.Controllers;

[ApiController]
[Route("[controller]")]
[EnableCors("AllowAll")]
public class FundraiserController : ControllerBase
{
    private readonly IFundraiserService _fundraiserService;

    public FundraiserController(IFundraiserService fundraiserService)
    {
        _fundraiserService = fundraiserService;
    }

    [HttpPost("donate")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Donate([FromBody] Donation donation)
    {
        var result = await _fundraiserService.DonateToFundraiserAsync(donation.AsDomainModel());
        if (result != null)
        {
            return StatusCode(406, result);
        }

        return Ok();
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ICollection<FundraiserBaseInfo>>> Get()
    {
        var result = await _fundraiserService.GetAllFundraisers();
        return Ok(result.Select(p => p.AsBaseInfo()).ToImmutableArray());
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<FundraiserFullInfo>> Get(int id)
    {
        var fundraiser = await _fundraiserService.GetFundraiser(id);

        if (fundraiser is null)
        {
            return NotFound();
        }

        return Ok(fundraiser.AsResource());
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult> CreateFundraiser(int ownerId, [FromBody] Resources.Fundraiser fundraiser)
    {
        await _fundraiserService.CreateFundraiserAsync(fundraiser.AsDomainModel(), ownerId);
        return Ok();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> DeleteFundraiser(int id)
    {
        await _fundraiserService.DeleteFundraiserAsync(id);
        return Ok();
    }
}