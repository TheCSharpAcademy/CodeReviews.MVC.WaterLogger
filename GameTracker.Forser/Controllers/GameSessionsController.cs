namespace GameTracker.Forser.Controllers
{
    public class GameSessionsController : Controller
    {
        private IGameInfoRepository GameInfoRepository { get; }
        private IGameSessionsRepository GameSessionsRepository { get; }

        public GameSessionsController(IGameInfoRepository gameInfoRepository, IGameSessionsRepository gameSessionsRepository)
        {
            GameInfoRepository = gameInfoRepository;
            this.GameSessionsRepository = gameSessionsRepository;
        }

        public async Task<IActionResult> Index()
        {
            List<GameSession> gameSessions = await GameSessionsRepository.GetAllGameSessionsAsync();

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
                    GameSessionsRepository.Create(gameSession);
                    await GameSessionsRepository.SaveAsync();
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(gameSession);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            var gameSession = await GameSessionsRepository.GetGameSessionAsync(id);

            if (id == null || gameSession == null)
            {
                return NotFound();
            }

            return View(gameSession);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gameSession = await GameSessionsRepository.GetGameSessionAsync(id);

            if (gameSession == null)
            {
                return Problem("Entity set 'GameSession' is null.");
            }

            if (gameSession != null)
            {
                await GameSessionsRepository.DeleteGameSessionAsync(gameSession);
            }

            await GameSessionsRepository.SaveAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            var gameSession = await GameSessionsRepository.GetGameSessionAsync(id);

            if (id == null || gameSession == null)
            {
                return NotFound();
            }

            CreateGameSession viewModel = new CreateGameSession();
            viewModel.GameSession = gameSession;

            await PopulateSelectedGameDropDown(viewModel);

            return View(viewModel);
        }

        private async Task PopulateSelectedGameDropDown(CreateGameSession viewModel)
        {
            viewModel.GameInfos = (await GameInfoRepository.GetAllAsync()).ToList()
                .Select(r => new SelectListItem { Text = r.GameTitle, Value = r.Id.ToString(), Selected = false } ).ToList();

            var selectedGame = GameInfoRepository.GetSelectedGame(viewModel.GameSession.GameId);

            if (selectedGame != null)
            {
                foreach (var game in viewModel.GameInfos) 
                {
                    if (game.Text == selectedGame.GameTitle)
                    {
                        game.Selected = true;
                    }
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int Id, CreateGameSession viewModel, int allGames)
        {
            var exisitingGameSession = await GameSessionsRepository.GetGameSessionAsync(Id);

            if (exisitingGameSession == null || viewModel.GameSession == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    exisitingGameSession.SessionStart = viewModel.GameSession.SessionStart;
                    exisitingGameSession.SessionEnd = viewModel.GameSession.SessionEnd;
                    exisitingGameSession.GameId = allGames;

                    await GameSessionsRepository.SaveAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await GameSessionExists(exisitingGameSession.Id) != true)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> GameSessionExists(int id)
        {
            if (await GameSessionsRepository.GetGameSessionAsync(id) != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
