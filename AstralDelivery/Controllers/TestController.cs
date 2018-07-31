using Microsoft.AspNetCore.Mvc;

namespace AstralDelivery.Controllers
{
    [Route("Admin/Test")]
    public class TestController : Controller
    {
        public TestController()
        {
        }
        
        [HttpGet("Sum")]
        public int Sum([FromQuery] int firstInt, [FromQuery]int secondInt)
        {
            return firstInt + secondInt;
        }
        
        [HttpPost("Multiply")]
        public int Multiply([FromBody]TestModel model)
        {
            return model.FirstInt * model.SecondtInt;
        }
    }
}