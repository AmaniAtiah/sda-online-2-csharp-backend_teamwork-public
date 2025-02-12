// using System.ComponentModel.DataAnnotations;
// using System.Security.Claims;
// using Backend.Services;
// using Backend.Dtos;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Mvc;
// using Backend.EntityFramework;

// namespace Backend.Controllers
// {
//     // admin can show all address or one address but can't add address or delete 
   
//     [ApiController]
//     [Route("/api/address")]
//     public class AddressController : ControllerBase
//     {
//         private readonly AddressService _addressService;

//         public AddressController(AddressService addressService)
//         {
//             _addressService = addressService;
//         }


//         // display all address
//         [Authorize(Roles = "Admin")]
//         [HttpGet]
//         public async Task<IActionResult> GetAllAddresses()
//         {
//             try
//             {
//                 var isAdmin = User.Claims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Admin");
//                 if (!isAdmin)
//                 {
//                     return ApiResponse.Forbidden("Only admin can visit this route");
//                 }
//                 var addresses = await _addressService.GetAllAddressesAsync();
//                 return ApiResponse.Success(addresses, "All addresses retrieved successfully");
//             }
//             catch (Exception ex)
//             {
//                 return ApiResponse.ServerError(ex.Message);
//             }
//         }


//         [Authorize]
//         [HttpGet("{addressId:guid}")]
//         public async Task<IActionResult> GetAddressById(Guid addressId, Guid userId)
//         {
//             try
//             {
//                  var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

//             if (string.IsNullOrEmpty(userIdString))
//             {
//                 return ApiResponse.UnAuthorized("User Id is misisng from token");
//             }

//             if (!Guid.TryParse(userIdString, out userId))
//             {
//                 return ApiResponse.BadRequest("Invalid User Id");
//             }
//                 var foundedAddress = await _addressService.GetAddressById(addressId, userId);
//                 if (foundedAddress != null)
//                 {
//                     return ApiResponse.Success(foundedAddress, "Address retrieved successfully");
//                 }
//                 return ApiResponse.NotFound("Address was not found");
//             }
//             catch (Exception ex)
//             {
//                 return ApiResponse.ServerError(ex.Message);
//             }
//         }



//         [Authorize]
//         [HttpPost]//Works
//         public async Task<IActionResult> AddAddress([FromBody] AddressDto newAddress)
//         {
//             try
//             {
//                 var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
//                 if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out Guid userId))
//                 {
//                     return ApiResponse.UnAuthorized("User Id is missing or invalid from token");
//                 }

//                 if (!ModelState.IsValid)
//                 {
//                     return BadRequest(ModelState);
//                 }

//                 var createdAddress = await _addressService.AddAddressService(new Address
//                 {
//                     AddressId = newAddress.AddressId,
//                     AddressLine = newAddress.AddressLine,
//                     City = newAddress.City,
//                     State = newAddress.State,
//                     Country = newAddress.Country,
//                     ZipCode = newAddress.ZipCode,
//                     UserId = newAddress.UserId
//                 }, userId);

//                 if (createdAddress != null)
//                 {
//                     return CreatedAtAction(nameof(GetAddressById), new { addressId = createdAddress.AddressId }, createdAddress);
//                 }
//                 else
//                 {
//                     return ApiResponse.ServerError("Failed to create the address.");
//                 }
//             }
//             catch (Exception ex)
//             {
//                 return ApiResponse.ServerError(ex.Message);
//             }
//         }

//         [Authorize]
//         [HttpPut("{addressId:guid}")]//Works
//         public async Task<IActionResult> UpdateAddress(Guid addressId, Address updateAddres)
//         {
//             try
//             {
//                 var isUser = User.Claims.Any(c => c.Type == ClaimTypes.Role && c.Value == "User");
//                 if (!isUser)
//                 {
//                     return ApiResponse.Forbidden("Only User can visit this route");
//                 }
//                 var address = await _addressService.UpdateAddressService(addressId, updateAddres);

//                 if (address == null)
//                 {
//                     return ApiResponse.NotFound("Address was not found");
//                 }
//                 return ApiResponse.Success(address, "Address updated successfully");
//             }
//             catch (Exception ex)
//             {
//                 return ApiResponse.ServerError(ex.Message);
//             }
//         }
//         [Authorize]
//         [HttpDelete("{addressId:guid}")]//Works
//         public async Task<IActionResult> DeleteAddress(Guid addressId)
//         {
//             try
//             {
//                 var isUser = User.Claims.Any(c => c.Type == ClaimTypes.Role && c.Value == "User");
//                 if (!isUser)
//                 {
//                     return ApiResponse.Forbidden("Only User can visit this route");
//                 }
//                 var result = await _addressService.DeleteAddressService(addressId);

//                 if (!result)
//                 {
//                     return ApiResponse.NotFound("Address was not found");
//                 }
//                 return NoContent();
//             }
//             catch (Exception ex)
//             {
//                 return ApiResponse.ServerError(ex.Message);
//             }


//         }





//     }
// }
