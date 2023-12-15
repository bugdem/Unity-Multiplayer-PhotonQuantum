using Quantum;
using UnityEngine;

public class CustomCallbacks : QuantumCallbacks 
{
	public RuntimePlayer PlayerData;

	public override void OnGameStart(Quantum.QuantumGame game)
	{
		// IsPaused is true when a player late joins or reconnects to a room.
		// This prevents the spawning of another player object when reconnecting.
		if (game.Session.IsPaused) return;

		foreach (var localPlayer in game.GetLocalPlayers())
		{
			Debug.Log("CustomCallbacks - sending player: " + localPlayer);
			game.SendPlayerData(localPlayer, PlayerData);
		}
	}

	public override void OnGameResync(Quantum.QuantumGame game)
	{
		// OnGameResync is called when a player reconnects.
		Debug.Log("Detected Resync. Verified tick: " + game.Frames.Verified.Number);
	}
}

