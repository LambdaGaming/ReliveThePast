using System;
using EXILED;

namespace ReliveThePast
{
	public class Plugin : EXILED.Plugin
	{
		public bool EnableRelive;
		public static double ReliveRespawnTimer;
		public override string getName { get; } = "Re-live The Past - By DefyTheRush; Modified by LambdaGaming";

		public EventHandlers Handler;

		public void ReloadConfig()
		{
			EnableRelive = Config.GetBool( "relive_enable", true );
			if ( !EnableRelive )
				Log.Info( "Plugin disabled!" );
			else
			{
				Log.Info( "Plugin enabled!" );
				CheckRespawnValue();
			}
		}

		public void CheckRespawnValue()
		{
			try
			{
				ReliveRespawnTimer = Config.GetDouble( "relive_respawn_timer" );
			}
			catch ( Exception )
			{
				Log.Info( "Detected invalid value in the configuration file! Using default value of 0.05" );
			}
			finally
			{
				if ( ReliveRespawnTimer < 0.05 )
				{
					ReliveRespawnTimer = 0.05;
					Config.SetString( "relive_respawn_timer", ReliveRespawnTimer.ToString() );
					Log.Info( "ReliveThePast cannot use a value below 0.05 for respawning! Using default value of: " + ReliveRespawnTimer );
				}
			}
		}

		public override void OnDisable()
		{
			Events.RemoteAdminCommandEvent -= Handler.RunOnCommand;
			Events.PlayerDeathEvent -= Handler.RunOnPlayerDeath;
			Events.RoundRestartEvent -= Handler.RunOnRoundRestart;
			Handler = null;
		}

		public override void OnEnable()
		{
			ReloadConfig();
			if ( !EnableRelive )
				return;

			Log.Info( "Starting up \"Re-live The Past - By DefyTheRush; Modified by LambdaGaming\"" );
			Handler = new EventHandlers();
			Events.RoundRestartEvent += Handler.RunOnRoundRestart;
			Events.PlayerDeathEvent += Handler.RunOnPlayerDeath;
			Events.RemoteAdminCommandEvent += Handler.RunOnCommand;
		}

		public override void OnReload()
		{
			Log.Info( "Reloading \"Re-live The Past - By DefyTheRush; Modified by LambdaGaming\"!" );
		}
	}
}
