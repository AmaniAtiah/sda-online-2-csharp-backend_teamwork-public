public class AddressesService
{
    public static List<Address> _addresses = new List<Address>()
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

    public IEnumerable<Address> GetAllAddresses()
    {
        return _addresses;
    }

    // Method to get an address by its ID
    public Address? GetAddressById(Guid addressId)
    {
        return _addresses.Find(address => address.AddressId == addressId);
    }

    public Address? CreateAddressService(Address newAddress)
    {
        newAddress.AddressId = Guid.NewGuid();
        _addresses.Add(newAddress);
        return newAddress;
    }

    public Address UpdateAddressService(Guid AddressId, Address updateAddress)
    {
        var existingAddress = _addresses.FirstOrDefault(a => a.AddressId == AddressId);
        if (existingAddress != null)
        {
            existingAddress.AddressLine = updateAddress.AddressLine;
            existingAddress.City = updateAddress.City;
            existingAddress.State = updateAddress.State;
            existingAddress.Country = updateAddress.Country;
            existingAddress.ZipCode = updateAddress.ZipCode;
            existingAddress.UserId = updateAddress.UserId;
        }
        return existingAddress;
    }

    public bool DeleteAddressService(Guid addressId)
    {
        var addressToRemove = _addresses.FirstOrDefault(a => a.AddressId == addressId);
        if (addressToRemove != null)
        {
            _addresses.Remove(addressToRemove);
            return true;
        }
        return false;
    }



}

