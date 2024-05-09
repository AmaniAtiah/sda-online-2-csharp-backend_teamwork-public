using Backend.Helpers;
using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Backend.Controllers
{
    [ApiController]
    [Route("/api/addresses")]
    public class AddressController : ControllerBase
    {
        private readonly AddressesService _addressService;

        public AddressController(AddressesService addressService)
        {
            _addressService = addressService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAddresses()
        {
            try
            {
                var addresses = await _addressService.GetAllAddressesAsync();
                return ApiResponse.Success(addresses, "All addresses retrieved successfully");
            }
            catch (Exception ex)
            {
                return ApiResponse.ServerError(ex.Message);
            }

        }

        [HttpGet("{address_id:guid}")]
        public async Task<IActionResult> GetAddressById(Guid addressId)
        {
            try
            {
                var address = await _addressService.GetAddressById(addressId);
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

        [HttpPost]
        public async Task<IActionResult> CreateAddress(Address newAddress)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var createdAddress = await _addressService.CreateAddressService(newAddress);

                if (createdAddress != null)
                {
                    return CreatedAtAction(nameof(GetAddressById), new { address_id = createdAddress.AddressId }, createdAddress);
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




        [HttpPut("{addressId}")]
        public async Task<IActionResult> UpdateAddress(Guid addressId, Address updateAddress)
        {
            try
            {
                var address = await _addressService.UpdateAddressService(addressId, updateAddress);
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
                var result = await _addressService.DeleteAddressService(addressId);
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
