namespace Chor;

internal class Program
{
    static void Main(string[] args)
    {
        IMoneyHandler h100 = new OneHundredThousandHandler();
        IMoneyHandler h50 = new FiftyThousandHandler();
        IMoneyHandler h20 = new TwentyThousandHandler();
        IMoneyHandler h10 = new TenThousandHandler();
        IMoneyHandler h5 = new FiveThousandHandler();

        h100.SetNext(h50);
        h50.SetNext(h20);
        h20.SetNext(h10);
        h10.SetNext(h5);

        h100.Dispense(385_000);
    }
}
