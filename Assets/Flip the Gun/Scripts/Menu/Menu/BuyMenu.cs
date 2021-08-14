using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#if IAP
using UnityEngine.Purchasing;

// Deriving the Purchaser class from IStoreListener enables it to receive messages from Unity Purchasing.
public class BuyMenu : MonoBehaviour, IStoreListener
#else
public class BuyMenu : MonoBehaviour
#endif
{
	[Tooltip("No Internet gameobject.")]
	public GameObject noInternet;
	[Tooltip("Product list gameobject.")]
	public Transform list;
	[Tooltip("All purchases restored text.")]
	public Animation restoredPurchases;


#if IAP

	//Products.
	public static string removeAds =    "removeads";   
	public static string pinchOfCoins = "pileofcoins";
	public static string rocketPack =  "rocketpack"; 
	public static string sniperPack =  "sniperpack"; 

	private static IStoreController m_StoreController;          // The Unity Purchasing system.
	private static IExtensionProvider m_StoreExtensionProvider; // The store-specific Purchasing subsystems.
	
	void Start()
	{
		list = transform.GetChild(2).GetChild(0);
		if(PlayerPrefs.GetInt("DisableAds") == 1)
		{
			list.GetChild(0).GetChild(1).gameObject.SetActive(false);
			list.GetChild(0).GetChild(2).gameObject.SetActive(true);			
		}
		else 
			list.GetChild(0).GetChild(1).GetChild(0).GetComponent<Text>().text = "$" + Data.properties.iap.removeAdsPrice.ToString();
		
		list.GetChild(1).GetChild(1).GetChild(0).GetComponent<Text>().text = "$" + Data.properties.iap.pileOfCoinsPrice.ToString();
		
		if(PlayerPrefs.GetInt("SniperPackBought") == 1)
		{
			list.GetChild(2).GetChild(1).gameObject.SetActive(false);
			list.GetChild(2).GetChild(2).gameObject.SetActive(true);			
		}
		else 
			list.GetChild(2).GetChild(1).GetChild(0).GetComponent<Text>().text = "$" + Data.properties.iap.sniperPackPrice.ToString();
		
		if(PlayerPrefs.GetInt("RocketPackBought") == 1)
		{
			list.GetChild(3).GetChild(1).gameObject.SetActive(false);
			list.GetChild(3).GetChild(2).gameObject.SetActive(true);			
		}
		else 		
			list.GetChild(3).GetChild(1).GetChild(0).GetComponent<Text>().text = "$" + Data.properties.iap.rocketPackPrice.ToString();
	}

	void OnEnable()
	{	

		//If device has Internet connection.
		if(Coroutines.isConnected)
		{		
			// If we haven't set up the Unity Purchasing reference
			if (m_StoreController == null)
			{
				// Begin to configure our connection to Purchasing
				InitializePurchasing();
			}
		}
		//If device has no Internet connection.
		else
		{
			//Enable No Internet gameobject and disable buy menu.
			noInternet.SetActive(true);
			gameObject.SetActive(false);
		}
	}
	
	public void InitializePurchasing() 
	{
		// If we have already connected to Purchasing ...
		if (IsInitialized())
		{
			// ... we are done here.
			return;
		}
		
		// Create a builder, first passing in a suite of Unity provided stores.
		var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

		builder.AddProduct(removeAds, ProductType.NonConsumable);
		builder.AddProduct(pinchOfCoins, ProductType.Consumable);
		builder.AddProduct(rocketPack, ProductType.NonConsumable);
		builder.AddProduct(sniperPack, ProductType.NonConsumable);

		
		// Kick off the remainder of the set-up with an asynchrounous call, passing the configuration 
		// and this class' instance. Expect a response either in OnInitialized or OnInitializeFailed.
		UnityPurchasing.Initialize(this, builder);
	}
	
	
	private bool IsInitialized()
	{
		// Only say we are initialized if both the Purchasing references are set.
		return m_StoreController != null && m_StoreExtensionProvider != null;
	}
	
#endif
	//If player pressed to buy remove ads.
	public void BuyRemoveAds()
	{
#if IAP	
		BuyProductID(removeAds);
#endif
	}
	//If player pressed to buy  coins.
	public void BuyPinchOfCoins()
	{
#if IAP	
		BuyProductID(pinchOfCoins);
#endif		
	}
	//If player pressed to buy  rocket pack.
	public void BuyRocketPack()
	{
#if IAP	
		BuyProductID(rocketPack);
#endif		
	}
	//If player pressed to buy sniper pack.
	public void BuySniperPack()
	{
#if IAP	
		BuyProductID(sniperPack);
#endif		
	}
	
	public void RestoreAllPurchases()
	{
#if IAP	
		RestorePurchases();
		restoredPurchases.Play();
#endif		
	}	

	
#if IAP	
	void BuyProductID(string productId)
	{
		// If Purchasing has been initialized ...
		if (IsInitialized())
		{
			// ... look up the Product reference with the general product identifier and the Purchasing 
			// system's products collection.
			Product product = m_StoreController.products.WithID(productId);
			
			// If the look up found a product for this device's store and that product is ready to be sold ... 
			if (product != null && product.availableToPurchase)
			{
				Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
				// ... buy the product. Expect a response either through ProcessPurchase or OnPurchaseFailed 
				// asynchronously.
				m_StoreController.InitiatePurchase(product);
			}
			// Otherwise ...
			else
			{
				// ... report the product look-up failure situation  
				Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
			}
		}
		// Otherwise ...
		else
		{
			// ... report the fact Purchasing has not succeeded initializing yet. Consider waiting longer or 
			// retrying initiailization.
			Debug.Log("BuyProductID FAIL. Not initialized.");
		}
	}

	public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
	{
		// Purchasing has succeeded initializing. Collect our Purchasing references.
		Debug.Log("OnInitialized: PASS");
		
		// Overall Purchasing system, configured with products for this application.
		m_StoreController = controller;
		// Store specific subsystem, for accessing device-specific store features.
		m_StoreExtensionProvider = extensions;
	}
	
	
	public void OnInitializeFailed(InitializationFailureReason error)
	{
		// Purchasing set-up has not succeeded. Check error for reason. Consider sharing this reason with the user.
		Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
	}
	
	
	public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args) 
	{
		// A non consumable product has been purchased by this user.
		if (String.Equals(args.purchasedProduct.definition.id, removeAds, StringComparison.Ordinal))
		{
			//User removed ads.
			Debug.Log("User removed ads");
			list.GetChild(0).GetChild(1).gameObject.SetActive(false);
			list.GetChild(0).GetChild(2).gameObject.SetActive(true);			
			AdBanner.DestroyBanner();
			PlayerPrefs.SetInt("DisableAds", 1);
		}
		// A consumable product has been purchased by this user.
		else if (String.Equals(args.purchasedProduct.definition.id, pinchOfCoins, StringComparison.Ordinal))
		{
			//User got 10000 coins.
			Debug.Log("User got 10000 coins");
			Wallet.AddCoins(10000);
			Wallet.CoinBlast();
		}
		// A non consumable product has been purchased by this user.
		else if (String.Equals(args.purchasedProduct.definition.id, rocketPack, StringComparison.Ordinal))
		{
			//User bought rocket guns.
			Debug.Log("User got rocket guns pack");
			list.GetChild(3).GetChild(1).gameObject.SetActive(false);
			list.GetChild(3).GetChild(2).gameObject.SetActive(true);	
			PlayerPrefs.SetInt("RocketPackBought", 1);		
			PlayerPrefs.SetInt("GunBought9", 1);
			PlayerPrefs.SetInt("GunBought10", 1);
		}
		// A non consumable product has been purchased by this user.
		else if (String.Equals(args.purchasedProduct.definition.id, sniperPack, StringComparison.Ordinal))
		{
			//User bought sniper pack.
			Debug.Log("User got sniper guns pack");
			list.GetChild(2).GetChild(1).gameObject.SetActive(false);
			list.GetChild(2).GetChild(2).gameObject.SetActive(true);
			PlayerPrefs.SetInt("SniperPackBought", 1);		
			PlayerPrefs.SetInt("GunBought4", 1);
			PlayerPrefs.SetInt("GunBought8", 1);			
			
		}
		// Or ... an unknown product has been purchased by this user. Fill in additional products here....
		else 
		{
			Debug.Log(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));
		}

		// Return a flag indicating whether this product has completely been received, or if the application needs 
		// to be reminded of this purchase at next app launch. Use PurchaseProcessingResult.Pending when still 
		// saving purchased products to the cloud, and when that save is delayed. 
		return PurchaseProcessingResult.Complete;
	}

	public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
	{
		// A product purchase attempt did not succeed. Check failureReason for more detail. Consider sharing 
		// this reason with the user to guide their troubleshooting actions.
		Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
	}

	// Restore purchases previously made by this customer. Some platforms automatically restore purchases, like Google. 
	// Apple currently requires explicit purchase restoration for IAP, conditionally displaying a password prompt.
	void RestorePurchases()
	{
		// If Purchasing has not yet been set up ...
		if (!IsInitialized())
		{
			// ... report the situation and stop restoring. Consider either waiting longer, or retrying initialization.
			Debug.Log("RestorePurchases FAIL. Not initialized.");
			return;
		}
		
		// If we are running on an Apple device ... 
		if (Application.platform == RuntimePlatform.IPhonePlayer || 
			Application.platform == RuntimePlatform.OSXPlayer)
		{
			// ... begin restoring purchases
			Debug.Log("RestorePurchases started ...");
			
			// Fetch the Apple store-specific subsystem.
			var apple = m_StoreExtensionProvider.GetExtension<IAppleExtensions>();
			// Begin the asynchronous process of restoring purchases. Expect a confirmation response in 
			// the Action<bool> below, and ProcessPurchase if there are previously purchased products to restore.
			apple.RestoreTransactions((result) => {
				// The first phase of restoration. If no more responses are received on ProcessPurchase then 
				// no purchases are available to be restored.
				Debug.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");
			});
		}
		// Otherwise ...
		else
		{
			// We are not running on an Apple device. No work is necessary to restore purchases.
			Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
		}
	}

#endif	
}
