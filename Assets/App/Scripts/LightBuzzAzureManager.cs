using LightBuzz.Azure;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class LightBuzzAzureManager : MonoBehaviour
{
    private LightBuzzMobileServiceClient azureClient;
    private AppServiceTableDAO<TodoItem> todoTableDAO;
    private List<TodoItem> todoItems;

    [Tooltip("User interface")]
    [SerializeField]
    private HoloLensClickableElement element;

    [SerializeField]
    [Tooltip("The Azure App Service URL")]
    private string mobileAppUri = "https://testtodolightbuzz.azurewebsites.net";

    [SerializeField]
    [Tooltip("Support local database")]
    private bool supportLocalDatabase = true;

    private async void Start()
    {
        await Init();
    }

    private async Task Init()
    {
        // Initialize Azure
        azureClient = new SampleMobileClient(mobileAppUri, supportLocalDatabase);
        await azureClient.InitializeLocalStore();

        // Retrieve the items from the server
        todoTableDAO = new AppServiceTableDAO<TodoItem>(azureClient);
        todoItems = await todoTableDAO.FindAll();

        // Populate the UI
        float xOffset = 0f;

        foreach (TodoItem item in todoItems)
        {
            HoloLensClickableElement obj = Instantiate(element, new Vector3(xOffset, 0f, 8f), new Quaternion());
            obj.Setup(item);

            xOffset += obj.transform.localScale.x + 0.1f;
        }
    }
}
