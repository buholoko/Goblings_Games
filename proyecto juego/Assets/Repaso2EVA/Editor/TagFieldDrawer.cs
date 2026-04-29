using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(TagFieldAttribute))]
public class TagFieldDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (property.propertyType == SerializedPropertyType.String)
        {
            // Dibuja el selector de Tags de Unity
            property.stringValue = EditorGUI.TagField(position, label, property.stringValue);
        }
        else
        {
            EditorGUI.LabelField(position, label, "Usa [TagField] solo en strings");
        }
    }

}
