using UnityEngine;

public class ViewController : MonoBehaviour
{
    [SerializeField]
    private GameObject _cameraPrefab;
    [SerializeField]
    private GameObject _canvasPrefab;
    [SerializeField]
    private GameObject _eventSystemPrefab;

    public GameObject Canvas { get; private set; }

    private void Awake()
    {
        Instantiate(_eventSystemPrefab);
        var camera = Instantiate(_cameraPrefab);
        Canvas = Instantiate(_canvasPrefab);
        Canvas.GetComponent<Canvas>().worldCamera = camera.GetComponent<Camera>();
    }
}
