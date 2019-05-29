using PoC.SharpDiff.Domain.Models;

namespace PoC.SharpDiff.TestUtilities.Builders
{
    public class ContentBuilder
    {
        private int _id = int.MinValue;
        private string _leftContentString = "UG9DLlNoYXJwRGlmZi5UZXN0cw==";
        private string _rightContentString = "UG9DLlNoYXJwRGlmZi5UZXN0cw==";

        public Content Build()
        {
            return new Content
            {
                Id = _id,
                LeftContentData = _leftContentString,
                RightContentData = _rightContentString
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

        public ContentBuilder WithLeftContent(string data)
        {
            _leftContentString = data;
            return this;
        }

        public ContentBuilder WithRightContent(string data)
        {
            _rightContentString = data;
            return this;
        }
    }
}
