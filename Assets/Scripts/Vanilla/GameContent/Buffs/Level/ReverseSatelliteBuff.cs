﻿using MVZ2.GameContent.Enemies;
using MVZ2.Vanilla.Audios;
using MVZ2.Vanilla.Properties;
using MVZ2Logic.Level;
using PVZEngine.Buffs;
using PVZEngine.Level;
using PVZEngine.Modifiers;
using Tools.Mathematics;
using UnityEngine;

namespace MVZ2.GameContent.Buffs.Enemies
{
    [BuffDefinition(VanillaBuffNames.Level.reverseSatellite)]
    public class ReverseSatelliteBuff : BuffDefinition
    {
        public ReverseSatelliteBuff(string nsp, string name) : base(nsp, name)
        {
            AddModifier(new FloatModifier(LogicLevelProps.CAMERA_ROTATION, NumberOperator.Add, PROP_CAMERA_ROTATION));
        }
        public override void PostAdd(Buff buff)
        {
            base.PostAdd(buff);
            buff.Level.PlaySound(VanillaSoundID.boon);
        }
        public override void PostUpdate(Buff buff)
        {
            base.PostUpdate(buff);
            var time = buff.GetProperty<int>(PROP_TIME);
            time++;
            buff.SetProperty(PROP_TIME, time);

            var timeout = buff.GetProperty<int>(PROP_TIMEOUT);
            if (buff.Level.EntityExists(VanillaEnemyID.reverseSatellite))
            {
                timeout = MAX_TIMEOUT;
            }
            else
            {
                timeout--;
            }
            buff.SetProperty(PROP_TIMEOUT, timeout);


            var rotation = (time / (float)MAX_TIMEOUT) * MAX_ROTATION;
            buff.SetProperty(PROP_CAMERA_ROTATION, rotation);
            if (timeout <= 0)
            {
                buff.Remove();
            }
            if (!buff.Level.EntityExists(VanillaEnemyID.reverseSatellite))
            {
                buff.Remove();
            }
        }
        public static readonly VanillaBuffPropertyMeta PROP_CAMERA_ROTATION = new VanillaBuffPropertyMeta("CameraRotation");
        public static readonly VanillaBuffPropertyMeta PROP_TIME = new VanillaBuffPropertyMeta("Time");
        public static readonly VanillaBuffPropertyMeta PROP_TIMEOUT = new VanillaBuffPropertyMeta("Timeout");
        public const int MAX_TIMEOUT = 140;
        public const int MAX_ROTATION = 540;
    }
}
