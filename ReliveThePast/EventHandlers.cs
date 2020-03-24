using System;
using EXILED;
using MEC;

namespace ReliveThePast
{
    public class EventHandlers
    {
        Random randNum = new Random();
        public void RunOnPlayerDeath(ref PlayerDeathEvent d)
        {
            ReferenceHub hub = d.Player;
            Timing.CallDelayed(0.05f, () => RevivePlayer(hub));
        }

        public void RevivePlayer(ReferenceHub rh)
        {
            int num = randNum.Next(0, 7);

            switch (num)
            {
                case 0:
                    rh.characterClassManager.SetPlayersClass(RoleType.NtfCadet, rh.gameObject);
                    break;
                case 1:
                    rh.characterClassManager.SetPlayersClass(RoleType.ClassD, rh.gameObject);
                    break;
                case 2:
                    rh.characterClassManager.SetPlayersClass(RoleType.FacilityGuard, rh.gameObject);
                    break;
                case 3:
                    rh.characterClassManager.SetPlayersClass(RoleType.NtfLieutenant, rh.gameObject);
                    break;
                case 4:
                    rh.characterClassManager.SetPlayersClass(RoleType.NtfCadet, rh.gameObject);
                    break;
                case 5:
                    rh.characterClassManager.SetPlayersClass(RoleType.ChaosInsurgency, rh.gameObject);
                    break;
                case 6:
                    rh.characterClassManager.SetPlayersClass(RoleType.Scientist, rh.gameObject);
                    break;
                case 7:
                    rh.characterClassManager.SetPlayersClass(RoleType.NtfCommander, rh.gameObject);
                    break;
            }
        }
    }
}
