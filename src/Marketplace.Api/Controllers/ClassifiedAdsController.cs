using Marketplace.Api.Contracts;
using Marketplace.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.Api.Controllers;

[ApiController]
[Route("api/classified-ads")]
public class ClassifiedAdsController : ControllerBase
{
    private readonly IClassifiedAdsApplicationService _classifiedAdsApplicationService;

    public ClassifiedAdsController(IClassifiedAdsApplicationService classifiedAdsApplicationService)
    {
        _classifiedAdsApplicationService = classifiedAdsApplicationService;
    }
    
    [Route("create")]
    [HttpPost]
    public async Task<IActionResult> Post(ClassifiedAds.V1.Create command)
    {
        await _classifiedAdsApplicationService.Handle(command);
        return Ok();
    }
    
     
    [HttpPut("set-title")]
    public async Task<IActionResult> Put(ClassifiedAds.V1.SetTitle command)
    {
        await _classifiedAdsApplicationService.Handle(command);
        return Ok();
    }
}