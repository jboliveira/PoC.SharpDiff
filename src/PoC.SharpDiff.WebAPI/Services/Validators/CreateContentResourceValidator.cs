using FluentValidation;
using PoC.SharpDiff.Resources;
using System;

namespace PoC.SharpDiff.WebAPI.Services.Validators
{
    /// <summary>
    /// Create Content Request Validator
    /// </summary>
    public class CreateContentResourceValidator : AbstractValidator<CreateContentResource>
    {
        /// <summary>
        /// CreateContentRequestValidator constructor
        /// </summary>
        public CreateContentResourceValidator()
        {
            RuleFor(request => request.Data)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("'Data' is required.")
                .Must(IsValidBase64).WithMessage("'Data' must be a valid base64 encoded binary data.");
        }

        /// <summary>
        /// Checks if Data is a valid base64 encoded binary data.
        /// </summary>
        /// <returns><c>true</c>, if valid base64, <c>false</c> otherwise.</returns>
        /// <param name="data">JSON base64 encoded binary data.</param>
        private static bool IsValidBase64(string data)
        {
            var bytes = new Span<byte>(new byte[data.Length]);
            return Convert.TryFromBase64String(data, bytes, out int bytesWritten);
        }
    }
}
