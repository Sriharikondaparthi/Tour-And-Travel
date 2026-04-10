using Microsoft.AspNetCore.Mvc;
using SmartTravel.Models;
using System.Text;
using System.Text.Json;

namespace SmartTravel.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TripPlannerController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly HttpClient _http;

        public TripPlannerController(IConfiguration config, IHttpClientFactory factory)
        {
            _config = config;
            _http = factory.CreateClient();
        }

        [HttpPost("plan")]
        public async Task<IActionResult> PlanTrip([FromBody] TripPlanRequest req)
        {
            var apiKey = _config["Gemini:ApiKey"];
            var modelName = "gemini-2.0-flash";
            var url = $"https://generativelanguage.googleapis.com/v1beta/models/{modelName}:generateContent?key={apiKey}";

            var prompt = $@"You are an expert travel planner for India. Create a detailed, personalized travel itinerary based on:

Destination: {req.Destination}
Duration: {req.Duration}
Budget: {req.Budget} INR
Interests: {req.Interests}
Number of Travelers: {req.Travelers}

Please provide:
1. 📅 Day-by-day itinerary
2. 🏨 Recommended hotels (budget-appropriate)
3. 🍽️ Must-try local food
4. 🚗 Transportation tips
5. 💡 Pro travel tips
6. 💰 Estimated budget breakdown

Format the response in a clear, readable way with emojis. Be specific to Indian travel context.";

            var requestBody = new
            {
                contents = new[]
                {
                    new { parts = new[] { new { text = prompt } } }
                }
            };

            var jsonContent = new StringContent(
                JsonSerializer.Serialize(requestBody),
                Encoding.UTF8,
                "application/json"
            );

            _http.DefaultRequestHeaders.Clear();

            var response = await _http.PostAsync(url, jsonContent);
            var responseBody = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                return StatusCode((int)response.StatusCode, new { message = "AI service error", details = responseBody });

            using var doc = JsonDocument.Parse(responseBody);
            var text = doc.RootElement
                .GetProperty("candidates")[0]
                .GetProperty("content")
                .GetProperty("parts")[0]
                .GetProperty("text")
                .GetString();

            return Ok(new { plan = text });
        }
    }
}
