using LightBuzz.Azure;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class LightBuzzAzureManager : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The Azure App Service URL")]
    private string mobileAppUri = "https://testtodolightbuzz.azurewebsites.net";

    [SerializeField]
    [Tooltip("Support local database")]
    private bool supportLocalDatabase = true;

    [SerializeField]
    [Tooltip("The tile element to use to display the data")]
    private HoloLensClickableElement tile;

    [SerializeField]
    [Tooltip("The text log to display useful information to the user")]
    private TextMesh log;

    [SerializeField]
    [Tooltip("Distance between an item and the player")]
    [Range(1f, 15f)]
    private float listDistance = 4f;

    [SerializeField]
    [Tooltip("The number of columns of the tiled interface")]
    [Range(1, 10)]
    private int listColumns = 4;

    private LightBuzzMobileServiceClient azureClient;
    private AppServiceTableDAO<TodoItem> todoTableDAO;
    private List<TodoItem> todoItems;

    private async void Start()
    {
        await Init();
    }

    private async Task Init()
    {
        try
        {
            // Initialize Azure
            azureClient = new SampleMobileClient(mobileAppUri, supportLocalDatabase);
            await azureClient.InitializeLocalStore();

            // Retrieve the items from the server
            todoTableDAO = new AppServiceTableDAO<TodoItem>(azureClient);
            todoItems = await todoTableDAO.FindAll();

            // Populate the UI
            for (int i = 0; i < todoItems.Count; i++)
            {
                TodoItem item = todoItems[i];

                float x = (i % listColumns) * 1.2f;
                float y = (i / listColumns) * 1.2f;
                float z = listDistance;

                HoloLensClickableElement obj = Instantiate(tile, new Vector3(x, y, z), Quaternion.identity);
                obj.Setup(item);
                obj.OnClick += Item_Click;
            }

            log.gameObject.SetActive(false);
        }
        catch (Exception ex)
        {
            log.text = ex.ToString();
        }
    }

    private async void Item_Click(object sender, EventArgs e)
    {
        HoloLensClickableElement source = sender as HoloLensClickableElement;

        await todoTableDAO.Delete(source.Item);
    }
}
