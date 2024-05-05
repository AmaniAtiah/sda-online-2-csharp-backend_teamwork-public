using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace Backend.Controllers
{
    [ApiController]
    [Route("/api/addresses")]
    public class AddressController : ControllerBase
    {
        private readonly AddressesService _addressService;

        public AddressController()
        {
            _addressService = new AddressesService();
        }

        [HttpGet]
        public IActionResult GetAllAddresses()
        {
            var addresses = _addressService.GetAllAddresses();
            return Ok(addresses);
        }

        [HttpGet("{AddressId}")]
        public IActionResult GetAddressById(string addressId)
        {
            if (!Guid.TryParse(addressId, out Guid addressIdGuid))
            {
                return BadRequest("Invalid address ID Format");
            }
            var address = _addressService.GetAddressById(addressIdGuid);
            if (address == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(address);
            }
        }


        [HttpPost]
        public IActionResult CreateAddress(Address newAddress)
        {
            var createdAddress = _addressService.CreateAddressService(newAddress);
            return CreatedAtAction(nameof(GetAddressById), new { addressId = createdAddress.AddressId }, createdAddress);
        }

        [HttpPut("{addressId}")]
        public IActionResult UpdateAddress(string addressId, Address updateAddress)
        {
            if (!Guid.TryParse(addressId, out Guid addressIdGuid))
            {
                return BadRequest("Invalid address ID Format");
            }
            var address = _addressService.UpdateAddressService(addressIdGuid, updateAddress);
            if (address == null)
            {
                return NotFound();
            }
            return Ok(address);
        }

        [HttpDelete("{addressId}")]
        public IActionResult DeleteAddress(string addressId)
        {
            if (!Guid.TryParse(addressId, out Guid addressIdGuid))
            {
                return BadRequest("Invalid address ID Format");
            }
            var result = _addressService.DeleteAddressService(addressIdGuid);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

    }
}