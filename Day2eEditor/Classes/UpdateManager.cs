using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Day2eEditor
{
    public class UpdateManager
    {

        public Manifest manifest;
        public string manifestUrl = "";

        public async Task CheckForUpdatesAsync()
        {
            manifest = await GetManifestAsync(manifestUrl);

            // Compare main app version, download ZIP if newer
            // Compare plugin versions, download individual DLLs
            // Optionally: trigger restart or external updater

            // Add your own logic here
        }

        private async Task<Manifest> GetManifestAsync(string url)
        {
            using var client = new HttpClient();
            return await client.GetFromJsonAsync<Manifest>(url);
        }
    }
}
