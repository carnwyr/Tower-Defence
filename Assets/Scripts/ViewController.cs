using UnityEngine;
using UnityEngine.UI;

public class ViewController : MonoBehaviour
{
    [SerializeField]
    private GameObject _cameraPrefab;
    [SerializeField]
    private GameObject _canvasPrefab;
    [SerializeField]
    private GameObject _eventSystemPrefab;
    [SerializeField]
    private GameObject _backgroundPrefab;

    private Image _background;

    private ILevelSetter _levelSetter;

    public void Awake()
    {
        Instantiate(_eventSystemPrefab);
        var camera = Instantiate(_cameraPrefab);
        var canvas = Instantiate(_canvasPrefab);
        var background = Instantiate(_backgroundPrefab);
        canvas.GetComponent<Canvas>().worldCamera = camera.GetComponent<Camera>();
        background.transform.SetParent(canvas.transform);
        
        _background = background.GetComponent<Image>();
    }

    public void SetDependencies(ILevelSetter levelSetter)
    {
        _levelSetter = levelSetter;
        setCallbacks();
    }

    private void setCallbacks()
    {
        _levelSetter.DataLoaded += SetBackground;
    }

    private void SetBackground(LevelData levelData)
    {
        var sprite = levelData.Background;
        _background.sprite = sprite;
        _background.preserveAspect = true;
        _background.gameObject.GetComponent<AspectRatioFitter>().aspectRatio = sprite.bounds.size.x / sprite.bounds.size.y;
    }
}
