using System.ComponentModel;
using Exiled.API.Interfaces;

namespace ReliveThePast
{
	public sealed class Config : IConfig
	{
		[Description( "Indicates whether the plugin is enabled or not" )]
		public bool IsEnabled { get; set; } = true;

		[Description( "Whether or not debug messages should be shown in the console." )]
		public bool Debug { get; set; } = false;

		[Description( "Time it takes in seconds for players to respawn after dying. For instant respawn set to 0.05." )]
		public float RespawnTimer { get; set; } = 120f;

		[Description( "Time it takes in seconds before respawned Class D's spawn with a keycard. Set to 0 to disable." )]
		public float KeycardDelay { get; set; } = 300f;

		[Description( "Type of keycard that Class D's spawn with after the set time." )]
		public ItemType KeycardType { get; set; } = ItemType.KeycardJanitor;
	}
}
