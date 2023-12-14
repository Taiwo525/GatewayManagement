using GatewayManagement.Models;
using GatewayManagement.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

[ApiController]
[Route("api/gateways")]
public class GatewayController : ControllerBase
{
    private readonly IGatewayRepo _repository;

    public GatewayController(IGatewayRepo repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> GetGateways()
    {
        var gateways = await _repository.GetGatewaysAsync();
        return Ok(gateways);
    }

    [HttpGet("{serialNumber}")]
    public async Task<IActionResult> GetGatewayBySerialNumber(string serialNumber)
    {
        var gateway = await _repository.GetGatewayBySerialNumberAsync(serialNumber);

        if (gateway == null)
        {
            return NotFound();
        }

        return Ok(gateway);
    }

    [HttpPost]
    public async Task<IActionResult> AddGateway(Gateway gateway)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _repository.AddGatewayAsync(gateway);
        return CreatedAtAction(nameof(GetGatewayBySerialNumber), new { serialNumber = gateway.SerialNumber }, gateway);
    }


    [HttpPost("{serialNumber}/devices")]
    public async Task<IActionResult> AddPeripheralDevice(string serialNumber, PeripheralDevices device)
    {
        try
        {
            await _repository.AddPeripheralDeviceAsync(serialNumber, device);
            return Ok();
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{serialNumber}/devices/{deviceUid}")]
    public async Task<IActionResult> RemovePeripheralDevice(string serialNumber, int deviceUid)
    {
        try
        {
            await _repository.RemovePeripheralDeviceAsync(serialNumber, deviceUid);
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}


//using GatewayManagement.Models;
//using GatewayManagement.Repository.Interface;
//using Microsoft.AspNetCore.Mvc;

//namespace GatewayManagement.Controllers
//{
//    [ApiController]
//    //[Route("api/[controller]")]
//    public class GatewayController : ControllerBase
//    {
//        private readonly IGatewayRepo _gatewayRepo;

//        public GatewayController(IGatewayRepo gatewayRepo)
//        {
//            _gatewayRepo = gatewayRepo;
//        }

//        [HttpGet]
//        public IActionResult GetAllGateways()
//        {
//            var gateways = _gatewayRepo.GetAllGateways();
//            return Ok(gateways);
//        }

//        [HttpGet("{id}")]
//        public IActionResult GetGatewayById(int id)
//        {
//            var gateway = _gatewayRepo.GetGatewayById(id);

//            if (gateway == null)
//            {
//                return NotFound();
//            }

//            return Ok(gateway);
//        }

//        [HttpPost]
//        public IActionResult CreateGateway([FromBody] Gateway gateway)
//        {
//            var createdGateway = _gatewayRepo.CreateGateway(gateway);
//            return CreatedAtAction(nameof(GetGatewayById), new { id = createdGateway.Id }, createdGateway);
//        }

//        [HttpPut("{id}")]
//        public IActionResult UpdateGateway(int id, [FromBody] Gateway gateway)
//        {
//            var existingGateway = _gatewayRepo.GetGatewayById(id);

//            if (existingGateway == null)
//            {
//                return NotFound();
//            }

//            gateway.Id = id;
//            var updatedGateway = _gatewayRepo.UpdateGateway(gateway);

//            return Ok(updatedGateway);
//        }

//        [HttpDelete("{id}")]
//        public IActionResult DeleteGateway(int id)
//        {
//            var existingGateway = _gatewayRepo.GetGatewayById(id);

//            if (existingGateway == null)
//            {
//                return NotFound();
//            }

//            _gatewayRepo.DeleteGateway(id);

//            return NoContent();
//        }
//    }
//}



////using GatewayManagement.Models;
////using GatewayManagement.Repository.Implementation;
////using Microsoft.AspNetCore.Http;
////using Microsoft.AspNetCore.Mvc;
////using GatewayManagement.Repository.Interface;

////namespace GatewayManagement.Controllers
////{
////    [Route("api/[controller]")]
////    [ApiController]
////    public class GatewayController : ControllerBase
////    {
////        private readonly GatewayRepo gatewayRepository;
////        private readonly AuthenticationService authenticationService;

////        public GatewayController(GatewayRepo gatewayRepository, AuthenticationService authenticationService)
////        {
////            this.gatewayRepository = gatewayRepository;
////            this.authenticationService = authenticationService;
////        }

////        [HttpPost]
////        public async Task<IActionResult> CreateGateway([FromBody] Gateway gateway)
////        {
////            // Implement authentication and authorization checks
////            if (await authenticationService.ValidateUserToken(Request.Headers["Authorization"]))
////            {
////                var createdGateway = gatewayRepository.CreateGateway(gateway);
////                return CreatedAtRoute("GetGateway", new { id = createdGateway.Id }, createdGateway);
////            }
////            else
////            {
////                return Unauthorized();
////            }
////        }

////        [HttpGet]
////        public async Task<IActionResult> GetAllGateways()
////        {
////            // Implement authentication and authorization checks
////            if (await authenticationService.ValidateUserToken(Request.Headers["Authorization"]))
////            {
////                var gateways = gatewayRepository.GetAllGateways();
////                return Ok(gateways);
////            }
////            else
////            {
////                return Unauthorized();
////            }
////        }

////        [HttpGet("{id}")]
////        public async Task<IActionResult> GetGatewayById(int id)
////        {
////            // Implement authentication and authorization checks
////            if (await authenticationService.ValidateUserToken(Request.Headers["Authorization"]))
////            {
////                var gateway = gatewayRepository.GetGatewayById(id);
////                if (gateway != null)
////                {
////                    return Ok(gateway);
////                }
////                else
////                {
////                    return NotFound();
////                }
////            }
////            else
////            {
////                return Unauthorized();
////            }
////        }

////        [HttpPut("{id}")]
////        public async Task<IActionResult> UpdateGateway(int id, [FromBody] Gateway gateway)
////        {
////            // Implement authentication and authorization checks
////            if (await authenticationService.ValidateUserToken(Request.Headers["Authorization"]))
////            {
////                gateway.Id = id;
////                var updatedGateway = gatewayRepository.UpdateGateway(gateway);
////                if (updatedGateway != null)
////                {
////                    return Ok(updatedGateway);
////                }
////                else
////                {
////                    return NotFound();
////                }
////            }
////            else
////            {
////                return Unauthorized();
////            }
////        }

////        [HttpDelete("{id}")]
////        public async Task<IActionResult> DeleteGateway(int id)
////        {
////            // Implement authentication and authorization checks
////            if (await authenticationService.ValidateUserToken(Request.Headers["Authorization"]))
////            {
////                gatewayRepository.DeleteGateway(id);
////                return NoContent();
////            }
////            else
////            {
////                return Unauthorized();
////            }
////        }
////    }
////}
