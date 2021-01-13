using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace HostManager.Services
{
    public class HtmlStringGenerator
    {
        private Dictionary<string, string> Parameters;
        private string FilePath;

        public HtmlStringGenerator(string filePath)
        {
            FilePath = filePath;
            Parameters = new Dictionary<string, string>();
        }

        public void AddParameters(string key, string value)
        {
            Parameters.Add(key, value);
        }

        public async Task<string> GenerateBody()
        {
            var content = await ReadAsync();

            foreach (var item in Parameters)
            {
                content = content.Replace(item.Key, item.Value);
            }

            return content;
        }

        private async Task<string> ReadAsync()
        {
            return await File.ReadAllTextAsync(FilePath);
        }

        public async Task WriteAsync(string filePath, string content)
        {
            await File.WriteAllTextAsync(filePath, content);
        }
    }
}
