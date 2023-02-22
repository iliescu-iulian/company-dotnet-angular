using Company.DataSource;
using Company.DataSource.Core;
using Company.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Company.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly IDataSource<CompanyData> _dataSource;

        public CompanyController(IDataSource<CompanyData> dataSource)
        {
            _dataSource = dataSource;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var data = _dataSource.Data;
                return Ok(CompanyDataDto.Map(data));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }
    }
}
