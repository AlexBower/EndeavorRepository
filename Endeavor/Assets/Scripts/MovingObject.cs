using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovingObject : MonoBehaviour
{
    static public float moveTime = 0.2f;
    public LayerMask blockingLayer;

    public Sprite[] movingSouthSprites = new Sprite[4];
    private Sprite[] movingWestSprites = new Sprite[4];
    private Sprite[] movingNorthSprites = new Sprite[4];
    private Sprite[] movingEastSprites = new Sprite[4];

    public BoxCollider2D boxCollider;
    public Rigidbody2D rb2D;
    public float inverseMoveTime;

    private bool takeLeftStep = true;
    private SpriteRenderer spriteRenderer;

    protected virtual void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        SetSpriteArrayToDefault(movingSouthSprites);
        SetSpriteArrayToDefault(movingWestSprites);
        SetSpriteArrayToDefault(movingNorthSprites);
        SetSpriteArrayToDefault(movingEastSprites);

        boxCollider = GetComponent<BoxCollider2D>();
        rb2D = GetComponent<Rigidbody2D>();
        inverseMoveTime = 1f / moveTime;
    }

    private void SetSpriteArrayToDefault(Sprite[] arrayOfSprites)
    {
        for (int i = 0; i < arrayOfSprites.Length; i++)
        {
            if (arrayOfSprites[i] == null)
            {
                arrayOfSprites[i] = spriteRenderer.sprite;
            }
        }
    }

    protected bool Move(int xDir, int yDir, out RaycastHit2D hit)
    {
        Vector2 start = transform.position;
        Vector2 end = start + new Vector2(xDir, yDir);

        boxCollider.enabled = false;
        hit = Physics2D.Linecast(start, end, blockingLayer); 
        boxCollider.enabled = true;

        if (hit.transform == null)
        {
            StartCoroutine(SmoothMovement(end));
            return true;
        }

        return false;
    }

    protected virtual IEnumerator SmoothMovement(Vector3 end)
    {
        float sqrRemainingDistance = (transform.position - end).sqrMagnitude;

        Sprite[] currentMovingSprites = movingSouthSprites;

        if ((transform.position - end).y == 1)
        {
            currentMovingSprites = movingSouthSprites;
        }
        if ((transform.position - end).x == 1)
        {
            currentMovingSprites = movingWestSprites;
        }
        if ((transform.position - end).y == -1)
        {
            currentMovingSprites = movingNorthSprites;
        }
        if ((transform.position - end).x == -1)
        {
            currentMovingSprites = movingEastSprites;
        }

        spriteRenderer.sprite = currentMovingSprites[0];

        bool isStepping = false;
        while (sqrRemainingDistance > float.Epsilon)
        {
            Vector3 newPosition = Vector3.MoveTowards(rb2D.position, end, inverseMoveTime * Time.deltaTime);
            if (Player.isChangingArea)
            {
                newPosition = Player.endingLocation;
                end = Player.endingLocation;
                Player.isChangingArea = false;
                spriteRenderer.sprite = currentMovingSprites[2];
            }

            rb2D.MovePosition(newPosition);
            sqrRemainingDistance = (transform.position - end).sqrMagnitude;

            if (sqrRemainingDistance <= 0.9f && !isStepping)
            {
                if (takeLeftStep)
                {
                    spriteRenderer.sprite = currentMovingSprites[1];
                    takeLeftStep = false;
                }
                else
                {
                    spriteRenderer.sprite = currentMovingSprites[3];
                    takeLeftStep = true;
                }
                
                isStepping = true;
            }
            else if (sqrRemainingDistance <= 0.3f)
            {
                spriteRenderer.sprite = currentMovingSprites[2];
            }

            yield return null;
        }
    }

    enum Direction
    {
        south,
        west,
        north,
        east
    }
}
