using System.ComponentModel;
using Exiled.API.Interfaces;

namespace ReliveThePast
{
	public sealed class Config : IConfig
	{
		[Description( "Indicates whether the plugin is enabled or not" )]
		public bool IsEnabled { get; set; } = true;

		[Description( "Time it takes in seconds for players to respawn after dying. (For instant respawn set to 0.05)" )]
		public float RespawnTimer { get; private set; } = 120f;
	}
}
