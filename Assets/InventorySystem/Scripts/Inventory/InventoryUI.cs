﻿/*  Copyright 2020 Gabriel Pasquale Rodrigues Scavone
 *
 *  Licensed under the Apache License, Version 2.0 (the "License");
 *  you may not use this file except in compliance with the License.
 *  You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 *  Unless required by applicable law or agreed to in writing, software
 *  distributed under the License is distributed on an "AS IS" BASIS,
 *  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *  See the License for the specific language governing permissions and
 *  limitations under the License.
 * 
 * 
 *  
 *  This code is responsable for the UI of the inventory
 *  
 */

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

namespace UniversalInventorySystem
{
    [System.Serializable]
    public class InventoryUI : MonoBehaviour
    {
        //Slots
        public bool generateUIFromSlotPrefab;
        public GameObject generatedUIParent;

        public GameObject slotPrefab;

        public Canvas canvas;

        public GameObject DontDropItemRect;

        public List<GameObject> slots;

        public bool showAmount = true;

        public GameObject dragObj;

        public bool hideDragObj;
        public bool useOnClick;

        //Sahder
        public Color outlineColor;

        public float outlineSize;

        //Toggle inventory
        public bool hideInventory;

        public KeyCode toggleKey;
        public GameObject togglableObject;

        public bool dropOnCloseCrafting = false;
        public Vector3 dropPos = Vector3.zero;
        public Vector3 randomFactor = Vector3.zero;

        //Inv
        public Inventory inv;

        public Item item;

        //Craft
        public bool isCraftInventory;

        public Vector2Int gridSize;

        public bool allowsPatternCrafting;

        public GameObject[] productSlots;

        [HideInInspector]
        public bool isDraging;
        [HideInInspector]
        public int? dragSlotNumber = null;
        [HideInInspector]
        public bool shouldSwap;
        [HideInInspector]
        public List<Item> pattern = new List<Item>();
        [HideInInspector]
        public List<int> amount = new List<int>();

        public void SetInventory(Inventory _inv) => inv = _inv;
        public Inventory GetInventory() => inv;

        public void Start()
        {
            //inv.AddItem(item, 1);
            if (isCraftInventory)
            {
                inv.AddItem(InventoryHandler.current.GetItem(0, 0), 1);

                inv.slotAmounts += productSlots.Length;
                for(int i = 0; i < productSlots.Length; i++)
                    inv.slots.Add(Slot.nullSlot);

                foreach (GameObject g in productSlots)
                {
                    slots.Add(g);
                }
            }

            var b = Instantiate(dragObj, canvas.transform);
            b.name = $"DRAGITEMOBJ_{name}_{UnityEngine.Random.Range(int.MinValue, int.MaxValue)}";
            b.AddComponent<DragSlot>();
            b.SetActive(false);
            if (hideDragObj) b.hideFlags = HideFlags.HideInHierarchy;
            dragObj = b;

            InventoryController.inventoriesUI.Add(this);
            if (!generateUIFromSlotPrefab)
            {
                for (int i = 0; i < slots.Count; i++)
                {
                    slots[i].name = i.ToString();
                    for (int j = 0; j < slots[i].transform.childCount; j++)
                    {
                        Image image;
                        if (slots[i].transform.GetChild(j).TryGetComponent<Image>(out image))
                        {
                            ItemDragHandler drag;
                            if (slots[i].transform.GetChild(j).TryGetComponent(out drag))
                            {
                                drag.canvas = canvas;
                                drag.invUI = this;
                            }
                            else
                            {
                                drag = slots[i].transform.GetChild(j).gameObject.AddComponent<ItemDragHandler>();
                                drag.canvas = canvas;
                                drag.invUI = this;
                            }

                            Tooltip tooltip;
                            if (slots[i].transform.GetChild(j).TryGetComponent(out tooltip))
                            {
                                tooltip.canvas = canvas;
                                tooltip.invUI = this;
                                tooltip.slotNum = i;
                            } else
                            {
                                tooltip = slots[i].transform.GetChild(j).gameObject.AddComponent<Tooltip>();
                                tooltip.canvas = canvas;
                                tooltip.invUI = this;
                                tooltip.slotNum = i;
                            }
                        }
                    }
                }
            }

            if (!canvas.TryGetComponent(out ItemDropHandler _)) canvas.gameObject.AddComponent<ItemDropHandler>();

            if (isCraftInventory)
            {
                for (int i = 0; i < gridSize.x * gridSize.y; i++)
                {
                    pattern.Add(null);
                    amount.Add(0);
                }

                for (int i = gridSize.x * gridSize.y; i < inv.slots.Count; i++)
                { 
                    inv.slots[i] = Slot.SetSlotProperties(inv[i], true, SlotProtection.Remove | SlotProtection.Swap, null);
                }

            }
        }

