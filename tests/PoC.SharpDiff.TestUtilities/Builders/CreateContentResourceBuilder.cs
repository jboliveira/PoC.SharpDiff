using PoC.SharpDiff.Resources;

namespace PoC.SharpDiff.TestUtilities.Builders
{
    public class CreateContentResourceBuilder
    {
        private string _data = "UG9DLlNoYXJwRGlmZi5UZXN0cw==";

        public CreateContentResource Build()
        {
            return new CreateContentResource
            {
                Data = _data
            };
        }

        public static implicit operator CreateContentResource(CreateContentResourceBuilder instance)
        {
            return instance.Build();
        }

        public CreateContentResourceBuilder WithData(string data)
        {
            _data = data;
            return this;
        }
    }
}
