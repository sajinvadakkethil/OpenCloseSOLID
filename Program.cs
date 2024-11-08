using System;
using System.Collections.Generic;

// Base Discount interface
public interface IDiscount
{
	decimal ApplyDiscount(decimal price);
}

// Specific discount types
public class NoDiscount : IDiscount
{
	public decimal ApplyDiscount(decimal price)
	{
		return price; // No discount applied
	}
}

public class PercentageDiscount : IDiscount
{
	private readonly decimal _percentage;

	public PercentageDiscount(decimal percentage)
	{
		_percentage = percentage;
	}

	public decimal ApplyDiscount(decimal price)
	{
		return price - (price * _percentage / 100);
	}
}

public class FixedDiscount : IDiscount
{
	private readonly decimal _amount;

	public FixedDiscount(decimal amount)
	{
		_amount = amount;
	}

	public decimal ApplyDiscount(decimal price)
	{
		return price - _amount;
	}
}

// Order class that depends on IDiscount to calculate final price
public class Order
{
	public decimal TotalAmount { get; set; }
	private readonly IDiscount _discount;

	public Order(decimal totalAmount, IDiscount discount)
	{
		TotalAmount = totalAmount;
		_discount = discount;
	}

	public decimal GetFinalAmount()
	{
		return _discount.ApplyDiscount(TotalAmount);
	}
}

// Example usage
class Program
{
	static void Main()
	{
		var orderWithNoDiscount = new Order(100, new NoDiscount());
		Console.WriteLine("Final price with no discount: " + orderWithNoDiscount.GetFinalAmount());

		var orderWithPercentageDiscount = new Order(100, new PercentageDiscount(10));
		Console.WriteLine("Final price with 10% discount: " + orderWithPercentageDiscount.GetFinalAmount());

		var orderWithFixedDiscount = new Order(100, new FixedDiscount(15));
		Console.WriteLine("Final price with fixed discount of $15: " + orderWithFixedDiscount.GetFinalAmount());
	}
}
