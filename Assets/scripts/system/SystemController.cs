using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class SystemController : MonoBehaviour {


	public static List<Book> Library = new List<Book>();

	public static List<Student> Students = new List<Student>();

	public static List<string>Course = new List<string>();
	public static List<string>Professor = new List<string>();

	public static bool IsLoggedIn;
	public static Student LoggedStudent;
	public static Book CurrentBook;


	void Start()
	{

		for (int i = 0; i < Library.Count; i++)
		{

			if (Course.Contains(Library[i].Course))
			{
				continue;
			}

			Course.Add(Library[i].Course);
		}


		for (int i = 0; i < Library.Count; i++)
		{

			if (Professor.Contains(Library[i].Professor))
			{
				continue;
			}

			Professor.Add(Library[i].Professor);
		}

	}


}
