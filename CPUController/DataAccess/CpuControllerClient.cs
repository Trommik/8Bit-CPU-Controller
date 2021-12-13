using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using CPUController.Core;

namespace CPUController.DataAccess
{
    public class CpuControllerClient : ICpuControllerClient
    {
        private readonly HttpClient _client = new();

        private readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true,
        };

        /// <summary>
        /// Initializes a new <see cref="CpuControllerClient"/> for the given <paramref name="endpoint"/>.
        /// </summary>
        /// <param name="endpoint">The endpoint of the cpu controller api. </param>
        public CpuControllerClient(string endpoint)
        {
            _client.BaseAddress = new Uri(endpoint);

            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        /// <inheritdoc />
        public async Task<bool> CheckConnection()
        {
            HttpResponseMessage response = await _client.GetAsync(string.Empty);

            string data = await response.Content.ReadAsStringAsync();

            return data.Equals("8-Bit CPU REST API v1");
        }

        /// <inheritdoc />
        public async Task<CpuModeResponse> GetMode()
        {
            HttpResponseMessage response = await _client.GetAsync("/mode");

            string data = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<CpuModeResponse>(data, _jsonOptions) ?? throw new NullReferenceException("Could not deserialize object!");
        }

        /// <inheritdoc />
        public async Task<CpuCodeResponse> GetCodeStatus()
        {
            HttpResponseMessage response = await _client.GetAsync("/code");

            string data = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<CpuCodeResponse>(data, _jsonOptions) ?? throw new NullReferenceException("Could not deserialize object!");
        }

        /// <inheritdoc />
        public async Task<CpuControlWordResponse> GetControlWord()
        {
            HttpResponseMessage response = await _client.GetAsync("/controlword");

            string data = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<CpuControlWordResponse>(data, _jsonOptions) ?? throw new NullReferenceException("Could not deserialize object!");
        }

        /// <inheritdoc />
        public async Task<CpuInstructionResponse> GetInstructionStatus()
        {
            HttpResponseMessage response = await _client.GetAsync("/instruction");

            string data = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<CpuInstructionResponse>(data, _jsonOptions) ?? throw new NullReferenceException("Could not deserialize object!");
        }

        /// <inheritdoc />
        public async Task SetMode(CpuMode mode)
        {
            HttpResponseMessage response = await _client.PostAsync($"/control?mode={mode.ToString().ToLower()}", null!);
            response.EnsureSuccessStatusCode();
        }

        /// <inheritdoc />
        public async Task SetCode(IEnumerable<byte> code)
        {
            var content = new StringContent(JsonSerializer.Serialize(code), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PostAsync("/code", content);
            response.EnsureSuccessStatusCode();
        }

        /// <inheritdoc />
        public async Task Reset()
        {
            HttpResponseMessage response = await _client.PostAsync("/reset", null!);
            response.EnsureSuccessStatusCode();
        }

        /// <inheritdoc />
        public void Dispose()
        {
            _client.Dispose();
        }
    }
}