using BLL.Caching;
﻿using BLL.DTOs;
﻿using BLL.Services.Players;
﻿using Microsoft.AspNetCore.Authorization;
﻿using Microsoft.AspNetCore.Mvc;
﻿using System.Security.Claims;
﻿
﻿namespace WebChattingServer.Controllers
﻿{
﻿    [Authorize]
﻿    [ApiController]
﻿    [Route("player/stage")]
﻿    public class PlayerStageController : ControllerBase
﻿    {
﻿        private readonly IPlayerManager _playerManager;
﻿        private readonly IPlayerChapterService _playerChapterService;
﻿        public PlayerStageController(IPlayerManager manager, IPlayerChapterService playerChapterService)
﻿        {
﻿            _playerManager = manager;
﻿            _playerChapterService = playerChapterService;
﻿        }
﻿        [HttpPost("changed")]
﻿        public IActionResult StageChanged(ChapterDTO chapter)
﻿        {
﻿            string? id = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
﻿            if (!string.IsNullOrEmpty(id) && int.TryParse(id, out int val))
﻿            {
﻿                Player player = _playerManager.GetPlayer(val);
﻿                bool success = _playerChapterService.ChapterChanged(player, chapter.Chapter, chapter.Stage);
﻿                return success ? Ok() : BadRequest();
﻿            }
﻿            return Unauthorized();
﻿        }
﻿        [HttpPost("enemy-dead")]
﻿        public IActionResult EnemyDead(EnemyDeadDTO enemyDead)
﻿        {
﻿            string? id = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
﻿            if (!string.IsNullOrEmpty(id) && int.TryParse(id, out int val))
﻿            {
﻿                Player player = _playerManager.GetPlayer(val);
﻿                _playerChapterService.EnemyDead(player, enemyDead.EnemyCount);
﻿                return Ok();
﻿            }
﻿            return Unauthorized();
﻿        }
﻿    }
﻿}
﻿
