using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
public class FileIO : MonoBehaviour
{


	void Awake()
	{
		ParseBooks();
		//	ParseStudents();
	}

	void ParseBooks()
	{

		TextAsset asset = (TextAsset)Resources.Load("book_new");
		string text = asset.text;

		string[] data = text.Split('|');

		Debug.Log("Length =>" + data.Length);
		for (int i = 0; i < data.Length; i++)
		{
			Debug.Log(data[i]);
		}

		for (int i = 0; i < data.Length; i++)
		{
			Book book = new Book();
			try
			{
				string[] textSplit = data[i].Split('_');

				book.ISBN = textSplit[0];
				book.Title = textSplit[1];
				book.Author = textSplit[2];

				if (textSplit[3].Contains(","))
				{
					string[] semesterSplit = textSplit[3].Split(',');
					for (int w = 0; w < semesterSplit.Length; w++)
					{
						book.Semester.Add(semesterSplit[w]);
					}
				} else
				{
					book.Semester.Add(textSplit[3]);
				}


				if (textSplit[4].Contains(","))
				{
					string[] courseSplit = textSplit[4].Split(',');
					for (int w = 0; w < courseSplit.Length; w++)
					{
						book.Course.Add(courseSplit[w]);
					}
				} else
				{
					book.Course.Add(textSplit[4]);
				}


				if (textSplit[5].Contains(","))
				{
					string[] sectionSplit = textSplit[5].Split(',');
					for (int w = 0; w < sectionSplit.Length; w++)
					{
						book.SectionNumber.Add(sectionSplit[w]);
					}
				} else
				{
					book.SectionNumber.Add(textSplit[5]);
				}


				if (textSplit[6].Contains(","))
				{
					string[] profSplit = textSplit[6].Split(',');
					for (int w = 0; w < profSplit.Length; w++)
					{
						book.Professor.Add(profSplit[w]);
					}
				} else
				{
					book.Professor.Add(textSplit[6]);
				}


				if (textSplit[7].Contains(","))
				{
					string[] crnSplit = textSplit[7].Split(',');
					for (int w = 0; w < crnSplit.Length; w++)
					{
						book.CRN.Add(crnSplit[w]);
					}
				} else
				{
					book.CRN.Add(textSplit[7]);
				}




				// book.Professor = textSplit[6];

				book.Importance = textSplit[8].ToString();
				book.NewStock = int.Parse(textSplit[9]);
				book.UsedStock = int.Parse(textSplit[10]);
				book.RentStock = int.Parse(textSplit[11]);
				book.EbookStock = int.Parse(textSplit[12]);
				book.NewPrice = float.Parse(textSplit[13]);
				book.UsedPrice = float.Parse(textSplit[14]);
				book.RentPrice = float.Parse(textSplit[15]);
				book.EbookPrice = float.Parse(textSplit[16]);
				book.Description = textSplit[17].ToString();
				book.Cover = (Sprite)Resources.Load<Sprite>("bookImages/" + book.ISBN);

				SystemController.Library.Add(book);
			} catch (System.Exception e)
			{
				Debug.Log(book.Title + " " + i + " " + e);
			}

		}




	}

	private void ParseStudents()
	{
		TextAsset asset = (TextAsset)Resources.Load("students");
		string text = asset.text;
		string[] data = text.Split('|');
		for (int i = 0; i < data.Length; i++)
		{
			string[] studentSplit = data[i].Split('_');
			Student student = new Student();
			student.firstName = studentSplit[0];
			student.lastName = studentSplit[1];
			student.userName = studentSplit[2];
			student.password = studentSplit[3];
			student.aid = float.Parse(studentSplit[4]);

			SystemController.Students.Add(student);


		}

	}


	public string GetFileContents(string path)
	{
		string text = "";
		StreamReader reader = new StreamReader(path);

		while (!reader.EndOfStream)
		{
			text += reader.ReadLine() + "\n";
		}

		reader.Close( );

		return text;
	}

}


public class Book
{
	public Sprite Cover;
	public string Title;
	public string Author;
	public List<string> Semester = new List<string>();
	public List<string> Course = new List<string>();

	public List<string> SectionNumber = new List<string>();
	public List<string> Professor = new List<string>();
	public List<string> CRN = new List<string>();

	public string Importance;
	public string ISBN;
	public string Description;

	public string additionalInfo;


	public int NewStock;
	public int UsedStock;
	public int RentStock;
	public int EbookStock;
	public int Quantity;


	public float NewPrice;
	public float UsedPrice;
	public float RentPrice;
	public float EbookPrice;

	public float TotalPrice;
	public float ShippingPrice;
	public float SalesTax = 0.07f;

	public SystemEnum.BookType bookType;


	public Book()
	{

	}





}
[System.Serializable]
public class AddressInfo
{
	public string name;
	public string addressOne;
	public string addressTwo;
	public string city;
	public string state;
	public string zipcode;
}


public class Customer
{
	public string firstName;
	public string lastName;

	public string email;

	public string addressOne;
	public string addressTwo;
	public string city;
	public string state;
	public string zipcode;
}

public class Student : Customer
{
	public float aid;
	public string userName;
	public string password;
}