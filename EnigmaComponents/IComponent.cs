namespace EnigmaComponents
{
    public interface IComponent
    {
        int Encode(int input, bool isBeforeReflector = true);
        void Reset();
    }
} 