using MVZ2.GameContent.Enemies;
using PVZEngine.Level;

namespace MVZ2.Vanilla.Enemies
{
    [EntityBehaviourDefinition(VanillaEnemyNames.megaMutantZombie)]
    public class MegaMutantZombie : MutantZombieBase
    {
        public MegaMutantZombie(string nsp, string name) : base(nsp, name)
        {
        }
    }

}