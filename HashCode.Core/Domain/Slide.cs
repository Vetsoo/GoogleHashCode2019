using System.Collections.Generic;

namespace HashCode.Core.Domain
{
    public class Slide
    {
        public List<Photo> Photos { get; set; }
        public List<string> UniqueTags { get; set; }
    }
}
