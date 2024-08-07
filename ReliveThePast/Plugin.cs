using Exiled.API.Enums;
using Exiled.API.Features;
using System;
using events = Exiled.Events.Handlers;

namespace ReliveThePast
{
	public class Plugin : Plugin<Config>
	{
		private EventHandlers EventHandlers;
		public override Version Version { get; } = new Version( 2, 4, 3 );
		public override Version RequiredExiledVersion { get; } = new Version( 8, 0, 0 );
		public override string Author { get; } = "OPGman";
		public override PluginPriority Priority { get; } = PluginPriority.Medium;

		public override void OnEnabled()
		{
			base.OnEnabled();
			EventHandlers = new EventHandlers( this );
			events.Player.Died += EventHandlers.RunOnPlayerDeath;
			events.Server.RoundStarted += EventHandlers.OnRoundStart;
			events.Server.RoundEnded += EventHandlers.OnRoundEnd;
		}

		public override void OnDisabled()
		{
			base.OnDisabled();
			events.Player.Died -= EventHandlers.RunOnPlayerDeath;
			events.Server.RoundStarted -= EventHandlers.OnRoundStart;
			events.Server.RoundEnded -= EventHandlers.OnRoundEnd;
			EventHandlers = null;
		}
	}
}
