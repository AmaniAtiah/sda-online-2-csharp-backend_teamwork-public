using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Backend.Models;
using Backend.Services;
using Backend.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    // admin can show all address or one address but can't add address or delete 
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("/api/admins/address")]
    public class AdminAddressController : ControllerBase
    {
        private readonly AddressService _addressService;

        public AdminAddressController(AddressService addressService)
        {
            _addressService = addressService;
        }


        // display all address
        [HttpGet]
        public async Task<IActionResult> GetAllAddresses()
        {
            try
            {
                var isAdmin = User.Claims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Admin");
                if (!isAdmin)
                {
                    return ApiResponse.Forbidden("Only admin can visit this route");
                }
                var addresses = await _addressService.GetAllAddressesAsync();
                return ApiResponse.Success(addresses, "All addresses retrieved successfully");
            }
            catch (Exception ex)
            {
                return ApiResponse.ServerError(ex.Message);
            }
        }

        // admin can show any address
        [HttpGet("{addressId}")]
        public async Task<IActionResult> ShowAddress(Guid addressId)
        {
            try
            {
                var isAdmin = User.Claims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Admin");
                if (!isAdmin)
                {
                    return ApiResponse.Forbidden("Only admin can visit this route");
                }
                
                var address = await _addressService.ShowAddressByAdmin(addressId);
                if (address != null)
                {
                    return ApiResponse.Success(address, "Address retrieved successfully");
                }
                else
                {
                    return ApiResponse.NotFound("Address was not found");
                }
            }
            catch (Exception ex)
            {
                return ApiResponse.ServerError(ex.Message);
            }
        }



    }
}
