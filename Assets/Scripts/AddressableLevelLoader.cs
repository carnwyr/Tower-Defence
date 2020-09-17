using UnityEngine.AddressableAssets;

public class AddressableLevelLoader : ILevelLoader
{
    private ISceneSetter _sceneSetter;

    public AddressableLevelLoader(ISceneSetter sceneSetter)
    {
        _sceneSetter = sceneSetter;
    }

    public void LoadLevel(int levelNumber)
    {
        var levelName = "Level" + levelNumber.ToString();
        var loadHandle = Addressables.LoadAssetAsync<LevelData>(levelName);
        loadHandle.Completed += _sceneSetter.SetScene;
    }
}
