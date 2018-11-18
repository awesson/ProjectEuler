using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectEuler.Polynomials
{
	public class Polynomial<TCoefficientType>
	{
		readonly private List<PolynomialTerm<TCoefficientType>> m_Terms;

		public Polynomial()
		{
			m_Terms = new List<PolynomialTerm<TCoefficientType>>();
		}

		public Polynomial(List<PolynomialTerm<TCoefficientType>> terms)
		{
			m_Terms = terms;
		}

		public Polynomial(IEnumerable<PolynomialTerm<TCoefficientType>> terms)
		{
			m_Terms = terms.ToList();
		}

		public void AddTerm(PolynomialTerm<TCoefficientType> term)
		{
			m_Terms.Add(term);
		}

		public void AddTerm(int power, TCoefficientType coefficient)
		{
			m_Terms.Add(new PolynomialTerm<TCoefficientType>(power, coefficient));
		}

		public TCoefficientType EvaluateForX(TCoefficientType x)
		{
			dynamic val = 0;
			foreach (var term in m_Terms)
			{
				dynamic coef = term.Coefficient;
				dynamic dynX = x;
				val += coef * BasicMath.Pow(dynX, term.Power);
			}
			return val;
		}
	}

	public struct PolynomialTerm<TCoefficientType>
	{
		public int Power { get; set; }
		public TCoefficientType Coefficient { get; set; }

		public PolynomialTerm(int power, TCoefficientType coefficient)
		{
			Power = power;
			Coefficient = coefficient;
		} 
	}
}