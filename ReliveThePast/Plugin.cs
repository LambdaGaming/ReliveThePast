using System;
using EXILED;

namespace ReliveThePast
{
    public class Plugin : EXILED.Plugin
    {
        public bool EnableRelive;
        public override string getName { get; } = "Re-live The Past - By DefyTheRush";

        public EventHandlers Handler;

        public void ReloadConfig()
        {
            EnableRelive = Config.GetBool("enable_relive", true);
            if (!EnableRelive)
                Log.Info("Plugin disabled!");
            else
                Log.Info("Plugin enabled!");
        }
        public override void OnDisable()
        {
            Events.PlayerDeathEvent -= Handler.RunOnPlayerDeath;
            Handler = null;
        }

        public override void OnEnable()
        {
            ReloadConfig();
            if (!EnableRelive)
                return;

            Info("Starting up \"Re-live The Past - By DefyTheRush\"");
            Handler = new EventHandlers();
            Events.PlayerDeathEvent += Handler.RunOnPlayerDeath;
        }

        public override void OnReload()
        {
            Info("Reloading \"Re-live The Past\"!");
        }
    }
}
