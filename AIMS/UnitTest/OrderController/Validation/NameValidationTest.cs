using System.Xml.Linq;
using AIMS.Utils;
using NUnit.Framework;

namespace AIMS.UnitTest.Controller.Validation
{
    [TestFixture]
    public class NameValidationTest
    {
        private DeliveryInfoValidator _validator;

        [SetUp]
        public void SetUp()
        {
            _validator = new DeliveryInfoValidator();
        }

        [Test]
        public void ValidateName_WithValidInputs_ReturnsNull()
        {
            var name = "Lê Văn Tuấn Đạt";

            var result = _validator.ValidateName(name);

            Assert.That(result, Is.Null);
        }

        [Test]
        public void ValidateName_EmptyName_ReturnsErrorMessage()
        {
            var name = "";

            var result = _validator.ValidateName(name);

            Assert.That(result, Is.EqualTo("Vui lòng nhập tên người nhận hàng."));
        }

        [Test]
        public void ValidateName_ContainDigits_ReturnsErrorMessage()
        {
            var name = "Lê 12";

            var result = _validator.ValidateName(name);

            Assert.That(result, Is.EqualTo("Tên không hợp lệ. Vui lòng nhập tên chỉ chứa chữ cái và dấu cách."));
        }

        [Test]
        public void ValidateName_ContainSpecialCharacter_ReturnsErrorMessage()
        {
            var name = "Lê @";

            var result = _validator.ValidateName(name);

            Assert.That(result, Is.EqualTo("Tên không hợp lệ. Vui lòng nhập tên chỉ chứa chữ cái và dấu cách."));
        }

        [Test]
        public void ValidateName_WithMoreThan_30_Characters_ReturnsErrorMessage()
        {
            var name = new string('A', 31);

            var result = _validator.ValidateName(name);

            Assert.That(result, Is.EqualTo("Tên không hợp lệ. Vui lòng nhập tên không quá 30 ký tự."));
        }
    }
}
