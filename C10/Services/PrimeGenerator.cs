using CleanCodeProject.C10.Interfaces;

namespace CleanCodeProject.C10.Services;

public class PrimeGenerator : IPrimeGenerator
{
    private const int MAX_ORDINAL = 30;
    private readonly PrimeCalculator _calculator;

    public PrimeGenerator()
    {
        _calculator = new PrimeCalculator();
    }

    public int[] Generate(int maxPrimes)
    {
        var primes = new int[maxPrimes + 1];
        primes[1] = 2; // 初始化第一個質數

        for (int i = 2; i <= maxPrimes; i++)
        {
            primes[i] = _calculator.FindNextPrime(primes[i - 1]);
        }

        return primes;
    }
}