using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Win_Viewer.backend.Models;
using Win_Viewer.backend.Services;

namespace Win_Viewer.backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceController : ControllerBase
    {
        private readonly DeviceDiscoveryService _deviceService;
    public DeviceController(DeviceDiscoveryService deviceService)
        => _deviceService = deviceService;

    [HttpPost("register")]
    public IActionResult Register(DeviceInfo info)
    {
        _deviceService.RegisterOrUpdate(info);
        return Ok();
    }

    [HttpGet]
    public IActionResult List() => Ok(_deviceService.ListAll());
    }
} 
