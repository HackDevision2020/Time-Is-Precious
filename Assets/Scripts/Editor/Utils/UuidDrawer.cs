using UnityEditor;
using UnityEngine;
using Utils;

namespace Editor.Utils
{
    [CustomPropertyDrawer(typeof(UuidAttribute))]
    public class UuidDrawer : PropertyDrawer
    {
        public override void OnGUI(
            Rect position,
            SerializedProperty property,
            GUIContent label)
        {
            string assetPath = AssetDatabase.GetAssetPath(property.serializedObject.targetObject.GetInstanceID());
            string uuid = AssetDatabase.AssetPathToGUID(assetPath);

            property.stringValue = uuid;

            Rect textField = position;
            textField.height = 16;
            EditorGUI.LabelField(position, label, new GUIContent(property.stringValue));
        }
    }
}
