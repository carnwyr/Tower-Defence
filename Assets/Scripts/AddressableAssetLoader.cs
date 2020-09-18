using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AddressableAssetLoader<T> : IAssetLoader<T>
{
    public AsyncOperationHandle<T> LoadAsset(string assetName)
    {
        var loadHandle = Addressables.LoadAssetAsync<T>(assetName);
        return loadHandle;
    }
}
