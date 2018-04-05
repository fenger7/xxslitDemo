﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Ctrl_FuelSlot : MonoBehaviour, IPointerDownHandler
{
    private Model_Item item;

    public Model_Item Item
    {
        get { return item; }

        set
        {
            item = value;
            GetComponent<View_FuelSlot>().InitView(value);
        }
    }

    public void UpdateAmount(Model_Item item)
    {
        GetComponent<View_FuelSlot>().UpdateAmount(Item);
    }

    /// <summary>
    /// 鼠标左键单击
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerDown(PointerEventData eventData)
    {
        //手上的物品
        Ctrl_PickUp pickUp = Ctrl_InventoryManager.Instance.PickUpItem.GetComponent<Ctrl_PickUp>();
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            //如果手上有物品
            if (pickUp.Item != null)
            {
                //如果当前格子没有物品
                if (Item == null)
                {
                    //只有木材可以替换
                    if (pickUp.Item.materialType == "Fuel")
                    {
                        //按住CTRL键,一下添加一个
                        if (Input.GetKey(KeyCode.LeftControl))
                        {
                            Item = new Model_Item
                            {
                                id = pickUp.Item.id,
                                itemName = pickUp.Item.itemName,
                                itemType = pickUp.Item.itemType,
                                materialType = pickUp.Item.materialType,
                                equipmentType = pickUp.Item.equipmentType,
                                maxStack = pickUp.Item.maxStack,
                                currentNumber = 1,
                                buyPriceByGold = pickUp.Item.buyPriceByGold,
                                buyPriceByDiamond = pickUp.Item.buyPriceByDiamond,
                                sellPriceByGold = pickUp.Item.sellPriceByGold,
                                sellPriceByDiamond = pickUp.Item.sellPriceByDiamond,
                                minLevel = pickUp.Item.minLevel,
                                sellable = pickUp.Item.sellable,
                                tradable = pickUp.Item.tradable,
                                destroyable = pickUp.Item.destroyable,
                                description = pickUp.Item.description,
                                sprite = pickUp.Item.sprite,
                                useDestroy = pickUp.Item.useDestroy,
                                useHealth = pickUp.Item.useHealth,
                                useMagic = pickUp.Item.useMagic,
                                useExperience = pickUp.Item.useExperience,
                                equipHealthBonus = pickUp.Item.equipHealthBonus,
                                equipManaBonus = pickUp.Item.equipManaBonus,
                                equipDamageBonus = pickUp.Item.equipDamageBonus,
                                equipDefenseBonus = pickUp.Item.equipDefenseBonus,
                                equipSpeedcBonus = pickUp.Item.equipSpeedcBonus,
                                modelPrefab = pickUp.Item.modelPrefab
                            };
                            //如果手上的物品数量只剩一个
                            if (pickUp.Item.currentNumber - 1 == 0)
                            {
                                pickUp.Item = null;
                            }
                            else
                            {
                                pickUp.Item.currentNumber -= 1;
                                pickUp.UpdateAmount();
                            }

                            return;
                        }

                        Item = pickUp.Item;
                        pickUp.Item = null;
                    }
                }
                else
                {
                    //如果手上的物品跟燃烧的物品一样
                    if (pickUp.Item.id == Item.id)
                    {
                        //按住CTRL键,一下添加一个
                        if (Input.GetKey(KeyCode.LeftControl))
                        {
                            //当前燃料格子未满
                            if (Item.currentNumber < Item.maxStack)
                            {
                                item.currentNumber++;
                                UpdateAmount(Item); //更新个数UI显示
                                //如果手上的物品数量只剩一个
                                if (pickUp.Item.currentNumber - 1 == 0)
                                {
                                    pickUp.Item = null;
                                }
                                else
                                {
                                    pickUp.Item.currentNumber -= 1;
                                    pickUp.UpdateAmount();
                                }
                            }

                            return;
                        }

                        //如果燃烧的材料达到上限的个数大于手上物品的个数
                        if (Item.maxStack - Item.currentNumber > pickUp.Item.currentNumber)
                        {
                            Item.currentNumber += pickUp.Item.currentNumber;
                            UpdateAmount(Item); //更新个数UI显示
                            pickUp.Item = null;
                        }
                        else
                        {
                            pickUp.Item.currentNumber -= (Item.maxStack - Item.currentNumber);
                            Item.currentNumber = Item.maxStack;
                            UpdateAmount(Item); //更新个数UI显示
                            pickUp.UpdateAmount(); //更新UI显示
                        }
                    }
                    else
                    {
                        //如果都有物品,交换
                        Model_Item tempItem;
                        tempItem = Item;
                        Item = pickUp.Item;
                        pickUp.Item = tempItem;
                    }
                }
            }
        }

        if (eventData.button == PointerEventData.InputButton.Right)
        {
            //手上无物品
            if (pickUp.Item == null)
            {
                pickUp.Item = Item;
                Item = null;
            }
        }
    }
}