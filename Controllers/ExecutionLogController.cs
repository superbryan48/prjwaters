using Microsoft.AspNetCore.Mvc;
using System.Data;
// 改這行 using
using Microsoft.Data.SqlClient;

using System.Text.Json;
using Swashbuckle.AspNetCore.Filters;

namespace prjwaters.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExecutionLogController : ControllerBase
    {
        private readonly IConfiguration _config;

        public ExecutionLogController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost("insert")]
        [SwaggerRequestExample(typeof(JsonElement), typeof(InsertLogExample))]
        public IActionResult InsertLog([FromBody] JsonElement jsonData)
        {
            var connStr = _config.GetConnectionString("DefaultConnection");

            using var conn = new SqlConnection(connStr);
            using var cmd = new SqlCommand("InsertExecutionLogByJson", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@json", jsonData.GetRawText());

            conn.Open();
            cmd.ExecuteNonQuery();

            return Ok(new { result = "Log inserted successfully" });
        }
        [HttpGet("all")]
        public IActionResult GetLogs()
        {
            var connStr = _config.GetConnectionString("DefaultConnection");
            var result = new List<object>();

            using var conn = new SqlConnection(connStr);
            using var cmd = new SqlCommand("SELECT * FROM dbo.MyOffice_ExecutionLog", conn);
            conn.Open();

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                result.Add(new
                {
                    DeLog_AutoID = reader.GetInt64(0),
                    DeLog_StoredPrograms = reader.GetString(1),
                    DeLog_GroupID = reader.GetGuid(2),
                    DeLog_isCustomDebug = reader.GetBoolean(3),
                    DeLog_ExecutionProgram = reader.GetString(4),
                    DeLog_ExecutionInfo = reader.IsDBNull(5) ? null : reader.GetString(5),
                    DeLog_verifyNeeded = reader.IsDBNull(6) ? (bool?)null : reader.GetBoolean(6),
                    DeLog_ExDateTime = reader.GetDateTime(7)    
                });
            }

            return Ok(result);
        }
        [HttpPost("update")]
        public IActionResult UpdateLog([FromBody] JsonElement jsonData)
        {
            var connStr = _config.GetConnectionString("DefaultConnection");
            using var conn = new SqlConnection(connStr);
            using var cmd = new SqlCommand("UpdateExecutionLogByJson", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@json", jsonData.GetRawText());

            conn.Open();
            cmd.ExecuteNonQuery();

            return Ok(new { message = "更新成功" });
        }

        [HttpPost("delete")]
        public IActionResult DeleteLog([FromBody] JsonElement jsonData)
        {
            var connStr = _config.GetConnectionString("DefaultConnection");
            using var conn = new SqlConnection(connStr);
            using var cmd = new SqlCommand("DeleteExecutionLogByJson", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@json", jsonData.GetRawText());

            conn.Open();
            cmd.ExecuteNonQuery();

            return Ok(new { message = "刪除成功" });
        }

    }
}
