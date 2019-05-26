﻿using System;
using PoC.SharpDiff.WebAPI.Domain.Models;

namespace PoC.SharpDiff.WebAPI.Domain.Services.Responses
{
	public class ContentResponse
	{
		public Content Content { get; private set; }
		public bool Success { get; private set; }
		public string Message { get; private set; }

		private ContentResponse(bool success, string message)
		{
			Success = success;
			Message = message;
		}

		private ContentResponse(bool success, string message, Content content) : this(success, message)
		{
			Content = content;
		}

		/// <summary>
		/// Creates a success response.
		/// </summary>
		/// <param name="content">Saved content.</param>
		/// <returns>Response.</returns>
		public ContentResponse(Content content) : this(true, string.Empty, content)
		{ }

		/// <summary>
		/// Creates am error response.
		/// </summary>
		/// <param name="message">Error message.</param>
		/// <returns>Response.</returns>
		public ContentResponse(string message) : this(false, message, null)
		{ }

		/// <summary>
		/// Gets the content binary data.
		/// </summary>
		/// <returns>The binary data.</returns>
		public byte[] GetContentBinaryData()
		{
			return Convert.FromBase64String(Content?.Base64String);
		}
	}
}
