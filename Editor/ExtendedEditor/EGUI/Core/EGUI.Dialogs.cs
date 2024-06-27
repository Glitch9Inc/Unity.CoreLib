using System;
using UnityEditor;

namespace Glitch9.ExtendedEditor
{
    public static partial class EGUI
    {
        private const string OK = "Ok";
        private const string CANCEL = "Cancel";

        public static bool Notify(string message) => EditorUtility.DisplayDialog("Info", message, OK);
        public static bool Notify(string title, string message) => EditorUtility.DisplayDialog(title, message, OK);
        public static bool Confirmation(string message) => EditorUtility.DisplayDialog("Confirmation", message, OK, CANCEL);
        public static bool Confirmation(string title, string message) => EditorUtility.DisplayDialog(title, message, OK, CANCEL);
        public static bool Error(string message) => EditorUtility.DisplayDialog("Error", message, OK);
        public static bool Error(Exception exception) => EditorUtility.DisplayDialog("Error", exception.Message, OK);
    }
}
