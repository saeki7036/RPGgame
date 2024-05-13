using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class EnemyBaseEditer : EditorWindow
{
    [MenuItem("Window/OriginalPanels/EnemyBaseEditer", priority = 3)]
    static public void CreateWindow()
    {
        EditorWindow.GetWindow<EnemyBaseEditer>();
    }
    private Vector2 scrollPosition;

    [SerializeField]
    private GUISkin _skin;

    [SerializeField]
    private EnemyDataBase BaseData;

    [SerializeField]
    private string BaseDataPath;

    [SerializeField]
    private int Selected_Index;

    // Update is called once per frame
    private void OnEnable()
    {
        this._skin = AssetDatabase.LoadAssetAtPath<GUISkin>("Assets/Datas/GUISkin_Data.guiskin");
        var defaultData = Resources.Load<EnemyDataBase>("EnemyDataBase");
        this.BaseData = defaultData.Clone();
        this.BaseDataPath = AssetDatabase.GetAssetPath(defaultData);
    }

    private void OnGUI()
    {
        if (BaseData == null) this.BaseData = AssetDatabase.LoadAssetAtPath<EnemyDataBase>(this.BaseDataPath).Clone();
        using (new EditorGUILayout.VerticalScope(_skin.GetStyle("Header"), GUILayout.MaxHeight(90f)))
        {
            Undo.RecordObject(BaseData, "Modify FileName or Caption of EnemyDataBase");
            BaseData.FileName = EditorGUILayout.TextArea(BaseData.FileName);
            BaseData.FileCaption = EditorGUILayout.TextArea(BaseData.FileCaption,
                GUILayout.Height(EditorGUIUtility.singleLineHeight));

            using (new EditorGUILayout.HorizontalScope())
            {
                if (GUILayout.Button("敵を追加", GUILayout.MaxWidth(120f), GUILayout.MaxHeight(20f)))
                {
                    Undo.RecordObject(BaseData, "Add Enemy");
                    int Enemy_ID_Length = this.BaseData.ID.Length;
                    System.Array.Resize(ref this.BaseData.ID, Enemy_ID_Length + 1);
                    this.BaseData.ID[Enemy_ID_Length] = new EnemyDataBase.Menber()
                    {
                        Name = "名前を入力してください",
                        max_hp = 1,
                        atk = 1,
                        def = 1,
                        luk = 1,
                        agi = 1,
                        exp = 1,
                        caption = "説明文",
                    };
                }

                GUILayout.FlexibleSpace();

                if (GUILayout.Button("元に戻す", GUILayout.MaxWidth(60f), GUILayout.MaxHeight(20f)))
                {
                    this.BaseData = AssetDatabase.LoadAssetAtPath<EnemyDataBase>(this.BaseDataPath).Clone();
                    EditorGUIUtility.editingTextField = false;
                }

                if (GUILayout.Button("保存", GUILayout.MaxWidth(60f), GUILayout.MaxHeight(20f)))
                {
                    var data = AssetDatabase.LoadAssetAtPath<EnemyDataBase>(this.BaseDataPath);
                    EditorUtility.CopySerialized(this.BaseData, data);
                    EditorUtility.SetDirty(data);
                    AssetDatabase.SaveAssets();
                }
            }
        }

        using (new EditorGUILayout.HorizontalScope(GUILayout.MaxHeight(800f)))
        {
            using (var scroll = new EditorGUILayout.ScrollViewScope(scrollPosition, GUILayout.MinWidth(315f)))
            {
                using (new EditorGUILayout.HorizontalScope())
                {
                    EditorGUILayout.LabelField("ID", GUILayout.MaxWidth(30f));
                    EditorGUILayout.LabelField("|", GUILayout.MaxWidth(10f));
                    EditorGUILayout.LabelField("Enemy Name", GUILayout.MaxWidth(170f));
                    EditorGUILayout.LabelField("|", GUILayout.MaxWidth(10f));
                    EditorGUILayout.LabelField("編集ボタン", GUILayout.MaxWidth(60f));
                }
                scrollPosition = scroll.scrollPosition;

                for (int i = 0; i < this.BaseData.ID.Length; i++)
                {
                    var data = this.BaseData.ID[i];
                    using (new EditorGUILayout.HorizontalScope(_skin.GetStyle("Scroll")))
                    {
                        EditorGUILayout.LabelField(i.ToString(), GUILayout.MaxWidth(30f));
                        EditorGUILayout.LabelField("|", GUILayout.MaxWidth(10f));
                        EditorGUILayout.LabelField(data.Name, GUILayout.MaxWidth(170f));
                        EditorGUILayout.LabelField("|", GUILayout.MaxWidth(10f));
                        if (GUILayout.Button("編集", GUILayout.MaxWidth(60f)))
                        {
                            Undo.RecordObject(this, "Select Enemy");
                            this.Selected_Index = i;
                        }
                    }
                }

            }

            using (new EditorGUILayout.VerticalScope())
            {               
                if (0 <= this.Selected_Index && this.Selected_Index < this.BaseData.ID.Length)
                {
                    using (new EditorGUILayout.VerticalScope(_skin.GetStyle("Inspector")))
                    {
                        var selectedItem = this.BaseData.ID[this.Selected_Index];
                        EditorGUILayout.LabelField("ID", this.Selected_Index.ToString());
                        selectedItem.Object = (GameObject)EditorGUILayout.ObjectField("オブジェクト", selectedItem.Object, typeof(GameObject));
                        selectedItem.Name = EditorGUILayout.TextField("敵の名前", selectedItem.Name);                      
                        selectedItem.max_hp = EditorGUILayout.IntField("最大HP", selectedItem.max_hp);
                        selectedItem.atk = EditorGUILayout.IntField("攻撃力", selectedItem.atk);
                        selectedItem.def = EditorGUILayout.IntField("防御力", selectedItem.def);
                        selectedItem.luk = EditorGUILayout.IntField("運の良さ", selectedItem.luk);
                        selectedItem.agi = EditorGUILayout.IntField("回避率", selectedItem.agi); 
                        selectedItem.exp = EditorGUILayout.IntField("経験値", selectedItem.exp);
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
                DragAndDrop.objectReferences[0] is EnemyDataBase)
            {
                DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
                Event.current.Use();
            }
        }
        else if (Event.current.type == EventType.DragPerform)
        {
            Undo.RecordObject(this, "Change ItemDataBase");
            this.BaseData = ((EnemyDataBase)DragAndDrop.objectReferences[0]).Clone();
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
