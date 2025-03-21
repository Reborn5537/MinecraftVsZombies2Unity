﻿using MVZ2Logic.Level.Components;
using PVZEngine;
using PVZEngine.Level;
using UnityEngine;

namespace MVZ2Logic.Level
{
    public static partial class LogicLevelExt
    {
        public static ISoundComponent GetSoundComponent(this LevelEngine level)
        {
            return level.GetComponent<ISoundComponent>();
        }
        public static void PlaySound(this LevelEngine level, NamespaceID id, Vector3 position, float pitch = 1, float volume = 1)
        {
            var component = level.GetSoundComponent();
            if (component == null)
                return;
            component.PlaySound(id, position, pitch, volume);
        }
        public static void PlaySound(this LevelEngine level, NamespaceID id, float pitch = 1, float volume = 1)
        {
            var component = level.GetSoundComponent();
            if (component == null)
                return;
            component.PlaySound(id, pitch, volume);
        }
        public static void AddLoopSoundEntity(this LevelEngine level, NamespaceID soundID, long id)
        {
            var component = level.GetSoundComponent();
            if (component == null)
                return;
            component.AddLoopSoundEntity(soundID, id);
        }
        public static void RemoveLoopSoundEntity(this LevelEngine level, NamespaceID soundID, long id)
        {
            var component = level.GetSoundComponent();
            if (component == null)
                return;
            component.RemoveLoopSoundEntity(soundID, id);
        }
        public static bool HasLoopSoundEntities(this LevelEngine level, NamespaceID id)
        {
            var component = level.GetSoundComponent();
            if (component == null)
                return false;
            return component.HasLoopSoundEntities(id);
        }
        public static bool HasLoopSoundEntity(this LevelEngine level, NamespaceID soundID, long id)
        {
            var component = level.GetSoundComponent();
            if (component == null)
                return false;
            return component.HasLoopSoundEntity(soundID, id);
        }
        public static void StopAllLoopSounds(this LevelEngine level)
        {
            var component = level.GetSoundComponent();
            if (component == null)
                return;
            component.StopAllLoopSounds();
        }
        public static bool IsPlayingSound(this LevelEngine level, NamespaceID id)
        {
            var component = level.GetSoundComponent();
            if (component == null)
                return false;
            return component.IsPlayingSound(id);
        }
        public static bool IsPlayingLoopSound(this LevelEngine level, NamespaceID id)
        {
            var component = level.GetSoundComponent();
            if (component == null)
                return false;
            return component.IsPlayingLoopSound(id);
        }
        public static NamespaceID[] GetLoopSounds(this LevelEngine level)
        {
            var component = level.GetSoundComponent();
            if (component == null)
                return null;
            return component.GetLoopSounds();
        }
    }
}
