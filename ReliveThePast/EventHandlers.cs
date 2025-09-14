using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using Exiled.Events.EventArgs.Server;
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
		bool DeconSoon = false;
		bool SpawnWithKeycard = false;

		public void RunOnPlayerDeath( DiedEventArgs ev )
		{
			Player hub = ev.Player;
			if ( RespawnAllowed() && plugin.Config.RespawnTimer > 0 )
				Timing.CallDelayed( plugin.Config.RespawnTimer, () => RevivePlayer( hub ) );
		}

		public void RevivePlayer( Player ply )
		{
			if ( ply.Role == RoleTypeId.Spectator && RespawnAllowed() )
			{
				int num = randNum.Next( 0, 2 );
				switch ( num )
				{
					case 0:
						ply.Role.Set( RoleTypeId.Scientist );
						break;
					case 1:
						ply.Role.Set( RoleTypeId.ClassD );
						if ( SpawnWithKeycard )
							ply.AddItem( plugin.Config.KeycardType );
						break;
				}
			}
		}

		public bool RespawnAllowed()
		{
			return !Warhead.IsDetonated && !Map.IsLczDecontaminated && !Warhead.IsInProgress && !DeconSoon && WaveManager.Waves.Count > 0;
		}

		public void OnRoundStart()
		{
			Timing.CallDelayed( 645f, () => DeconSoon = true );
			if ( plugin.Config.KeycardDelay > 0 )
				Timing.CallDelayed( plugin.Config.KeycardDelay, () => SpawnWithKeycard = true );
		}

		public void OnRoundEnd( RoundEndedEventArgs ev )
		{
			DeconSoon = false;
		}
	}
}
