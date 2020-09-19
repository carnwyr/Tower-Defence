using System;
using UnityEngine;
using UnityEngine.UI;

public class LevelViewController : MonoBehaviour
{
    [SerializeField]
    private GameObject _backgroundPrefab;
    [SerializeField]
    private GameObject _towerPrefab;

    private Image _background;
    private Action _unsubscribe;

    private void OnDestroy()
    {
        _unsubscribe();
    }

    public void Init(GameObject canvas)
    {
        var background = Instantiate(_backgroundPrefab);
        background.transform.SetParent(canvas.transform, false);

        _background = background.GetComponent<Image>();
    }

    public void SetCallbacks(ILevelSetter levelSetter)
    {
        levelSetter.DataLoaded += SetBackground;

        _unsubscribe = () => RemoveCallbacks(levelSetter);
    }

    private void RemoveCallbacks(ILevelSetter levelSetter)
    {
        if (levelSetter != null)
        {
            levelSetter.DataLoaded -= SetBackground;
        }
    }

    private void SetBackground(LevelData levelData)
    {
        var sprite = levelData.Background;
        _background.sprite = sprite;
        _background.preserveAspect = true;
        _background.gameObject.GetComponent<AspectRatioFitter>().aspectRatio = sprite.bounds.size.x / sprite.bounds.size.y;
    }
}
