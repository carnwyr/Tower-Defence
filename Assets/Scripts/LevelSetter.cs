using System;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class LevelSetter : ILevelSetter
{
    private readonly IAssetLoader<LevelData> _levelLoader;

    public event ProcessData DataLoaded;

    public LevelSetter(IAssetLoader<LevelData> levelLoader)
    {
        _levelLoader = levelLoader;
    }

    public void SetUpLevel(int levelNumber)
    {
        var handle = _levelLoader.LoadAsset("Level" + levelNumber.ToString());
        handle.Completed += OnDataLoaded;
    }

    protected void OnDataLoaded(AsyncOperationHandle<LevelData> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            DataLoaded(handle.Result);
        }
    }
}
