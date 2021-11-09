<Query Kind="Program" />

void Main()
{
	//524899
		FindFactors(15).Dump();
		ggT(545,15).Dump();
		
}

private long ggT(long a, long b)
{
		long c = 1;
		while (c != 0)
		{
			c = a % b;
			a = b;
			b = c;
		}
		return a;
}

private List<long> FindFactors(long num)
{
	List<long> result = new List<long>();

	// Take out the 2s.
	while (num % 2 == 0)
	{
		result.Add(2);
		num /= 2;
	}

	// Take out other primes.
	long factor = 3;
	while (factor * factor <= num)
	{
		if (num % factor == 0)
		{
			// This is a factor.
			result.Add(factor);
			num /= factor;
		}
		else
		{
			// Go to the next odd number.
			factor += 2;
		}
	}

	// If num is not 1, then whatever is left is prime.
	if (num > 1) result.Add(num);

	return result;
}
/*
11.31 
/ 20
*/

#region
/*
0,58  / 11   = 0,0527272727272727272727272727  -  DivisionKomma
228   / 0,6  = 380                             -  DivisionKomma
7345  / 0,6  = 12241,666666666666666666666667  -  DivisionKomma
59,1  / 0,11 = 537,27272727272727272727272727  -  DivisionKomma
7972  / 8    = 996,5                           -  DivisionKomma
104   / 6    = 17,333333333333333333333333333  -  DivisionKomma
2243  / 20   = 112,15                          -  DivisionKomma
11,31 / 20   = 0,5655                          -  DivisionKomma
66,41 / 0,1  = 664,1                           -  DivisionKomma
19,57 / 0,03 = 652,33333333333333333333333333  -  DivisionKomma
23,33 / 13   = 1,7946153846153846153846153846  -  DivisionKomma
696,1 / 1    = 696,1                           -  DivisionKomma
313   / 0,11 = 2845,4545454545454545454545455  -  DivisionKomma
982,8 / 1,3  = 756                             -  DivisionKomma
95,42 / 1,9  = 50,221052631578947368421052632  -  DivisionKomma
60,24 / 0,5  = 120,48                          -  DivisionKomma
452,8 / 2    = 226,4                           -  DivisionKomma
89,27 / 0,19 = 469,84210526315789473684210526  -  DivisionKomma
6,23  / 0,6  = 10,383333333333333333333333333  -  DivisionKomma
45,59 / 0,09 = 506,55555555555555555555555556  -  DivisionKomma
269   / 0,14 = 1921,4285714285714285714285714  -  DivisionKomma
6107  / 1,8  = 3392,7777777777777777777777778  -  DivisionKomma
7787  / 16   = 486,6875                        -  DivisionKomma
98,87 / 8    = 12,35875                        -  DivisionKomma
95,41 / 19   = 5,0215789473684210526315789474  -  DivisionKomma
695   / 0,6  = 1158,3333333333333333333333333  -  DivisionKomma
9634  / 0,7  = 13762,857142857142857142857143  -  DivisionKomma
312,7 / 1    = 312,7                           -  DivisionKomma
9535  / 0,19 = 50184,210526315789473684210526  -  DivisionKomma
87,29 / 20   = 4,3645                          -  DivisionKomma
54,62 / 0,18 = 303,44444444444444444444444444  -  DivisionKomma
98,93 / 19   = 5,2068421052631578947368421053  -  DivisionKomma
4,62  / 2    = 2,31                            -  DivisionKomma
23,5  / 3    = 7,8333333333333333333333333333  -  DivisionKomma
485,7 / 0,5  = 971,4                           -  DivisionKomma
27,32 / 16   = 1,7075                          -  DivisionKomma
6482  / 0,18 = 36011,111111111111111111111111  -  DivisionKomma
*/
#endregion