        List<GameObject> GenerateUI(int slotAmount)
        {
            List<GameObject> gb = new List<GameObject>();
            for (int i = 0; i < slotAmount; i++)
            {
                var g = Instantiate(slotPrefab, Vector3.zero, Quaternion.identity);
                g.transform.SetParent(generatedUIParent.transform);
                g.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                var tmp = g.transform.GetComponentInChildren<ItemDragHandler>();
                tmp.canvas = canvas;
                tmp.invUI = this;
                g.name = i.ToString();
                gb.Add(g);

                for (int j = 0; j < g.transform.childCount; j++)
                {
                    Image image;
                    if (g.transform.GetChild(j).TryGetComponent<Image>(out image))
                    {
                        ItemDragHandler drag;
                        if (g.transform.GetChild(j).TryGetComponent(out drag))
                        {
                            drag.canvas = canvas;
                            drag.invUI = this;
                        }
                        else
                        {
                            drag = g.transform.GetChild(j).gameObject.AddComponent<ItemDragHandler>();
                            drag.canvas = canvas;
                            drag.invUI = this;
                        }

                        Tooltip tooltip;
                        if (g.transform.GetChild(j).TryGetComponent(out tooltip))
                        {
                            tooltip.canvas = canvas;
                            tooltip.invUI = this;
                            tooltip.slotNum = i;
                        }
                        else
                        {
                            tooltip = g.transform.GetChild(j).gameObject.AddComponent<Tooltip>();
                            tooltip.canvas = canvas;
                            tooltip.invUI = this;
                            tooltip.slotNum = i;
                        }
                    }
                }
            }
            slots = gb;
            return gb;
        }

