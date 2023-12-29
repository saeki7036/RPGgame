using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class ItemEditer : EditorWindow
{
    [MenuItem("Window/OriginalPanels/ItemEditer", priority = 2)]
    static public void CreateWindow()
    {
        EditorWindow.GetWindow<ItemEditer>();
    }
    private Vector2 scrollPosition;

    [SerializeField]
    private GUISkin skin;

    [SerializeField]
    private ItemData itemData;
    [SerializeField]
    private string itemDataPath;
    [SerializeField]
    private int selectedIndex;

    private void OnEnable()
    {
        this.skin = AssetDatabase.LoadAssetAtPath<GUISkin>("Assets/Test/GUISkin_Item.guiskin");
        var defaultData = Resources.Load<ItemData>("ItemData");
        this.itemData = defaultData.Clone();
        this.itemDataPath = AssetDatabase.GetAssetPath(defaultData);
    }




    private void OnGUI()
    {
        using (new EditorGUILayout.VerticalScope(skin.GetStyle("Header")))
        {
            Undo.RecordObject(itemData, "Modify FileName or Caption of ItemData");
            itemData.FileName = EditorGUILayout.TextField(itemData.FileName);
            itemData.FileCaption = EditorGUILayout.TextArea(itemData.FileCaption,
                GUILayout.Height(EditorGUIUtility.singleLineHeight * 2f));

            using (new EditorGUILayout.HorizontalScope())
            {
                if (GUILayout.Button("アイテムを追加", GUILayout.MaxWidth(120f), GUILayout.MaxHeight(40f)))
                {
                    Undo.RecordObject(itemData, "Add Item");
                    int itemLength = this.itemData.items.Length;
                    System.Array.Resize(ref this.itemData.items, itemLength + 1);
                    this.itemData.items[itemLength] = new ItemData.Item()
                    {
                        name = "名前を入力してください",
                        caption = "説明文",
                    };
                }
                GUILayout.FlexibleSpace();

                if (GUILayout.Button("元に戻す", GUILayout.MaxWidth(120f), GUILayout.MaxHeight(40f)))
                {
                    this.itemData = AssetDatabase.LoadAssetAtPath<ItemData>(this.itemDataPath).Clone();
                    EditorGUIUtility.editingTextField = false;
                }

                if (GUILayout.Button("保存", GUILayout.MaxWidth(120f), GUILayout.MaxHeight(40f)))
                {
                    var data = AssetDatabase.LoadAssetAtPath<ItemData>(this.itemDataPath);
                    EditorUtility.CopySerialized(this.itemData, data);
                    EditorUtility.SetDirty(data);
                    AssetDatabase.SaveAssets();
                }
            }
        }

        using (new EditorGUILayout.HorizontalScope())
        {
            using (var scroll = new EditorGUILayout.ScrollViewScope(scrollPosition, GUILayout.MinWidth(340f)))
            {
                scrollPosition = scroll.scrollPosition;

                for (int i = 0; i < this.itemData.items.Length; i++)
                {
                    var data = this.itemData.items[i];
                    using (new EditorGUILayout.HorizontalScope(skin.GetStyle("Scroll")))
                    {
                        EditorGUILayout.LabelField(i.ToString(), GUILayout.MaxWidth(30f));
                        EditorGUILayout.LabelField(data.id.ToString(), GUILayout.MaxWidth(50f));
                        EditorGUILayout.LabelField(data.name, GUILayout.MaxWidth(170f));
                        if (GUILayout.Button("編集", GUILayout.MaxWidth(70f)))
                        {
                            Undo.RecordObject(this, "Select Item");
                            this.selectedIndex = i;
                        }
                    }
                }

            }

            using (new EditorGUILayout.VerticalScope(skin.GetStyle("Inspector")))
            {

                if (0 <= this.selectedIndex && this.selectedIndex < this.itemData.items.Length)
                {
                    //Undo.RecordObject(itemData, "Modify ItemData at " + this.selectedIndex);
                    var selectedItem = this.itemData.items[this.selectedIndex];
                    selectedItem.id = EditorGUILayout.IntField("ID", selectedItem.id);
                    selectedItem.name = EditorGUILayout.TextField("アイテム名", selectedItem.name);
                    selectedItem.type = (ItemType)EditorGUILayout.EnumPopup("アイテムタイプ", selectedItem.type);
                    selectedItem.caption = EditorGUILayout.TextArea(selectedItem.caption, GUILayout.Height(EditorGUIUtility.singleLineHeight * 4f));
                }
                //GUILayout.FlexibleSpace();
            }
        }

        if (Event.current.type == EventType.DragUpdated)
        {
            if (DragAndDrop.objectReferences != null &&
                DragAndDrop.objectReferences.Length > 0 &&
                DragAndDrop.objectReferences[0] is ItemData)
            {
                DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
                Event.current.Use();
            }
        }
        else if (Event.current.type == EventType.DragPerform)
        {
            Undo.RecordObject(this, "Change ItemData");
            this.itemData = ((ItemData)DragAndDrop.objectReferences[0]).Clone();
            this.itemDataPath = DragAndDrop.paths[0];
            DragAndDrop.AcceptDrag();
            Event.current.Use();
        }

        if (DragAndDrop.visualMode == DragAndDropVisualMode.Copy)
        {
            var rect = new Rect(Vector2.zero, this.position.size);
            var bgColor = Color.white * new Color(1f, 1f, 1f, 0.2f);
            EditorGUI.DrawRect(rect, bgColor);
            EditorGUI.LabelField(rect, "ここにアイテムデータをドラッグ＆ドロップしてください", this.skin.GetStyle("D&D"));
        }
    }
    public void AddItemsToMenu(GenericMenu menu)
    {
        menu.AddItem(new GUIContent("Original Menu"), false, () => Debug.Log("Press Menu!"));
    }
}
