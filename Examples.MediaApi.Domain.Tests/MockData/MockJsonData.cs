using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Examples.MediaApi.Domain.Tests
{
    public static class MockJsonData
    {
        public static string Albums()
        {
            return GetResource("Albums");
        }

        public static string Photos()
        {
            return GetResource("Photos");
        }

        private static string GetResource(string filename)
        {
            var assembly = typeof(MockJsonData).Assembly;
            var resourceName = $"{assembly.GetName().Name}.MockData.{filename}.json";

            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream == null)
                {
                    throw new Exception($"Embedded resource file not found: {resourceName}");
                }

                using (var streamReader = new StreamReader(stream, Encoding.UTF8))
                {
                    return streamReader.ReadToEnd();
                }
            }
        }
    }
}
