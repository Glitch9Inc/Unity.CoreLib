using System;
using UnityEditor;

namespace Glitch9.ExEditor
{
    public static partial class ExGUI
    {
        private const string OK = "Ok";
        private const string CANCEL = "Cancel";

        public static bool Dialog(string message) => EditorUtility.DisplayDialog("Info", message, OK);
        public static bool Dialog(string title, string message) => EditorUtility.DisplayDialog(title, message, OK);
        public static bool Ask(string message) => EditorUtility.DisplayDialog("Confirmation", message, OK, CANCEL);
        public static bool Ask(string title, string message) => EditorUtility.DisplayDialog(title, message, OK, CANCEL);
        public static bool Error(string message) => EditorUtility.DisplayDialog("Error", message, OK);
        public static bool Error(Exception exception) => EditorUtility.DisplayDialog("Error", exception.Message, OK);
    }
}
