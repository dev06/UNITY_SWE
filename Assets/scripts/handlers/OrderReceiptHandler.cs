using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class OrderReceiptHandler : MonoBehaviour {

	public GameObject orderReceiptTemplate;
	public Transform templateParent;

	public CheckoutController checkoutController;


	public Text subtotalText;
	public Text shippingText;
	public Text SalesTaxText;
	public Text orderTotalText;

	public Text billingInfoText;
	public Text dateText;
	public Text invoiceNumberText;
	public Text iDText;
	public Text paymentType;
	public Text time;


	void OnEnable()
	{
		EventManager.OnButtonClick += OnButtonClick;
	}

	void OnDisable()
	{
		EventManager.OnButtonClick -= OnButtonClick;

	}
	void Start () {
	}

	void Update () {
	}


	void OnButtonClick(SystemEnum.ButtonID buttonID)
	{

	}


	public void PopulateOrderReceipts()
	{
		foreach (Transform t in templateParent) {
			Destroy(t.gameObject);
		}

		float offset = 1.2f;
		float startOffset = 1.0f;
		string booksPurchased = "";

		for (int i = 0 ; i < ShoppingCartController.BooksInCart.Count; i++)
		{
			GameObject clone = (GameObject)Instantiate(orderReceiptTemplate, Vector3.zero, Quaternion.identity) as GameObject;
			float height = clone.GetComponent<RectTransform>().rect.height;

			clone.transform.SetParent(templateParent);
			RectTransform rt = clone.GetComponent<RectTransform>();
			rt.offsetMin = new Vector2(0, 0);
			rt.offsetMax = new Vector2(0, height);
			rt.localScale = new Vector3(1, 1, 1);
			rt.anchoredPosition = new Vector3(0 , (-rt.rect.height * i * offset), 0);
			templateParent.GetComponent<RectTransform>().sizeDelta = new Vector2(templateParent.GetComponent<RectTransform>().sizeDelta.x,  rt.rect.height * (i + 1) * offset);
			if (ShoppingCartController.BooksInCart[i].bookType == SystemEnum.BookType.Ebook)
			{
				ShoppingCartController.BooksInCart[i].additionalInfo = "Download Link(s): https://www.kennesawonlinebookstore.com/" + GenerateGUID();
			} else if (ShoppingCartController.BooksInCart[i].bookType == SystemEnum.BookType.Rental)
			{
				ShoppingCartController.BooksInCart[i].additionalInfo = "Due Date: " + GenerateDueDate();

			}
			clone.GetComponent<OrderReceiptTemplate>().SetBook(ShoppingCartController.BooksInCart[i]);
			checkoutController.cartController.UpdateInventory(ShoppingCartController.BooksInCart[i]);
			booksPurchased += "x" + ShoppingCartController.BooksInCart[i].Quantity + " " + ShoppingCartController.BooksInCart[i].Title + " " + ShoppingCartController.BooksInCart[i].additionalInfo + " ";
		}


		subtotalText.text = "$" + ShoppingCartController.Subtotal;
		shippingText.text = "$" + ShoppingCartController.Shipping;
		SalesTaxText.text = "$" + ShoppingCartController.SalesTax;
		orderTotalText.text = "$" + ShoppingCartController.OrderTotal;
		billingInfoText.text = checkoutController.billingAddressInfo.name + "\n" +
		                       checkoutController.billingAddressInfo.addressOne + "\n" +
		                       checkoutController.billingAddressInfo.city + ", " +
		                       checkoutController.billingAddressInfo.state + ", " +
		                       checkoutController.billingAddressInfo.zipcode + "," + checkoutController.billingAddressInfo.country ;

		dateText.text = "Date " + System.DateTime.Now.Month + "/" +  System.DateTime.Now.Day + "/" + System.DateTime.Now.Year;
		long invoice = Random.Range(00000000, 99999999);
		invoiceNumberText.text = "Invoice # " + invoice;

		time.text = "Date / Time : " + System.DateTime.Now;


		paymentType.text = "Payment Type " + CheckoutPaymentHandler.selectedPaymentType + "";
		if (SystemController.LoggedStudent != null)
		{
			iDText.text = "Customer ID " + SystemController.LoggedStudent.userName + "_" + invoice;
		} else
		{
			iDText.text = "Customer ID " + checkoutController.billingAddressInfo.name + "_" + invoice;
		}


		if (CheckoutPaymentHandler.selectedPaymentType == SystemEnum.PaymentType.FinancialAid)
		{
			if (SystemController.LoggedStudent != null)
			{
				checkoutController.cartController.UpdateFinancialAid(ShoppingCartController.OrderTotal);
			}
		}


		FileIO.CurrentInvoice = "";
		FileIO.CurrentInvoice = "CustomerID " + checkoutController.billingAddressInfo.name + "_" + invoice + " " + "Time " + System.DateTime.Now + " PaymentType " +
		                        CheckoutPaymentHandler.selectedPaymentType + " Invoice Number " + invoice + " Order Total " + ShoppingCartController.OrderTotal + " Books " + booksPurchased;
		Email.Send(checkoutController.shippingEmail.text, FileIO.CurrentInvoice);
		FileIO.CreateInvoice();

	}


	private string GenerateGUID()
	{
		return System.Guid.NewGuid().ToString();
	}


	private string GenerateDueDate()
	{
		int day = int.Parse(System.DateTime.Now.Day.ToString());
		int month = int.Parse(System.DateTime.Now.Month.ToString());
		int year = int.Parse(System.DateTime.Now.Year.ToString());
		if (month + 4 > 12)
		{
			return ((month + 4) - 12) + "/" + day + "/" + (year + 1);
		} else
		{
			return (month + 4) + "/" + day + "/" + year;
		}
	}

}
