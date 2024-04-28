using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ItemBaseEditer : EditorWindow
{
    [MenuItem("Window/OriginalPanels/ItemBaseEditer", priority = 1)]
    static public void CreateWindow()
    {
        EditorWindow.GetWindow<ItemBaseEditer>();
    }
    private Vector2 scrollPosition;

    [SerializeField]
    private GUISkin _skin;

    [SerializeField]
    private ItemDataBase BaseData;

    [SerializeField]
    private string BaseDataPath;

    [SerializeField]
    private int Selected_Index;

    private int Slider_value = 0;
    // Update is called once per frame
    private void OnEnable()
    {
        this._skin = AssetDatabase.LoadAssetAtPath<GUISkin>("Assets/Datas/GUISkin_Data.guiskin");
        var defaultData = Resources.Load<ItemDataBase>("ItemDataBase");
        this.BaseData = defaultData.Clone();
        this .BaseDataPath = AssetDatabase.GetAssetPath(defaultData);
    }

    private void OnGUI()
    {
        if(BaseData == null) this.BaseData = AssetDatabase.LoadAssetAtPath<ItemDataBase>(this.BaseDataPath).Clone();
        using (new EditorGUILayout.VerticalScope(_skin.GetStyle("Header"), GUILayout.MaxHeight(90f)))
        {
            Undo.RecordObject(BaseData, "Modify FileName or Caption of ItemDataBase");
            BaseData.FileName = EditorGUILayout.TextArea(BaseData.FileName);
            BaseData.FileCaption = EditorGUILayout.TextArea(BaseData.FileCaption,
                GUILayout.Height(EditorGUIUtility.singleLineHeight));

            using (new EditorGUILayout.HorizontalScope())
            {
                if (GUILayout.Button("アイテムを追加", GUILayout.MaxWidth(120f), GUILayout.MaxHeight(20f)))
                {
                    Undo.RecordObject(BaseData, "Add Item");
                    int Item_ID_Length = this.BaseData.items[Slider_value].category.ID.Length;
                    System.Array.Resize(ref this.BaseData.items[Slider_value].category.ID, Item_ID_Length + 1);
                    this.BaseData.items[Slider_value].category.ID[Item_ID_Length] = new ItemDataBase.Items.Category.Menber()
                    {
                        Name = "名前を入力してください",
                        Efect1 = 0,
                        Efect2 = 0,
                        Efect3 = 0,
                        caption = "説明文",
                    };
                }

                Slider_value = (int)EditorGUILayout.Slider(Slider_value, 0, this.BaseData.items.Length -1,
                    GUILayout.MaxWidth(160f), GUILayout.MaxHeight(20f));

                EditorGUILayout.LabelField("|:",
                    GUILayout.MaxWidth(20f), GUILayout.MaxHeight(20f));

                EditorGUILayout.LabelField(this.BaseData.items[Slider_value].type.ToString(),
                    GUILayout.MaxWidth(80f), GUILayout.MaxHeight(20f));


                GUILayout.FlexibleSpace();

                if (GUILayout.Button("元に戻す", GUILayout.MaxWidth(60f), GUILayout.MaxHeight(20f)))
                {
                    this.BaseData = AssetDatabase.LoadAssetAtPath<ItemDataBase>(this.BaseDataPath).Clone();
                    EditorGUIUtility.editingTextField = false;
                }

                if (GUILayout.Button("保存", GUILayout.MaxWidth(60f), GUILayout.MaxHeight(20f)))
                {
                    var data = AssetDatabase.LoadAssetAtPath<ItemDataBase>(this.BaseDataPath);
                    EditorUtility.CopySerialized(this.BaseData, data);
                    EditorUtility.SetDirty(data);
                    AssetDatabase.SaveAssets();
                }
            }
        }

        using (new EditorGUILayout.HorizontalScope( GUILayout.MaxHeight(800f)))
        {
            using (var scroll = new EditorGUILayout.ScrollViewScope(scrollPosition, GUILayout.MinWidth(315f)))
            {
                using (new EditorGUILayout.HorizontalScope())
                {
                    EditorGUILayout.LabelField("ID", GUILayout.MaxWidth(30f));
                    EditorGUILayout.LabelField("|", GUILayout.MaxWidth(10f));
                    EditorGUILayout.LabelField("Item Name", GUILayout.MaxWidth(170f));
                    EditorGUILayout.LabelField("|", GUILayout.MaxWidth(10f));
                    EditorGUILayout.LabelField("編集ボタン", GUILayout.MaxWidth(60f));
                }
                    scrollPosition = scroll.scrollPosition;

                for (int i = 0; i < this.BaseData.items[Slider_value].category.ID.Length; i++)
                {
                    var data = this.BaseData.items[Slider_value].category.ID[i];
                    using (new EditorGUILayout.HorizontalScope(_skin.GetStyle("Scroll")))
                    {
                        EditorGUILayout.LabelField(i.ToString(), GUILayout.MaxWidth(30f));
                        EditorGUILayout.LabelField("|", GUILayout.MaxWidth(10f));
                        EditorGUILayout.LabelField(data.Name, GUILayout.MaxWidth(170f));
                        EditorGUILayout.LabelField("|", GUILayout.MaxWidth(10f));
                        if (GUILayout.Button("編集", GUILayout.MaxWidth(60f)))
                        {
                            Undo.RecordObject(this, "Select Item");
                            this.Selected_Index = i;
                        }
                    }
                }

            }

            using (new EditorGUILayout.VerticalScope())
            {
                EditorGUILayout.LabelField("Caption", GUILayout.MaxHeight(20f));

                if (0 <= this.Selected_Index && this.Selected_Index < this.BaseData.items[Slider_value].category.ID.Length)
                {
                    using (new EditorGUILayout.VerticalScope(_skin.GetStyle("Inspector")))
                    {
                        //Undo.RecordObject(itemData, "Modify ItemData at " + this.selectedIndex);
                        var selectedItem = this.BaseData.items[Slider_value].category.ID[this.Selected_Index];

                        EditorGUILayout.LabelField("ID", this.Selected_Index.ToString());
                        selectedItem.Name = EditorGUILayout.TextField("アイテム名", selectedItem.Name);
                        selectedItem.Efect1 = EditorGUILayout.IntField("Efect1", selectedItem.Efect1);
                        selectedItem.Efect2 = EditorGUILayout.IntField("Efect2", selectedItem.Efect2);
                        selectedItem.Efect3 = EditorGUILayout.IntField("Efect3", selectedItem.Efect3);

                        EditorGUILayout.LabelField("Caption", GUILayout.MaxHeight(25f));

                        selectedItem.caption = EditorGUILayout.TextArea(selectedItem.caption, GUILayout.Height(EditorGUIUtility.singleLineHeight * 4f));
                    }
                }
                //GUILayout.FlexibleSpace();
            }
        }

        if (Event.current.type == EventType.DragUpdated)
        {
            if (DragAndDrop.objectReferences != null &&
                DragAndDrop.objectReferences.Length > 0 &&
                DragAndDrop.objectReferences[0] is ItemDataBase)
            {
                DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
                Event.current.Use();
            }
        }
        else if (Event.current.type == EventType.DragPerform)
        {
            Undo.RecordObject(this, "Change ItemDataBase");
            this.BaseData = ((ItemDataBase)DragAndDrop.objectReferences[0]).Clone();
            this.BaseDataPath = DragAndDrop.paths[0];
            DragAndDrop.AcceptDrag();
            Event.current.Use();
        }
        if (DragAndDrop.visualMode == DragAndDropVisualMode.Copy)
        {
            var rect = new Rect(Vector2.zero, this.position.size);
            var bgColor = Color.white * new Color(1f, 1f, 1f, 0.2f);
            EditorGUI.DrawRect(rect, bgColor);
            EditorGUI.LabelField(rect, "ここにアイテムデータをドラッグ＆ドロップしてください", this._skin.GetStyle("D&D"));
        }
    }
    public void AddItemsToMenu(GenericMenu menu)
    {
        menu.AddItem(new GUIContent("Original Menu"), false, () => Debug.Log("Press Menu!"));
    }
}
