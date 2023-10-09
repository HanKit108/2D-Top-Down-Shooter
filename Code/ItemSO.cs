using UnityEngine;
using UnityEditor;

public class ScriptableObjectIdAttribute : PropertyAttribute { }

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(ScriptableObjectIdAttribute))]
public class ScriptableObjectIdDrawer : PropertyDrawer {
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        GUI.enabled = false;
        
        Object owner = property.serializedObject.targetObject;
        // This is the unity managed GUID of the scriptable object, which is always unique :3
        string unityManagedGuid = AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(owner));
        
        if (property.stringValue != unityManagedGuid) {
            property.stringValue = unityManagedGuid;
        }
        EditorGUI.PropertyField(position, property, label, true);
        
        GUI.enabled = true;
    }
}
#endif

[CreateAssetMenu(fileName ="new Item", menuName = "Item", order = 51)]
public class ItemSO : ScriptableObject {

    [ScriptableObjectId]
    public string id;
    
    public string Name;
    public Sprite Sprite;
    public bool IsStackable;
}
