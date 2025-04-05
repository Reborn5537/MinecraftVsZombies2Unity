﻿using System.Collections.Generic;
using PVZEngine;
using PVZEngine.Entities;

namespace MVZ2.Vanilla.Entities
{
    [PropertyRegistryRegion]
    public static class VanillaProjectileProps
    {
        public static readonly PropertyMeta ROLLS = new PropertyMeta("rolls");
        public static bool Rolls(this Entity entity) => entity.GetProperty<bool>(ROLLS);
        public static readonly PropertyMeta KILL_ON_GROUND = new PropertyMeta("killOnGround");
        public static bool KillOnGround(this Entity entity) => entity.GetProperty<bool>(KILL_ON_GROUND); 
        public static readonly PropertyMeta PIERCING = new PropertyMeta("piercing");
        public static readonly PropertyMeta POINT_TO_DIRECTION = new PropertyMeta("pointToDirection");
        public static readonly PropertyMeta DAMAGE_EFFECTS = new PropertyMeta("damageEffects");
        public static readonly PropertyMeta NO_DESTROY_OUTSIDE_LAWN = new PropertyMeta("noDestroyOutsideLawn");
        public static readonly PropertyMeta COLLIDING_ENTITIES = new PropertyMeta("collidingEntities");
        public static readonly PropertyMeta NO_HIT_ENTITIES = new PropertyMeta("noHitEntities");
        public static readonly PropertyMeta TRACKING_SOURCE = new PropertyMeta("trackingSource");
        public static readonly PropertyMeta TRACKING_ANGLE = new PropertyMeta("trackingAngle");
        public static readonly PropertyMeta TRACKING_RANGE = new PropertyMeta("trackingRange");
        public static readonly PropertyMeta TRACKING_SPEED_MOD = new PropertyMeta("trackingSpeedMod");

        public static void SetTrackingParams(
        this Entity projectile,
        string sourceId,
        float angleCorrection = 0f,
        float range = 800f,
        float speedMod = 1f)
        {
            projectile.SetProperty(TRACKING_SOURCE, sourceId);
            projectile.SetProperty(TRACKING_ANGLE, angleCorrection);
            projectile.SetProperty(TRACKING_RANGE, range);
            projectile.SetProperty(TRACKING_SPEED_MOD, speedMod);
        }

        public static bool IsTrackingProjectile(this Entity projectile)
        {
            return !string.IsNullOrEmpty(projectile.GetProperty<string>(TRACKING_SOURCE));
        }

        public static float GetTrackingAngle(this Entity projectile)
        {
            return projectile.GetProperty<float>(TRACKING_ANGLE);
        }

        public static float GetTrackingRange(this Entity projectile)
        {
            return projectile.GetProperty<float>(TRACKING_RANGE);
        }

        public static float GetTrackingSpeedMod(this Entity projectile)
        {
            return projectile.GetProperty<float>(TRACKING_SPEED_MOD);
        }

        public static bool PointsTowardDirection(this Entity entity)
        {
            return entity.GetProperty<bool>(POINT_TO_DIRECTION);
        }
        public static bool WillDestroyOutsideLawn(this Entity projectile)
        {
            return !projectile.GetProperty<bool>(NO_DESTROY_OUTSIDE_LAWN);
        }
        public static bool IsPiercing(this Entity projectile)
        {
            return projectile.GetProperty<bool>(PIERCING);
        }
        public static void SetPiercing(this Entity projectile, bool value)
        {
            projectile.SetProperty(PIERCING, value);
        }
        public static NamespaceID[] GetDamageEffects(this Entity projectile)
        {
            return projectile.GetProperty<NamespaceID[]>(DAMAGE_EFFECTS);
        }
        public static List<EntityColliderReference> GetProjectileCollidingColliders(this Entity projectile)
        {
            return projectile.GetProperty<List<EntityColliderReference>>(COLLIDING_ENTITIES);
        }
        public static void SetProjectileCollidingEntities(this Entity projectile, List<EntityColliderReference> value)
        {
            projectile.SetProperty(COLLIDING_ENTITIES, value);
        }
        public static void AddProjectileCollidingEntity(this Entity projectile, EntityColliderReference reference)
        {
            var entities = projectile.GetProjectileCollidingColliders();
            if (entities == null)
            {
                entities = new List<EntityColliderReference>();
                projectile.SetProjectileCollidingEntities(entities);
            }
            entities.Add(reference);
        }
        public static bool RemoveProjectileCollidingEntity(this Entity projectile, EntityColliderReference reference)
        {
            var entities = projectile.GetProjectileCollidingColliders();
            if (entities == null)
                return false;
            return entities.RemoveAll(e => e == reference) > 0;
        }
    }
}
