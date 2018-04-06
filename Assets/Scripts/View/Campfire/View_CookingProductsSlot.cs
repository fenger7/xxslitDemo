﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class View_CookingProductsSlot : MonoBehaviour {

    [SerializeField] private GameObject icon;
    [SerializeField] private Text count;
    /// <summary>
    /// UI初始化
    /// </summary>
    /// <param name="item"></param>
    public void InitView(Model_Item item)
    {
        if (item != null)
        {
            icon.gameObject.SetActive(true);
            icon.GetComponent<Image>().sprite = Resources.Load<Sprite>(item.sprite);
            count.text = item.currentNumber.ToString();
        }
        else
        {
            icon.gameObject.SetActive(false);
            count.text = " ";
        }
    }
    /// <summary>
    /// 更新个数显示
    /// </summary>
    /// <param name="item"></param>
    public void UpdateAmount(Model_Item item)
    {
        count.text = item.currentNumber.ToString();
    }
}
