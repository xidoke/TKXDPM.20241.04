using System;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using AIMS.Services;

public class DeliveryInfoValidator
{
    private readonly ProvinceService _provinceService;
    private readonly DistrictService _districtService;
    private readonly WardService _wardService;
    public DeliveryInfoValidator(ProvinceService provinceService, DistrictService districtService, WardService wardService)
    {
        _provinceService = provinceService;
        _districtService = districtService;
        _wardService = wardService;
    }

    public async Task<string> ValidateDeliveryInfoAsync(string name, string phoneNumber, string address, string selectedCity, string selectedDistrict, string selectedWard)
    {
        // Validate Name
        if (!ValidateName(name))
        {
            return "Invalid name. Name should only contain letters and be less than 30 characters.";
        }

        // Validate Phone Number
        if (!ValidatePhoneNumber(phoneNumber))
        {
            return "Invalid phone number. Phone number should start with '0' and have 10 digits.";
        }

        // Validate Address
        if (string.IsNullOrEmpty(address) || address.Length > 100)
        {
            return "Invalid address. Address cannot be empty and should be less than 100 characters.";
        }

        // Validate City, District, and Ward
        if (string.IsNullOrEmpty(selectedCity) || string.IsNullOrEmpty(selectedDistrict) || string.IsNullOrEmpty(selectedWard))
        {
            return "Please select a valid City, District, and Ward.";
        }

        var selectedProvince = await _provinceService.GetProvinceByNameAsync(selectedCity);
        if (selectedProvince == null)
        {
            return "Invalid Province. Please select a valid City.";
        }

        var selectedDistrictObj = await _districtService.GetDistrictByNameAndProvinceAsync(selectedDistrict, selectedProvince.Id);
        if (selectedDistrictObj == null)
        {
            return "The selected District does not belong to the selected Province. Please change the address.";
        }

        var selectedWardObj = await _wardService.GetWardByNameAndDistrictAndProvinceAsync(selectedWard, selectedDistrictObj.Id, selectedProvince.Id);
        if (selectedWardObj == null)
        {
            return "The selected Ward does not belong to the selected District and Province. Please change the address.";
        }

        return "Delivery information is valid. Proceeding with the order.";
    }

    // Validate Name: Only letters and no longer than 30 characters
    private bool ValidateName(string name)
    {
        if (string.IsNullOrEmpty(name)) return false;
        if (name.Length > 30) return false;

        foreach (char ch in name)
        {
            if (!char.IsLetter(ch)) return false;
        }

        return true;
    }

    // Validate Phone Number: Starts with "0", contains only digits, and has exactly 10 digits
    private bool ValidatePhoneNumber(string phoneNumber)
    {
        if (string.IsNullOrEmpty(phoneNumber)) return false;
        if (!phoneNumber.StartsWith("0")) return false;
        if (phoneNumber.Length != 10) return false;

        // Ensure phone number only contains digits
        return Regex.IsMatch(phoneNumber, @"^\d{10}$");
    }

    // Validate Address: Not empty, max 100 characters
    private bool ValidateAddress(string address)
    {
        return !string.IsNullOrEmpty(address) && address.Length <= 100;
    }
}
