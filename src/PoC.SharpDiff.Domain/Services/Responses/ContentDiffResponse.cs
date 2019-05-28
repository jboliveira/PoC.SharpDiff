using System.Collections.Generic;
using PoC.SharpDiff.Domain.Models;

namespace PoC.SharpDiff.Domain.Services.Responses
{
    public class ContentDiffResponse
    {
        public List<ContentDiff> Differences { get; private set; }
        public string Message { get; private set; }

        public ContentDiffResponse(string message)
        {
            Message = message;
        }

        public ContentDiffResponse(List<ContentDiff> differences) : this(string.Empty)
        {
            Differences = differences;
        }
    }
}
