using System.Text.Json;
using Swashbuckle.AspNetCore.Filters;

public class InsertLogExample : IExamplesProvider<JsonElement>
{
    public JsonElement GetExamples()
    {
        var json = @"
        [
          {
            ""DeLog_StoredPrograms"": ""sp_insertTest"",
            ""DeLog_GroupID"": ""a7b1f392-6be7-4fd4-a109-123456789abc"",
            ""DeLog_isCustomDebug"": true,
            ""DeLog_ExecutionProgram"": ""SwaggerAPI"",
            ""DeLog_ExecutionInfo"": ""測試資訊"",
            ""DeLog_verifyNeeded"": false,
            ""DeLog_ExDateTime"": ""2025-07-20T12:00:00""
          }
        ]";
        return JsonDocument.Parse(json).RootElement;
    }
}
