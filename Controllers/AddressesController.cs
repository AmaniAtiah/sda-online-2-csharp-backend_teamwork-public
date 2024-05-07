using Microsoft.AspNetCore.Mvc;
using Backend.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Backend.Helpers;
using Backend.EntityFramework;

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

                Console.WriteLine($"{addresses.ToList()}.Count");

                if (addresses.ToList().Count < 1)
                {
                    return NotFound(new ErrorResponse
                    {
                        Success = false,
                        Message = "No address"
                    });
                }

                return Ok(new SuccessResponse<IEnumerable<Address>>()
                {
                    Success = true,
                    Data = addresses,
                    Message = "Addresses returned successfully."
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorResponse() { Message = ex.Message });
            }
        }

        [HttpGet("{addressId}")]
        public async Task<IActionResult> GetAddressById(string AddressId)
        {
            try
            {
                if (!Guid.TryParse(AddressId, out Guid AddressIdGuid))
                {
                    return BadRequest("Invalid address ID Format");
                }
                var address = await _addressService.GetAddressByIdAsync(AddressIdGuid);
                if (address == null)
                {
                    return NotFound();
                }
                return Ok(new SuccessResponse<Address>() { Data = address, Message = "Address retrieved successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorResponse() { Message = ex.Message });
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
                return CreatedAtAction(nameof(GetAddressById), new { addressId = createdAddress.AddressId }, createdAddress);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorResponse() { Message = ex.Message });
            }
        }

        [HttpPut("{addressId}")]
        public async Task<IActionResult> UpdateAddress(string addressId, Address updateAddress)
        {
            try
            {
                if (!Guid.TryParse(addressId, out Guid addressIdGuid))
                {
                    return BadRequest("Invalid address ID Format");
                }
                var address = await _addressService.UpdateAddressService(addressIdGuid, updateAddress);
                if (address == null)
                {
                    return NotFound();
                }
                return Ok(new SuccessResponse<Address>() { Data = address, Message = "Address updated successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorResponse() { Message = ex.Message });
            }
        }

        [HttpDelete("{addressId}")]
        public async Task<IActionResult> DeleteAddress(string addressId)
        {
            try
            {
                if (!Guid.TryParse(addressId, out Guid addressIdGuid))
                {
                    return BadRequest("Invalid address ID Format");
                }
                var result = await _addressService.DeleteAddressService(addressIdGuid);
                if (!result)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorResponse() { Message = ex.Message });
            }
        }
    }
}
