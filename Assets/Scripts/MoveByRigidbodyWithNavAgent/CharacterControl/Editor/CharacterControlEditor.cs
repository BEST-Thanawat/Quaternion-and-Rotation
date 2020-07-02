using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.TerrainAPI;
using UnityEngine;

[CustomEditor(typeof(CharacterControl))]
public class CharacterControlEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        CharacterControl control = (CharacterControl)target;
        if(GUILayout.Button("Setup Ragdollparts (BodyParts)"))
        {
            control.SetRagdollParts();
        }
    }
}
