namespace Chor;

internal class Program
{
    static void Main(string[] args)
    {





        //IMoneyHandler h100 = new OneHundredThousandHandler();
        //IMoneyHandler h50 = new FiftyThousandHandler();
        //IMoneyHandler h20 = new TwentyThousandHandler();
        //IMoneyHandler h10 = new TenThousandHandler();
        //IMoneyHandler h5 = new FiveThousandHandler();

        //h100.SetNext(h50);
        //h50.SetNext(h20);
        //h20.SetNext(h10);
        //h10.SetNext(h5);

        //h100.Dispense(385_000);
    }
}



public class Solution
{
    public IList<IList<int>> Permute(int[] nums)
    {
        HashSet<IList<int>> result = new HashSet<IList<int>>();

        int n = 1;
        for(int i = 1; i <= nums.Length; i++)
        {
            n *= i; // 1 * 2 * 3 * 4
        }
        IList<int> ints = new List<int>(nums);
        result.Add(ints);
        int i1 = 0;
        for (int i = 1; i < n; i++)
        {
            if(i1 == nums.Length - 1)
            {
                i1 = 0;
            }

            int temp = nums[i1];
            nums[i1] = nums[i1 + 1];
            nums[i1 + 1] = temp;

            i1++;

            result.Add(new List<int>(nums));
        }

        return result.ToList();
    }
}

// 1 2 3
// 1 3 2
// 2 1 3
// 2 3 1
// 3 2 1
// 3 1 2


// 1 3 1
// 1 1 3
// 3 1 1

// 0 1 2 3
// 1 0 2 3
// 1 2 0 3
// 1 2 3 0
// 2 1 3 0
// 2 3 1 0
// 2 3 1 0
// 3 2 1 0
// 3 1 2 0
// 3 1 0 2
// 1 3 0 2
// 1 0 3 2
// 1 0 2 3
// 0 1 2 3
// 0 2 1 3
// 0 2 3 1
// 2 0 3 1
// 2 3 0 1
// 2 3 1 0
// 3 2 1 0
// 3 1 2 0
// 3 1 0 2
// 1 3 0 2
// 1 0 3 2
// 1 0 2 3
// 0 1 2 3

// 




