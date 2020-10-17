using System;
using System.Net;

namespace Comics.DTO
{
    public class DataWrapper
    {
        public int Code { get; set; }
        public string Status { get; set; }
        public string Copyright { get; set; }
        public string AttributionText { get; set; }
        public string AttributionHTML { get; set; }
        public DataContainer Data { get; set; }
        public string Etag { get; set; }

        public DataWrapper(bool success = false)
        {
            if (success)
            {
                Code = (int)HttpStatusCode.OK;
                Status = HttpStatusCode.OK.ToString();
                Copyright = $"© {DateTime.Today.Year} MARVEL";
                AttributionText = $"Data provided by Marvel. {Copyright}";
                AttributionHTML = $"<a href=\"http://marvel.com\">{AttributionText}</a>";
                Etag = string.Empty; // TODO: calculate this value properly -  A digest value of the content returned by the call.
                Data = new DataContainer();
            }
        }
    }
}
