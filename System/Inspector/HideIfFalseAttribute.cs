using UnityEngine;

namespace Glitch9.ExEditor
{
    public class HideIfFalseAttribute : PropertyAttribute
    {
        public string ConditionalSourceField = "";

        public HideIfFalseAttribute(string booleanFieldName)
        {
            this.ConditionalSourceField = booleanFieldName;
        }
    }
}