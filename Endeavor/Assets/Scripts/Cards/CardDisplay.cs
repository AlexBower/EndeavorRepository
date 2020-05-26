using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    public Card card;

    public Text nameText;
    public Text descriptionText;
    public Text flavorText;

    public Image artworkImage;

    public List<Image> buttonImages;

    public static Dictionary<Card.ButtonOption, Sprite> buttonOptionToSprite;
    public static Sprite completedSprite;

    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(1f, 1f, 1f);

        nameText.text = card.name;
        descriptionText.text = card.description;
        flavorText.text = card.flavorText;

        artworkImage.sprite = card.artwork;

        SetupButtonImages();

        transform.localScale = new Vector3(250f, 250f, 250f);
    }

    private void SetupButtonImages()
    {
        SetupStaticVariables();

        List<Card.ButtonOption> buttonOptions = card.buttonOptions;
        buttonImages = new List<Image>();

        for (int i = 0; i < buttonOptions.Count; i++)
        {
            GameObject currentButtonObject = new GameObject("ButtonTask" + i);
            Image currentButtonImage = currentButtonObject.AddComponent<Image>();
            currentButtonImage.sprite = buttonOptionToSprite[buttonOptions[i]];

            currentButtonObject.GetComponent<RectTransform>().SetParent(transform);

            currentButtonImage.rectTransform.anchoredPosition3D = new Vector3(0f, 0f, 0f);
            currentButtonImage.rectTransform.sizeDelta = new Vector2(0.12f, 0.12f);
            currentButtonImage.rectTransform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            currentButtonObject.SetActive(true);

            buttonImages.Add(currentButtonImage);
        }

        MiddleAlignXValuesOfImages(buttonImages.ToArray(), 0.14f, 0.14f);
    }

    private void SetupStaticVariables()
    {
        if (buttonOptionToSprite == null)
            buttonOptionToSprite = new Dictionary<Card.ButtonOption, Sprite>();
        if (!buttonOptionToSprite.ContainsKey(Card.ButtonOption.UP))
            buttonOptionToSprite.Add(Card.ButtonOption.UP, Resources.Load<Sprite>("sprites/cards/buttontasks/upsprite"));
        if (!buttonOptionToSprite.ContainsKey(Card.ButtonOption.DOWN))
            buttonOptionToSprite.Add(Card.ButtonOption.DOWN, Resources.Load<Sprite>("sprites/cards/buttontasks/downsprite"));
        if (!buttonOptionToSprite.ContainsKey(Card.ButtonOption.LEFT))
            buttonOptionToSprite.Add(Card.ButtonOption.LEFT, Resources.Load<Sprite>("sprites/cards/buttontasks/leftsprite"));
        if (!buttonOptionToSprite.ContainsKey(Card.ButtonOption.RIGHT))
            buttonOptionToSprite.Add(Card.ButtonOption.RIGHT, Resources.Load<Sprite>("sprites/cards/buttontasks/rightsprite"));
        if (!buttonOptionToSprite.ContainsKey(Card.ButtonOption.A))
            buttonOptionToSprite.Add(Card.ButtonOption.A, Resources.Load<Sprite>("sprites/cards/buttontasks/asprite"));
        if (!buttonOptionToSprite.ContainsKey(Card.ButtonOption.B))
            buttonOptionToSprite.Add(Card.ButtonOption.B, Resources.Load<Sprite>("sprites/cards/buttontasks/bsprite"));
        if (!buttonOptionToSprite.ContainsKey(Card.ButtonOption.X))
            buttonOptionToSprite.Add(Card.ButtonOption.X, Resources.Load<Sprite>("sprites/cards/buttontasks/xsprite"));
        if (!buttonOptionToSprite.ContainsKey(Card.ButtonOption.Y))
            buttonOptionToSprite.Add(Card.ButtonOption.Y, Resources.Load<Sprite>("sprites/cards/buttontasks/ysprite"));
        if (!buttonOptionToSprite.ContainsKey(Card.ButtonOption.RANDOM))
            buttonOptionToSprite.Add(Card.ButtonOption.RANDOM, Resources.Load<Sprite>("sprites/cards/buttontasks/randomsprite"));

        if (completedSprite == null)
            completedSprite = Resources.Load<Sprite>("sprites/cards/buttontasks/completedsprite");
    }

    private void MiddleAlignXValuesOfImages(Image[] images, float xSpacing, float yOffset)
    {
        for (int i = 0; i < images.Length; i++)
        {
            float newX = ((i * xSpacing) - (images.Length * xSpacing / 2)) + images[i].rectTransform.anchoredPosition.x;
            newX += (xSpacing / 2);
            images[i].rectTransform.anchoredPosition = new Vector3(newX, images[i].rectTransform.anchoredPosition.y - yOffset);
        }
    }
}
