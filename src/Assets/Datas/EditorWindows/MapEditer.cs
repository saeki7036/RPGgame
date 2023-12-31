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
    private void OnEnable()
    {
       
    }
    int toolbarInt = 0;
    // Update is called once per frame
    private void OnGUI()
    {
        using (new EditorGUILayout.VerticalScope(GUILayout.MaxWidth(10f))) 
        {
            using (new EditorGUILayout.HorizontalScope())
            {
                EditorGUILayout.LabelField("ID", GUILayout.MaxWidth(300f));
                EditorGUILayout.LabelField("|", GUILayout.MaxWidth(10f));
                EditorGUILayout.LabelField("Item Name", GUILayout.MaxWidth(170f));
                EditorGUILayout.LabelField("|", GUILayout.MaxWidth(10f));
                EditorGUILayout.LabelField("ï“èWÉ{É^Éì", GUILayout.MaxWidth(60f));
                EditorGUILayout.IntPopup("Ç€Ç¡Ç’Ç†Ç¡Ç’", 10, new string[]
                { "è≠Ç»Çﬂ", "ïÅí ", "ëΩÇﬂ", }, new int[] { 1, 10, 100 });
                ;
                string[] toolbarStrings = { "Toolbar1", "Toolbar2", "Toolbar3" };

               
                    toolbarInt = GUILayout.Toolbar(toolbarInt, toolbarStrings);
                

            }

        }

        using (new EditorGUILayout.VerticalScope(GUILayout.MaxWidth(10f)))
        {
            for (int X = 0; X < 10; X++)
            {
                using (new EditorGUILayout.HorizontalScope())
                {
                    EditorGUILayout.LabelField(X.ToString(), GUILayout.MaxWidth(10f));

                    for (int Y = 0; Y < 10; Y++)
                    {
                        using (new EditorGUILayout.VerticalScope())
                        {
                            EditorGUILayout.LabelField(Y.ToString(), GUILayout.MaxWidth(10f));
                        }

                    }
                }
            }
        }

    }
}
