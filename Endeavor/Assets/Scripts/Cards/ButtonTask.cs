using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTask : MonoBehaviour
{
    public static Sprite completedSprite;
    public static Dictionary<ButtonOption, Sprite> buttonOptionToSprite;

    public enum ButtonOption
    {
        DOWN, LEFT, UP, RIGHT, A, B, X, Y, RANDOM
    }

    private ButtonOption buttonOption;
    private bool isCompleted;

    // Start is called before the first frame update
    void Start()
    {
        SetupStaticVariables();
        isCompleted = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetupStaticVariables()
    {
        if (buttonOptionToSprite == null)
            buttonOptionToSprite = new Dictionary<ButtonOption, Sprite>();
        if (!buttonOptionToSprite.ContainsKey(ButtonOption.UP))
            buttonOptionToSprite.Add(ButtonOption.UP, Resources.Load<Sprite>("sprites/cards/buttontasks/upsprite"));
        if (!buttonOptionToSprite.ContainsKey(ButtonOption.DOWN))
            buttonOptionToSprite.Add(ButtonOption.DOWN, Resources.Load<Sprite>("sprites/cards/buttontasks/downsprite"));
        if (!buttonOptionToSprite.ContainsKey(ButtonOption.LEFT))
            buttonOptionToSprite.Add(ButtonOption.LEFT, Resources.Load<Sprite>("sprites/cards/buttontasks/leftsprite"));
        if (!buttonOptionToSprite.ContainsKey(ButtonOption.RIGHT))
            buttonOptionToSprite.Add(ButtonOption.RIGHT, Resources.Load<Sprite>("sprites/cards/buttontasks/rightsprite"));
        if (!buttonOptionToSprite.ContainsKey(ButtonOption.A))
            buttonOptionToSprite.Add(ButtonOption.A, Resources.Load<Sprite>("sprites/cards/buttontasks/asprite"));
        if (!buttonOptionToSprite.ContainsKey(ButtonOption.B))
            buttonOptionToSprite.Add(ButtonOption.B, Resources.Load<Sprite>("sprites/cards/buttontasks/bsprite"));
        if (!buttonOptionToSprite.ContainsKey(ButtonOption.X))
            buttonOptionToSprite.Add(ButtonOption.X, Resources.Load<Sprite>("sprites/cards/buttontasks/xsprite"));
        if (!buttonOptionToSprite.ContainsKey(ButtonOption.Y))
            buttonOptionToSprite.Add(ButtonOption.Y, Resources.Load<Sprite>("sprites/cards/buttontasks/ysprite"));
        if (!buttonOptionToSprite.ContainsKey(ButtonOption.RANDOM))
            buttonOptionToSprite.Add(ButtonOption.RANDOM, Resources.Load<Sprite>("sprites/cards/buttontasks/randomsprite"));

        if (completedSprite == null)
            completedSprite = Resources.Load<Sprite>("sprites/cards/buttontasks/completedsprite");
    }

    public void SetButtonOption(ButtonOption buttonOption)
    {
        SetupStaticVariables();
        this.buttonOption = buttonOption;
        GetComponent<SpriteRenderer>().sprite = buttonOptionToSprite[buttonOption];
    }

    public void Complete()
    {
        isCompleted = true;
        GetComponent<SpriteRenderer>().sprite = completedSprite;
    }
}
