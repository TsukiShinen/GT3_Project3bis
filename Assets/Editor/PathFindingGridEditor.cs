using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Grid))]
class PathFindingGridEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Grid myTarget = (Grid)target;

        DrawDefaultInspector();
        if (GUILayout.Button("Toogle Gizmos"))
            myTarget.EnableGizmos = !myTarget.EnableGizmos;
    }
}