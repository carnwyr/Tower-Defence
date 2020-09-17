using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayController
{
    private ILevelLoader _levelLoader;

    public GameplayController(ILevelLoader levelLoader)
    {
        _levelLoader = levelLoader;
    }

    public void Start()
    {
        _levelLoader.LoadLevel(1);
    }

    public void Update()
    {
        
    }
}
