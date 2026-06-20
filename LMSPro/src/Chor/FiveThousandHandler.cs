namespace Chor;

public class FiveThousandHandler : IMoneyHandler
{
    private IMoneyHandler? _next;
    public void SetNext(IMoneyHandler next)
    {
        _next = next;
    }
    public void Dispense(int amount) // 5.000
    {
        var count = amount / 5_000;
        if (count > 0)
        {
            Console.WriteLine($"5 000 so'mlik: {count} ta");
        }
        var remainder = amount % 5_000;
        if (remainder > 0)
        {
            _next?.Dispense(remainder);
        }
    }
}

