using AIMS.Utils;
using NUnit.Framework;

namespace AIMS.UnitTest.Controller.Validation
{
    [TestFixture]
    public class AddressValidationTest
    {
        private DeliveryInfoValidator _validator;

        [SetUp]
        public void SetUp()
        {
            _validator = new DeliveryInfoValidator();
        }

        [Test]
        public void ValidateAddress_WithValidInputs_ReturnsNull()
        {
            var address = "123 Main St";
            var city = "Hồ Chí Minh";
            var district = "Quận 1";
            var ward = "Phường 1";

            var result = _validator.ValidateAddress(address, city, district, ward);

            Assert.That(result, Is.Null);
        }

        [Test]
        public void ValidateAddress_WithInvalidCharacters_ReturnsErrorMessage()
        {
            var address = "Invalid@Address!";
            var city = "Hồ Chí Minh";
            var district = "Quận 1";
            var ward = "Phường 1";

            var result = _validator.ValidateAddress(address, city, district, ward);

            Assert.That(result, Is.EqualTo("Địa chỉ không hợp lệ. Vui lòng chỉ nhập chữ cái, dấu cách, dấu gạch chéo (/) và dấu phẩy (,)."));
        }

        [Test]
        public void ValidateAddress_WithEmptyAddress_ReturnsErrorMessage()
        {
            var address = "";
            var city = "Hồ Chí Minh";
            var district = "Quận 1";
            var ward = "Phường 1";

            var result = _validator.ValidateAddress(address, city, district, ward);

            Assert.That(result, Is.EqualTo("Vui lòng nhập địa chỉ, trong khoảng 100 ký tự."));
        }

        [Test]
        public void ValidateAddress_WithAddressExceedingMaxLength_ReturnsErrorMessage()
        {
            var address = new string('A', 101);
            var city = "Hồ Chí Minh";
            var district = "Quận 1";
            var ward = "Phường 1";

            var result = _validator.ValidateAddress(address, city, district, ward);

            Assert.That(result, Is.EqualTo("Vui lòng nhập địa chỉ, trong khoảng 100 ký tự."));
        }

        [Test]
        public void ValidateAddress_WithMissingCity_ReturnsErrorMessage()
        {
            var address = "123 Main St, District 1";
            var city = "";
            var district = "";
            var ward = "";

            var result = _validator.ValidateAddress(address, city, district, ward);

            Assert.That(result, Is.EqualTo("Vui lòng chọn Tỉnh/Thành phố!"));
        }


        [Test]
        public void ValidateAddress_WithMissingDistrict_ReturnsErrorMessage()
        {
            var address = "123 Main St, District 1";
            var city = "Thành phố Hồ Chí Minh";
            var district = "";
            var ward = "";

            var result = _validator.ValidateAddress(address, city, district, ward);

            Assert.That(result, Is.EqualTo("Vui lòng chọn Quận/Huyện!"));
        }

        [Test]
        public void ValidateAddress_WithMissingWard_ReturnsErrorMessage()
        {
            var address = "123 Main St, District 1";
            var city = "Thành phố Hồ Chí Minh";
            var district = "Quận Bình Thạnh";
            var ward = "";

            var result = _validator.ValidateAddress(address, city, district, ward);

            Assert.That(result, Is.EqualTo("Vui lòng chọn Phường/Xã!"));
        }


    }
}
