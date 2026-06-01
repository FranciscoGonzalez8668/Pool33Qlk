using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(ShotPowerAttribute))]
public class ShotPowerDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUIUtility.singleLineHeight * 2 + 4;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        ShotPowerAttribute attr = (ShotPowerAttribute)attribute;
        float value = property.floatValue;

        Rect sliderRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
        EditorGUI.Slider(sliderRect, property, 0f, attr.max, label);

        Rect barRect = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight + 2, position.width, EditorGUIUtility.singleLineHeight - 2);
        float t = value / attr.max;
        EditorGUI.ProgressBar(barRect, t, $"Potencia: {value:F1} / {attr.max}");
    }
}
