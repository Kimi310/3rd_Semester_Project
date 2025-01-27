﻿using API.Exceptions;
using DataAccess.Interfaces;
using DataAccess.Models;
using DataAccess.Types.Enums;
using Service.Services.Interfaces;
using Service.TransferModels.Requests;
using Service.TransferModels.Responses;

namespace Service.Services;

public class GameService(IGameRepository gameRepository) : IGameService
{
    public GameResponseDTO NewGameFromMonday(int prize)
    {
        var game = new Game();

        // Get the current date
        var today = DateTime.Now;

        // Calculate the last Monday
        int daysSinceMonday = (int)today.DayOfWeek - (int)DayOfWeek.Monday;
        if (daysSinceMonday < 0)
        {
            daysSinceMonday += 7; // Adjust for Sundays
        }
        var lastMonday = today.AddDays(-daysSinceMonday);

        // Set the game's date to the last Monday
        game.Id = Guid.NewGuid();
        game.Date = DateOnly.FromDateTime(lastMonday);
        game.Prizepool = prize;
        game.StartingPrizepool = prize;
        game.Status = GameStatus.Active;
        DateTime now = DateTime.Now;
        int daysUntilSaturday = ((int)DayOfWeek.Saturday - (int)now.DayOfWeek + 7) % 7;
        DateTime nextSaturday = now.AddDays(daysUntilSaturday).Date.AddHours(17); // Add 17:00 (5 PM)
        game.Enddate = nextSaturday;
        var activeGame = gameRepository.GetActiveGame();
        if (activeGame != null)
        {
            activeGame.Status = GameStatus.Inactive;
        }
        
        return GameResponse(gameRepository.NewGame(game,activeGame));
    }
    
    public GameResponseDTO NewGame(decimal prize)
    {
        var game = new Game();
        
        game.Id = Guid.NewGuid();
        game.Date = DateOnly.FromDateTime(DateTime.Now);
        DateTime now = DateTime.Now;
        int daysUntilSaturday = ((int)DayOfWeek.Saturday - (int)now.DayOfWeek + 7) % 7;
        DateTime nextSaturday = now.AddDays(daysUntilSaturday).Date.AddHours(17); // Add 17:00 (5 PM)
        game.Enddate = nextSaturday;
        game.Prizepool = prize;
        game.StartingPrizepool = prize;
        game.Status = GameStatus.Active;
        
        var activeGame = gameRepository.GetActiveGame();
        if (activeGame != null)
        {
            activeGame.Status = GameStatus.Inactive;
        }
        
        return GameResponse(gameRepository.NewGame(game,activeGame));
    }

    public bool IsAnyGame()
    {
        var activeGame = gameRepository.GetActiveGame();
        if (activeGame != null)
        {
            return true;
        }
        return false;
    }
    
    public WinningNumbersResponseDTO SetWinningNumbers (WinningNumbersRequestDTO data) {
        var game = gameRepository.GetActiveGame();
        if (game == null || game.Id != data.GameId)
        {
            throw new ErrorException("Game", "Game not found or not active.");
        }
        
        var winningNumbersEntities = data.WinningNumbers.Select(num => new WinningNumbers
        {
            Id = Guid.NewGuid(),
            GameId = data.GameId,
            Number = num
        }).ToList();

        gameRepository.AddWinningNumbers(winningNumbersEntities);
        
        var winningNumbersList = winningNumbersEntities.Select(e => e.Number).ToList();

        return WinningNumbersResponseDTO.FromGame(game, winningNumbersList);
    }

    public void UpdatePrizePool(decimal newPrizePool)
    {
        gameRepository.UpdatePrizePool(newPrizePool);
    }

    public List<GameResponseDTO> GetAllGames()
    {
        var games = gameRepository.GetAllGames();
        var gamesDto = games.Select(g => GameResponse(g)).ToList();
        return gamesDto;
    }

    public GameResponseDTO GetActiveGame()
    {
        var game = gameRepository.GetActiveGame();
        return GameResponse(game);
    }
    
    
    private GameResponseDTO GameResponse(Game game)
    {
        var winningNumbers = GetWinningNumbers(game.Id);
        return new GameResponseDTO().FromGame(game, winningNumbers.Select(e => e.Number).ToList());
    }
    

    public GameResponseDTO getGameById(Guid gameId)
    {
        var game = gameRepository.GetGameById(gameId);
        var winningNumbers = GetWinningNumbers(gameId);
        return new GameResponseDTO().FromGame(game, winningNumbers.Select(e => e.Number).ToList());
    }

    public List<WinningNumbers> GetWinningNumbers(Guid gameId)
    {
        var winningNumbers = gameRepository.GetWinningNumbers(gameId);
        return winningNumbers;
    }


}