namespace CleanCodeProject.C10.Services;

public class PrimeCalculator
{
    private const int MAX_ORDINAL = 30;
    private readonly int[] _multiples;
    private int _ordinal;
    private int _nextSquare;

    public PrimeCalculator()
    {
        _multiples = new int[MAX_ORDINAL + 1];
        _ordinal = 2;
        _nextSquare = 9;
    }

    public int FindNextPrime(int previousPrime)
    {
        int candidate = previousPrime + (previousPrime == 2 ? 1 : 2);

        while (!IsPrime(candidate))
        {
            candidate += 2;
        }

        return candidate;
    }

    private bool IsPrime(int number)
    {
        UpdateOrdinalIfNeeded(number);
        return !HasDivisors(number);
    }

    private void UpdateOrdinalIfNeeded(int number)
    {
        if (number == _nextSquare)
        {
            _ordinal++;
            _nextSquare = _ordinal * _ordinal;
            _multiples[_ordinal - 1] = number;
        }
    }

    private bool HasDivisors(int number)
    {
        for (int i = 2; i < _ordinal; i++)
        {
            UpdateMultiples(i, number);
            if (_multiples[i] == number)
            {
                return true;
            }
        }
        return false;
    }

    private void UpdateMultiples(int index, int number)
    {
        while (_multiples[index] < number)
        {
            _multiples[index] += index * 2;
        }
    }
}