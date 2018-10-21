using System;
using System.Runtime.Remoting.Messaging;

using UnityEditor;
using UnityEngine;
using UnityEditorInternal;

[CustomEditor(typeof(CharaSelectManager))]
public class CharaSelectManagerEditor : Editor
{
    private SerializedProperty charaSelectorSetsProp;

    private SerializedProperty pl1keyProp;
    private SerializedProperty pl1FirstIndexProp;
    private SerializedProperty pl2keyProp;
    private SerializedProperty pl2FirstIndexProp;

    private SerializedProperty pressedWaitTimeProp;
    private SerializedProperty pressedReactionIntervalTimeProp;
    private SerializedProperty pressedRapidIntervalTimeProp;

    private SerializedProperty selectorSpriteProp;

    private SerializedProperty pl1ImagesProp;
    private SerializedProperty pl2ImagesProp;

    private SerializedProperty nextToStageSelectImageProp;
    private SerializedProperty nextToStageSelectIntervalTimeProp;

	private SerializedProperty prevToTitleImageProp;
	private SerializedProperty prevToTitleIntervalTimeProp;

	private bool isOpen_ = true;
    private int currentIndex;

    private void OnEnable()
    {
        this.charaSelectorSetsProp = serializedObject.FindProperty("charaSelectorSets");

        this.pl1keyProp = serializedObject.FindProperty("pl1key");
        this.pl2keyProp = serializedObject.FindProperty("pl2key");
        this.pl1FirstIndexProp = serializedObject.FindProperty("pl1FirstIndex");
        this.pl2FirstIndexProp = serializedObject.FindProperty("pl2FirstIndex");

        this.pressedWaitTimeProp = serializedObject.FindProperty("pressedWaitTime");
        this.pressedReactionIntervalTimeProp = serializedObject.FindProperty("pressedReactionIntervalTime");
        this.pressedRapidIntervalTimeProp = serializedObject.FindProperty("pressedRapidIntervalTime");
        this.selectorSpriteProp = serializedObject.FindProperty("sprites");

        this.pl1ImagesProp = serializedObject.FindProperty("pl1Images");
        this.pl2ImagesProp = serializedObject.FindProperty("pl2Images");

        this.nextToStageSelectImageProp = serializedObject.FindProperty("nextToStageSelectImage");
        this.nextToStageSelectIntervalTimeProp = serializedObject.FindProperty("nextToStageSelectIntervalTime");

		this.prevToTitleImageProp = serializedObject.FindProperty("prevToTitleImage");
		this.prevToTitleIntervalTimeProp = serializedObject.FindProperty("prevToTitleIntervalTime");
	}

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        // OnEnable();

        EditorGUILayout.PropertyField(this.pl1keyProp, true);
        EditorGUILayout.PropertyField(this.pl1FirstIndexProp, true);
        EditorGUILayout.PropertyField(this.pl2keyProp, true);
        EditorGUILayout.PropertyField(this.pl2FirstIndexProp, true);
        EditorGUILayout.PropertyField(this.pressedWaitTimeProp, true);
        EditorGUILayout.PropertyField(this.pressedReactionIntervalTimeProp, true);
        EditorGUILayout.PropertyField(this.pressedRapidIntervalTimeProp, true);
        EditorGUILayout.PropertyField(this.selectorSpriteProp, true);

        EditorGUILayout.Space();
        
        bool isOpen = EditorGUILayout.Foldout(this.isOpen_, new GUIContent("CharaSelectorSets", "キャラの画像セット"));
        if (this.isOpen_ != isOpen)
        {
            this.isOpen_ = isOpen;
        }

