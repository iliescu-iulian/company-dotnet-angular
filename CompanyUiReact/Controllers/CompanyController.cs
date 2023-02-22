using Company.DataSource;
using Company.DataSource.Core;
using Microsoft.AspNetCore.Mvc;

namespace CompanyUiReact.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
                return Ok(Dto.CompanyDataDto.Map(data));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }
    }
}