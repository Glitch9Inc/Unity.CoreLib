namespace Glitch9
{
    public interface ISelectable
    {
        bool IsSelected { get; }
        void Select();
        void Deselect();
    }
}