using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class MapEditer : EditorWindow
{
    [MenuItem("Window/OriginalPanels/EditorWindow", priority = 2)]
    static public void CreateWindow()
    {
        EditorWindow.GetWindow<MapEditer>();
    }

    [SerializeField]
    private MapDataBase BaseData;

    [SerializeField]
    private string BaseDataPath;

    [SerializeField]
    private GUISkin _skin;
    private void OnEnable()
    {
        var defaultData = Resources.Load<MapDataBase>("MapDataBase");
        this.BaseData = defaultData.Clone();
        this.BaseDataPath = AssetDatabase.GetAssetPath(defaultData);
    }

   
    const int BOARD_WIDTH = 30;
    const int BOARD_HEIGHT = 20;
    private Vector2 scrollPosition;
    private int Slider_value = 0;
   
    // Update is called once per frame




    private void SetData(int N)
    {
        for (int X = 0; X < BOARD_WIDTH; X++)
            for (int Y = 0; Y < BOARD_HEIGHT; Y++)
                BaseData.preset[N].presetData[Y, X] = Map_Object.EndWall;
    }
    private void OnGUI()
    {
        using (new EditorGUILayout.VerticalScope())
        {
            //Undo.RecordObject(BaseData, "Modify FileName or Caption of MapDataBase");
            using (new EditorGUILayout.HorizontalScope())
            {
             
                Slider_value = (int)EditorGUILayout.Slider("Map_ID", Slider_value, 0, this.BaseData.preset.Length - 1, GUILayout.MaxWidth(300f));
                EditorGUILayout.LabelField("||Map:", GUILayout.MaxWidth(40f));
               
                GUILayout.FlexibleSpace();
                EditorGUILayout.LabelField("|Floor:", GUILayout.MaxWidth(40f));
                if (GUILayout.Button("  マップを追加", GUILayout.MaxWidth(120f), GUILayout.MaxHeight(20f)))
                {
                    Undo.RecordObject(BaseData, "Add Map");
                    int Map_Length = this.BaseData.preset.Length;
                    System.Array.Resize(ref this.BaseData.preset, Map_Length + 1);
                    this.BaseData.preset[Map_Length] = new MapDataBase.Preset()
                    {
                        presetData = new Map_Object[BOARD_HEIGHT, BOARD_WIDTH],
                        Point = new MapDataBase.Preset.Floor[1],
                    };

                    SetData(Map_Length);
                }
                
                if (GUILayout.Button("ポイントを追加", GUILayout.MaxWidth(120f), GUILayout.MaxHeight(20f)))
                {
                    Undo.RecordObject(BaseData, "Add Point");
                    int Point_Length = this.BaseData.preset[Slider_value].Point.Length;
                    System.Array.Resize(ref this.BaseData.preset[Slider_value].Point, Point_Length + 1);
                    this.BaseData.preset[Slider_value].Point[Point_Length]  = new MapDataBase.Preset.Floor();
                    {
                        Vector2Int LowerLeftPos = new(0, 0);
                        Vector2Int UpperRightPos = new(0, 0);
                    };
                }
                if (GUILayout.Button("元に戻す", GUILayout.MaxWidth(60f), GUILayout.MaxHeight(20f)))
                {
                    this.BaseData = AssetDatabase.LoadAssetAtPath<MapDataBase>(this.BaseDataPath).Clone();
                    EditorGUIUtility.editingTextField = false;
                }

                if (GUILayout.Button("保存", GUILayout.MaxWidth(60f), GUILayout.MaxHeight(20f)))
                {
                    var data = AssetDatabase.LoadAssetAtPath<MapDataBase>(this.BaseDataPath);
                    EditorUtility.CopySerialized(this.BaseData, data);
                    EditorUtility.SetDirty(data);
                    AssetDatabase.SaveAssets();
                }
            }
        }

        using (new EditorGUILayout.HorizontalScope(GUILayout.MaxWidth(20f)))
        {
            if(0< this.BaseData.preset.Length)
                for (int X = 0; X < BOARD_WIDTH; X++)
                {
                    using (new EditorGUILayout.VerticalScope())
                    {

                        for (int Y = BOARD_HEIGHT - 1; Y >= 0; Y--)
                        {
                            using (new EditorGUILayout.HorizontalScope())
                            {
                                if (X == 0)
                                    EditorGUILayout.LabelField(Y.ToString(), GUILayout.MaxWidth(20f));
                                
                                Map_Object map_Object = BaseData.preset[Slider_value].presetData[Y, X];
                                switch (map_Object)
                                {
                                    case Map_Object.EndWall:
                                        GUI.backgroundColor = Color.red;
                                        break;
                                    case Map_Object.Spase:
                                        GUI.backgroundColor = Color.gray;
                                        break;
                                    case Map_Object.Water:
                                        GUI.backgroundColor = Color.blue;
                                        break;
                                    case Map_Object.Wall:
                                        GUI.backgroundColor = Color.green;
                                        break;
                                    case Map_Object.Floor:
                                        GUI.backgroundColor = Color.cyan;
                                        break;
                                    case Map_Object.Road:
                                        GUI.backgroundColor = Color.yellow;
                                        break;
                                    default:
                                        GUI.backgroundColor = Color.white;
                                        break;
                                }

                                BaseData.preset[Slider_value].presetData[Y, X] =
                                (Map_Object)EditorGUILayout.EnumPopup(BaseData.preset[Slider_value].presetData[Y, X],
                                GUILayout.MaxWidth(20f), GUILayout.MaxHeight(20f));

                                GUI.backgroundColor = Color.white;
                            }
                        }
                        using (new EditorGUILayout.HorizontalScope())
                        {
                            if (X == 0)
                                EditorGUILayout.LabelField("", GUILayout.MaxWidth(20f));
                            EditorGUILayout.LabelField(X.ToString(), GUILayout.MaxWidth(20f));
                        }
                    }
                }
            using (new EditorGUILayout.HorizontalScope(GUILayout.MaxWidth(300f)))
            {
                if (0 < this.BaseData.preset.Length&&0 < this.BaseData.preset[Slider_value].Point.Length)
                using (new EditorGUILayout.VerticalScope(GUILayout.MaxWidth(300f)))
                {
                    using (new EditorGUILayout.HorizontalScope())
                    {
                        EditorGUILayout.LabelField("No.", GUILayout.MaxWidth(20f));
                        EditorGUILayout.LabelField("|", GUILayout.MaxWidth(12f)); 
                        EditorGUILayout.LabelField("Pos", GUILayout.MaxWidth(25f));
                        EditorGUILayout.LabelField("|", GUILayout.MaxWidth(10f));
                        EditorGUILayout.LabelField("X", GUILayout.MaxWidth(40f));
                        EditorGUILayout.LabelField("Y", GUILayout.MaxWidth(30f));
                    }
                    using (var scroll = new EditorGUILayout.ScrollViewScope(scrollPosition, GUILayout.MinWidth(180f)))
                    {
                        scrollPosition = scroll.scrollPosition;
                       
                        for (int i = 0; i < this.BaseData.preset[Slider_value].Point.Length; i++)
                        {
                            using (new EditorGUILayout.HorizontalScope())
                            {
                                EditorGUILayout.LabelField(i.ToString(), GUILayout.MaxWidth(20f));
                                EditorGUILayout.LabelField("|", GUILayout.MaxWidth(10f));
                                using (new EditorGUILayout.VerticalScope())
                                {
                                    EditorGUILayout.LabelField("左下", GUILayout.MaxWidth(40f));
                                    EditorGUILayout.LabelField("右上", GUILayout.MaxWidth(40f));
                                }
                               
                                using (new EditorGUILayout.VerticalScope())
                                {
                                    BaseData.preset[Slider_value].Point[i].LowerLeftPos = 
                                            EditorGUILayout.Vector2IntField("", BaseData.preset[Slider_value].Point[i].LowerLeftPos, 
                                            GUILayout.MaxWidth(80f));

                                    BaseData.preset[Slider_value].Point[i].UpperRightPos = 
                                            EditorGUILayout.Vector2IntField("", BaseData.preset[Slider_value].Point[i].UpperRightPos,
                                            GUILayout.MaxWidth(80f));
                                }                 
                            }
                        }
                    }
                }
            }
        }

        if (Event.current.type == EventType.DragUpdated)
        {
            if (DragAndDrop.objectReferences != null &&
                DragAndDrop.objectReferences.Length > 0 &&
                DragAndDrop.objectReferences[0] is MapDataBase)
            {
                DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
                Event.current.Use();
            }
        }
        else if (Event.current.type == EventType.DragPerform)
        {
            Undo.RecordObject(this, "Change MapDataBase");
            this.BaseData = ((MapDataBase)DragAndDrop.objectReferences[0]).Clone();
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
