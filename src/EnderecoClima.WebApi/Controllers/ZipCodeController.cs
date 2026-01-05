using EnderecoClima.Core.ZipCodeLookupAggregate.DomainObjects;
using EnderecoClima.WebApi.Services.ZipCodes;
using Microsoft.AspNetCore.Mvc;

namespace EnderecoClima.WebApi.Controllers;

[ApiController]
[Route("cep")]
public sealed partial class ZipCodesController : ControllerBase
{
    private readonly IZipCodeLookupService _service;

    public ZipCodesController(IZipCodeLookupService service) => _service = service;

    [HttpGet("{zipCode}")]
    [ProducesResponseType(typeof(AdressResponse), StatusCodes.Status200OK)]
    public async Task<ActionResult<AdressResponse>> Get([FromRoute] string zipCode, CancellationToken ct)
    {
        var cep = ZipCode.Create(zipCode);

        var result = await _service.LookupAsync(cep.Value, ct);
        if (result is null) return NotFound();

        return Ok(result.ToGetResponse());
    }
}