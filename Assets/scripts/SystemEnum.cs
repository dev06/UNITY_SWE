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

	}

	public enum PageType
	{
		Home,
		Login,
		SearchResults,
		None,
		BookDescription,

	}

}
