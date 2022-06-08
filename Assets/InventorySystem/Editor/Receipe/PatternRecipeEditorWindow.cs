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

using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UniversalInventorySystem;

public class PatternRecipeEditorWindow : ExtendEditorWindow
{
    bool patternRecipe = true;
    bool patternConfg = false;
    bool patternCreation = false;
    bool allowBigValues;

    Vector2 scrollPos;
    Vector2 creationPos;
    Vector2 slectablesItem;

    Vector2Int gridSize;

    int selectedIndex;

    public static void Open(PatternRecipe dataObject)
    {
        PatternRecipeEditorWindow window = GetWindow<PatternRecipeEditorWindow>("Item Data Editor");
        window.serializedObject = new SerializedObject(dataObject);
    }

    private void OnGUI()
    {
        if(serializedObject == null)
        {
            EditorGUILayout.LabelField("Open a recipe pattern");
            return;
        }

        serializedObject.Update();

        EditorGUILayout.BeginHorizontal();
        currentProperty = serializedObject.FindProperty("");
        EditorGUILayout.BeginVertical("box", GUILayout.MaxWidth(150), GUILayout.MinWidth(100), GUILayout.ExpandHeight(true));

        if(GUILayout.Button("Pattern Recipe"))
        {
            patternRecipe = true;
            patternConfg = false;
            patternCreation = false;
        }
        if (GUILayout.Button("Pattern Config"))
        {
            patternRecipe = false;
            patternConfg = true;
            patternCreation = false;
        }
        if (GUILayout.Button("Pattern Creation"))
        {
            patternRecipe = false;
            patternConfg = false;
            patternCreation = true;
        }

        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical("box", GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));

