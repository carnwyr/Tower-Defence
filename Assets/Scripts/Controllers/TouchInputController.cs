using UnityEngine;
using System;

public class TouchInputController : IInputController
{
    public void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Ended)
            {
                Vector2 pos = touch.position;
                RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);
                if (hit.collider != null)
                {
                    if (hit.collider.CompareTag("TowerClickArea"))
                    {
                        var tower = hit.collider.transform.parent.gameObject.GetComponent<Tower>();
                        tower.LevelUp();
                    }
                }
            }
        }
    }
}
