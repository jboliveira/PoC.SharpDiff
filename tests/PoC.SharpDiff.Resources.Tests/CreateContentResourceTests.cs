using PoC.SharpDiff.TestUtilities.Builders;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace PoC.SharpDiff.Resources.Tests
{
    [Trait("Category", "Unit")]
    public static class CreateContentResourceTests
    {
        [Fact]
        public static void AllFieldsPresentAndWithinValidationRules_ModelStateValid()
        {
            var sut = new CreateContentResourceBuilder().Build();
            var context = new ValidationContext(sut, null);
            var validationResults = new List<ValidationResult>();

            var valid = Validator.TryValidateObject(sut, context, validationResults, true);

            Assert.True(valid);
        }
    }
}
