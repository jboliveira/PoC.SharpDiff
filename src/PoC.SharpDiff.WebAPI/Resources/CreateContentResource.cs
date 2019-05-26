namespace PoC.SharpDiff.WebAPI.Resources
{
	/// <summary> 
	/// Request to create a new Content 
	/// </summary>
	public class CreateContentResource
	{
		/// <summary>
		/// JSON base64 encoded binary data
		/// </summary>
		public string Data { get; set; }
	}
}
