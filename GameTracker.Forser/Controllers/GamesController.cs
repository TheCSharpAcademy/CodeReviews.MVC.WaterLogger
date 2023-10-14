namespace GameTracker.Forser.Controllers
{
    public class GamesController : Controller
    {
        private IGameInfoRepository GameInfoRepository { get; }

        public GamesController(IGameInfoRepository gameInfoRepository)
        {
            GameInfoRepository = gameInfoRepository;
        }

        public async Task<IActionResult> Index()
        {
            List<GameInfo> games = await GameInfoRepository.GetAllGamesAsync();

            return View(games);
        }

        public async Task<IActionResult> Details(int? id)
        {
            var game = await GameInfoRepository.GetGameAsync(id);

            return View(game);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            var game = await GameInfoRepository.GetGameAsync(id);

            if (id == null || game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var game = await GameInfoRepository.GetGameAsync(id);

            if (game == null)
            {
                return Problem("Entity set 'Game' is null");
            }

            if (game != null)
            {
                await GameInfoRepository.DeleteGameAsync(game);
            }

            await GameInfoRepository.SaveAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind ("GameTitle", "GameDescription")] GameInfo gameInfo)
        {
            if (ModelState.IsValid)
            {
                if (gameInfo != null)
                {
                    GameInfoRepository.Create(gameInfo);
                    await GameInfoRepository.SaveAsync();
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(gameInfo);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            var game = await GameInfoRepository.GetGameAsync(id);

            if (id == null || game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        [HttpPost]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind ("Id")] int Id, [Bind ("GameTitle", "GameDescription")] GameInfo game)
        {
            var exisitingGame = await GameInfoRepository.GetGameAsync(Id);

            if (exisitingGame == null || game == null) 
            { 
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    exisitingGame.GameTitle = game.GameTitle;
                    exisitingGame.GameDescription = game.GameDescription;

                    await GameInfoRepository.SaveAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await GameExists(exisitingGame.Id) != true)
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

        private async Task<bool> GameExists(int id)
        {
            if (await GameInfoRepository.GetGameAsync (id) != null)
            {
                return true;
            }
            return false;
        }
    }
}
