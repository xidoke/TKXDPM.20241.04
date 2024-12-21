using System;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using AIMS.Services;

public class DeliveryInfoValidator
{

    public DeliveryInfoValidator()
    {
    }

    public string ValidateDeliveryInfo(string name, string phoneNumber, string address, string selectedCity, string selectedDistrict, string selectedWard)
    {
        if (!ValidateName(name))
        {
            return "Invalid name. Name should only contain letters and be less than 30 characters.";
        }

        if (!ValidatePhoneNumber(phoneNumber))
        {
            return "Invalid phone number. Phone number should start with '0' and have 10 digits.";
        }

        var addressValidationResult = ValidateAddress(address, selectedCity, selectedDistrict, selectedWard);
        if (!string.IsNullOrEmpty(addressValidationResult))
        {
            return addressValidationResult;
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
            if (!char.IsLetter(ch) && ch != ' ') return false;
        }

        return true;
    }

    // Validate Phone Number: Starts with "0", contains only digits, and has exactly 10 digits
    private bool ValidatePhoneNumber(string phoneNumber)
    {
        if (string.IsNullOrEmpty(phoneNumber)) return false;
        if (!phoneNumber.StartsWith("0")) return false;
        if (phoneNumber.Length != 10) return false;

        return Regex.IsMatch(phoneNumber, @"^\d{10}$");
    }

    // Validate Address: Not empty, max 100 characters, and includes valid city, district, and ward
    private string ValidateAddress(string address, string selectedCity, string selectedDistrict, string selectedWard)
    {
        if (string.IsNullOrEmpty(address) || address.Length > 100)
        {
            return "Please ensure the address is not empty, within 100 characters.";
        }

        // Kiểm tra thành phố
        if (string.IsNullOrEmpty(selectedCity))
        {
            return "Vui lòng chọn Tỉnh/Thành phố!";
        }

        // Kiểm tra Quận/Huyện
        if (string.IsNullOrEmpty(selectedDistrict))
        {
            return "Vui lòng chọn Quận/Huyện!";
        }

        // Kiểm tra Phường/Xã
        if (string.IsNullOrEmpty(selectedWard))
        {
            return "Vui lòng chọn Phường/Xã!";
        }

        return null;
    }
}
