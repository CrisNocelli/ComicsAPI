using System.Collections.Generic;

namespace Comics.DTO
{
    public class DataContainer
    {
        public int Offset { get; set; }
        public int Limit { get; set; } = 20;
        public int Total { get; set; }
        public int Count { get; set; }
        public List<ResultFullView> Results { get; set; }

        public DataContainer()
        {
            Results = new List<ResultFullView>();
        }
    }
}
