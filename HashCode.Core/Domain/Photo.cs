using System.Collections.Generic;

namespace HashCode.Core.Domain
{
    public class Photo
    {
        public int Id { get; set; }
        public bool IsHorizontal { get; set; }
        public int NumberOfTags { get; set; }
        public List<string> Tags { get; set; }
    }
}
