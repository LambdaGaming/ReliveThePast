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
		bool AllowRespawning = true;
		bool DeconSoon = false;

		public void RunOnPlayerDeath( DiedEventArgs ev )
		{
			Player hub = ev.Target;
			if ( CheckAllowed() )
				Timing.CallDelayed( plugin.Config.RespawnTimer, () => RevivePlayer( hub ) );
		}

		public void RunOnCommand( SendingRemoteAdminCommandEventArgs ev )
		{
			try
			{
				string arg = ev.Name;
				Player sender = ev.Sender;
				if ( arg.ToLower() == "allowautorespawn" )
				{
					if ( ev.IsAllowed )
					{
						sender.RemoteAdminMessage( "You are not authorized to use this command" );
						return;
					}
					if ( !AllowRespawning )
					{
						sender.RemoteAdminMessage( "Auto respawning enabled!" );
						Map.Broadcast( 5, "<color=green>Auto respawning enabled!</color>" );
						AllowRespawning = true;
					}
					else
					{
						sender.RemoteAdminMessage( "Auto respawning disabled!" );
						Map.Broadcast( 5, "<color=red>Auto respawning disabled!</color>" );
						AllowRespawning = false;
					}
				}
			}
			catch ( Exception )
			{
				Log.Info( "There was an error handling this command" );
			}
		}

		public void RevivePlayer( Player ply )
		{
			if ( ply.Role != RoleType.Spectator || !CheckAllowed() ) return;
			int num = randNum.Next( 0, 2 );
			switch ( num )
			{
				case 0:
					ply.SetRole( RoleType.Scientist );
					break;
				case 1:
					ply.SetRole( RoleType.ClassD );
					break;
			}
		}

		public bool CheckAllowed()
		{
			if ( AllowRespawning && !Warhead.IsDetonated && !Map.IsLCZDecontaminated && !Warhead.IsInProgress && !DeconSoon )
				return true;
			return false;
		}

		public void OnRoundStart()
		{
			Timing.CallDelayed( 645f, () => DeconSoon = true );
		}

		public void OnRoundEnd( RoundEndedEventArgs ev )
		{
			DeconSoon = false;
		}
	}
}
