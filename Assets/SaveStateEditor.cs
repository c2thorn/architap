using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class SaveStateEditor : MonoBehaviour {

	[CustomEditor(typeof(SaveStateController))]
	public class ObjectBuilderEditor : Editor
	{
		public override void OnInspectorGUI()
		{
			DrawDefaultInspector();
			
			SaveStateController myScript = (SaveStateController)target;
			if(GUILayout.Button("Load"))
			{
				myScript.LoadData();
			}

			if(GUILayout.Button("Save"))
			{
				myScript.SaveData();
			}

			if(GUILayout.Button("Delete Save"))
			{
				myScript.DeleteAll();
			}
		}
	}
}
