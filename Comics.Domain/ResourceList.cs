using System.Collections.Generic;

namespace Comics.Domain
{
    public class ResourceList
    {
        public int Available { get; set; }
        public int Returned { get; set; }
        public string CollectionURI { get; set; }
    }

    public class ResourceList<T> : ResourceList
    {
        public List<T> Items { get; set; }
    }
}
