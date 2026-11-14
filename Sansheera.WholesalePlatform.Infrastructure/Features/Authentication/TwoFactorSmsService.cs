using Microsoft.Extensions.Configuration;
using Sansheera.WholesalePlatform.Application.Abstractions.Authentication;
using System.Net.Http.Json;
using System.Text.Json;

namespace Sansheera.WholesalePlatform.Infrastructure.Features.Authentication
{
    public sealed class TwoFactorSmsService : ISmsService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly string _templateName;

        public TwoFactorSmsService(
            HttpClient httpClient,
            IConfiguration configuration)
        {
            _httpClient = httpClient;

            _apiKey = configuration["TwoFactor:ApiKey"]
                ?? throw new InvalidOperationException(
                    "TwoFactor API key is not configured.");

            _templateName = configuration["TwoFactor:TemplateName"]
                ?? throw new InvalidOperationException(
                    "TwoFactor template name is not configured.");

            if (string.IsNullOrWhiteSpace(_apiKey) ||
                string.IsNullOrWhiteSpace(_templateName))
            {
                throw new InvalidOperationException(
                    "TwoFactor configuration is incomplete.");
            }

            _httpClient.BaseAddress = new Uri(
                configuration["TwoFactor:BaseUrl"]
                ?? "https://2factor.in");
        }

        public async Task SendOtpAsync(
            string phoneNumber,
            string otp,
            CancellationToken cancellationToken)
        {
            var normalizedPhone = NormalizeIndianPhoneNumber(phoneNumber);

            using var request = new HttpRequestMessage(
                HttpMethod.Post,
                "/API/V1/OTP/SEND");

            request.Headers.Add("X-API-Key", _apiKey);

            request.Content = JsonContent.Create(new
            {
                to = normalizedPhone,
                template_name = _templateName,
                var1 = otp
            });

            using var response = await _httpClient.SendAsync(
                request,
                cancellationToken);

            var responseBody = await response.Content.ReadAsStringAsync(
                cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException(
                    $"OTP provider request failed with HTTP status {(int)response.StatusCode}.");
            }

            using var document = JsonDocument.Parse(responseBody);

            var root = document.RootElement;

            if (!root.TryGetProperty("status", out var status) ||
                !string.Equals(
                    status.GetString(),
                    "sent",
                    StringComparison.OrdinalIgnoreCase))
            {
                throw new HttpRequestException(
                    "OTP provider did not accept the SMS request.");
            }
        }

        private static string NormalizeIndianPhoneNumber(string phoneNumber)
        {
            var digits = new string(
                phoneNumber.Where(char.IsDigit).ToArray());

            if (digits.Length == 10)
            {
                return $"+91{digits}";
            }

            if (digits.Length == 12 && digits.StartsWith("91"))
            {
                return $"+{digits}";
            }

            throw new ArgumentException(
                "Phone number must be a valid Indian mobile number.",
                nameof(phoneNumber));
        }
    }
}