        bool hasGenerated = false;
        public void Update()
        {
            //Initialize if not yet
            if (!inv.hasInitializated)
                inv.Initialize();
            
            //Create UI
            if (generateUIFromSlotPrefab && !hasGenerated)
            {
                GenerateUI(inv.slotAmounts);
                hasGenerated = true;
            }

            ///TODO: Add Event here
            //Hiding Inventory
            if (hideInventory)
            {
                if (Input.GetKeyDown(toggleKey) && !isDraging)
                {
                    if (isCraftInventory && dropOnCloseCrafting)
                    {
                        for (int i = 0; i < inv.slots.Count; i++)
                        {
                            var item = inv.slots[i];
                            Vector3 finalDropPos = dropPos;
                            finalDropPos.x += Random.Range(-randomFactor.x, randomFactor.x);
                            finalDropPos.y += Random.Range(-randomFactor.y, randomFactor.y);
                            finalDropPos.z += Random.Range(-randomFactor.z, randomFactor.z);
                            inv.DropItem(item.amount, finalDropPos, slot: i);
                        }
                    }
                    togglableObject.SetActive(!togglableObject.activeInHierarchy);
                }
            }

            //Iterating slots go
            for (int i = 0; i < inv.slots.Count; i++)
            {
                // Create pattern grid
                if (isCraftInventory && i < pattern.Count)
                {
                    pattern[i] = inv.slots[i].item;
                    amount[i] = inv.slots[i].amount;
                }
                if (i >= slots.Count) break;

                // Rendering null Slot
                Image image;
                TextMeshProUGUI text;
                if (inv.slots[i].item == null)
                {
                    for (int j = 0; j < slots[i].transform.childCount; j++)
                    {

                        if (slots[i].transform.GetChild(j).TryGetComponent<Image>(out image))
                        {
                            image.sprite = null;
                            image.color = new Color(0, 0, 0, 0);
                        }
                        else if (slots[i].transform.GetChild(j).TryGetComponent(out text))
                            text.text = "";
                    }
                    continue;
                }

                // Rendering slot
                for (int j = 0; j < slots[i].transform.childCount; j++)
                {
                    if (slots[i].transform.GetChild(j).TryGetComponent<Image>(out image))
                    {
                        if (inv.slots[i].item.hasDurability)
                        {
                            if (inv.slots[i].item.durabilityImages.Count > 0)
                            {
                                image.sprite = GetNearestSprite(inv, inv.slots[i].durability, i);
                                image.color = new Color(1, 1, 1, 1);
                            }
                            else
                            {
                                image.sprite = inv.slots[i].item.sprite;
                                image.color = new Color(1, 1, 1, 1);
                            }
                        }
                        else
                        {
                            image.sprite = inv.slots[i].item.sprite;
                            image.color = new Color(1, 1, 1, 1);
                        }
                    }
                    else if (slots[i].transform.GetChild(j).TryGetComponent(out text) && showAmount && inv[i].item.showAmount)
                        text.text = inv.slots[i].amount.ToString();
                    else if (slots[i].transform.GetChild(j).TryGetComponent(out text))
                        text.text = "";
                }

                if (dragObj.GetComponent<DragSlot>().GetSlotNumber() == i && isDraging)
                {
                    if (inv.slots[i].amount - dragObj.GetComponent<DragSlot>().GetAmount() == 0)
                    {
                        for (int j = 0; j < slots[i].transform.childCount; j++)
                        {

                            if (slots[i].transform.GetChild(j).TryGetComponent<Image>(out image))
                            {
                                image.sprite = null;
                                image.color = new Color(0, 0, 0, 0);
                            }
                            else if (slots[i].transform.GetChild(j).TryGetComponent(out text))
                                text.text = "";
                        }
                    }
                    else
                    {
                        for (int j = 0; j < slots[i].transform.childCount; j++)
                        {
                            if (slots[i].transform.GetChild(j).TryGetComponent(out text) && showAmount && inv[i].item.showAmount)
                                text.text = (inv.slots[i].amount - dragObj.GetComponent<DragSlot>().GetAmount()).ToString();
                            else if (slots[i].transform.GetChild(j).TryGetComponent(out text))
                                text.text = "";
                        }
                    }
                }

                if (!isCraftInventory)
                {
                    slots[i].GetComponent<Button>().onClick.RemoveAllListeners();
                    var index = i;
                    slots[i].GetComponent<Button>().onClick.AddListener(() =>
                    {
                    //Debug.Log($"Slot {slots[index].name} was clicked");
                        if (useOnClick)
                            inv.UseItemInSlot(index);
                    });
                }
            }

            //Dont use on click if is crafting inventory
            if (isCraftInventory)
            {
                CraftItemData products = inv.CraftItem(new CraftItemData(pattern.ToArray(), amount.ToArray()), gridSize, false, true, productSlots.Length);

                List<Item> productsItem = new List<Item>();
                if (products != CraftItemData.nullData && products.items.Length <= productSlots.Length)
                {
                    if (products.items.Length == productSlots.Length)
                    {
                        for (int k = 0; k < products.items.Length; k++)
                        {
                            productsItem.Add(inv.slots[gridSize.x * gridSize.y + k].item ?? products.items[k]);
                        }
                    }
                    else
                    {
                        for(int i = 0; i < productSlots.Length - products.items.Length + 1; i++)
                        {
                            productsItem = new List<Item>();
                            for (int k = 0; k < products.items.Length; k++)
                            {
                                if (gridSize.x * gridSize.y + k + i >= inv.slots.Count) break;
                                if (inv.slots[gridSize.x * gridSize.y + k + i].item == products.items[k] || inv.slots[gridSize.x * gridSize.y + k + i].item == null)
                                {
                                    productsItem.Add(inv.slots[gridSize.x * gridSize.y + k + i].item ?? products.items[k]);
                                    if (Enumerable.SequenceEqual(products.items, productsItem.ToArray()))
                                    {
                                        i = int.MaxValue - 1;
                                        break;
                                    }
                                }
                            }
                        }
                    }

                }

                int productIndex = 0;
                for (int i = 0; i < productSlots.Length; i++)
                {
                    // If there is a item in the product slot it renders it and go to the next one
                    if (inv.slots[(gridSize.x * gridSize.y) + i].hasItem)
                    {
                        // Iterating the childs
                        for (int j = 0; j < slots[(gridSize.x * gridSize.y) + i].transform.childCount; j++)
                        {
                            if (slots[(gridSize.x * gridSize.y) + i].transform.GetChild(j).TryGetComponent(out Image image))
                            {
                                // Having durability it renders the corresponding durability image
                                if (inv.slots[(gridSize.x * gridSize.y) + i].item.hasDurability)
                                {
                                    if (inv.slots[(gridSize.x * gridSize.y) + i].item.durabilityImages.Count > 0)
                                    {
                                        image.sprite = GetNearestSprite(inv, inv.slots[(gridSize.x * gridSize.y) + i].durability, (gridSize.x * gridSize.y) + i);
                                        image.color = new Color(1, 1, 1, 1);
                                    }
                                    else
                                    {
                                        image.sprite = inv.slots[(gridSize.x * gridSize.y) + i].item.sprite;
                                        image.color = new Color(1, 1, 1, 1);
                                    }
                                    //productIndex++;
                                }
                                else
                                {
                                    image.sprite = inv.slots[(gridSize.x * gridSize.y) + i].item.sprite;
                                    image.color = new Color(1, 1, 1, 1);
                                    //productIndex++;
                                }
                            }
                            else if (slots[(gridSize.x * gridSize.y) + i].transform.GetChild(j).TryGetComponent(out TextMeshProUGUI text) && showAmount && inv[(gridSize.x * gridSize.y) + i].item.showAmount)
                                text.text = inv.slots[(gridSize.x * gridSize.y) + i].amount.ToString();
                            else if (slots[(gridSize.x * gridSize.y) + i].transform.GetChild(j).TryGetComponent(out text))
                                text.text = "";
                        }

                        if(products != null && products != CraftItemData.nullData)
                            if (inv[(gridSize.x * gridSize.y) + i].item == (products?.items[productIndex] ?? null) && 
                                inv[(gridSize.x * gridSize.y) + i].amount + (products?.amounts[productIndex] ?? int.MaxValue) <= inv[(gridSize.x * gridSize.y) + i].item.maxAmount
                                ) 
                                productIndex++;

                        //For click and drag
                        productSlots[i].GetComponent<Button>().onClick.RemoveAllListeners();
                        var index = i;
                        productSlots[i].GetComponent<Button>().onClick.AddListener(() =>
                        {
                            if (products.items != null && products.items.Length <= productSlots.Length)
                            {
                                // If you dont want the other of the items in the product slot to matter this line should be different, It shoud check if the
                                // List have the same items, not the same sequence.
                                if (Enumerable.SequenceEqual(products.items, productsItem.ToArray()))
                                {
                                    inv.CraftItem(new CraftItemData(pattern.ToArray(), amount.ToArray()), gridSize, true, true, productSlots.Length);
                                }
                            }
                        });
                    }
                    else if (products.items != null && products.items.Length <= productSlots.Length && productIndex < products.items.Length)
                    {
                        bool nextIndex = false;
                        for (int j = 0; j < slots[(gridSize.x * gridSize.y) + i].transform.childCount; j++)
                        {
                            // Iterating the childs
                            if (slots[(gridSize.x * gridSize.y) + i].transform.GetChild(j).TryGetComponent(out Image image))
                            {
                                if (products.items[productIndex].hasDurability)
                                {
                                    if (products.items[productIndex].durabilityImages.Count > 0)
                                    {
                                        image.sprite = GetNearestSprite(products.items[productIndex], products.items[productIndex].maxDurability);
                                        image.color = new Color(1, 1, 1, .7f);
                                    }
                                    else
                                    {
                                        image.sprite = products.items[productIndex].sprite;
                                        image.color = new Color(1, 1, 1, .7f);
                                    }
                                    nextIndex = true;
                                }
                                else
                                {
                                    image.sprite = products.items[productIndex].sprite;
                                    image.color = new Color(1, 1, 1, .7f);
                                    nextIndex = true;
                                }
                            }
                            else if (productSlots[i].transform.GetChild(j).TryGetComponent(out TextMeshProUGUI text) && showAmount && products.items[productIndex].showAmount)
                            {
                                text.text = products.amounts[productIndex].ToString();
                                nextIndex = true;
                            }
                            else if (productSlots[i].transform.GetChild(j).TryGetComponent(out text))
                            {
                                text.text = "";
                                nextIndex = true;
                            }
                        }

                        if(nextIndex)
                            productIndex++;

                        //For click and drag
                        productSlots[i].GetComponent<Button>().onClick.RemoveAllListeners();
                        var index = i;
                        productSlots[i].GetComponent<Button>().onClick.AddListener(() =>
                        {
                            //Debug.Log($"Product slot {slots[index].name} was clicked");
                            inv.CraftItem(new CraftItemData(pattern.ToArray(), amount.ToArray()), gridSize, true, true, productSlots.Length);
                        });

                    }
                    else
                    {
                        if (!inv.slots[(gridSize.x * gridSize.y) + i].hasItem)
                        {
                            for (int j = 0; j < slots[i].transform.childCount; j++)
                            {
                                if (slots[(gridSize.x * gridSize.y) + i].transform.GetChild(j).TryGetComponent(out Image image))
                                {
                                    image.sprite = null;
                                    image.color = new Color(0, 0, 0, 0);
                                }
                                else if (productSlots[i].transform.GetChild(j).TryGetComponent(out TextMeshProUGUI text))
                                    text.text = "";
                            }
                        }
                    }
                }
            }
        }
        
