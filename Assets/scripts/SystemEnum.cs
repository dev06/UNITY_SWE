using UnityEngine;
using System.Collections;

public class SystemEnum

{


	public enum SearchType
	{
		Keyword,
		Professor,
		Course
	}


	public enum BookType
	{
		None,
		New,
		Used,
		Rental,
		Ebook
	}


	public enum ButtonID
	{
		None,
		Search,
		Header_Login,
		CustomerLogin,
		Header_Logout,
		Header_Home,
		Book_Cover,
		UpArrow,
		DownArrow,
		AddToCart,
		NewButton,
		UsedButton,
		RentButton,
		EbookButton,
		ShoppingCart,
		CartUpArrow,
		CartDownArrow,
		RemoveBook,
		ContinueShopping,
		Checkout,
		BackArrow,
		CheckoutNext,
		LoginRentalPrompt_Login,
		LoginRentalPrompt_RemoveRental,
		LoginRentalPrompt_CloseDialog,
		ContactUs,
		AboutUs,
		CheckoutBack,
		CloseApp,
		EmptyShoppingCart,

	}

	public enum PageType
	{
		Home,
		Login,
		SearchResults,
		None,
		BookDescription,
		ShoppingCart,
		Checkout,
		ContactUs,
		AboutUs,

	}

	public enum PaymentType
	{
		None,
		CreditCard,
		FinancialAid,
		PayPal
	}


	public static string[] States =
	{
		"Alabama",
		"Alaska",
		"Arizona",
		"Arkansas",
		"California",
		"Colorado",
		"Connecticut",
		"Delaware",
		"Florida",
		"Georgia",
		"Hawaii",
		"Idaho",
		"Illinois",
		"Indiana",
		"Iowa",
		"Kansas",
		"Kentucky",
		"Louisiana",
		"Maine",
		"Maryland",
		"Massachusetts",
		"Michigan",
		"Minnesota",
		"Mississippi",
		"Missouri",
		"Montana",
		"Nebraska",
		"Nevada",
		"New Hampshire",
		"New Jersey",
		"New Mexico",
		"New York",
		"North Carolina",
		"North Dakota",
		"Ohio",
		"Oklahoma",
		"Oregon",
		"Pennsylvania",
		"Rhode Island",
		"South Carolina",
		"South Dakota",
		"Tennessee",
		"Texas",
		"Utah",
		"Vermont",
		"Virginia",
		"Washington",
		"West Virginia",
		"Wisconsin",
		"Wyoming"
	};

}
