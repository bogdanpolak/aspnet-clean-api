namespace CleanApi.Core.Contracts
{
    public interface IRandomizer
    {
        int GenInRange(int low, int high);
        int GenInt(int maxValue);
    }
}