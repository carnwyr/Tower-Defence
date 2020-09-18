using System;
using UnityEngine.ResourceManagement.AsyncOperations;

public class LevelSetter : ILevelSetter
{
    private readonly IAssetLoader<LevelData> _levelLoader;

    public event Action<LevelData> DataLoaded;

    public LevelSetter(IAssetLoader<LevelData> levelLoader)
    {
        _levelLoader = levelLoader;
    }

    ~LevelSetter()
    {
        if (DataLoaded != null)
            foreach (var d in DataLoaded.GetInvocationList())
                DataLoaded -= (d as Action<LevelData>);
    }

    public void SetUpLevel(int levelNumber)
    {
        var handle = _levelLoader.LoadAsset("Level" + levelNumber.ToString());
        handle.Completed += OnDataLoaded;
    }

    protected void OnDataLoaded(AsyncOperationHandle<LevelData> handle)
    {
        handle.Completed -= OnDataLoaded;

        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            DataLoaded?.Invoke(handle.Result);
        } else
        {
            UnityEngine.Debug.LogError("Can't load asset " + handle.DebugName + " " + handle.OperationException.ToString());
        }
    }
}
