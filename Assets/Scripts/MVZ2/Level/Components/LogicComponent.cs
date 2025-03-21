using MVZ2.Vanilla;
using MVZ2Logic.Level.Components;
using PVZEngine;
using PVZEngine.Level;

namespace MVZ2.Level.Components
{
    public partial class LogicComponent : MVZ2Component, ILogicComponent
    {
        public LogicComponent(LevelEngine level, LevelController controller) : base(level, componentID, controller)
        {
        }
        public void BeginLevel()
        {
            Controller.StartLevelTransition();
        }
        public void StopLevel()
        {
            Controller.StopLevel();
        }
        public void SaveStateData()
        {
            Main.LevelManager.SaveLevel();
        }
        public bool IsGamePaused()
        {
            return Controller.IsGamePaused();
        }
        public bool IsGameStarted()
        {
            return Controller.IsGameStarted();
        }
        public bool IsGameOver()
        {
            return Controller.IsGameOver();
        }
        public bool IsGameRunning()
        {
            return Controller.IsGameRunning();
        }
        public static readonly NamespaceID componentID = new NamespaceID(VanillaMod.spaceName, "logic");
    }
}