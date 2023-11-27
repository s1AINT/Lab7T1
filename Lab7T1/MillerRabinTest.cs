using System;
using System.Numerics;

class MillerRabinTest
{
    static Random random = new Random();

    static bool MillerRabinTestPrime(BigInteger n, int k)
    {
        if (n <= 1 || n == 4)
            return false;
        if (n <= 3)
            return true;

        BigInteger d = n - 1;
        int s = 0;

        while (d % 2 == 0)
        {
            d /= 2;
            s++;
        }

        int successCount = 0;
        for (int i = 0; i < k; i++)
        {
            BigInteger a = GenerateRandomBigInteger(2, n - 2);
            BigInteger x = BigInteger.ModPow(a, d, n);

            if (x == 1 || x == n - 1)
            {
                successCount++;
                continue;
            }

            for (int r = 1; r < s; r++)
            {
                x = BigInteger.ModPow(x, 2, n);

                if (x == 1)
                    return false;
                if (x == n - 1)
                {
                    successCount++;
                    break;
                }
            }
        }

        double probability = (double)successCount / k;
        return probability > 0.5; // Adjust as needed
    }

    static BigInteger GenerateRandomBigInteger(BigInteger minValue, BigInteger maxValue)
    {
        int length = (int)(maxValue - minValue).ToByteArray().LongLength;

        byte[] randomBytes = new byte[length];
        random.NextBytes(randomBytes);

        BigInteger value = new BigInteger(randomBytes);

        value = BigInteger.Remainder(value, maxValue - minValue);
        value += minValue;

        return value;
    }

    static void Main()
    {
        Console.Write("Enter a number to test for primality (n > 3): ");
        BigInteger n = BigInteger.Parse(Console.ReadLine());

        if (n <= 3 || n % 2 == 0)
        {
            Console.WriteLine("Input should be an odd number greater than 3.");
            return;
        }

        Console.Write("Enter the number of rounds (k): ");
        int k = int.Parse(Console.ReadLine());

        bool isPrime = MillerRabinTestPrime(n, k);

        if (isPrime)
        {
            double probability = Math.Pow(0.5, k);
            Console.WriteLine($"{n} is  a prime number with % of {1 - probability:P2}.");
        }
        else
        {
            Console.WriteLine($"{n} is composite.");
        }
    }
}
