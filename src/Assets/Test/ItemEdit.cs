using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class ItemEdit : EditorWindow
{
    [MenuItem("Window/ItemSet/ItemEdit", priority = 1)]
    static public void CreateWindow()
    {
        EditorWindow.GetWindow<ItemEdit>();
    }
    [SerializeField] private GUISkin skin; 
    private Vector2 scrollPosition;
    private string fileTitle = "File Title";
    private string fileCaption = "File Caption";

    private void OnEnable()
    {
        this.skin = AssetDatabase.LoadAssetAtPath<GUISkin>("Assets/Test/EditGUISkin.guiskin");
    }




    private void OnGUI()
    {
        using (new EditorGUILayout.VerticalScope(this.skin.GetStyle("Header")))
        {
            EditorGUILayout.LabelField("Assets/Test/Map1Items.json");
            fileTitle = EditorGUILayout.TextField(fileTitle);
            fileCaption = EditorGUILayout.TextArea(fileCaption, 
                GUILayout.Height(EditorGUIUtility.singleLineHeight * 2f));

            using (new EditorGUILayout.HorizontalScope())
            {
                GUILayout.Button("アイテム追加");
                GUILayout.FlexibleSpace();
                GUILayout.Button("元に戻す");
                GUILayout.Button("保存");
            }
        }



        using (var scroll = new EditorGUILayout.ScrollViewScope(scrollPosition, GUILayout.MinWidth(320f)))
        {
            scrollPosition = scroll.scrollPosition;

            for (int i = 0; i < 100; i++) 
            {
                using (new EditorGUILayout.HorizontalScope())
                {
                    EditorGUILayout.LabelField("アイテム名", GUILayout.MaxWidth(50f));
                    GUILayout.Button(">_<", GUILayout.MaxWidth(50f));
                }
            }
                
        }
    }
}
