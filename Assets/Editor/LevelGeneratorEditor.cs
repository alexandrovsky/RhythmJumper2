using UnityEngine;
using System.Collections;
using UnityEditor;
//[CustomEditor((typeof(LevelGenerator)))]
public class LevelGeneratorEditor : Editor {

	private bool listVisibility = true;
	LevelGenerator lg;
//	public override void OnInspectorGUI(){
//
//		
//
//
//
//	}

	public override void OnInspectorGUI()
	{
		lg = (LevelGenerator)target;

//		if(!lg.sequences){
//			lg.sequences = new LevelSequence[1];
//		}

		int numOfSequences = 2; //lg.sequences.Length;

		numOfSequences = EditorGUILayout.IntField("Number of Sequences", numOfSequences);

		if(numOfSequences != lg.sequences.Length){
			
		}


	}

	public void ListIterator(string propertyPath, ref bool visible)
	{
		SerializedProperty listProperty = serializedObject.FindProperty(propertyPath);
		visible = EditorGUILayout.Foldout(visible, listProperty.name);
		if (visible)
		{
			EditorGUI.indentLevel++;
			for (int i = 0; i < listProperty.arraySize; i++)
			{
				SerializedProperty elementProperty = listProperty.GetArrayElementAtIndex(i);
				Rect drawZone = GUILayoutUtility.GetRect(0f, 16f);
				bool showChildren = EditorGUI.PropertyField(drawZone, elementProperty); 
			}
			EditorGUI.indentLevel--;
		}
	}

}
