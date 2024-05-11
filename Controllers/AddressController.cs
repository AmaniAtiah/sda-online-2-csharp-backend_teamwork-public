using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("/api/address")]
    public class AddressController : ControllerBase
    {
        private readonly AddressService _addressService;

        public AddressController(AddressService addressService)
        {
            _addressService = addressService;
        }

        [Authorize(Roles = "Admin")]
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

     



        [Authorize]
        [HttpGet("{addressId}")]
        public async Task<IActionResult> GetAddressById(Guid addressId, Guid userId)
        {
            try
            {
                
                 var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out  userId))
                {
                    return ApiResponse.UnAuthorized("User Id is missing or invalid from token");
                }

                         var isAdmin = User.Claims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Admin");
            if (!isAdmin)
            {
                return ApiResponse.Forbidden("Only admin can visit this route");
            }
                
               
                var address = await _addressService.GetAddressById(addressId, userId);
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

     


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddAddressService(Address newAddress)
        {
            try
            {
                var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out Guid userId))
                {
                    return ApiResponse.UnAuthorized("User Id is missing or invalid from token");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var createdAddress = await _addressService.AddAddressService(newAddress, userId);

                if (createdAddress != null)
                {
                    return CreatedAtAction(nameof(GetAddressById), new { addressId = createdAddress.AddressId }, createdAddress);
                }
                else
                {
                    return ApiResponse.ServerError("Failed to create the address.");
                }
            }
           
            catch (Exception ex)
            {
                return ApiResponse.ServerError(ex.Message);
            }
        }

        [Authorize]
        [HttpPut("{addressId}")]
        public async Task<IActionResult> UpdateAddress(Guid addressId, Address updateAddress)
        {
            try
            {
                var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out Guid userId))
                {
                    return ApiResponse.UnAuthorized("User Id is missing or invalid from token");
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var address = await _addressService.UpdateAddressService(addressId, updateAddress, userId);
                if (address != null)
                {
                    return ApiResponse.Success(address, "Address updated successfully");
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

        [HttpDelete("{addressId}")]
        public async Task<IActionResult> DeleteAddress(Guid addressId)
        {
            try
            {
                var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out Guid userId))
                {
                    return ApiResponse.UnAuthorized("User Id is missing or invalid from token");
                }


                var result = await _addressService.DeleteAddressService(addressId, userId);
                if (result)
                {
                    return NoContent();
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
