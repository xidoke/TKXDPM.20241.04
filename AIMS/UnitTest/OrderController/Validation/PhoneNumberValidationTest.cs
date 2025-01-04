using AIMS.Utils;
using NUnit.Framework;

namespace AIMS.UnitTest.Controller.Validation
{
    [TestFixture]
    public class PhoneNumberValidationTest
    {
        private DeliveryInfoValidator _validator;

        [SetUp]
        public void SetUp()
        {
            _validator = new DeliveryInfoValidator();
        }

        [Test]
        public void ValidatePhoneNumber_WithValidInputs_ReturnsNull()
        {
            var phoneNumber = "0363676728";

            var result = _validator.ValidatePhoneNumber(phoneNumber);

            Assert.That(result, Is.Null);
        }

        [Test]
        public void ValidatePhoneNumber_EmptyPhoneNumber_ReturnsErrorMessage()
        {
            var phoneNumber = "";

            var result = _validator.ValidatePhoneNumber(phoneNumber);

            Assert.That(result, Is.EqualTo("Vui lòng nhập số điện thoại."));
        }

        [Test]
        public void ValidatePhoneNumber_NotStartingWith_0_ReturnsErrorMessage()
        {
            var phoneNumber = "9363676728";

            var result = _validator.ValidatePhoneNumber(phoneNumber);

            Assert.That(result, Is.EqualTo("Số điện thoại không hợp lệ. Vui lòng nhập số bắt đầu bằng số '0'."));
        }

        [Test]
        public void ValidatePhoneNumber_ContainCharacter_ReturnsErrorMessage()
        {
            var phoneNumber = "036367672a";

            var result = _validator.ValidatePhoneNumber(phoneNumber);

            Assert.That(result, Is.EqualTo("Số điện thoại không hợp lệ. Vui lòng chỉ nhập chữ số."));
        }

        [Test]
        public void ValidatePhoneNumber_ContainSpecialCharacter_ReturnsErrorMessage()
        {
            var phoneNumber = "036367672a";

            var result = _validator.ValidatePhoneNumber(phoneNumber);

            Assert.That(result, Is.EqualTo("Số điện thoại không hợp lệ. Vui lòng chỉ nhập chữ số."));
        }

        [Test]
        public void ValidatePhoneNumber_ContainSpace_ReturnsErrorMessage()
        {
            var phoneNumber = "036367672 ";

            var result = _validator.ValidatePhoneNumber(phoneNumber);

            Assert.That(result, Is.EqualTo("Số điện thoại không hợp lệ. Vui lòng chỉ nhập chữ số."));
        }

        [Test]
        public void ValidatePhoneNumber_WithLessThan_10_Digits_ReturnsErrorMessage()
        {
            var phoneNumber = "036367672";

            var result = _validator.ValidatePhoneNumber(phoneNumber);

            Assert.That(result, Is.EqualTo("Số điện thoại không hợp lệ. Vui lòng nhập số có đúng 10 chữ số."));
        }

        [Test]
        public void ValidatePhoneNumber_WithMoreThan_10_Digits_ReturnsErrorMessage()
        {
            var phoneNumber = "03636767289";

            var result = _validator.ValidatePhoneNumber(phoneNumber);

            Assert.That(result, Is.EqualTo("Số điện thoại không hợp lệ. Vui lòng nhập số có đúng 10 chữ số."));
        }

    }

}
