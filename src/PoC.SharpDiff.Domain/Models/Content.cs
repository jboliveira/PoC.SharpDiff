namespace PoC.SharpDiff.Domain.Models
{
    /// <summary>
    /// Content.
    /// </summary>
    public class Content
    {
        public Content() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Content"/> class.
        /// </summary>
        /// <param name="id">Identifier.</param>
        public Content(int id)
        {
            Id = id;
        }

        public void SetLeftContent(string data)
        {
            LeftContentData = data;
        }

        public void SetRightContent(string data)
        {
            RightContentData = data;
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the left content data
        /// </summary>
        public string LeftContentData { get; set; }

        /// <summary>
        /// Gets or sets the right content data
        /// </summary>
        public string RightContentData { get; set; }
    }
}
