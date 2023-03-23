using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropController : MonoBehaviour
{
    public bool isClicked = true;

    GrowController gc;

    private void Start()
    {
        gc = FindObjectOfType<GrowController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && isClicked)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                Debug.Log(hit.collider.gameObject.name);
                XPManager.instance.AddXP(100);
                ShopManager.instance.AddMoney(50);

                isClicked = false;

                gc.isGrowing = true;
                gc.timer = 0;

                Debug.Log(gc.timer);
            }
        }
    }
}
