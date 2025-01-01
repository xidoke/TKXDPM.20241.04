using System.Text.RegularExpressions;

namespace AIMS.Utils
{
    public class DeliveryInfoValidator
    {
        public string Validate(string name, string phoneNumber, string address, string selectedCity,string selectedDistrict, string selectedWard, string email, string shippingMethod)
        {
            var nameValidationResult = ValidateName(name);
            if (!string.IsNullOrEmpty(nameValidationResult)) return nameValidationResult;
            var phoneNumberValidationResult = ValidatePhoneNumber(phoneNumber);
            if (!string.IsNullOrEmpty(phoneNumberValidationResult)) return phoneNumberValidationResult;
            var emailValidationResult = ValidateEmail(email);
            if (!string.IsNullOrEmpty(emailValidationResult)) return emailValidationResult;
            var addressValidationResult = ValidateAddress(address, selectedCity, selectedDistrict, selectedWard);
            if (!string.IsNullOrEmpty(addressValidationResult)) return addressValidationResult;
            if (!ValidateShippingMethod(shippingMethod)) return "Vui lòng chọn phương thức vận chuyển.";
            return "Thông tin giao hàng hợp lệ.";
        }

        private string? ValidateName(string name)
        {
            if (string.IsNullOrEmpty(name)) return "Vui lòng nhập tên người nhận hàng.";
            if (name.Length > 30)  return "Tên không hợp lệ. Vui lòng nhập tên không quá 30 ký tự.";
            foreach (char ch in name)
            {
                if (!char.IsLetter(ch) && ch != ' ') return "Tên không hợp lệ. Vui lòng nhập tên chỉ chứa chữ cái và dấu cách.";
            }
            return null;
        }

        private string? ValidatePhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber)) return "Vui lòng nhập số điện thoại.";
            if (!phoneNumber.StartsWith("0")) return "Số điện thoại không hợp lệ. Vui lòng nhập số bắt đầu bằng số '0'.";
            if (phoneNumber.Length != 10) return "Số điện thoại không hợp lệ. Vui lòng nhập số có đúng 10 chữ số.";
            if (!Regex.IsMatch(phoneNumber, Constants.PHONE_NUMBER_REGEX)) return "Số điện thoại không hợp lệ. Vui lòng chỉ nhập chữ số.";
            return null;
        }

        private string? ValidateAddress(string address, string selectedCity, string selectedDistrict, string selectedWard)
        {
            if (string.IsNullOrEmpty(address) || address.Length > 100) return "Vui lòng nhập địa chỉ, trong khoảng 100 ký tự.";
            if (string.IsNullOrEmpty(selectedCity)) return "Vui lòng chọn Tỉnh/Thành phố!";
            if (string.IsNullOrEmpty(selectedDistrict)) return "Vui lòng chọn Quận/Huyện!";
            if (string.IsNullOrEmpty(selectedWard)) return "Vui lòng chọn Phường/Xã!";
            return null;
        }

        private string? ValidateEmail(string email)
        {
            if (string.IsNullOrEmpty(email)) return "Vui lòng nhập địa chỉ email.";
            if (!Regex.IsMatch(email, Constants.EMAIL_REGEX)) return "Địa chỉ email không hợp lệ. Vui lòng cung cấp email hợp lệ.";
            return null;
        }

        private bool ValidateShippingMethod(string shippingMethod)
        {
            return !string.IsNullOrEmpty(shippingMethod);
        }
    }
}
