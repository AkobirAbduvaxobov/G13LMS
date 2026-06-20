namespace Chor;

public class OneHundredThousandHandler : IMoneyHandler
{
    private IMoneyHandler? _next; // h50

    public void SetNext(IMoneyHandler next)
    {
        _next = next;
    }

    public void Dispense(int amount)
    {
        var count = amount / 100_000; // 3

        if (count > 0)
        {
            Console.WriteLine($"100 000 so'mlik: {count} ta");
        }

        var remainder = amount % 100_000; // 85000

        if (remainder > 0)
        {
            _next?.Dispense(remainder);
        }
    }
}
