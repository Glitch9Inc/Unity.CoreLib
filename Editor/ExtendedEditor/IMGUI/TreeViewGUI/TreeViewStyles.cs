using UnityEngine;

namespace Glitch9.ExtendedEditor.IMGUI
{
    public class TreeViewStyles
    {
        private static GUIStyle _childWindowTitle;
        public static GUIStyle ChildWindowTitle
        {
            get
            {
                if (_childWindowTitle == null)
                {
                    _childWindowTitle = new GUIStyle(GUI.skin.label)
                    {
                        fontStyle = FontStyle.Bold,
                        fontSize = 16,
                    };
                }
                return _childWindowTitle;
            }
        }

        private static GUIStyle _childWindowSubtitleLeft;
        public static GUIStyle ChildWindowSubtitleLeft
        {
            get
            {
                if (_childWindowSubtitleLeft == null)
                {
                    _childWindowSubtitleLeft = new GUIStyle(GUI.skin.label)
                    {
                        //fontStyle = FontStyle.Italic,
                        fontSize = 11,
                    };
                }
                return _childWindowSubtitleLeft;
            }
        }

        private static GUIStyle _childWindowSubtitleRight;
        public static GUIStyle ChildWindowSubtitleRight
        {
            get
            {
                if (_childWindowSubtitleRight == null)
                {
                    _childWindowSubtitleRight = new GUIStyle(GUI.skin.label)
                    {
                        fontSize = 11,
                        alignment = TextAnchor.MiddleRight
                    };
                }
                return _childWindowSubtitleRight;
            }
        }

        private static GUIStyle _editWindowBody;
        public static GUIStyle EditWindowBody
        {
            get
            {
                if (_editWindowBody == null)
                {
                    _editWindowBody = new GUIStyle()
                    {
                        padding = new RectOffset(5, 5, 10, 5) 
                    };
                }
                return _editWindowBody;
            }
        }

        private static GUIStyle _wordWrapTextField;
        public static GUIStyle WordWrapTextField
        {
            get
            {
                if (_wordWrapTextField == null)
                {
                    _wordWrapTextField = new GUIStyle(GUI.skin.textField)
                    {
                        wordWrap = true
                    };
                }
                return _wordWrapTextField;
            }
        }
    }
}