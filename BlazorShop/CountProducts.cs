namespace BlazorShop;

public class CountProducts
{
    private int count = 0;
    public event Action? OnChange;

    public int Count
    {
        get => count;
        set
        {
            count = value;
            NotifyStateChanged();
        }
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}