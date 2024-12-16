using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;

public class DropChestScript : MonoBehaviour
{
    private ChestPermValues chestPermValues;
    [SerializeField] private Sprite closedChestSprite1;
    [SerializeField] private Sprite closedChestSprite2;
    [SerializeField] private Sprite openChestSprite;
    [SerializeField] private QTE_Handler qte_Handler;
    [SerializeField] private string hintText = "Press E to open chest";
    [SerializeField] private TMP_Text hint_TXT;
    [SerializeField] private Transform hint_Panel;
    [SerializeField] private float activationDistance = 3f;

    [SerializeField] private Lockpick_Handler lock_handler;
    [SerializeField] private XP_Script xp_Handler;
    [SerializeField] private PuzzleHandler puzzleHandler;
    [SerializeField] private PostProcessingColor postProcessing;
    private Transform player;
    private bool isAllowed;
    private bool overwriteHint;

    private void Start()
    {
        chestPermValues = GameObject.Find("ChestPermValues").GetComponent<ChestPermValues>();
        FindAndAssignReferences();
        isAllowed = true;

    }

    private void FindAndAssignReferences()
    {
        postProcessing = chestPermValues.postProcessing;
        hint_TXT = chestPermValues.hint_TXT;
        hint_Panel = chestPermValues.hint_Panel;
        lock_handler = chestPermValues.lock_handler;
        xp_Handler = chestPermValues.xp_Handler;
        puzzleHandler = chestPermValues.puzzleHandler;
        player = chestPermValues.player;
        qte_Handler = chestPermValues.qte_Handler;
    }

    private void OpenChest(SpriteRenderer targetSprite)
    {
        if (targetSprite != null)
        {
            Vector3Int cellPosition = Vector3Int.RoundToInt(targetSprite.transform.position);
            if (targetSprite.sprite == closedChestSprite1)
            {
                int randNum = Random.Range(1, 4);
                if (randNum == 1)
                {
                    lock_handler.ChestAttempt(0, 100, false, cellPosition, "closed1");
                }
                else if(randNum == 2)
                {
                    qte_Handler.ChestAttempt(0, 100, false, cellPosition, "closed1");
                }
                else if(randNum == 3)
                {
                    puzzleHandler.ChestAttempt(0, 100, false, cellPosition, "closed1");
                }
                else if (randNum == 4)
                {
                    lock_handler.ChestAttempt(0, 100, false, cellPosition, "closed1");
                }
            }
            else if (targetSprite.sprite == closedChestSprite2)
            {
                int randNum = Random.Range(1, 4);
                if (randNum == 1)
                {
                    lock_handler.ChestAttempt(200, 400, true, cellPosition, "closed2");
                }
                else if (randNum == 2)
                {
                    qte_Handler.ChestAttempt(200, 400, true, cellPosition, "closed2");
                }
                else if (randNum == 3)
                {
                    puzzleHandler.ChestAttempt(200, 400, true, cellPosition, "closed2");
                }
                else if(randNum == 4)
                {
                    lock_handler.ChestAttempt(200, 400, true, cellPosition, "closed2");
                }
            }
            targetSprite.sprite = openChestSprite;
            hint_Panel.gameObject.SetActive(false);
            hint_TXT.text = "";
            isAllowed = false;

            Destroy(gameObject);
        }
        else
        {
            Debug.Log("There is an issue here!");
        }
    }

    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);

        if (distance <= activationDistance)
        {
            if (!overwriteHint)
            {
                hint_Panel.gameObject.SetActive(true);
                hint_Panel.position = transform.position + new Vector3(0, 1, 0);
                hint_TXT.text = hintText;

                if (Input.GetKeyDown(KeyCode.E) && isAllowed)
                {
                    hint_Panel.gameObject.SetActive(false);
                    hint_TXT.text = "";
                    SpriteRenderer chestSpriteRenderer = GetComponent<SpriteRenderer>();
                    OpenChest(chestSpriteRenderer);
                }
            }
        }
        else
        {
            if (!overwriteHint)
            {
                hint_Panel.gameObject.SetActive(false);
                hint_TXT.text = "";
            }
        }
    }
}