        if (patternRecipe)
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("id"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("key"));
        }

        if (patternConfg)
        {
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

            EditorGUILayout.PropertyField(serializedObject.FindProperty("numberOfFactors"));
            serializedObject.FindProperty("factors").arraySize = serializedObject.FindProperty("numberOfFactors").intValue;

            serializedObject.FindProperty("factors").isExpanded = EditorGUILayout.Foldout(serializedObject.FindProperty("factors").isExpanded, new GUIContent("Factors"));

            if (serializedObject.FindProperty("factors").isExpanded)
            {
                EditorGUI.indentLevel++;
                for(int i = 0;i < serializedObject.FindProperty("factors").arraySize; i++)
                {
                    var item = serializedObject.FindProperty("factors").GetArrayElementAtIndex(i).objectReferenceValue as Item;
                    GUIContent content = new GUIContent();
                    if(item != null)
                    {
                        content.image = item.sprite.texture;
                        content.text = item.name + $" (Element {i})";
                    }
                    
                    EditorGUILayout.ObjectField(serializedObject.FindProperty("factors").GetArrayElementAtIndex(i), content);
                }
                EditorGUI.indentLevel--;
            }

            EditorGUILayout.Separator();

            EditorGUILayout.PropertyField(serializedObject.FindProperty("numberOfProducts"));

            serializedObject.FindProperty("products").arraySize = serializedObject.FindProperty("numberOfProducts").intValue;
            serializedObject.FindProperty("amountProducts").arraySize = serializedObject.FindProperty("numberOfProducts").intValue;

            serializedObject.FindProperty("products").isExpanded = EditorGUILayout.Foldout(serializedObject.FindProperty("products").isExpanded, new GUIContent("Products"));

            if (serializedObject.FindProperty("products").isExpanded)
            {
                EditorGUI.indentLevel++;
                for (int i = 0; i < serializedObject.FindProperty("products").arraySize; i++)
                {
                    var item = serializedObject.FindProperty("products").GetArrayElementAtIndex(i).objectReferenceValue as Item;
                    GUIContent content = new GUIContent();
                    if (item != null)
                    {
                        content.image = item.sprite.texture;
                        content.text = $"({i}) " + item.name;
                    }

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.ObjectField(serializedObject.FindProperty("products").GetArrayElementAtIndex(i), content);
                    serializedObject.FindProperty("amountProducts").GetArrayElementAtIndex(i).intValue = EditorGUILayout.IntField(new GUIContent("Amount"), serializedObject.FindProperty("amountProducts").GetArrayElementAtIndex(i).intValue);
                    EditorGUILayout.EndHorizontal();
                }
                EditorGUI.indentLevel--;
            }

            EditorGUILayout.EndScrollView();
        }

        if (patternCreation)
        {
            EditorGUILayout.BeginHorizontal();
            //----SELECTION-------//
            #region SelectItem

            EditorGUILayout.BeginVertical("box", GUILayout.MaxWidth(230));
            slectablesItem = EditorGUILayout.BeginScrollView(slectablesItem);

            List<Item> items = new List<Item>();
            for(int i = 0; i < serializedObject.FindProperty("factors").arraySize; i++)
            {
                items.Add(serializedObject.FindProperty("factors").GetArrayElementAtIndex(i).objectReferenceValue as Item);
            }
            
            List<GUIContent> possibleItems = new List<GUIContent>();
            foreach (Item item in items)
            {
                if (item == null)
                {
                    Debug.LogError("Null item provided");
                    continue;
                }
                Texture2D texture = item.sprite.texture;
                possibleItems.Add(new GUIContent(item.name, texture));
            }
            possibleItems.Add(new GUIContent("Erase item"));
            possibleItems.Add(new GUIContent("Erase one item"));

            selectedIndex = GUILayout.SelectionGrid(selectedIndex, possibleItems.ToArray(), 1, GUILayout.MaxHeight(possibleItems.Count * 40));

            EditorGUILayout.EndScrollView();
            EditorGUILayout.EndVertical();

            #endregion
            //-----GRID-----//
            #region Grid

            EditorGUILayout.BeginVertical();

            creationPos = EditorGUILayout.BeginScrollView(creationPos);

            var tmp = EditorGUILayout.Vector2IntField("Grid size", serializedObject.FindProperty("gridSize").vector2IntValue);

            //Safity features
            if(tmp.x >= 0 && tmp.y >= 0)
            {
                if(tmp.x * tmp.y == 900) serializedObject.FindProperty("gridSize").vector2IntValue = tmp;
                if (tmp.x * tmp.y >= 900)
                {
                    EditorGUILayout.BeginHorizontal("box");
                    EditorGUILayout.HelpBox($"You`re inserting more than 900 slots ({tmp.x * tmp.y}), with more than this amount, depending on your setup, unity may crash, and you may need to delete this recipe. Also in game performace might be affected with a recipe of this size", MessageType.Warning);
                    if (GUILayout.Button($"Allow big values ({allowBigValues})"))
                    {
                        allowBigValues = !allowBigValues;
                    }
                    if (allowBigValues)
                    {
                        serializedObject.FindProperty("gridSize").vector2IntValue = tmp;
                    }
                    EditorGUILayout.EndHorizontal();

                }else serializedObject.FindProperty("gridSize").vector2IntValue = tmp;
            }
            gridSize = serializedObject.FindProperty("gridSize").vector2IntValue;

            serializedObject.FindProperty("pattern").arraySize = gridSize.x * gridSize.y;
            serializedObject.FindProperty("amountPattern").arraySize = gridSize.x * gridSize.y;

            var gridRect = EditorGUILayout.BeginVertical(GUILayout.ExpandHeight(true));
            
            for(int i = 0;i < gridSize.y; i++)
            {
                EditorGUILayout.BeginHorizontal();
                for(int j = 0;j < gridSize.x; j++)
                {
                    var sizeOffset = 30 / gridSize.y;

                    var xOffsetValue = j * (gridRect.width / gridSize.x);
                    var yOffsetValue = i * (gridRect.width / gridSize.x);
                    var sideValue = (gridRect.width / gridSize.x) - sizeOffset;

                    if((sideValue + sizeOffset) * gridSize.y > gridRect.height)
                    {
                        xOffsetValue = j * (gridRect.height / gridSize.y);
                        yOffsetValue = i * (gridRect.height / gridSize.y);
                        sideValue = (gridRect.height / gridSize.y) - sizeOffset;
                    }

                    GUIContent btnContent = new GUIContent();
                    var slot = serializedObject.FindProperty("pattern").GetArrayElementAtIndex(i * gridSize.x + j).objectReferenceValue;
                    if (slot as Item != null)
                    {
                        btnContent.image = (slot as Item).sprite.texture;
                        if(sideValue > 80)
                            btnContent.text = (slot as Item).name;
                        else
                            btnContent.text = "";
                    } 
                    else
                    {
                        if (sideValue > 70)
                            btnContent.text = "Place Item";
                        else
                            btnContent.text = "Null";
                    }

                    Rect btnRect = new Rect(gridRect.x + xOffsetValue, gridRect.y + yOffsetValue, sideValue, sideValue);
                    
                    if(GUI.Button(btnRect, btnContent))
                    {
                        //i * gridsize.x + j
                        if (selectedIndex == items.Count)
                        {
                            serializedObject.FindProperty("pattern").GetArrayElementAtIndex(i * gridSize.x + j).objectReferenceValue = null;
                            serializedObject.FindProperty("amountPattern").GetArrayElementAtIndex(i * gridSize.x + j).intValue = 0;
                        }else if (selectedIndex == items.Count + 1)
                        {
                            if (serializedObject.FindProperty("amountPattern").GetArrayElementAtIndex(i * gridSize.x + j).intValue > 0)
                            {
                                var a = serializedObject.FindProperty("amountPattern").GetArrayElementAtIndex(i * gridSize.x + j).intValue--;
                                if (a - 1 <= 0) serializedObject.FindProperty("pattern").GetArrayElementAtIndex(i * gridSize.x + j).objectReferenceValue = null;
                            }
                        }
                        else if (selectedIndex < items.Count)
                        {
                            if(serializedObject.FindProperty("pattern").GetArrayElementAtIndex(i * gridSize.x + j).objectReferenceValue == items[selectedIndex])
                            {
                                if(serializedObject.FindProperty("amountPattern").GetArrayElementAtIndex(i * gridSize.x + j).intValue < (serializedObject.FindProperty("pattern").GetArrayElementAtIndex(i * gridSize.x + j).objectReferenceValue as Item).maxAmount)
                                serializedObject.FindProperty("amountPattern").GetArrayElementAtIndex(i * gridSize.x + j).intValue++;
                            } else
                            {
                                serializedObject.FindProperty("amountPattern").GetArrayElementAtIndex(i * gridSize.x + j).intValue = 1;
                            }
                            serializedObject.FindProperty("pattern").GetArrayElementAtIndex(i * gridSize.x + j).objectReferenceValue = items[selectedIndex];
                        }
                    }
                    Rect labelRect = new Rect(gridRect.x + xOffsetValue + sideValue - 20, gridRect.y + yOffsetValue + (sideValue / 2) - 20, sideValue, sideValue);
                    if (serializedObject.FindProperty("pattern").GetArrayElementAtIndex(i * gridSize.x + j).objectReferenceValue != null)
                        EditorGUI.LabelField(labelRect, serializedObject.FindProperty("amountPattern").GetArrayElementAtIndex(i * gridSize.x + j).intValue.ToString());
                }
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndVertical();

            EditorGUILayout.EndVertical();
            EditorGUILayout.EndScrollView();

            #endregion

            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.EndVertical();

        EditorGUILayout.EndHorizontal();

        serializedObject.ApplyModifiedProperties();
    }
}
