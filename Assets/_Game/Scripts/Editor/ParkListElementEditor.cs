using OurWorld.Scripts.Views.ParksList;
using UnityEditor;
using UnityEngine;

namespace OurWorld.Scripts.Editor
{
    [CustomEditor(typeof(ParkListElement))]
    public class ParkListElementEditor : UnityEditor.Editor
    {
        private ParkListElement TargetScript => base.target as ParkListElement;
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if (GUILayout.Button("Open Panel", EditorStyles.miniButton))
            {
                TargetScript.Open();
            }

            if (GUILayout.Button("Close Panel", EditorStyles.miniButton))
            {
                TargetScript.Close();
            }
        }
    }
}