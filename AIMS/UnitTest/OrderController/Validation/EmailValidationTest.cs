using AIMS.Utils;
using NUnit.Framework;
namespace AIMS.UnitTest.Controller.Validation
{
    [TestFixture]
    public class EmailValidationTest
    {
        private DeliveryInfoValidator _validator;

        [SetUp]
        public void SetUp()
        {
            _validator = new DeliveryInfoValidator();
        }

        [Test]
        public void ValidateEmail_WithValidEmail_ReturnsNull()
        {
            var email = "test.email@example.com";

            var result = _validator.ValidateEmail(email);

            Assert.That(result, Is.Null);
        }

        [Test]
        public void ValidateEmail_WithUppercase_ReturnsNull()
        {
            var email = "TEST.EMAIL@EXAMPLE.COM";

            var result = _validator.ValidateEmail(email);

            Assert.That(result, Is.Null);
        }

        [Test]
        public void ValidateEmail_EmptyEmail_ReturnsErrorMessage()
        {
            var email = "";

            var result = _validator.ValidateEmail(email);

            Assert.That(result, Is.EqualTo("Vui lòng nhập địa chỉ email."));
        }

        [Test]
        public void ValidateEmail_InvalidFormat_ReturnsErrorMessage()
        {
            var email = "invalid-email";

            var result = _validator.ValidateEmail(email);

            Assert.That(result, Is.EqualTo("Địa chỉ email không hợp lệ. Vui lòng cung cấp email hợp lệ."));
        }

        [Test]
        public void ValidateEmail_WithMultipleAtSymbols_ReturnsErrorMessage()
        {
            var email = "user@sub@domain.com";

            var result = _validator.ValidateEmail(email);

            Assert.That(result, Is.EqualTo("Địa chỉ email không hợp lệ. Vui lòng cung cấp email hợp lệ."));
        }

        [Test]
        public void ValidateEmail_ContainComma_ReturnsErrorMessage()
        {
            var email = "user@domain,com";

            var result = _validator.ValidateEmail(email);

            Assert.That(result, Is.EqualTo("Địa chỉ email không hợp lệ. Vui lòng cung cấp email hợp lệ."));
        }

        [Test]
        public void ValidateEmail_StartsWithDot_ReturnsErrorMessage()
        {
            var email = ".user@domain.com";

            var result = _validator.ValidateEmail(email);

            Assert.That(result, Is.EqualTo("Địa chỉ email không hợp lệ. Vui lòng cung cấp email hợp lệ."));
        }

        [Test]
        public void ValidateEmail_ContainsConsecutiveDots_ReturnsErrorMessage()
        {
            var email = "user@domain..com";

            var result = _validator.ValidateEmail(email);

            Assert.That(result, Is.EqualTo("Địa chỉ email không hợp lệ. Vui lòng cung cấp email hợp lệ."));
        }

        [Test]
        public void ValidateEmail_UnsupportedDomain_ReturnsErrorMessage()
        {
            var email = "user@unsupported.xyz";

            var result = _validator.ValidateEmail(email);

            Assert.That(result, Is.EqualTo("Địa chỉ email không hợp lệ. Vui lòng cung cấp email hợp lệ."));

        }

        [Test]
        public void ValidateEmail_DomainEndWithHyphen_ReturnsErrorMessage()
        {
            var email = "user@domain-.com";

            var result = _validator.ValidateEmail(email);

            Assert.That(result, Is.EqualTo("Địa chỉ email không hợp lệ. Vui lòng cung cấp email hợp lệ."));

        }

        [Test]
        public void ValidateEmail_MissingTLD_ReturnsErrorMessage()
        {
            var email = "user@domain";

            var result = _validator.ValidateEmail(email);

            Assert.That(result, Is.EqualTo("Địa chỉ email không hợp lệ. Vui lòng cung cấp email hợp lệ."));
        }

        [Test]
        public void ValidateEmail_WithUnsupportedTLD_ReturnsErrorMessage()
        {
            var email = "user@domain.xyz";

            var result = _validator.ValidateEmail(email);

            Assert.That(result, Is.EqualTo("Địa chỉ email không hợp lệ. Vui lòng cung cấp email hợp lệ."));
        }  
    }
}


