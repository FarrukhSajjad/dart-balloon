using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;

public class Purchaser : MonoBehaviour, IDetailedStoreListener
{
    private IStoreController m_storeController;
    private IExtensionProvider m_storeExtensionProvider;

    //add items
    public static string RemoveAds = "removeads.dartballon";

    public static Purchaser Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
            return;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        InitIAP();
    }

    public void InitIAP()
    {
        if (IsInitialized())
        {
            return;
        }

        var iapBuilder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        iapBuilder.AddProduct(RemoveAds, ProductType.NonConsumable);

        UnityPurchasing.Initialize(this, iapBuilder);
    }

    private bool IsInitialized()
    {
        return m_storeController != null && m_storeExtensionProvider != null;
    }

    public void OnPurcahseItem(string itemId)
    {
        Debug.Log(itemId);
        if (IsInitialized() == false) return;
        m_storeController.InitiatePurchase(itemId);
    }


    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        this.m_storeController = controller;
        this.m_storeExtensionProvider = extensions;

        foreach (var item in controller.products.all)
        {
            Debug.Log(item.metadata.localizedTitle);
        }
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log("Initialization Failed");
    }

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        throw new System.NotImplementedException();
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureDescription failureDescription)
    {
        throw new System.NotImplementedException();
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.Log("Purchase Failed: " + product.definition);
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
    {
        //add response in here
        if (string.Equals(purchaseEvent.purchasedProduct.definition.id, RemoveAds, System.StringComparison.Ordinal))
        {
            //Remove Ads
            PlayerPrefs.SetInt("RemoveAds", 1);
        }

        return PurchaseProcessingResult.Complete;
    }

    public void OnRestoreButtonPressed()
    {
        if (IsInitialized())
        {
            m_storeExtensionProvider.GetExtension<IAppleExtensions>().RestoreTransactions(OnTransactionsRestoredEvent);
        }
    }

    private void OnTransactionsRestoredEvent(bool success, string msg)
    {
        if(success)
        {
            Debug.Log("Successfully Restored");

            foreach (var item in m_storeController.products.all)
            {
                if(item.definition.id == RemoveAds && item.definition.type == ProductType.NonConsumable && HasPurchaseReciept(item.receipt))
                {
                    //Remove Ads
                    PlayerPrefs.SetInt("RemoveAds", 1);

                }
            }
        }
        else
        {
            Debug.Log("Restore Failed");
        }
    }

    private bool HasPurchaseReciept(string receiptID)
    {
        return !string.IsNullOrEmpty(receiptID);
    }
}
