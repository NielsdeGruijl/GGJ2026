using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TilemapGenerator))]
public class GenerateTilemapButton : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        DrawDefaultInspector();

        TilemapGenerator script = target as TilemapGenerator;
        
        if (GUILayout.Button("Generate Tilemap"))
        {
            script.GenerateChunk();
        }
    }
}
