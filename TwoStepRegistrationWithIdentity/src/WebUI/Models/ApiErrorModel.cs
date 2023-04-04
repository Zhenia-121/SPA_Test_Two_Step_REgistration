using System.Text.Json;

namespace TwoStepRegistrationWithIdentity.Models;

public class ApiErrorModel
{
    public int Code { get; set; }
    
    public string Message { get; set; }

    public string Description { get; set; }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
    }
}
