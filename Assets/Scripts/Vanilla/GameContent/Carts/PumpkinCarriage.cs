using MVZ2.Vanilla.Entities;
using PVZEngine.Level;

namespace MVZ2.GameContent.Carts
{
    [EntityBehaviourDefinition(VanillaCartNames.pumpkinCarriage)]
    public class PumpkinCarriage : CartBehaviour
    {
        public PumpkinCarriage(string nsp, string name) : base(nsp, name)
        {
        }
    }
}