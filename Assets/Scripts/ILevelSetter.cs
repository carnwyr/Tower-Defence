public delegate void ProcessData(LevelData data);

public interface ILevelSetter
{
    event ProcessData DataLoaded;

    void SetUpLevel(int levelNumber);
}
