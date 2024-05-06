public class AddressesService
{
    private static List<Address> _addresses = new List<Address>()
    {
         new Address
    {
        AddressId = Guid.NewGuid(),
        AddressLine = "456 Elm St",
        City = "Townsville",
        State = "Provinceville",
        Country = "Countryland",
        ZipCode = "67890",
        UserId = Guid.NewGuid()
    },
    new Address
    {
        AddressId = Guid.NewGuid(),
        AddressLine = "789 Oak St",
        City = "Villageville",
        State = "Countyville",
        Country = "Countryland",
        ZipCode = "54321",
        UserId = Guid.NewGuid()
    },
    new Address
    {
        AddressId = Guid.NewGuid(),
        AddressLine = "101 Pine St",
        City = "Hamletville",
        State = "Territoryville",
        Country = "Countryland",
        ZipCode = "98765",
        UserId = Guid.NewGuid()
    }, new Address
        {
            AddressId = Guid.NewGuid(),
            AddressLine = "123 Main St",
            City = "Cityville",
            State = "Stateville",
            Country = "Countryland",
            ZipCode = "12345",
            UserId = Guid.NewGuid()
        }
    };

    public async Task<IEnumerable<Address>> GetAllAddressesAsync()
    {
        await Task.CompletedTask;
        return _addresses;
    }

    public Task<Address?> GetAddressByIdAsync(Guid addressId)
    {
        var address = _addresses.FirstOrDefault(address => address.AddressId == addressId);
        return Task.FromResult<Address?>(address);
    }


    public Task<Address> CreateAddressService(Address newAddress)
    {
        newAddress.AddressId = Guid.NewGuid();
        _addresses.Add(newAddress);
        return Task.FromResult(newAddress);
    }

    public Task<Address> UpdateAddressService(Guid addressId, Address updateAddress)
    {
        var existingAddress = _addresses.FirstOrDefault(a => a.AddressId == addressId);
        if (existingAddress != null)
        {
            existingAddress.AddressLine = updateAddress.AddressLine;
            existingAddress.City = updateAddress.City;
            existingAddress.State = updateAddress.State;
            existingAddress.Country = updateAddress.Country;
            existingAddress.ZipCode = updateAddress.ZipCode;
            existingAddress.UserId = updateAddress.UserId;
        }
        return Task.FromResult(existingAddress);
    }

    public Task<bool> DeleteAddressService(Guid addressId)
    {
        var addressToRemove = _addresses.FirstOrDefault(a => a.AddressId == addressId);
        if (addressToRemove != null)
        {
            _addresses.Remove(addressToRemove);
            return Task.FromResult(true);
        }
        return Task.FromResult(false);
    }



}

