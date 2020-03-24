using System;
using EXILED;
using EXILED.Extensions;
using MEC;

namespace ReliveThePast
{
    public class EventHandlers
    {
        Random randNum = new Random();
        float RespawnTimerValue = (float) Plugin.ReliveRespawnTimer;
        bool IsWarheadDetonated;
        bool IsDecontanimationActivated;

        public void RunOnPlayerDeath(ref PlayerDeathEvent d)
        {
            ReferenceHub hub = d.Player;
            IsWarheadDetonated = Map.IsNukeDetonated;
            IsDecontanimationActivated = Map.IsLCZDecontaminated;
            Timing.CallDelayed(RespawnTimerValue, () => RevivePlayer(hub));
        }

        public void RevivePlayer(ReferenceHub rh)
        {
            int num = randNum.Next(0, 8);

            switch (num)
            {
                case 0:
                    rh.characterClassManager.SetPlayersClass(RoleType.NtfCadet, rh.gameObject);
                    break;
                case 1:  
                    if (!IsWarheadDetonated && !IsDecontanimationActivated)
                    {
                        rh.characterClassManager.SetPlayersClass(RoleType.ClassD, rh.gameObject);
                        return;
                    }
                    if (!IsWarheadDetonated && IsDecontanimationActivated)
                    {
                        rh.characterClassManager.SetPlayersClass(RoleType.ChaosInsurgency, rh.gameObject);
                        return;
                    }
                    if (IsWarheadDetonated || IsDecontanimationActivated)
                        rh.characterClassManager.SetPlayersClass(RoleType.ChaosInsurgency, rh.gameObject);
                    break;
                case 2:
                    if (!IsWarheadDetonated)
                    {
                        rh.characterClassManager.SetPlayersClass(RoleType.FacilityGuard, rh.gameObject);
                        return;
                    }
                    rh.characterClassManager.SetPlayersClass(RoleType.NtfCommander, rh.gameObject);
                    break;
                case 3:
                    rh.characterClassManager.SetPlayersClass(RoleType.NtfLieutenant, rh.gameObject);
                    break;
                case 4:
                    rh.characterClassManager.SetPlayersClass(RoleType.NtfScientist, rh.gameObject);
                    break;
                case 5:
                    rh.characterClassManager.SetPlayersClass(RoleType.ChaosInsurgency, rh.gameObject);
                    break;
                case 6:
                    if (!IsWarheadDetonated && !IsDecontanimationActivated)
                    {
                        rh.characterClassManager.SetPlayersClass(RoleType.Scientist, rh.gameObject);
                        return;
                    }
                    if (!IsWarheadDetonated && IsDecontanimationActivated)
                    {
                        rh.characterClassManager.SetPlayersClass(RoleType.NtfLieutenant, rh.gameObject);
                        return;
                    }
                    if (IsWarheadDetonated || IsDecontanimationActivated)
                        rh.characterClassManager.SetPlayersClass(RoleType.NtfLieutenant, rh.gameObject);
                    break;
                case 7:
                    rh.characterClassManager.SetPlayersClass(RoleType.NtfCommander, rh.gameObject);
                    break;
                case 8:
                    rh.characterClassManager.SetPlayersClass(RoleType.Spectator, rh.gameObject);
                    break;
            }
        }
    }
}
