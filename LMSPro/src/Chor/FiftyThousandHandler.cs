namespace Chor;

public class FiftyThousandHandler : IMoneyHandler
{
    private IMoneyHandler? _next; // h20

    public void SetNext(IMoneyHandler next)
    {
        _next = next;
    }

    public void Dispense(int amount) // 85.000
    {
        var count = amount / 50_000; // 1

        if (count > 0)
        {
            Console.WriteLine($"50 000 so'mlik: {count} ta");
        }

        var remainder = amount % 50_000; // 35000

        if (remainder > 0)
        {
            _next?.Dispense(remainder);
        }
    }
}
