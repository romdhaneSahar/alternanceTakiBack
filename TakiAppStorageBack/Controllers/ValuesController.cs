using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Data;
using System.Data.SqlClient;
using TakiAppStorageBack.models;

namespace TakiAppStorageBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class allFilesController : ControllerBase
    {


        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        public allFilesController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;

        }
        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
            select idFile,fileName,fileDate,fileSize,state
             
                     from dbo.files";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("TakiAppStorageBack");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(table);
        }

  
       
       
        
       
    }
}

