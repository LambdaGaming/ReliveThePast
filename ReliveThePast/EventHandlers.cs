using System;
using EXILED;
using EXILED.Extensions;
using MEC;

namespace ReliveThePast
{
	public class EventHandlers
	{
		Random randNum = new Random();
		float RespawnTimerValue = ( float ) Plugin.ReliveRespawnTimer;
		bool IsWarheadDetonated;
		bool IsDecontanimationActivated;
		bool IsWarheadInProgress;
		bool AllowRespawning = true;

		public void RunOnPlayerDeath( ref PlayerDeathEvent d )
		{
			ReferenceHub hub = d.Player;
			IsWarheadDetonated = Map.IsNukeDetonated;
			IsDecontanimationActivated = Map.IsLCZDecontaminated;
			IsWarheadInProgress = Map.IsNukeInProgress;
			if ( CheckAllowed() )
				Timing.CallDelayed( RespawnTimerValue, () => RevivePlayer( hub ) );
		}

		public void RunOnCommand( ref RACommandEvent r )
		{
			try
			{
				string arg = r.Command;
				ReferenceHub sender = r.Sender.SenderId == "SERVER CONSOLE" || r.Sender.SenderId == "GAME CONSOLE" ? PlayerManager.localPlayer.GetPlayer() : Player.GetPlayer( r.Sender.SenderId );

				if ( arg.ToLower() == "allowautorespawn" )
				{
					r.Allow = false;
					if ( !sender.CheckPermission( "rtp.allow" ) )
					{
						r.Sender.RAMessage( "You are not authorized to use this command" );
						return;
					}
					if ( !AllowRespawning )
					{
						r.Sender.RAMessage( "Auto respawning enabled!" );
						Map.Broadcast( "<color=green>Auto respawning enabled!</color>", 5 );
						AllowRespawning = true;
					}
					else
					{
						r.Sender.RAMessage( "Auto respawning disabled!" );
						Map.Broadcast( "<color=red>Auto respawning disabled!</color>", 5 );
						AllowRespawning = false;
					}
				}
			}
			catch ( Exception )
			{
				Log.Info( "There was an error handling this command" );
			}
		}

		public void RevivePlayer( ReferenceHub rh )
		{
			if ( rh.GetRole() != RoleType.Spectator || !CheckAllowed() ) return;
			int num = randNum.Next( 0, 2 );
			switch ( num )
			{
				case 0:
					rh.characterClassManager.SetPlayersClass( RoleType.Scientist, rh.gameObject );
					break;
				case 1:
					rh.characterClassManager.SetPlayersClass( RoleType.ClassD, rh.gameObject );
					break;
			}
		}

		public bool CheckAllowed()
		{
			IsWarheadDetonated = Map.IsNukeDetonated;
			IsDecontanimationActivated = Map.IsLCZDecontaminated;
			IsWarheadInProgress = Map.IsNukeInProgress;
			if ( AllowRespawning && !IsWarheadDetonated && !IsDecontanimationActivated && !IsWarheadInProgress )
				return true;
			return false;
		}
	}
}
