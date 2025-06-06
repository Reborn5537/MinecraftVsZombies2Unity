﻿using System.Linq;
using MVZ2.GameContent.Difficulties;
using MVZ2.Vanilla.Audios;
using MVZ2.Vanilla.Callbacks;
using MVZ2.Vanilla.Entities;
using MVZ2.Vanilla.Level;
using MVZ2.Vanilla.Properties;
using MVZ2Logic.Level;
using PVZEngine;
using PVZEngine.Armors;
using PVZEngine.Definitions;
using PVZEngine.Entities;
using PVZEngine.Level;
using PVZEngine.Triggers;
using Tools;

namespace MVZ2.GameContent.Stages
{
    public class WaveStageBehaviour : StageBehaviour
    {
        public WaveStageBehaviour(StageDefinition stageDef) : base(stageDef)
        {
            stageDef.SetProperty(EngineStageProps.CONTINUED_FIRST_WAVE_TIME, 180);
            stageDef.SetProperty(VanillaStageProps.WAVE_MAX_TIME, 900);
            stageDef.SetProperty(VanillaStageProps.WAVE_ADVANCE_TIME, 300);
            stageDef.SetProperty(VanillaStageProps.WAVE_ADVANCE_HEALTH_PERCENT, 0.6f);
        }
        public void RunWaveTimer(LevelEngine level)
        {
            var waveTimer = GetWaveTimer(level);
            waveTimer.Run();
            CheckWaveAdvancement(level);
            if (waveTimer.Expired)
            {
                NextWaveOrHugeWave(level);
            }
        }
        public void RunBossWave(LevelEngine level)
        {
            var waveTimer = GetWaveTimer(level);
            waveTimer.Run();
            CheckWaveAdvancement(level);
            if (waveTimer.Expired)
            {
                SetWaveMaxHealth(level, 0);
                waveTimer.ResetTime(level.GetWaveMaxTime());
                level.RunWave();
            }
        }
        #region 回调
        public override void PrepareForBattle(LevelEngine level)
        {
            level.AreaDefinition.PrepareForBattle(level);
            if (level.Difficulty != VanillaDifficulties.hard && level.Difficulty != VanillaDifficulties.hell && level.CurrentFlag <= 0)
            {
                var cartRef = level.GetProperty<NamespaceID>(EngineAreaProps.CART_REFERENCE);
                level.SpawnCarts(cartRef, VanillaLevelExt.CART_START_X, 20);
            }
        }
        public override void Start(LevelEngine level)
        {
            var time = level.CurrentFlag > 0 ? level.GetContinutedFirstWaveTime() : level.GetFirstWaveTime();
            var waveTimer = new FrameTimer(time);
            SetWaveTimer(level, waveTimer);
            level.WaveState = STATE_NOT_STARTED;
        }
        public override void Update(LevelEngine level)
        {
            switch (level.WaveState)
            {
                case STATE_NOT_STARTED:
                    NotStartedUpdate(level);
                    break;
                case STATE_STARTED:
                    StartedUpdate(level);
                    break;
                case STATE_HUGE_WAVE_APPROACHING:
                    HugeWaveApproachingUpdate(level);
                    break;
                case STATE_FINAL_WAVE:
                    FinalWaveUpdate(level);
                    break;
            }
            level.CheckGameOver();
        }
        public override void PostHugeWaveEvent(LevelEngine level)
        {
            base.PostHugeWaveEvent(level);
            if (level.IgnoreHugeWaveEvent())
                return;
            level.AreaDefinition.PostHugeWaveEvent(level);
        }
        public override void PostFinalWaveEvent(LevelEngine level)
        {
            base.PostHugeWaveEvent(level);
            level.AreaDefinition.PostFinalWaveEvent(level);
        }
        public override void PostEnemySpawned(Entity entity)
        {
            AddWaveMaxHealth(entity.Level, entity.GetMaxHealth() + (entity.EquipedArmor?.GetMaxHealth() ?? 0));
        }
        #endregion

        #region 波次
        protected void CheckWaveAdvancement(LevelEngine level)
        {
            var waveTimer = GetWaveTimer(level);
            // 每15帧检测一次，还剩一秒钟结束则不检测。
            if (!waveTimer.PassedInterval(15) || waveTimer.Frame <= 30)
                return;

            // 已经不存在存活的敌人了，直接加速。
            if (CountAliveEnemies(level) <= 0)
            {
                waveTimer.Frame = 30;
                return;
            }

            // 下一波是一大波，除非已经没有存活的敌人了，否则不会加速。
            if (level.IsHugeWave(level.CurrentWave + 1))
                return;

            // 还没到加速时间。
            if (waveTimer.Frame >= waveTimer.MaxFrame - level.GetWaveAdvanceTime())
                return;

            // 敌人的血量没有低于阈值，不加速。
            if (!CheckEnemiesRemainedHealth(level))
                return;

            // 加速。
            waveTimer.Frame = 30;
        }
        private void NextWaveOrHugeWave(LevelEngine level)
        {
            if (level.IsHugeWave(level.CurrentWave + 1))
            {
                TriggerHugeWaveApproaching(level);
                return;
            }
            NextWave(level);
        }
        private void TriggerHugeWaveApproaching(LevelEngine level)
        {
            level.WaveState = STATE_HUGE_WAVE_APPROACHING;
            var waveTimer = GetWaveTimer(level);
            waveTimer.ResetTime(180);
            level.Triggers.RunCallback(VanillaCallbacks.POST_HUGE_WAVE_APPROACH, c => c(level));
        }
        private void NextWave(LevelEngine level)
        {
            var waveTimer = GetWaveTimer(level);
            waveTimer.ResetTime(level.GetWaveMaxTime());
            SetWaveMaxHealth(level, 0);
            level.NextWave();
            if (level.IsFinalWave(level.CurrentWave))
            {
                level.WaveState = VanillaLevelStates.STATE_FINAL_WAVE;
                if (HasFinalWave)
                {
                    SetFinalWaveEventTimer(level, new FrameTimer(60));
                    level.Triggers.RunCallback(VanillaCallbacks.POST_FINAL_WAVE, c => c(level));
                }
            }
        }
        #endregion

