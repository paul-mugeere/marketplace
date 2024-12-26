using Marketplace.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.Api.Controllers;

[ApiController]
[Route("api/classified-ads")]
public class ClassifiedAdsController : ControllerBase
{
    private readonly IHandleCommand<object> classifiedAdsCommandHandler;

    public ClassifiedAdsController(IHandleCommand<object> classifiedAdsCommandHandler)
    {
        this.classifiedAdsCommandHandler = classifiedAdsCommandHandler;
    }
}