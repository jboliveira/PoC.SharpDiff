namespace PoC.SharpDiff.Domain.Models
{
    /// <summary>
    /// Represents a difference between content data.
    /// </summary>
    public class ContentDiff
    {
        /// <summary>
        /// Offset of the difference.
        /// </summary>
        public int Offset { get; set; }

        /// <summary>
        /// Length of the difference.
        /// </summary>
        public int Length { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentDiff"/> class.
        /// </summary>
        /// <param name="offset">The offset of the difference.</param>
        /// <param name="length">The length of the difference.</param>
        public ContentDiff(int offset, int length)
        {
            Offset = offset;
            Length = length;
        }
    }
}
