using System;
using Company.DataSource;
using Company.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Company.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly IDataReader _dataReader;

        public CompanyController(IDataReader dataReader)
        {
            _dataReader = dataReader;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var data = _dataReader.ReadAll();
                return Ok(CompanyDataDto.Map(data));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }
    }
}