        /// <summary>
        /// Get nearest sprite of durability
        /// </summary>
        /// <param name="inv">inventory</param>
        /// <param name="durability">Usualy the durability in the slot</param>
        /// <param name="slot">slot number</param>
        /// <returns>The nearest Sprite</returns>
        public static Sprite GetNearestSprite(Inventory inv, int durability, int slot)
        {
            var minDif = int.MaxValue;
            var index = 0;
            for(int i = inv.slots[slot].item.durabilityImages.Count - 1; i >= 0;i--)
            {
                int dif = inv.slots[slot].item.durabilityImages[i].durability - durability;
                if (dif < 0) break;
                if (dif < minDif)
                {
                    minDif = dif;
                    index = i;
                }
            }
            return inv.slots[slot].item.durabilityImages[index].sprite;
        }

        /// <summary>
        /// Get nearest sprite of durability
        /// </summary>
        /// <param name="item">The item with durability</param>
        /// <param name="durability">The atual durability</param>
        /// <returns>The nearest Sprite</returns>
        public static Sprite GetNearestSprite(Item item, int durability)
        {
            var minDif = int.MaxValue;
            var index = 0;
            for (int i = item.durabilityImages.Count - 1; i >= 0; i--)
            {
                int dif = item.durabilityImages[i].durability - durability;
                if (dif < 0) break;
                if (dif < minDif)
                {
                    minDif = dif;
                    index = i;
                }
            }
            return item.durabilityImages[index].sprite;
        }
    }
}