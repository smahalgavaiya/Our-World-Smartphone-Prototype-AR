﻿using UnityEngine;
using UniversalInventorySystem;

public class DefaultUseBehaviour
{
    public void OnUse(object sender, InventoryHandler.UseItemEventArgs e)
    {
        Debug.Log("Use Item Behaviour (DefaultUseBehaviour(does nothing change this on the item SO))");
    }
}
