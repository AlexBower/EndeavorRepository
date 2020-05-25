using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public static GameObject buttonTaskPrefab;
    public static GameObject cardImagePrefab;

    private GameObject cardImage;
    private string title;
    private List<ButtonTask.ButtonOption> buttonOptions;
    private List<GameObject> buttonTasks;
    private bool isSelected;
    private string description;
    private string flavorText;

    // Start is called before the first frame update
    void Start()
    {
        if (buttonTaskPrefab == null)
            buttonTaskPrefab = Resources.Load("prefabs/cards/buttontask") as GameObject;
        if (cardImagePrefab == null)
            cardImagePrefab = Resources.Load("prefabs/cards/cardimage") as GameObject;


        title = "no name";
        buttonOptions = new List<ButtonTask.ButtonOption>();
        buttonOptions.Add(ButtonTask.ButtonOption.A);
        buttonOptions.Add(ButtonTask.ButtonOption.DOWN);
        buttonOptions.Add(ButtonTask.ButtonOption.LEFT);
        buttonOptions.Add(ButtonTask.ButtonOption.RANDOM);

        CreateCard();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateCard()
    {
        SetupButtonTasks();

        SetupCardImage();

        transform.localScale = new Vector3(3, 3, 1);
    }

    private void SetupButtonTasks()
    {
        buttonTasks = new List<GameObject>();

        for (int i = 0; i < buttonOptions.Count; i++)
        {
            GameObject currentButtonTask = Instantiate(buttonTaskPrefab,
                new Vector3(transform.position.x, transform.position.y, transform.position.z),
                Quaternion.identity);
            currentButtonTask.transform.parent = transform;

            currentButtonTask.GetComponent<ButtonTask>().SetButtonOption(buttonOptions[i]);
            
            buttonTasks.Add(currentButtonTask);
        }

        MiddleAlignXValues(buttonTasks.ToArray(), 0.14f, 0.07f);
    }

    private void MiddleAlignXValues(GameObject[] gameObjects, float xSpacing, float yOffset)
    {
        for (int i = 0; i < gameObjects.Length; i++)
        {
            float newX = ((i * xSpacing) - (gameObjects.Length * xSpacing / 2)) + gameObjects[i].transform.position.x;
            newX += (xSpacing / 2);
            gameObjects[i].transform.position = new Vector3(newX, gameObjects[i].transform.position.y - yOffset);
        }
    }

    private void SetupCardImage()
    {
        cardImage = Instantiate(cardImagePrefab,
                new Vector3(transform.position.x, transform.position.y, transform.position.z),
                Quaternion.identity);
        cardImage.transform.parent = transform;

        string fileName = title;
        fileName.Replace(" ", string.Empty);
        Sprite cardImageSprite = Resources.Load<Sprite>("sprites/cards/cardimages/" + fileName);
        if (cardImageSprite == null)
        {
            cardImageSprite = Resources.Load<Sprite>("sprites/cards/cardimages/default");
        }
        cardImage.GetComponent<SpriteRenderer>().sprite = cardImageSprite;

        cardImage.transform.position = new Vector3(cardImage.transform.position.x, cardImage.transform.position.y + 0.20f);
    }
}
