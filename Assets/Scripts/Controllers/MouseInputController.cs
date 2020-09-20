using UnityEngine;
using System;

public class MouseInputController : IInputController
{
    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(new Vector2(mousePos.x, mousePos.y), Vector2.zero);
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
