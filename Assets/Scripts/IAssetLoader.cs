using UnityEngine.ResourceManagement.AsyncOperations;

public interface IAssetLoader<T>
{
    AsyncOperationHandle<T> LoadAsset(string assetName);
}
