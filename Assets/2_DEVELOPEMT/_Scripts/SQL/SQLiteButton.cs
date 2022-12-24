using UnityEngine;
using UnityEditor;

namespace akb.Core.Database
{
    [CustomEditor(typeof(SQLTester))]
    public class SQLiteButton : Editor
    {
        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("Create DB"))
            {
                SQLiteHandler handler = new SQLiteHandler();
            }
        }
    }
}