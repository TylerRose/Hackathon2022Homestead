﻿using Homestead.Shared;
using System.Collections.Concurrent;

namespace Homestead.Server.SignalR;
public class GameLookup : IGameLookup
{
    ConcurrentDictionary<string, Game> _gameLookup = new();
    public IEnumerable<Game> ListGames { get => _gameLookup.Values.ToList(); }

    public Game? GetGame(string gameId)
    {
        _gameLookup.TryGetValue(gameId, out var game);
        return game;
    }

    public bool AddGame(Game game)
    {
        return _gameLookup.TryAdd(game.GameId, game);
    }

}
