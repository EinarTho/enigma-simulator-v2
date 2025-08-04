namespace EnigmaComponents
{
    public interface IRotor : IComponent
    {
        int Position { get; }
        int NotchPosition { get; }
        string Name { get; }
        void Rotate(int steps = 1);
        void SetPosition(int position);
        bool IsAtNotch();
    }
} 