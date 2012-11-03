using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;

namespace Ebuy.Website.Api
{
    public class AuctionCsvFormatter : BufferedMediaTypeFormatter
    {
        public AuctionCsvFormatter()
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/csv"));
        }

        public override bool CanWriteType(Type type)
        {
            if (type == typeof(Auction))
            {
                return true;
            }
            else
            {
                Type enumerableType = typeof(IEnumerable<Auction>);
                return enumerableType.IsAssignableFrom(type);
            }
        }

        public override bool CanReadType(Type type)
        {
            return false;
        }

        public override void WriteToStream(Type type, object value, Stream stream, System.Net.Http.HttpContent content)
        {
            var source = value as IEnumerable<Auction>;
            if (source != null)
            {
                foreach (var item in source)
                {
                    WriteItem(item, stream);
                }
            }
            else
            {
                var item = value as Auction;
                if (item != null)
                {
                    WriteItem(item, stream);
                }
            }
        }

        private void WriteItem(Auction item, Stream stream)
        {
            var writer = new StreamWriter(stream);
            writer.WriteLine("{0},{1},{2}",
            Encode(item.Title),
            Encode(item.Description),
            Encode(item.CurrentPrice));
            writer.Flush();
        }

        static readonly char[] _specialChars = new char[] { ',', '\n', '\r', '"' };
        private string Encode(object o)
        {
            string result = "";
            if (o != null)
            {
                string data = o.ToString();
                if (data.IndexOfAny(_specialChars) != -1)
                {
                    result = String.Format("\"{0}\"", data.Replace("\"", "\"\""));
                }
            }
            return result;
        }
    }
}