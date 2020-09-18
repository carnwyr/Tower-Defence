using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayController
{
    private readonly ILevelSetter _levelsetter;

    public GameplayController(ILevelSetter levelsetter)
    {
        _levelsetter = levelsetter;
    }

    public void Start()
    {
        _levelsetter.SetUpLevel(1);
    }

    public void Update()
    {
        
    }
}
