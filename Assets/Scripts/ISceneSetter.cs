using UnityEngine.ResourceManagement.AsyncOperations;

public interface ISceneSetter
{
    void SetScene(AsyncOperationHandle<LevelData> handle);
}
