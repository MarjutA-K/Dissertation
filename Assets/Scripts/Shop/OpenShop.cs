using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenShop : MonoBehaviour
{
    public GameObject shop;

    public void ShopOpener()
    {
        if (shop != null)
        {
            shop.SetActive(true);
        }
    }

    public void CloseShop()
    {
        shop.SetActive(false);
    }
}
