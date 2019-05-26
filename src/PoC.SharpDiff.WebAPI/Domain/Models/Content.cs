namespace PoC.SharpDiff.WebAPI.Domain.Models
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
		/// <param name="direction">Direction LEFT/RIGHT.</param>
		/// <param name="base64String">Base64 string.</param>
		public Content(int id, ContentDirection direction, string base64String)
		{
			Id = id;
			Direction = direction;
			Base64String = base64String;
		}

		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// Gets or sets the direction.
		/// </summary>
		public ContentDirection Direction { get; set; }

		/// <summary>
		/// Gets or sets the base64 string.
		/// </summary>
		public string Base64String { get; set; }
	}
}
