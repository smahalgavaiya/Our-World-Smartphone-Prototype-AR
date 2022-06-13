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
 *  This is an Editor Script, it is responsible for drawing the inpector or drawer of the class in the attribute before the class
 */

using UnityEditor;
using UnityEngine;
using UniversalInventorySystem;

[CustomPropertyDrawer(typeof(PatternRecipe))]
public class PatternRecipeDrawer : PropertyDrawer
{
    bool showRecipe = false;
    int baseAmount = 1;
    int amount = 1;
    float basey;

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return 18f * amount;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUIUtility.wideMode = true;

        position.height /= amount;

        EditorGUI.PropertyField(position, property);

        var buttonRect = new Rect(position.x, position.y, 165, position.height);
        if (property.objectReferenceValue != null)
        {
            if (GUI.Button(buttonRect, new GUIContent("Open Editor of " + label.text)))
            {
                PatternRecipeEditorWindow.Open(property.objectReferenceValue as PatternRecipe);
            }
            buttonRect.x += 165;
            buttonRect.width = 85;
            if (GUI.Button(buttonRect, new GUIContent("Show Recipe")))
            {
                showRecipe = !showRecipe;
            }

            SerializedObject serializedObject = null;
            if (property.objectReferenceValue != null) serializedObject = new SerializedObject(property.objectReferenceValue);
            if (serializedObject != null)
            {
                if (showRecipe)
                {
                    amount = (serializedObject.FindProperty("gridSize").vector2IntValue.y * 2) + 1;
                    SerializedProperty pattern = serializedObject.FindProperty("pattern");
                    SerializedProperty patternAmount = serializedObject.FindProperty("amountPattern");
                    Vector2Int gridSize = serializedObject.FindProperty("gridSize").vector2IntValue;
                    position.y += position.height;
                    Rect patternPos = new Rect(position.x, position.y, position.width, position.height + 18);
                    GUIContent content = new GUIContent();
                    basey = position.y;
                    for (int i = 0;i < gridSize.y; i++)
                    {
                        for (int j = 0; j < gridSize.x; j++)
                        {
                            //(pattern.GetArrayElementAtIndex(i * gridSize.y + j).objectReferenceValue as Item).sprite.texture
                            var item = pattern.GetArrayElementAtIndex(i * gridSize.x + j).objectReferenceValue as Item;
                            var pAmount = patternAmount.GetArrayElementAtIndex(i * gridSize.x + j).intValue;
                            if (item == null) 
                            {
                                content.text = "null";
                                content.image = null;
                            } else
                            {
                                content.text = "";
                                content.image = item.sprite.texture;
                            }

                            EditorGUI.LabelField(patternPos, content);
                            GUIContent secondContent = new GUIContent();
                            secondContent.text = pAmount == 0 ? "" : pAmount.ToString(); 
                            EditorGUI.LabelField(patternPos, secondContent);
                            patternPos.x += 36;
                        }
                        patternPos.y += patternPos.height;
                        patternPos.x -= gridSize.x * 36;
                    }
                    

                    SerializedProperty products = serializedObject.FindProperty("products");
                    SerializedProperty productsAmount = serializedObject.FindProperty("amountProducts");
                    patternPos.y -= patternPos.height * ((float)gridSize.y / 2f);
                    patternPos.y -= 18;
                    patternPos.x += (gridSize.x * 36) + 18;
                    EditorGUI.LabelField(patternPos, "=");
                    patternPos.x += 18;
                    for (int i = 0;i < products.arraySize; i++)
                    {
                        var productItem = products.GetArrayElementAtIndex(i).objectReferenceValue as Item;
                        var pAmount = productsAmount.GetArrayElementAtIndex(i).intValue;
                        if (productItem == null)
                        {
                            content.text = "null";
                            content.image = null;
                        }
                        else
                        {
                            content.text = "";
                            content.image = productItem.sprite.texture;
                        }
                        EditorGUI.LabelField(patternPos, content);
                        GUIContent secondContent = new GUIContent();
                        secondContent.text = pAmount == 0 ? "" : pAmount.ToString();
                        EditorGUI.LabelField(patternPos, secondContent);
                        patternPos.x += 36;
                    }
                    patternPos.y = basey;
                    EditorGUI.SelectableLabel(patternPos, $"id: {serializedObject.FindProperty("id").intValue}, key: {serializedObject.FindProperty("key").stringValue}");
                }
                else amount = baseAmount;
            }
        }

        //base.OnGUI(position, property, label);
    }
}
