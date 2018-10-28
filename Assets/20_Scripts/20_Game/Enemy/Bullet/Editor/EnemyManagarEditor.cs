using System.Collections;
using System.Collections.Generic;
using Ria;
using UnityEngine;
using UnityEditor;

/*
[CustomEditor (typeof(Ria.EnemyManager))]
public class EnemyManagarEditor : Editor
{
	bool isInitialized = false;
	bool Enemy1List = false;

	public override void OnInspectorGUI()
	{
		Ria.EnemyManager enemy = this.target as Ria.EnemyManager;
		//EditorGUILayout.PropertyField(enemy.Enemy1Patterns , new GUIContent("敵の種類"), true);

		if (!isInitialized) InitializeList(list.Count);

		
		if (this.Enemy1List == EditorGUILayout.Foldout(this.Enemy1List, "List"))
		{
			for (int i = 0; i < enemy.Enemy1Patterns.Count; i++)
			{
				enemy.Enemy1Patterns[i] =
					(Ria.EnemyManager.EnemyPattern) EditorGUILayout.EnumPopup("敵の種類", enemy.Enemy1Patterns[i]);
				
				enemy.Enemy1Patterns[i] =
					(EnemyManager.EnemyPattern) EditorGUILayout.EnumPopup("敵の種類", enemy.Enemy1Patterns[i]);
					
				
				if (enemy.Enemy1Patterns[i] == enemy.)
				{
					
				}
				

			}

			if (GUILayout.Button("Add"))
			{
				enemy.Enemy1Patterns.Add(new EnemyManager.EnemyPattern());
			}
		}

	EditorUtility.SetDirty( target );
			
	}
}
*/