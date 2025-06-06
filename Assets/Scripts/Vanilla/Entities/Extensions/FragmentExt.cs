﻿using MVZ2.GameContent.Damages;
using MVZ2.GameContent.Effects;
using MVZ2.Vanilla.Properties;
using PVZEngine;
using PVZEngine.Damages;
using PVZEngine.Entities;

namespace MVZ2.Vanilla.Entities
{
    public static class FragmentExt
    {
        private const string PROP_REGION = "fragments";
        #region 碎片
        public static void InitFragment(this Entity entity)
        {
            var fragment = entity.CreateFragment();
            var fragmentRef = new EntityID(fragment);
            entity.SetFragment(fragmentRef);
        }
        public static void UpdateFragment(this Entity entity)
        {
            var fragment = entity.GetOrCreateFragment();
            Fragment.AddEmitSpeed(fragment, entity.GetFragmentTickDamage());
            entity.SetFragmentTickDamage(0);
        }
        public static void PostFragmentDeath(this Entity entity, DeathInfo damageInfo)
        {
            if (damageInfo.Effects.HasEffect(VanillaDamageEffects.REMOVE_ON_DEATH))
                return;
            var fragment = entity.GetOrCreateFragment();
            Fragment.AddEmitSpeed(fragment, 500);
        }
        public static EntityID GetFragment(this Entity entity)
        {
            return entity.GetProperty<EntityID>(PROP_FRAGMENT);
        }
        public static void SetFragment(this Entity entity, EntityID value)
        {
            entity.SetProperty(PROP_FRAGMENT, value);
        }
        public static float GetFragmentTickDamage(this Entity entity)
        {
            return entity.GetProperty<float>(PROP_TICK_DAMAGE);
        }
        public static void SetFragmentTickDamage(this Entity entity, float value)
        {
            entity.SetProperty(PROP_TICK_DAMAGE, value);
        }
        public static void AddFragmentTickDamage(this Entity entity, float value)
        {
            entity.SetFragmentTickDamage(entity.GetFragmentTickDamage() + value);
        }
        public static Entity CreateFragment(this Entity entity)
        {
            var fragment = entity.Level.Spawn(VanillaEffectID.fragment, entity.Position, entity);
            fragment.SetParent(entity);
            Fragment.UpdateFragmentID(fragment);
            return fragment;
        }
        public static Entity GetOrCreateFragment(this Entity entity)
        {
            var fragmentRef = entity.GetFragment();
            var fragment = fragmentRef?.GetEntity(entity.Level);
            if (fragment == null || !fragment.Exists())
            {
                fragment = entity.CreateFragment();
                fragmentRef = new EntityID(fragment);
                entity.SetFragment(fragmentRef);
            }
            return fragment;
        }
        [PropertyRegistry(PROP_REGION)]
        public static readonly VanillaEntityPropertyMeta PROP_FRAGMENT = new VanillaEntityPropertyMeta("Fragment");
        [PropertyRegistry(PROP_REGION)]
        public static readonly VanillaEntityPropertyMeta PROP_TICK_DAMAGE = new VanillaEntityPropertyMeta("TickDamage");
        #endregion

        #region 治疗粒子
        public static Entity CreateHealParticles(this Entity entity)
        {
            var particles = entity.Level.Spawn(VanillaEffectID.healParticles, entity.Position, entity);
            particles.SetParent(entity);
            var fragmentRef = new EntityID(particles);
            entity.SetHealParticles(fragmentRef);
            return particles;
        }
        public static void UpdateHealParticles(this Entity entity)
        {
            var healing = entity.GetTickHealing();
            if (healing > 0)
            {
                var fragment = entity.GetOrCreateHealParticles();
                HealParticles.AddEmitSpeed(fragment, entity.GetTickHealing() * 0.4f);
                entity.SetTickHealing(0);
            }
        }
        public static Entity GetOrCreateHealParticles(this Entity entity)
        {
            var reference = entity.GetHealParticles();
            var particles = reference?.GetEntity(entity.Level);
            if (particles == null || !particles.Exists())
            {
                particles = entity.CreateHealParticles();
            }
            return particles;
        }
        public static float GetTickHealing(this Entity entity)
        {
            return entity.GetProperty<float>(PROP_TICK_HEALING);
        }
        public static void SetTickHealing(this Entity entity, float value)
        {
            entity.SetProperty(PROP_TICK_HEALING, value);
        }
        public static void AddTickHealing(this Entity entity, float value)
        {
            entity.SetTickHealing(entity.GetTickHealing() + value);
        }
        public static EntityID GetHealParticles(this Entity entity)
        {
            return entity.GetProperty<EntityID>(PROP_HEALING_PARTICLES);
        }
        public static void SetHealParticles(this Entity entity, EntityID value)
        {
            entity.SetProperty(PROP_HEALING_PARTICLES, value);
        }
        [PropertyRegistry(PROP_REGION)]
        public static readonly VanillaEntityPropertyMeta PROP_HEALING_PARTICLES = new VanillaEntityPropertyMeta("HealingParticles");
        [PropertyRegistry(PROP_REGION)]
        public static readonly VanillaEntityPropertyMeta PROP_TICK_HEALING = new VanillaEntityPropertyMeta("TickHealing");
        #endregion
    }
}
