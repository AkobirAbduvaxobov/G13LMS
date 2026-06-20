namespace Chor;

public class TwentyThousandHandler : IMoneyHandler
{
    private IMoneyHandler? _next;
    public void SetNext(IMoneyHandler next)
    {
        _next = next;
    }
    public void Dispense(int amount)
    {
        var count = amount / 20_000;
        if (count > 0)
        {
            Console.WriteLine($"20 000 so'mlik: {count} ta");
        }
        var remainder = amount % 20_000;
        if (remainder > 0)
        {
            _next?.Dispense(remainder);
        }
    }
}
