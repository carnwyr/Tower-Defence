using UnityEngine;

public class ViewController : MonoBehaviour
{
    [SerializeField]
    private GameObject _cameraPrefab;
    [SerializeField]
    private GameObject _canvasBackgroundPrefab;
    [SerializeField]
    private GameObject _canvasUIPrefab;
    [SerializeField]
    private GameObject _eventSystemPrefab;

    public GameObject CanvasBackground { get; private set; }
    public GameObject CanvasUI { get; private set; }

    private void Awake()
    {
        Instantiate(_eventSystemPrefab);
        var camera = Instantiate(_cameraPrefab);
        CanvasBackground = Instantiate(_canvasBackgroundPrefab);
        CanvasBackground.GetComponent<Canvas>().worldCamera = camera.GetComponent<Camera>();
        CanvasUI = Instantiate(_canvasUIPrefab);
    }
}
