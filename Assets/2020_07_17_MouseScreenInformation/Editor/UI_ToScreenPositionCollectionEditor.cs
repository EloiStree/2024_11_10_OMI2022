using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(UI_ToScreenPositionCollection))]
public class UI_ToScreenPositionCollectionEditor : Editor
{
    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Load UI In Scene"))
        {
            UI_ToScreenPositionCollection t = (UI_ToScreenPositionCollection)target;
            t.m_screenZonesUI = GetAllObjectsOnlyInScene().ToArray();
        }
        DrawDefaultInspector();
        
    }
    List<UI_ToScreenPosition> GetAllObjectsOnlyInScene()
    {
        List<UI_ToScreenPosition> objectsInScene = new List<UI_ToScreenPosition>();

        foreach (GameObject go in Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[])
        {
            if (!EditorUtility.IsPersistent(go.transform.root.gameObject) && !(go.hideFlags == HideFlags.NotEditable || go.hideFlags == HideFlags.HideAndDontSave))
            {
                UI_ToScreenPosition ui = go.GetComponent<UI_ToScreenPosition>();
                if(ui!=null)
                    objectsInScene.Add(ui);
            
            }
        }

        return objectsInScene;
    }
}
