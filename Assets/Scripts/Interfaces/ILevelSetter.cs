using System;

public interface ILevelSetter
{
    event Action<LevelData> DataLoaded;

    void SetUpLevel(int levelNumber);
}
