using OurWorld.Scripts.Navigation.Directions;
using OurWorld.Scripts.Providers.MapAPIProviders;
using UnityEditor;
using UnityEngine;

namespace OurWorld.Scripts.Editor
{
    [CustomEditor(typeof(DirectionsController))]
    public class DirectionsControllerEditor : UnityEditor.Editor
    {
        private DirectionsController TargetScript => base.target as DirectionsController;
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if (GUILayout.Button("CreateDirections", EditorStyles.miniButton))
            {
                TargetScript.Initialize(new MapboxDirectionsAPIProvider());
                TargetScript.CreateDirectionsForTarget(new DataModels.GeolocationData.Geolocation(32.7050417737766,39.99658896597421));
            }

            if (GUILayout.Button("DisposeDirections", EditorStyles.miniButton))
            {
                TargetScript.Dispose();
            }
        }
    }
}