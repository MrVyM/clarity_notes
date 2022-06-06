using Newtonsoft.Json; // Install Newtonsoft.Json with NuGet
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ClarityNotes
{
    public class TranslationResult
    {
        public DetectedLanguage DetectedLanguage { get; set; }
        public TextResult SourceText { get; set; }
        public Translation[] Translations { get; set; }
    }

    public class DetectedLanguage
    {
        public string Language { get; set; }
        public float Score { get; set; }
    }

    public class TextResult
    {
        public string Text { get; set; }
        public string Script { get; set; }
    }

    public class Translation
    {
        public string Text { get; set; }
        public TextResult Transliteration { get; set; }
        public string To { get; set; }
        public Alignment Alignment { get; set; }
        public SentenceLength SentLen { get; set; }
    }

    public class Alignment
    {
        public string Proj { get; set; }
    }

    public class SentenceLength
    {
        public int[] SrcSentLen { get; set; }
        public int[] TransSentLen { get; set; }
    }

    public class Traductor
    {
        private static readonly string subscriptionKey = "6b72a8a19f454a52b7cba7add52c31e3";
        public static string SubscriptionKey => subscriptionKey;

        private static readonly string endpoint = "https://api.cognitive.microsofttranslator.com/";
        public static string Endpoint => endpoint;

        // Add your location, also known as region. The default is global.
        // This is required if using a Cognitive Services resource.
        private static readonly string location = "francecentral";

        public static async Task<string> Traduce(string subscriptionKey, string endpoint, string route, string imputText)
        {
            object[] body = new object[] { new { Text = imputText } };
            var requestBody = JsonConvert.SerializeObject(body);

            string textResult = null;

            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage())
            {
                // Build the request.
                request.Method = HttpMethod.Post;
                request.RequestUri = new Uri(endpoint + route);
                request.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");
                request.Headers.Add("Ocp-Apim-Subscription-Key", subscriptionKey);
                request.Headers.Add("Ocp-Apim-Subscription-Region", location);

                // Send the request and get response.
                HttpResponseMessage response = await client.SendAsync(request).ConfigureAwait(false);
                // Return response as a string.
                string result = await response.Content.ReadAsStringAsync();
                TranslationResult[] deserializedOutput = JsonConvert.DeserializeObject<TranslationResult[]>(result);
                // Iterate over the deserialized results.

                textResult += deserializedOutput[0].Translations[0].Text;
            }
            return textResult;
        }
    }
}
