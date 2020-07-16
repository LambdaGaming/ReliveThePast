using Exiled.API.Enums;
using Exiled.API.Features;
using events = Exiled.Events.Handlers;

namespace ReliveThePast
{
	public class Plugin : Plugin<Config>
	{
		private EventHandlers EventHandlers;

		public override PluginPriority Priority { get; } = PluginPriority.Medium;

		public override void OnEnabled()
		{
			base.OnEnabled();
			Log.Info( "Starting up \"Re-live The Past - By DefyTheRush; Modified by LambdaGaming\"" );
			EventHandlers = new EventHandlers( this );
			events.Player.Died += EventHandlers.RunOnPlayerDeath;
			events.Server.SendingRemoteAdminCommand += EventHandlers.RunOnCommand;
			Log.Info( $"Successfully loaded." );
		}

		public override void OnDisabled()
		{
			base.OnDisabled();
			events.Player.Died -= EventHandlers.RunOnPlayerDeath;
			events.Server.SendingRemoteAdminCommand -= EventHandlers.RunOnCommand;
			EventHandlers = null;
		}
	}
}
