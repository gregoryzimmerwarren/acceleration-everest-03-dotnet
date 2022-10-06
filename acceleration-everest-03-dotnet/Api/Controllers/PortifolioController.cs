using AppServices.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PortifolioController : ControllerBase
{
    private readonly IPortifolioAppService _portifolioAppService;

    public PortifolioController(IPortifolioAppService portifolioAppService)
    {
        _portifolioAppService = portifolioAppService ?? throw new System.ArgumentNullException(nameof(portifolioAppService));
    }
}