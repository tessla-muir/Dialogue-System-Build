using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace Game.Dialogue.Editor
{
    public class DialogueEditor : EditorWindow
    {
        Dialogue selectedDialogue = null;

        // Creates dialogue window
        [MenuItem("Window/Dialogue Editor")]
        private static void ShowEditorWindow()
        {
            var window = GetWindow<DialogueEditor>();
            window.titleContent = new GUIContent("Dialogue Editor");
            window.Show();
        }

        // Calls editor if a Dialogue SO is opened
        [OnOpenAsset(1)]
        public static bool OnOpenAsset(int instanceID, int line)
        {
            // Get object from instance ID
            Dialogue dialogue = EditorUtility.InstanceIDToObject(instanceID) as Dialogue;

            if (dialogue != null)
            {
                ShowEditorWindow();
                return true;
            }
            return false;
        }

        // Called when an object becomes enabled and active
        private void OnEnable() {
            Selection.selectionChanged += OnSelectionChanged;
        }

        private void OnSelectionChanged()
        {
            Dialogue newDialogue = Selection.activeObject as Dialogue;
            if (newDialogue != null)
            {
                selectedDialogue = newDialogue;
                Repaint(); // Calls OnGUI
            }
        }

        private void OnGUI() {
            if (selectedDialogue == null)
            {
                EditorGUILayout.LabelField("No Dialogue Selected.");
            }
            else
            {
                EditorGUILayout.LabelField(selectedDialogue.name);
            }
        }
    }
}

