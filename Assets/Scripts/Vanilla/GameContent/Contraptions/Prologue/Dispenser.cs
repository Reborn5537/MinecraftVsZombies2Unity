﻿using MVZ2.Vanilla.Entities;
using MVZ2.Vanilla.Properties;
using PVZEngine;
using PVZEngine.Entities;
using PVZEngine.Level;
using Tools;
using MVZ2.GameContent.Buffs.Armors;

namespace MVZ2.GameContent.Contraptions
{
    [EntityBehaviourDefinition(VanillaContraptionNames.dispenser)]
    public class Dispenser : DispenserFamily
    {
        public Dispenser(string nsp, string name) : base(nsp, name)
        {
        }

        public override void Init(Entity entity)
        {
            base.Init(entity);
            InitShootTimer(entity);
            var evocationTimer = new FrameTimer(115);
            SetEvocationTimer(entity, evocationTimer);
        }
        protected override void UpdateAI(Entity entity)
        {
            base.UpdateAI(entity);
            if (!entity.IsEvoked())
            {
                ShootTick(entity);
                return;
            }

            EvokedUpdate(entity);
        }

        protected override void OnEvoke(Entity entity)
        {
            base.OnEvoke(entity);
            var evocationTimer = GetEvocationTimer(entity);
            evocationTimer.Reset();
            entity.SetEvoked(true);
        }
        public static FrameTimer GetEvocationTimer(Entity entity) => entity.GetBehaviourField<FrameTimer>(ID, PROP_EVOCATION_TIMER);
        public static void SetEvocationTimer(Entity entity, FrameTimer timer) => entity.SetBehaviourField(ID, PROP_EVOCATION_TIMER, timer);
        private void EvokedUpdate(Entity entity)
        {
            var evocationTimer = GetEvocationTimer(entity);
            evocationTimer.Run();
            if (evocationTimer.PassedInterval(2))//等第四章出了再改
            {
                var projectile = Shoot(entity);
                projectile.Velocity *= 2;
            }
            if (evocationTimer.Expired)
            {
                entity.SetEvoked(false);
                var shootTimer = GetShootTimer(entity);
                shootTimer.Reset();
            }
        }
        private static readonly NamespaceID ID = VanillaContraptionID.dispenser;
        public static readonly VanillaEntityPropertyMeta PROP_EVOCATION_TIMER = new VanillaEntityPropertyMeta("EvocationTimer");
    }
}
