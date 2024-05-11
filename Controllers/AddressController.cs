using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Backend.Dtos;

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

        [HttpGet("{addressId}")]
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
        [HttpPost]
        public async Task<IActionResult> AddAddressService([FromBody] AddressDto newAddress)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var createdAddress = await _addressService.AddAddressService(new Address
                {
                    AddressId = newAddress.AddressId,
                    AddressLine = newAddress.AddressLine,
                    City = newAddress.City,
                    State = newAddress.State,
                    Country = newAddress.Country,
                    ZipCode = newAddress.ZipCode,
                    UserId = newAddress.UserId
                });

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





        [HttpPut("{addressId}")]
        public async Task<IActionResult> UpdateAddress(Guid addressId, AddressDto updateAddressDto)
        {
            try
            {
                // Retrieve the existing address
                var existingAddress = await _addressService.GetAddressById(addressId);

                if (existingAddress == null)
                {
                    return ApiResponse.NotFound("Address was not found");
                }

                // Map the properties from the DTO to the existing address
                existingAddress.AddressLine = updateAddressDto.AddressLine;
                existingAddress.City = updateAddressDto.City;
                existingAddress.State = updateAddressDto.State;
                existingAddress.Country = updateAddressDto.Country;
                existingAddress.ZipCode = updateAddressDto.ZipCode;

                // Update the address
                var updatedAddress = await _addressService.UpdateAddressService(addressId, existingAddress);

                if (updatedAddress != null)
                {
                    return ApiResponse.Success(updatedAddress, "Address updated successfully");
                }
                else
                {
                    return ApiResponse.ServerError("Failed to update the address.");
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
