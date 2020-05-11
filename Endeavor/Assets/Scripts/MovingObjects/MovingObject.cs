using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovingObject : MonoBehaviour
{
    protected bool currentlyMovingHorizontal;

    static public float moveTime;
    static public LayerMask blockingLayer;
    static public float inverseMoveTime;

    static MovingObject()
    {
        moveTime = 0.05f;
        blockingLayer = -1;
        inverseMoveTime = 1f / moveTime;
    }

    public List<Sprite> mainListOfSprites;
    private Sprite[] movingSouthSprites = new Sprite[4];
    private Sprite[] movingWestSprites = new Sprite[4];
    private Sprite[] movingNorthSprites = new Sprite[4];
    private Sprite[] movingEastSprites = new Sprite[4];
    private Sprite[] currentMovingSprites = new Sprite[4];

    public BoxCollider2D boxCollider;
    public Rigidbody2D rb2D;

    private bool takeLeftStep = true;
    private SpriteRenderer spriteRenderer;
    private Sprite defaultSprite;

    private Location.Direction directionFacing;

    public static GameObject positionBusy;
    protected GameObject currentPositionBusy;

    public bool isCurrentlyMoving = false;

    protected virtual void Start()
    {
        if (positionBusy == null)
        {
            positionBusy = Resources.Load("prefabs/positionbusy") as GameObject;
        }

        if (blockingLayer == -1)
        {
            blockingLayer = 1 << LayerMask.NameToLayer("BlockingLayer");
        }

        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        SetSortingOrder();
        defaultSprite = spriteRenderer.sprite;

        for (int i = 0; i < 16; i++)
        {
            if (mainListOfSprites.Count <= i)
            {
                mainListOfSprites.Add(defaultSprite);
            }
        }

        SetSpriteArrayToMainArray(movingSouthSprites, 0);
        SetSpriteArrayToMainArray(movingWestSprites, 4);
        SetSpriteArrayToMainArray(movingNorthSprites, 8);
        SetSpriteArrayToMainArray(movingEastSprites, 12);

        SetDirection(Location.Direction.SOUTH);
        boxCollider = GetComponent<BoxCollider2D>();
        rb2D = GetComponent<Rigidbody2D>();
        
        currentPositionBusy = null;
    }

    public Location.Direction GetDirection()
    {
        return directionFacing;
    }

    public void SetDirection(Location.Direction dir)
    {
        switch (dir)
        {
            case Location.Direction.SOUTH:
                currentMovingSprites = movingSouthSprites;
                break;
            case Location.Direction.WEST:
                currentMovingSprites = movingWestSprites;
                break;
            case Location.Direction.NORTH:
                currentMovingSprites = movingNorthSprites;
                break;
            case Location.Direction.EAST:
                currentMovingSprites = movingEastSprites;
                break;
            case Location.Direction.STAY:
                spriteRenderer.sprite = currentMovingSprites[0];
                return;
        }
        directionFacing = dir;

        spriteRenderer.sprite = currentMovingSprites[0];
    }

    protected void SetDirectionFromEndingLocation(Vector3 end)
    {
        if ((transform.position - end).y == 1)
        {
            SetDirection(Location.Direction.SOUTH);
        }
        else if ((transform.position - end).x == 1)
        {
            SetDirection(Location.Direction.WEST);
        }
        else if ((transform.position - end).y == -1)
        {
            SetDirection(Location.Direction.NORTH);
        }
        else if ((transform.position - end).x == -1)
        {
            SetDirection(Location.Direction.EAST);
        }
        else
        {
            SetDirection(Location.Direction.STAY);
        }
    }

    private void SetSpriteArrayToMainArray(Sprite[] arrayOfSprites, int mainArrayStartIndex)
    {
        for (int i = 0; i < arrayOfSprites.Length; i++)
        {
            arrayOfSprites[i] = mainListOfSprites[i + mainArrayStartIndex];
        }
    }

    protected bool Move(Location.Direction direction, out RaycastHit2D hit)
    {
        switch (direction)
        {
            case Location.Direction.SOUTH:
                return Move(0, -1, out hit);
            case Location.Direction.WEST:
                return Move(-1, 0, out hit);
            case Location.Direction.NORTH:
                return Move(0, 1, out hit);
            case Location.Direction.EAST:
                return Move(1, 0, out hit);
            default:
                return Move(0, 0, out hit);
        }
    }

    protected bool Move(int xDir, int yDir, out RaycastHit2D hit)
    {
        if (!isCurrentlyMoving)
        {
            isCurrentlyMoving = true;

            if (xDir == 0 && yDir == 0)
            {
                hit = new RaycastHit2D();
                StartCoroutine(WaitAndMoveSuccessfull());
                isCurrentlyMoving = false;
                return true;
            }

            Vector3 start = transform.position;
            Vector3 end = start + new Vector3(xDir, yDir);

            SetDirectionFromEndingLocation(end);

            boxCollider.enabled = false;
            hit = Physics2D.Linecast(start, end, blockingLayer);
            boxCollider.enabled = true;

            if (hit.transform == null)
            {
                StartCoroutine(SmoothMovement(start, end));
                return true;
            }
            else
            {
                isCurrentlyMoving = false;
                return false;
            }
        }
        hit = new RaycastHit2D();
        return false;
    }

    private IEnumerator WaitAndMoveSuccessfull()
    {
        yield return new WaitForSeconds(MovingObject.moveTime - 0.01f);
        MoveSuccessfull();
    }

    protected virtual IEnumerator SmoothMovement(Vector3 start, Vector3 end)
    {
        currentPositionBusy = Instantiate(positionBusy);
        currentPositionBusy.transform.position = end;

        float sqrRemainingDistance = (transform.position - end).sqrMagnitude;

        bool isStepping = false;

        bool hasSentMoveSuccessfull = false;
        while (sqrRemainingDistance > float.Epsilon)
        {
            Vector3 newPosition = Vector3.MoveTowards(rb2D.position, end, inverseMoveTime * Time.deltaTime);

            rb2D.MovePosition(newPosition);
            sqrRemainingDistance = (transform.position - end).sqrMagnitude;

            if (sqrRemainingDistance <= 0.95f && !isStepping)
            {
                if (currentPositionBusy != null && currentPositionBusy.GetComponent<BoxCollider2D>().enabled)
                {
                    boxCollider.enabled = false;
                    currentPositionBusy.GetComponent<BoxCollider2D>().enabled = false;
                    if (Physics2D.Linecast(transform.position, end, blockingLayer).transform != null)
                    {
                        end = start;
                        sqrRemainingDistance = (transform.position - end).sqrMagnitude;
                        Destroy(currentPositionBusy);
                        hasSentMoveSuccessfull = true;
                    } else
                    {
                        currentPositionBusy.GetComponent<BoxCollider2D>().enabled = true;
                    }
                    boxCollider.enabled = true;
                }

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
            else if (sqrRemainingDistance <= 0.2f)
            {
                Destroy(currentPositionBusy);
                spriteRenderer.sprite = currentMovingSprites[2];
                if (!hasSentMoveSuccessfull)
                {
                    hasSentMoveSuccessfull = true;
                    MoveSuccessfull();
                }
            }
            yield return null;
        }
        isCurrentlyMoving = false;
    }

    virtual protected void MoveSuccessfull()
    {
        SetSortingOrder();
    }

    protected void SetSortingOrder()
    {
        GetComponent<SpriteRenderer>().sortingOrder = Mathf.RoundToInt(transform.position.y) * -1;
    }

    protected void MoveVertical(int horizontal, int vertical, out RaycastHit2D hit)
    {
        if (!Move(0, vertical, out hit) && horizontal != 0)
        {
            Move(horizontal, 0, out hit);
            currentlyMovingHorizontal = true;
        }
        else
        {
            currentlyMovingHorizontal = false;
        }
    }

    protected void MoveHorizontal(int horizontal, int vertical, out RaycastHit2D hit)
    {
        if (!Move(horizontal, 0, out hit) && vertical != 0)
        {
            Move(0, vertical, out hit);
            currentlyMovingHorizontal = false;
        }
        else
        {
            currentlyMovingHorizontal = true;
        }
    }

    public static bool VectorsAreClose(Vector3 vec1, Vector3 vec2)
    {
        return Mathf.Abs(vec1.x - vec2.x) <= 0.00001 &&
            Mathf.Abs(vec1.y - vec2.y) <= 0.00001;
    }

    public bool IsOnBlockedLocation()
    {
        bool returnVal = false;
        GetComponent<BoxCollider2D>().enabled = false;
        if (Physics2D.Linecast(transform.position, transform.position, blockingLayer).transform != null)
        {
            returnVal = true;
        }
        GetComponent<BoxCollider2D>().enabled = true;
        return returnVal;
    }
}
