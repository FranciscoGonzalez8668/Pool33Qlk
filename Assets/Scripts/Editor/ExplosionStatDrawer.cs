using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(ExplosionStatAttribute))]
public class ExplosionStatDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        ExplosionStatAttribute attr = (ExplosionStatAttribute)attribute;

        GUI.color = property.floatValue >= attr.warningThreshold ? Color.red : Color.white;
        EditorGUI.Slider(position, property, attr.min, attr.max, label);
        GUI.color = Color.white;
    }
}
