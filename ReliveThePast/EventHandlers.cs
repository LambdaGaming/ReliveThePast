using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using MEC;
using PlayerRoles;
using Respawning;
using System;

namespace ReliveThePast
{
	public class EventHandlers
	{
		private Plugin plugin;
		public EventHandlers( Plugin plugin ) => this.plugin = plugin;
		Random randNum = new Random();
		bool SpawnWithKeycard = false;

		private void RevivePlayer( Player ply )
		{
			if ( ply.Role == RoleTypeId.Spectator && RespawnAllowed() )
			{
				int num = randNum.Next( 0, 2 );
				switch ( num )
				{
					case 0:
						ply.Role.Set( RoleTypeId.Scientist, reason: SpawnReason.Respawn );
						break;
					case 1:
						ply.Role.Set( RoleTypeId.ClassD, reason: SpawnReason.Respawn );
						if ( SpawnWithKeycard )
							ply.AddItem( plugin.Config.KeycardType );
						break;
				}
			}
		}

		private bool RespawnAllowed()
		{
			bool deconSoon = Map.DecontaminationState == DecontaminationState.Countdown || Map.DecontaminationState == DecontaminationState.Lockdown;
			return !Warhead.IsDetonated && !Map.IsLczDecontaminated && !Warhead.IsInProgress && !deconSoon && WaveManager.Waves.Count > 0;
		}

		public void OnPlayerDeath( DiedEventArgs ev )
		{
			Player hub = ev.Player;
			if ( RespawnAllowed() && plugin.Config.RespawnTimer > 0 )
				Timing.CallDelayed( plugin.Config.RespawnTimer, () => RevivePlayer( hub ) );
		}

		public void OnRoundStart()
		{
			if ( plugin.Config.KeycardDelay > 0 )
				Timing.CallDelayed( plugin.Config.KeycardDelay, () => SpawnWithKeycard = true );
		}
	}
}
