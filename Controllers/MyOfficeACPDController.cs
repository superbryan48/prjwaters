using System.Data;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace prjwaters.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MyOfficeACPDController : ControllerBase
    {
        private readonly IConfiguration _config;
        public MyOfficeACPDController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {

            string connStr = _config.GetConnectionString("DefaultConnection");
            using var conn = new SqlConnection(connStr);
            using var cmd = new SqlCommand("GetAll_MyOffice_ACPD_JSON", conn);
            cmd.CommandType = CommandType.StoredProcedure;
        

            conn.Open();
           
            var json = cmd.ExecuteScalar()?.ToString();
            return Content(json ?? "[]", "application/json");
        }

        [HttpPost("insert")]
        public IActionResult Insert([FromBody] JsonElement jsonData)
        {
            return ExecuteSpWithJson("Insert_MyOffice_ACPD_JSON", jsonData);
        }

        [HttpPost("update")]
        public IActionResult Update([FromBody] JsonElement jsonData)
        {
            return ExecuteSpWithJson("Update_MyOffice_ACPD_JSON", jsonData);
        }

        [HttpPost("delete")]
        public IActionResult Delete([FromBody] JsonElement jsonData)
        {
            return ExecuteSpWithJson("Delete_MyOffice_ACPD_JSON", jsonData);
        }

        private IActionResult ExecuteSpWithJson(string spName, JsonElement jsonData)
        {
            string connStr = _config.GetConnectionString("DefaultConnection");
            using var conn = new SqlConnection(connStr);
            using var cmd = new SqlCommand(spName, conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@json", jsonData.GetRawText());
            conn.Open();
            cmd.ExecuteNonQuery();
            return Ok(new { status = "success" });
        }
    }

   
}
