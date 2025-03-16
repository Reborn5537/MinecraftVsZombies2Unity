﻿using MVZ2.GameContent.Buffs.Contraptions;
using MVZ2.GameContent.Pickups;
using MVZ2.Vanilla.Audios;
using MVZ2.Vanilla.Entities;
using MVZ2.Vanilla.Level;
using MVZ2.Vanilla.Properties;
using MVZ2Logic.Level;
using PVZEngine;
using PVZEngine.Entities;
using PVZEngine.Level;
using Tools;
using UnityEngine;

namespace MVZ2.GameContent.Contraptions
{
    [EntityBehaviourDefinition(VanillaContraptionNames.furnace)]
    public class Furnace : ContraptionBehaviour
    {
        public Furnace(string nsp, string name) : base(nsp, name)
        {
        }
        public override void Init(Entity entity)
        {
            base.Init(entity);

            entity.AddBuff<ProductionColorBuff>();

            var productionTimer = new FrameTimer(entity.RNG.Next(90, 375));
            SetProductionTimer(entity, productionTimer);

            var evocationTimer = new FrameTimer(EVOCATION_DURATION);
            SetEvocationTimer(entity, evocationTimer);
        }
        protected override void UpdateAI(Entity entity)
        {
            base.UpdateAI(entity);
            if (entity.IsEvoked())
            {
                EvokedUpdate(entity);
            }
            else
            {
                ProductionUpdate(entity);
            }
        }
        protected override void UpdateLogic(Entity entity)
        {
            base.UpdateLogic(entity);
            bool frozen = entity.IsAIFrozen();
            entity.SetAnimationBool("Frozen", frozen);
            entity.SetLightSource(!frozen);
        }

        protected override void OnEvoke(Entity entity)
        {
            base.OnEvoke(entity);
            var evocationTimer = GetEvocationTimer(entity);
            evocationTimer.Reset();
            entity.SetEvoked(true);
        }
        public static FrameTimer GetProductionTimer(Entity entity)
        {
            return entity.GetBehaviourField<FrameTimer>(ID, PROP_PRODUCTION_TIMER);
        }
        public static void SetProductionTimer(Entity entity, FrameTimer timer)
        {
            entity.SetBehaviourField(ID, PROP_PRODUCTION_TIMER, timer);
        }
        public static FrameTimer GetEvocationTimer(Entity entity)
        {
            return entity.GetBehaviourField<FrameTimer>(ID, PROP_EVOCATION_TIMER);
        }
        public static void SetEvocationTimer(Entity entity, FrameTimer timer)
        {
            entity.SetBehaviourField(ID, PROP_EVOCATION_TIMER, timer);
        }
        private void ProductionUpdate(Entity entity)
        {
            var productionTimer = GetProductionTimer(entity);
            productionTimer.Run(entity.GetProduceSpeed());
            if (entity.Level.IsNoEnergy())
            {
                productionTimer.Frame = productionTimer.MaxFrame;
            }

            var buffs = entity.GetBuffs<ProductionColorBuff>();
            foreach (var buff in buffs)
            {
                var color = buff.GetProperty<Color>(ProductionColorBuff.PROP_COLOR);
                float colorValue = color.a;
                if (productionTimer.Frame < 30)
                {
                    colorValue = Mathf.Lerp(1, 0, productionTimer.Frame / 30f);
                }
                else
                {
                    colorValue = Mathf.Max(0, colorValue - 1 / 30f);
                }
                color.r = 1;
                color.g = 1;
                color.b = 1;
                color.a = colorValue;
                buff.SetProperty(ProductionColorBuff.PROP_COLOR, color);
            }
            if (productionTimer.Expired)
            {
                if (entity.IsFriendlyEntity())
                {
                    entity.Produce(VanillaPickupID.redstone);
                    entity.PlaySound(VanillaSoundID.throwSound);
                }
                else
                {
                    entity.Level.AddEnergy(-50);
                }
                productionTimer.ResetTime(720);
            }
        }
        private void EvokedUpdate(Entity entity)
        {
            var evocationTimer = GetEvocationTimer(entity);
            evocationTimer.Run();
            if (evocationTimer.PassedInterval(EVOCATION_INTERVAL))
            {
                entity.Produce(VanillaPickupID.redstone);//可能会有小红石?
                entity.PlaySound(VanillaSoundID.potion);
            }
            if (evocationTimer.Expired)
            {
                entity.SetEvoked(false);
            }
        }
        public const int EVOCATION_INTERVAL = 5;
        public const int EVOCATION_REDSTONES = 4;//还原GMS2版
        public const int EVOCATION_DURATION = EVOCATION_INTERVAL * EVOCATION_REDSTONES;
        private static readonly Color productionColor = new Color(0.5f, 0.5f, 0.5f, 0);
        private static readonly NamespaceID ID = VanillaContraptionID.furnace;
        private static readonly VanillaEntityPropertyMeta PROP_EVOCATION_TIMER = new VanillaEntityPropertyMeta("EvocationTimer");
        private static readonly VanillaEntityPropertyMeta PROP_PRODUCTION_TIMER = new VanillaEntityPropertyMeta("ProductionTimer");
    }
}
