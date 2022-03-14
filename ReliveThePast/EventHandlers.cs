using Exiled.API.Features;
using Exiled.Events.EventArgs;
using MEC;
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
			Player hub = ev.Target;
			if ( RespawnAllowed() )
				Timing.CallDelayed( plugin.Config.RespawnTimer, () => RevivePlayer( hub ) );
		}

		public void RevivePlayer( Player ply )
		{
			if ( ply.Role == RoleType.Spectator && RespawnAllowed() )
			{
				int num = randNum.Next( 0, 2 );
				switch ( num )
				{
					case 0:
						ply.SetRole( RoleType.Scientist );
						if ( SpawnWithKeycard )
							ply.AddItem( plugin.Config.KeycardType );
						break;
					case 1:
						ply.SetRole( RoleType.ClassD );
						break;
				}
			}
		}

		public bool RespawnAllowed()
		{
			return !Warhead.IsDetonated && !Map.IsLczDecontaminated && !Warhead.IsInProgress && !DeconSoon;
		}

		public void OnRoundStart()
		{
			Timing.CallDelayed( 645f, () => DeconSoon = true );
			Timing.CallDelayed( plugin.Config.KeycardDelay, () => SpawnWithKeycard = true );
		}

		public void OnRoundEnd( RoundEndedEventArgs ev )
		{
			DeconSoon = false;
		}
	}
}
