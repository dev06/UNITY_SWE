using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class OrderSummaryHandler : MonoBehaviour {

	public GameObject orderSummaryTemplate;

	public Transform templateParent;

	public ShoppingCartController cartController;

	public CheckoutController checkoutController;


	public Text shippingInfo, billingInfo, payment, subTotal, shipping, salesTax, total;


	void OnEnable()
	{
		EventManager.OnButtonClick += OnButtonClick;
	}

	void OnDisable()
	{
		EventManager.OnButtonClick += OnButtonClick;
	}


	void OnButtonClick(SystemEnum.ButtonID buttonID)
	{

	}

	void Update()
	{
		UpdateInfoContainer();
	}




	public void PopulateOrderSummary()
	{

		foreach (Transform t in templateParent) {
			Destroy(t.gameObject);
		}

		float offset = 1.2f;
		float startOffset = 1.0f;
		for (int i = 0 ; i < ShoppingCartController.BooksInCart.Count; i++)
		{
			GameObject clone = (GameObject)Instantiate(orderSummaryTemplate, Vector3.zero, Quaternion.identity) as GameObject;
			float height = clone.GetComponent<RectTransform>().rect.height;

			clone.transform.SetParent(templateParent);
			RectTransform rt = clone.GetComponent<RectTransform>();
			rt.offsetMin = new Vector2(0, 0);
			rt.offsetMax = new Vector2(0, height);
			rt.localScale = new Vector3(1, 1, 1);
			rt.anchoredPosition = new Vector3(0 , (-rt.rect.height * i * offset) - startOffset, 0);
			templateParent.GetComponent<RectTransform>().sizeDelta = new Vector2(templateParent.GetComponent<RectTransform>().sizeDelta.x,  rt.rect.height * (i + 1) * offset);
			clone.GetComponent<OrderSummaryTemplate>().SetBook(ShoppingCartController.BooksInCart[i]);
		}

	}


	public void UpdateInfoContainer()
	{
		subTotal.text = "Subtotal \t\t\t $" + ShoppingCartController.Subtotal  + "";
		salesTax.text =  "Sales Tax \t\t\t $" + ShoppingCartController.SalesTax + "";
		shipping.text =  "Shipping \t\t\t $" + ShoppingCartController.Shipping + "";
		total.text =  "Order Total \t\t $" + ShoppingCartController.OrderTotal + "";
		payment.text = "Payment \t\t\t " + CheckoutPaymentHandler.selectedPaymentType + "";


		shippingInfo.text = checkoutController.shippingAddressInfo.name + "\n" +
		                    checkoutController.shippingAddressInfo.addressOne + "\n" +
		                    checkoutController.shippingAddressInfo.city + ", " +
		                    checkoutController.shippingAddressInfo.state + ", " +
		                    checkoutController.shippingAddressInfo.zipcode ;


		billingInfo.text = checkoutController.billingAddressInfo.name + "\n" +
		                   checkoutController.billingAddressInfo.addressOne + "\n" +
		                   checkoutController.billingAddressInfo.city + ", " +
		                   checkoutController.billingAddressInfo.state + ", " +
		                   checkoutController.billingAddressInfo.zipcode ;
	}
}
