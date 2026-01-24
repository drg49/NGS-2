public enum LevelMode
{
    LevelOne,
    LevelThree
}

public static class SceneContext
{
    // Default so normal playthroughs work
    public static LevelMode CurrentLevelMode = LevelMode.LevelOne;
}
