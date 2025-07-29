using Microsoft.AspNetCore.Mvc;

namespace WebChattingServer.Controllers
{
    [Route("test")]
    public class TestController : Controller
    {
        public TestController()
        {
            Console.WriteLine("ASDASDASDASDADS");
        }
        public void Test()
        {
            Console.WriteLine("Test");
        }
    }
}
