using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class SceneSetter : MonoBehaviour, ISceneSetter
{
    [SerializeField]
    private Image _background;

    public void SetScene(AsyncOperationHandle<LevelData> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            SetBackground(handle.Result.Background);
        }
    }

    private void SetBackground(Sprite sprite)
    {
        _background.sprite = sprite;
        _background.preserveAspect = true;
        _background.gameObject.GetComponent<AspectRatioFitter>().aspectRatio = sprite.bounds.size.x / sprite.bounds.size.y;
    }
}
