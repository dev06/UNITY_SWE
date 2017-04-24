using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class ShoppingCartTemplate : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

	public ShoppingCartQuantityContainer quantityContainer;
	public Book book;
	public Image cover;
	public Text title;
	public Text author;
	public Text isbn;
	public Text formatText;
	public Text unitPriceText;
	public Text quantityText;
	public Text totalText;

	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	public void SetBook(Book book)
	{
		this.book = book;
		cover.sprite = book.Cover;
		title.text = book.Title;
		author.text = book.Author;
		isbn.text = book.ISBN;
		formatText.text = book.bookType + "";
		unitPriceText.text = book.bookType ==  SystemEnum.BookType.New ? "$" + book.NewPrice + "" :
		                     book.bookType ==  SystemEnum.BookType.Used ?  "$" + book.UsedPrice + ""  :
		                     book.bookType ==  SystemEnum.BookType.Rental ? "$" +  book.RentPrice + "" :
		                     "$" + book.EbookPrice + "" ;
		quantityText.text = book.Quantity + "";
		book.TotalPrice = (float.Parse(unitPriceText.text.Substring(1, unitPriceText.text.Length - 1)) * book.Quantity);
		totalText.text = "$" + book.TotalPrice;


	}

	public virtual void OnPointerEnter(PointerEventData data)
	{
		SystemController.CurrentBookInCart = book;
	}
	public virtual void OnPointerExit(PointerEventData data)
	{
		SystemController.CurrentBookInCart = null;
	}
}
