using UnityEngine;
using System.Collections;
using System.IO;
public class FileIO : MonoBehaviour
{


	void Awake()
	{
		ParseBooks();
		ParseStudents();
	}

	void ParseBooks()
	{

		TextAsset asset = (TextAsset)Resources.Load("books");
		string text = asset.text;

		string[] data = text.Split('|');



		for (int i = 0; i < 44; i++)
		{
			try
			{
				string[] textSplit = data[i].Split('_');
				Book book = new Book();
				book.ISBN = textSplit[0];
				book.Title = textSplit[1];
				book.Author = textSplit[2];
				book.Semester = textSplit[3];
				book.Course = textSplit[4];
				book.SectionNumber = textSplit[5];
				book.Professor = textSplit[6];

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
				Debug.Log(i);
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
	public string Semester;
	public string Course;
	public string SectionNumber;
	public string Professor;
	public string CRN;
	public string Importance;
	public string ISBN;
	public string Description;


	public int NewStock;
	public int UsedStock;
	public int RentStock;
	public int EbookStock;
	public int Quantity;


	public float NewPrice;
	public float UsedPrice;
	public float RentPrice;
	public float EbookPrice;

	public SystemEnum.BookType bookType;


	public Book()
	{

	}


}


public class Customer
{
	public string firstName;
	public string lastName;
}

public class Student : Customer
{
	public float aid;
	public string userName;
	public string password;
}