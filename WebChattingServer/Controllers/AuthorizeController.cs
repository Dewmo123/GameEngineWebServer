using BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebChattingServer.Controllers
{
    public class AuthorizeController : ControllerBase
    {
        private IPlayerService _playerService;
        public AuthorizeController(IPlayerService service)
        {
            _playerService = service;
        }

    }
}