        #region 更新关卡
        protected virtual void NotStartedUpdate(LevelEngine level)
        {
            var waveTimer = GetWaveTimer(level);
            waveTimer.Run();
            if (waveTimer.Expired)
            {
                level.PlaySound(VanillaSoundID.awooga);
                level.WaveState = STATE_STARTED;
                level.LevelProgressVisible = true;
                NextWaveOrHugeWave(level);
            }
        }
        protected virtual void StartedUpdate(LevelEngine level)
        {
            RunWaveTimer(level);
        }
        protected virtual void HugeWaveApproachingUpdate(LevelEngine level)
        {
            var waveTimer = GetWaveTimer(level);
            waveTimer.Run();
            if (waveTimer.Expired)
            {
                level.PlaySound(VanillaSoundID.siren);
                level.WaveState = STATE_STARTED;
                NextWave(level);
                if (SpawnFlagZombie)
                {
                    level.SpawnFlagZombie();
                }
                level.RunHugeWaveEvent();
            }
        }
        protected virtual void FinalWaveUpdate(LevelEngine level)
        {
            if (HasFinalWave)
            {
                var finalWaveTimer = GetFinalWaveEventTimer(level);
                if (finalWaveTimer != null)
                {
                    finalWaveTimer.Run();
                    if (finalWaveTimer.Expired)
                    {
                        level.RunFinalWaveEvent();
                    }
                }
            }
        }
        #endregion

        #region 敌人血量
        public float CountAliveEnemies(LevelEngine level)
        {
            return level.FindEntities(e => e.IsAliveEnemy()).Length;
        }
        public bool CheckEnemiesRemainedHealth(LevelEngine level)
        {
            var enemies = level.FindEntities(e => e.IsAliveEnemy());
            var health = enemies.Sum(e => e.Health + (e.EquipedArmor?.Health ?? 0));
            return health <= level.GetWaveAdvanceHealthPercent() * GetWaveMaxHealth(level);
        }
        #endregion

        #region 关卡属性
        public FrameTimer GetWaveTimer(LevelEngine level) => level.GetBehaviourField<FrameTimer>(PROP_WAVE_TIMER);
        public void SetWaveTimer(LevelEngine level, FrameTimer value) => level.SetBehaviourField(PROP_WAVE_TIMER, value);

        public FrameTimer GetFinalWaveEventTimer(LevelEngine level) => level.GetBehaviourField<FrameTimer>(PROP_FINAL_WAVE_EVENT_TIMER);
        public void SetFinalWaveEventTimer(LevelEngine level, FrameTimer value) => level.SetBehaviourField(PROP_FINAL_WAVE_EVENT_TIMER, value);

        public float GetWaveMaxHealth(LevelEngine level) => level.GetBehaviourField<float>(PROP_WAVE_MAX_HEALTH);
        public void SetWaveMaxHealth(LevelEngine level, float value) => level.SetBehaviourField(PROP_WAVE_MAX_HEALTH, value);
        public void AddWaveMaxHealth(LevelEngine level, float value) => SetWaveMaxHealth(level, GetWaveMaxHealth(level) + value);
        #endregion

        #region 属性字段
        private const string PROP_REGION = "wave_stage";
        public bool SpawnFlagZombie { get; set; } = true;
        public bool HasFinalWave { get; set; } = true;
        [PropertyRegistry(PROP_REGION)]
        public static readonly VanillaLevelPropertyMeta PROP_WAVE_TIMER = new VanillaLevelPropertyMeta("WaveTimer");
        [PropertyRegistry(PROP_REGION)]
        public static readonly VanillaLevelPropertyMeta PROP_WAVE_MAX_HEALTH = new VanillaLevelPropertyMeta("WaveMaxHealth");
        [PropertyRegistry(PROP_REGION)]
        public static readonly VanillaLevelPropertyMeta PROP_FINAL_WAVE_EVENT_TIMER = new VanillaLevelPropertyMeta("FinalWaveEventTimer");
        public const int STATE_NOT_STARTED = VanillaLevelStates.STATE_NOT_STARTED;
        public const int STATE_STARTED = VanillaLevelStates.STATE_STARTED;
        public const int STATE_HUGE_WAVE_APPROACHING = VanillaLevelStates.STATE_HUGE_WAVE_APPROACHING;
        public const int STATE_FINAL_WAVE = VanillaLevelStates.STATE_FINAL_WAVE;
        public const int STATE_AFTER_FINAL_WAVE = VanillaLevelStates.STATE_AFTER_FINAL_WAVE;
        public const int STATE_BOSS_FIGHT = VanillaLevelStates.STATE_BOSS_FIGHT;
        public const int STATE_BOSS_FIGHT_2 = VanillaLevelStates.STATE_BOSS_FIGHT_2;
        public const int STATE_AFTER_BOSS = VanillaLevelStates.STATE_AFTER_BOSS;
        #endregion
    }
}
