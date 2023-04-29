using System.Text;

namespace VideoSpider.M3UParser
{
    //This class has bug!
    public static class M3UReader
    {
        private const string FILE_HEADER = "#EXTM3U";
        private const string TAG_VERSION = "#EXT-X-VERSION:";
        private const string TAG_STREAM_INFO = "#EXT-X-STREAM-INF:";
        private const string STREAM_INF_BANDWIDTH = "BANDWIDTH";
        private const string STREAM_INF_NAME = "NAME";
        private const string STREAM_INF_CODECS = "CODECS";
        private const string STREAM_INF_RESOLUTION = "RESOLUTION";
        private static readonly string[] Tags =
        {
            TAG_VERSION,
            TAG_STREAM_INFO
        };

        public static M3UFile Parse(string data)
        {
            using StringReader reader = new StringReader(data);
            string line = reader.ReadLine();

            if (line == null)
                throw new ArgumentException("Unable to read lines from data.", nameof(data));
            if (!FILE_HEADER.Equals(line))
                throw new ArgumentException("Incorrectly formatted data.", nameof(data));

            M3UFile result = new M3UFile();

            string previousLine = null;
            while ((line = reader.ReadLine()) != null)
            {
                string tag = GetTag(line);
                if (tag != null)
                {
                    SetTagValue(result, tag, line);
                }
                else if (previousLine != null && previousLine.StartsWith(TAG_STREAM_INFO))
                {
                    result.Streams[result.Streams.Count - 1].Path = line;
                }

                previousLine = line;
            }

            return result;
        }

        private static void ParseStreamInfo(M3UFile file, string tag, string line)
        {
            StreamInfo stream = new StreamInfo();
            StringBuilder builder = new StringBuilder();
            int quoteCount = 0;
            string currentStreamTag = null;

            string data = line.Substring(tag.Length);
            for (int i = 0; i <= data.Length; i++)
            {
                char? c = i < data.Length ? data[i] : (char?)null;

                if (c == '"')
                {
                    quoteCount++;
                }
                else if (c == '=' && quoteCount % 2 == 0)
                {
                    currentStreamTag = builder.ToString();
                    builder.Clear();
                }
                else if ((c == ',' && quoteCount % 2 == 0) || c == null)
                {
                    string value = builder.ToString();
                    builder.Clear();

                    if (STREAM_INF_BANDWIDTH.Equals(currentStreamTag))
                    {
                        stream.Bandwidth = long.Parse(value);
                    }
                    else if (STREAM_INF_NAME.Equals(currentStreamTag))
                    {
                        stream.Name = value;
                    }
                    else if (STREAM_INF_CODECS.Equals(currentStreamTag))
                    {
                        stream.Codecs = value;
                    }
                    else if (STREAM_INF_RESOLUTION.Equals(currentStreamTag))
                    {
                        string[] split = value.Split('x');
                        stream.ResolutionWidth = int.Parse(split[0]);
                        stream.ResolutionHeight = int.Parse(split[1]);
                    }
                }
                else
                {
                    builder.Append(c);
                }
            }

            file.AddStream(stream);
        }

        /// <summary>
        /// Extracts the value of the given tag from the given line and sets the corresponding property of the file to the
        /// extracted value.
        /// </summary>
        private static void SetTagValue(M3UFile file, string tag, string line)
        {
            string value = line.Substring(tag.Length);
            switch (tag)
            {
                case TAG_VERSION:
                    file.Version = value;
                    break;
                case TAG_STREAM_INFO:
                    ParseStreamInfo(file, tag, line);
                    break;
            }
        }

        /// <summary>
        /// Checks if the given string starts with one of the supported tags and returns the tag. Otherwise, returns null.
        /// </summary>
        private static string GetTag(string line)
        {
            foreach (string tag in Tags)
            {
                if (line.StartsWith(tag))
                {
                    return tag;
                }
            }

            return null;
        }
    }


}
