namespace Chor;

public interface IMoneyHandler
{
    void SetNext(IMoneyHandler next);

    void Dispense(int amount);
}
