using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class shopMenuSwitch : MonoBehaviour
{

    [SerializeField] Button shop_btn;
    [SerializeField] Button upgrade_btn;

    [SerializeField] GameObject shop;
    [SerializeField] GameObject upgrade;

    [SerializeField] int menuIndex;

    // Update is called once per frame
    void Update()
    {
        switch(menuIndex)
        {
            case 0:
                SwitchMenu(shop, shop_btn, true);
                SwitchMenu(upgrade, upgrade_btn, false);

                break;
            case 1:
                SwitchMenu(shop, shop_btn, false);
                SwitchMenu(upgrade, upgrade_btn, true);
                break;
        }
    }

    public void SwitchMenuButton(int menu)
    {
            menuIndex = menu;
    }

    void SwitchMenu(GameObject menu, Button Button ,bool ButtonEnabled)
    {
        menu.SetActive(ButtonEnabled);

        if (!ButtonEnabled)
        {
            Button.GetComponent<Image>().color = Color.grey;
        }
        else
        {
            Button.GetComponent<Image>().color = Color.white;
        }

    }
}
