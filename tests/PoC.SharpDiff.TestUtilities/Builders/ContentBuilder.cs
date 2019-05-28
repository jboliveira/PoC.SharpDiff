using PoC.SharpDiff.Domain.Models;

namespace PoC.SharpDiff.TestUtilities.Builders
{
    public class ContentBuilder
	{
		private int _id = int.MinValue;
		private ContentDirection _direction = ContentDirection.Left;
		private string _base64String = "UG9DLlNoYXJwRGlmZi5UZXN0cw==";

		public Content Build()
		{
			return new Content
			{
				Id = _id,
				Direction = _direction,
				Base64String = _base64String
			};
		}

		public static implicit operator Content(ContentBuilder instance)
		{
			return instance.Build();
		}

		public ContentBuilder WithId(int id)
		{
			_id = id;
			return this;
		}

		public ContentBuilder WithDirection(ContentDirection direction)
		{
			_direction = direction;
			return this;
		}

		public ContentBuilder WithBase64String(string base64String)
		{
			_base64String = base64String;
			return this;
		}
	}
}
