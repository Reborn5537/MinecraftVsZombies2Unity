﻿using MVZ2.GameContent.Detections;
using MVZ2.Vanilla.Detections;
using MVZ2.Vanilla.Enemies;
using MVZ2.Vanilla.Entities;
using MVZ2.Vanilla.Properties;
using PVZEngine;
using PVZEngine.Entities;
using PVZEngine.Level;
using UnityEngine;
using MVZ2.GameContent.Buffs.Enemies;
using MVZ2.GameContent.Damages;
using MVZ2.GameContent.Effects;
using MVZ2.GameContent.Models;
using MVZ2.Vanilla;
using MVZ2.Vanilla.Level;
using MVZ2Logic;
using PVZEngine.Damages;

namespace MVZ2.GameContent.Enemies
{
    [EntityBehaviourDefinition(VanillaEnemyNames.skeleton)]
    public class Skeleton : StateEnemy
    {
        public Skeleton(string nsp, string name) : base(nsp, name)
        {
            detector = new DispenserDetector()
            {
                ignoreHighEnemy = true,
                ignoreLowEnemy = true
            };
        }
        public override void Init(Entity entity)
        {
            base.Init(entity);
            var level = entity.Level;
            if (level.IsWaterLane(entity.GetLane()))
            {
                entity.AddBuff<BoatBuff>();
                entity.SetAnimationBool("HasBoat", true);
            }
        }
        protected override void UpdateAI(Entity enemy)
        {
            if (CanShoot(enemy))
            {
                if (enemy.Target != null && !ValidateTarget(enemy, enemy.Target))
                {
                    enemy.Target = null;
                }
                enemy.Target = FindTarget(enemy);
            }
            else
            {
                enemy.Target = null;
            }
            base.UpdateAI(enemy);
        }
        protected override void UpdateLogic(Entity entity)
        {
            base.UpdateLogic(entity);
            entity.SetAnimationFloat("BowBlend", 1 - Mathf.Pow(1 - GetBowPower(entity) / (float)BOW_POWER_MAX, 2));
            entity.SetAnimationBool("HasBoat", entity.HasBuff<BoatBuff>());
            entity.SetAnimationBool("ArrowVisible", !GetBowFired(entity));

            entity.SetAnimationInt("HealthState", entity.GetHealthState(2));
        }
        public static int GetBowPower(Entity enemy) => enemy.GetBehaviourField<int>(ID, PROP_BOW_POWER);
        public static bool GetBowFired(Entity enemy) => enemy.GetBehaviourField<bool>(ID, PROP_BOW_FIRED);
        public static void SetBowPower(Entity enemy, int value) => enemy.SetBehaviourField(ID, PROP_BOW_POWER, value);
        public static void SetBowFired(Entity enemy, bool value) => enemy.SetBehaviourField(ID, PROP_BOW_FIRED, value);
        protected override void UpdateStateWalk(Entity enemy)
        {
            base.UpdateStateWalk(enemy);
            SetBowFired(enemy, false);
            var bowPower = GetBowPower(enemy);
            if (bowPower > 0)
            {
                bowPower -= (int)(enemy.GetAttackSpeed() * BOW_POWER_RESTORE_SPEED);
                bowPower = Mathf.Max(bowPower, 0);
            }
            SetBowPower(enemy, bowPower);
        }
        protected override void UpdateStateAttack(Entity enemy)
        {
            base.UpdateStateAttack(enemy);
            RangedAttack(enemy, enemy.Target);
        }
        protected virtual bool CanShoot(Entity enemy)
        {
            return enemy.Position.x <= enemy.Level.GetEntityColumnX(enemy.Level.GetMaxColumnCount() - 1);
        }
        protected virtual Entity FindTarget(Entity entity)
        {
            var collider = detector.Detect(entity);
            return collider?.Entity;
        }
        protected virtual bool ValidateTarget(Entity entity, Entity target)
        {
            return detector.ValidateTarget(entity, target);
        }
        protected virtual void RangedAttack(Entity entity, Entity target)
        {
            bool bowFired = GetBowFired(entity);
            var bowPower = GetBowPower(entity);
            if (!bowFired)
            {
                bowPower += (int)(entity.GetAttackSpeed() * BOW_POWER_PULL_SPEED);
                if (bowPower >= BOW_POWER_MAX)
                {
                    bowPower = BOW_POWER_MAX;
                    SetBowFired(entity, true);

                    entity.ShootProjectile();
                }
            }
            else
            {
                bowPower -= (int)(entity.GetAttackSpeed() * BOW_POWER_RESTORE_SPEED);
                if (bowPower <= 0)
                {
                    bowPower = 0;
                    SetBowFired(entity, false);
                }
            }
            SetBowPower(entity, bowPower);
        }
        public override void PostDeath(Entity entity, DeathInfo info)
        {
            base.PostDeath(entity, info);
            if (entity.HasBuff<BoatBuff>())
            {
                entity.RemoveBuffs<BoatBuff>();
                // 掉落碎船掉落物
                var effect = entity.Level.Spawn(VanillaEffectID.brokenArmor, entity.GetCenter(), entity);
                effect.Velocity = new Vector3(effect.RNG.NextFloat() * 20 - 10, 5, 0);
                effect.ChangeModel(VanillaModelID.boatItem);
                effect.SetDisplayScale(entity.GetDisplayScale());
            }
         }
        private Detector detector;
        private static readonly NamespaceID ID = VanillaEnemyID.skeleton;
        public static readonly VanillaEntityPropertyMeta PROP_BOW_FIRED = new VanillaEntityPropertyMeta("bowFired");
        public static readonly VanillaEntityPropertyMeta PROP_BOW_POWER = new VanillaEntityPropertyMeta("bowPower");
        public const int BOW_POWER_PULL_SPEED = 130;//代号狙击手猎鹰
        public const int BOW_POWER_RESTORE_SPEED = 1000;
        public const int BOW_POWER_MAX = 10000;
    }
}
