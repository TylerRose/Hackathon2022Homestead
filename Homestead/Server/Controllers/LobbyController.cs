﻿using Homestead.Server.SignalR;
using Homestead.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Homestead.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class LobbyController
{
	private readonly CommunicationHub hub;
	private readonly IGameLookup lookup;

	public LobbyController(CommunicationHub hub, IGameLookup lookup)
	{
		this.hub = hub;
		this.lookup = lookup;
	}

	[HttpPost("/Join")]
	public async Task<int> JoinGame(string gameId)
	{
		var game = lookup.GetGame(gameId);
		int playerNumber;

		lock (game)
		{
			var player = game.Players.FirstOrDefault(x => x.IsBot);

			if (player is null)
			{
				return 0;
			}

			player.IsBot = false;
			playerNumber = player.PlayerNumber;
		}

		await hub.Groups.AddToGroupAsync(hub.Context.ConnectionId, gameId);
		return playerNumber;
	}

	[HttpGet("/Create")]
	public async Task<int> CreateGame([FromServices] GameEngine engine)
	{
		var game = engine.Start();

		// add game to lookup.
		//lookup

		await hub.Groups.AddToGroupAsync(hub.Context.ConnectionId, game.GameId);
		return 1;
	}

	public async Task<IEnumerable<string>> GetOpenGames() 
	{
        lookup.

    }
}
