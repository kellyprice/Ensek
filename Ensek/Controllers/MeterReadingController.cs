using Ensek.Library;
using Ensek.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Ensek.Controllers
{
    [Route("")]
    [ApiController]
    public class MeterReadingController : ControllerBase
    {
        private readonly MeterReading _meterReading;

        public MeterReadingController(MeterReading meterReading)
        {
            _meterReading = meterReading;
        }

        [HttpPost]
        [Route("meter-reading-uploads")]
        public ActionResult<IEnumerable<MeterReadingViewModel>> Post([FromForm] FileViewModel file)
        {
            try
            {
                return Ok(_meterReading.Process(file));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
