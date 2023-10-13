namespace GameTracker.Forser.Controllers
{
    public class GameSessionsController : Controller
    {
        public IGameInfoRepository GameInfoRepository { get; }
        public IGameSessionsRepository Sessions { get; }

        public GameSessionsController(IGameInfoRepository gameInfoRepository, IGameSessionsRepository gameSessions)
        {
            GameInfoRepository = gameInfoRepository;
            Sessions = gameSessions;
        }
        public async Task<IActionResult> Index()
        {
            List<GameSession> gameSessions = await Sessions.GetAllGameSessionsAsync();
            
            return View(gameSessions);
        }
        public async Task<IActionResult> Create()
        {
            CreateGameSession viewModel = new CreateGameSession();
            viewModel.GameSession = new GameSession() { SessionStart = DateTime.Now, SessionEnd = DateTime.Now.AddDays(1) };
            viewModel.GameInfos = (await GameInfoRepository.GetAllAsync()).ToList()
                .Select(s => new SelectListItem { Text = s.GameTitle, Value = s.Id.ToString() }).ToList();

            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GameSession gameSession, int GameName)
        {
            if (ModelState.IsValid)
            {
                if (gameSession != null)
                {
                    gameSession.GameId = GameName;
                    Sessions.Create(gameSession);
                    await Sessions.SaveAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            
            return View(gameSession);
        }
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null || _context.GameSession == null)
        //    {
        //        return NotFound();
        //    }

        //    var gameSession = await _context.GameSession.FindAsync(id);
        //    if (gameSession == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["GamePlayedId"] = new SelectList(_context.Set<Game>(), "GameId", "GameTitle", gameSession.GamePlayedId);
        //    return View(gameSession);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("SessionId,SessionStart,SessionEnd,GamePlayedId")] GameSession gameSession)
        //{
        //    if (id != gameSession.SessionId)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(gameSession);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!GameSessionExists(gameSession.SessionId))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["GamePlayedId"] = new SelectList(_context.Set<Game>(), "GameId", "GameTitle", gameSession.GamePlayedId);
        //    return View(gameSession);
        //}

        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null || _context.GameSession == null)
        //    {
        //        return NotFound();
        //    }

        //    var gameSession = await _context.GameSession
        //        .Include(g => g.GamePlayed)
        //        .FirstOrDefaultAsync(m => m.SessionId == id);
        //    if (gameSession == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(gameSession);
        //}

        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    if (_context.GameSession == null)
        //    {
        //        return Problem("Entity set 'GTContext.GameSession'  is null.");
        //    }
        //    var gameSession = await _context.GameSession.FindAsync(id);
        //    if (gameSession != null)
        //    {
        //        _context.GameSession.Remove(gameSession);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool GameSessionExists(int id)
        //{
        //  return (_context.GameSession?.Any(e => e.SessionId == id)).GetValueOrDefault();
        //}
    }
}
