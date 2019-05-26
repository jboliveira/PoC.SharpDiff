using System.Collections.Generic;
using PoC.SharpDiff.WebAPI.Domain.Models;

namespace PoC.SharpDiff.WebAPI.Domain.Helpers
{
	/// <summary>
	/// Content helper.
	/// </summary>
	public static class ContentHelper
	{
		/// <summary>
		/// Compare the specified contentLeft with contentRight.
		/// </summary>
		/// <returns>The differences.</returns>
		/// <param name="contentLeft">Content left.</param>
		/// <param name="contentRight">Content right.</param>
		public static IEnumerable<ContentDiff> Compare(byte[] contentLeft, byte[] contentRight)
		{
			for (int i = 0; i < contentLeft.Length; i++)
			{
				if (contentLeft[i] != contentRight[i])
				{
					int j = i;

					while ((contentLeft[j] != contentRight[j]) && j++ < contentLeft.Length - 1) 
						continue;

					yield return new ContentDiff(i, j - i);

					i = j;
				}
			}
		}
	}
}