        if (isOpen)
        {
            EditorGUI.indentLevel++;
            if (this.charaSelectorSetsProp != null && this.charaSelectorSetsProp.arraySize != 0)
            {
                // サイズ
                this.charaSelectorSetsProp.arraySize = EditorGUILayout.IntField(new GUIContent("size", "キャラセットのサイズ"), this.charaSelectorSetsProp.arraySize);

                // 配列番号をスライダーで
                currentIndex = EditorGUILayout.IntSlider(currentIndex, 0, this.charaSelectorSetsProp.arraySize - 1);

                // 選択した要素を表示
                SerializedProperty selectedItemProp = this.charaSelectorSetsProp.GetArrayElementAtIndex(currentIndex);
                EditorGUILayout.PropertyField(selectedItemProp, true);

                {
                    // SerializedProperty enumPCProp = selectedItemProp.FindPropertyRelative("PlayerCharacter");
                    // PlayerCharacterEnum pce = (PlayerCharacterEnum)Enum.ToObject(typeof(PlayerCharacterEnum), enumPCProp.enumValueIndex);
                    // pce = (PlayerCharacterEnum)EditorGUILayout.EnumPopup(new GUIContent("PlayerCharacterEnum", "プレイヤータイプ"), pce);

                    // EditorGUILayout.LabelField("立ち絵");
                    // //EditorGUILayout.BeginHorizontal();
                    // {
                    //     SerializedProperty spStandPL1Prop = selectedItemProp.FindPropertyRelative("StandPL1");
                    //     Sprite sp1 = spStandPL1Prop.objectReferenceValue as Sprite;
                    //     sp1 = EditorGUILayout.ObjectField(new GUIContent("Player1Side", "プレイヤー1側"), sp1, typeof(Sprite), false) as Sprite;

                    //     SerializedProperty spStandPL2Prop = selectedItemProp.FindPropertyRelative("StandPL2");
                    //     Sprite sp2 = spStandPL2Prop.objectReferenceValue as Sprite;
                    //     sp2 = EditorGUILayout.ObjectField(new GUIContent("Player2Side", "プレイヤー2側"), sp2, typeof(Sprite), false) as Sprite;
                    // }
                    // //EditorGUILayout.EndHorizontal();

                    // EditorGUILayout.LabelField("名前");
                    // //EditorGUILayout.BeginHorizontal();
                    // {
                    //     SerializedProperty spKanaProp = selectedItemProp.FindPropertyRelative("KanaName");
                    //     Sprite spK = spKanaProp.objectReferenceValue as Sprite;
                    //     spK = EditorGUILayout.ObjectField(new GUIContent("Kana", "カナ"), spK, typeof(Sprite), false) as Sprite;

                    //     SerializedProperty spAlpProp = selectedItemProp.FindPropertyRelative("AlpName");
                    //     Sprite spA = spAlpProp.objectReferenceValue as Sprite;
                    //     spA = EditorGUILayout.ObjectField(new GUIContent("Alphabet", "アルファベット"), spA, typeof(Sprite), false) as Sprite;
                    // }
                    // //EditorGUILayout.EndHorizontal();

                    // EditorGUILayout.LabelField("セレクター");
                    // //EditorGUILayout.BeginHorizontal();
                    // {
                    //     SerializedProperty spSelectorProp = selectedItemProp.FindPropertyRelative("Selector");
                    //     Sprite sp = spSelectorProp.objectReferenceValue as Sprite;
                    //     sp = EditorGUILayout.ObjectField(new GUIContent("SelectorIcon", "セレクターアイコン"), sp, typeof(Sprite), false) as Sprite;
                    // }
                    // //EditorGUILayout.EndHorizontal();
                }

                selectedItemProp.isExpanded = true;
            }
            EditorGUI.indentLevel--;
        }

        EditorGUILayout.PropertyField(this.pl1ImagesProp, true);
        EditorGUILayout.PropertyField(this.pl2ImagesProp, true);

		EditorGUILayout.PropertyField(this.nextToStageSelectImageProp, true);
		EditorGUILayout.PropertyField(this.nextToStageSelectIntervalTimeProp, true);

		EditorGUILayout.PropertyField(this.prevToTitleImageProp, true);
		EditorGUILayout.PropertyField(this.prevToTitleIntervalTimeProp, true);

		serializedObject.ApplyModifiedProperties();
    }
}