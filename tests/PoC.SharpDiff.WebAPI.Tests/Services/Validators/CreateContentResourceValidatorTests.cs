using FluentValidation.TestHelper;
using PoC.SharpDiff.Resources;
using PoC.SharpDiff.WebAPI.Services.Validators;
using Xunit;
using PoC.SharpDiff.TestUtilities;

namespace PoC.SharpDiff.WebAPI.Tests.Services.Validators
{
    [Trait("Category", "Unit")]
    public class CreateContentResourceValidatorTests
    {
        private CreateContentResourceValidator validator;

        public CreateContentResourceValidatorTests()
        {
            validator = new CreateContentResourceValidator();
        }

        [Fact]
        public void Validator_GivenNullString_ShouldThrowValidationErrorMessage()
        {
            validator.ShouldHaveValidationErrorFor(x => x.Data, (string)null)
                .WithErrorMessage("'Data' is required.");
        }

        [Fact]
        public void Validator_GivenEmptyString_ShouldThrowValidationErrorMessage()
        {
            validator.ShouldHaveValidationErrorFor(x => x.Data, string.Empty)
                .WithErrorMessage("'Data' is required.");
        }

        [Fact]
        public void Validator_GivenInvalidBase64Data_ShouldThrowValidationErrorMessage()
        {
            validator.ShouldHaveValidationErrorFor(x => x.Data, "xy")
                .WithErrorMessage("'Data' must be a valid base64 encoded binary data.");
        }

        [Fact]
        public void Validator_GivenInvalidPreconstructedObject_ShouldThrowValidationErrorMessage()
        {
            validator.ShouldHaveValidationErrorFor(x => x.Data, new CreateContentResource { Data = null })
                .WithErrorMessage("'Data' is required.");
        }

        [Fact]
        public void Validator_GivenValidBase64Data_ShouldNotThrowValidationErrorMessage()
        {
            var base64String = "hello world".ConvertToBase64FromUTF8String();

            validator.ShouldNotHaveValidationErrorFor(x => x.Data, base64String);
        }
    }
}
