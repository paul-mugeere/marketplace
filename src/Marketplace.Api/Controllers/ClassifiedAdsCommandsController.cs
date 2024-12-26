using Marketplace.Api.Contracts;
using Marketplace.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.Api.Controllers;

[ApiController]
[Route("api/classified-ads-with-commands")]
public class ClassifiedAdsCommandsController: ControllerBase
{
    // this allows for SRP but every dependency will have to be instantiated
    // plus all its dependencies per http request scope
    private readonly IHandleCommand<ClassifiedAds.V1.Create> _commandHandler;
    private readonly IHandleCommand<ClassifiedAds.V1.SetTitle> _setTitleCommandHandler;
    private readonly IHandleCommand<ClassifiedAds.V1.UpdateText> _updateTextCommandHandler;
    private readonly IHandleCommand<ClassifiedAds.V1.UpdatePrice> _updatePriceCommandHandler;

    public ClassifiedAdsCommandsController(IHandleCommand<ClassifiedAds.V1.Create> commandHandler, IHandleCommand<ClassifiedAds.V1.SetTitle> setTitleCommandHandler, IHandleCommand<ClassifiedAds.V1.UpdateText> updateTextCommandHandler, IHandleCommand<ClassifiedAds.V1.UpdatePrice> updatePriceCommandHandler)
    {
        _commandHandler = commandHandler;
        _setTitleCommandHandler = setTitleCommandHandler;
        _updateTextCommandHandler = updateTextCommandHandler;
        _updatePriceCommandHandler = updatePriceCommandHandler;
    }

    [Route("create")]
    [HttpPost]
    public async Task<IActionResult> Post(ClassifiedAds.V1.Create command)
    {
        await _commandHandler.Handle(command);
        return Ok();
    }

    
    [HttpPut("set-title")]
    public async Task<IActionResult> Put(ClassifiedAds.V1.SetTitle command)
    {
        await _setTitleCommandHandler.Handle(command);
        return Ok();
    }


    [HttpPut("update-text")]
    public async Task<IActionResult> Put(ClassifiedAds.V1.UpdateText command)
    {
        await _updateTextCommandHandler.Handle(command);
        return Ok();
    }

    [HttpPut("update-price")]
    public async Task<IActionResult> Put(ClassifiedAds.V1.UpdatePrice command)
    {
        await _updatePriceCommandHandler.Handle(command);
        return Ok();
    }
}