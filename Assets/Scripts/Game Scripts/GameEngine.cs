using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

// Enum representing the possible states of a grid cell
public enum SquareState
{
    Empty,
    Blue,
    Yellow
}

public class GridGame : MonoBehaviour
{
    private const int GridSize = 4; // Grid dimensions (4x4) (a const so it stays the same during runtime)
    private GameObject[,] gameBoard = new GameObject[GridSize, GridSize]; // Stores all grid squares

    [SerializeField] private Material blueMaterial;    // Material for Blue team
    [SerializeField] private Material yellowMaterial;  // Material for Yellow team
    [SerializeField] private Material defaultMaterial; // Default material for empty squares

    [SerializeField] private TextMeshProUGUI turnText;    // Displays current turn
    [SerializeField] private TextMeshProUGUI victoryText; // Displays game result
    [SerializeField] private TextMeshProUGUI actionText;  // Displays attack points
    [SerializeField] private Button attackButton; // Button to confirm an attack
    [SerializeField] private Button resetButton;  // Button to reset the game

    private bool isBlueTurn;  // Blue starts first (if true then blue turn)
    private bool isPhaseOne;  // Phase one: Selecting squares
    private int squaresColored;  // Tracks how many squares are assigned

    private GameObject selectedAttacker; // The square attacking
    private GameObject selectedDefender; // The square being attacked

    // --------------------------- Game Setup ---------------------------
    private void Start()
    {
        InitializeGame();
    }

    private void Update()
    {
        HandleMouseClick();
        CheckVictory();
    }

    void InitializeGame()
    {
        isBlueTurn = true;
        isPhaseOne = true;
        selectedAttacker = null;
        selectedDefender = null;
        squaresColored = 0;

        CreateGrid();
        UpdateTurnText();
        attackButton.gameObject.SetActive(false); // Hide attack button during phase one
        resetButton.onClick.AddListener(ResetGame); // Attach reset button event
    }

    // --------------------------- Grid Initialization ---------------------------
    private void CreateGrid()
    {
        float spacing = 2.0f; // Space between cubes

        for (int x = 0; x < GridSize; x++)
        {
            for (int z = 0; z < GridSize; z++)
            {
                GameObject square = GameObject.CreatePrimitive(PrimitiveType.Cube);
                square.transform.position = new Vector3(x * spacing, 0, z * spacing);
                square.GetComponent<MeshRenderer>().material = defaultMaterial;
                square.tag = SquareState.Empty.ToString(); // Set default state
                square.transform.SetParent(gameObject.transform); // Set the created square as a child for read-ability
                gameBoard[x, z] = square;
            }
        }
    }

    // --------------------------- Input Handling ---------------------------
    private void HandleMouseClick()
    {
        //Raycast for phase one
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                GameObject clickedSquare = hit.collider.gameObject;

                if (isPhaseOne)
                {
                    HandleColorSelection(clickedSquare);
                }
                else
                {
                    HandleAttackSelection(clickedSquare);
                }
            }
        }
    }

    // --------------------------- Phase One: Coloring ---------------------------
    private void HandleColorSelection(GameObject square)
    {
        //if its already tagged do nothing
        if (!square.CompareTag(SquareState.Empty.ToString())) return;

        // Assign color based on turn
        if (isBlueTurn)
        {
            square.GetComponent<MeshRenderer>().material = blueMaterial;
            square.tag = SquareState.Blue.ToString();
        }
        else
        {
            square.GetComponent<MeshRenderer>().material = yellowMaterial;
            square.tag = SquareState.Yellow.ToString();
        }

        squaresColored++;
        isBlueTurn = !isBlueTurn; // Switch turn
        UpdateTurnText();

        // Once all squares are colored, switch to attack phase
        if (squaresColored == GridSize * GridSize)
        {
            isPhaseOne = false;
            attackButton.gameObject.SetActive(true);
        }
    }

    // --------------------------- Phase Two: Attack Selection ---------------------------
    private void HandleAttackSelection(GameObject square)
    {
        if (selectedAttacker == null && square.tag == (isBlueTurn ? SquareState.Blue.ToString() : SquareState.Yellow.ToString()))
        {
            // Prevent selection if surrounded by the same color
            if (IsSurroundedBySameColor(square)) return;

            selectedAttacker = square;
            selectedAttacker.GetComponent<MeshRenderer>().material.color = Color.green; // Highlight attacker
        }
        else if (selectedAttacker != null && selectedDefender == null)
        {
            if (IsValidAttack(selectedAttacker, square))
            {
                selectedDefender = square;
                selectedDefender.GetComponent<MeshRenderer>().material.color = Color.red; // Highlight defender
            }
        }
    }

    private bool IsValidAttack(GameObject attacker, GameObject defender)
    {
        // Check to make sure squars are not attacking each other with the same color
        if (defender.tag != (isBlueTurn ? SquareState.Yellow.ToString() : SquareState.Blue.ToString())) return false;

        Vector3 attackerPos = attacker.transform.position;
        Vector3 defenderPos = defender.transform.position;

        // Check to make sure distance between picked squars is correct
        return Mathf.Abs(attackerPos.x - defenderPos.x) + Mathf.Abs(attackerPos.z - defenderPos.z) == 2.0f;
    }

    // --------------------------- Attack Execution ---------------------------
    public void PerformAttack()
    {
        // Check if neither are null before proceeding 
        if (selectedAttacker == null || selectedDefender == null) return;

        int attackPoints = CalculatePoints(selectedAttacker);
        int defensePoints = CalculatePoints(selectedDefender);

        DisplayActionText(attackPoints, defensePoints);

        // Resolve attack
        if (attackPoints > defensePoints)
        {
            selectedDefender.tag = selectedAttacker.tag;
        }
        else if (attackPoints < defensePoints)
        {
            selectedAttacker.tag = selectedDefender.tag;
        }

        RestoreColor(selectedAttacker);
        RestoreColor(selectedDefender);

        selectedAttacker = null;
        selectedDefender = null;
        isBlueTurn = !isBlueTurn;
        UpdateTurnText();

        if (CheckVictory()) EndGame();
    }

    // --------------------------- Utility Methods ---------------------------
    private void RestoreColor(GameObject square)
    {
        if (square.CompareTag(SquareState.Blue.ToString()))
            square.GetComponent<MeshRenderer>().material = blueMaterial;
        else if (square.CompareTag(SquareState.Yellow.ToString()))
            square.GetComponent<MeshRenderer>().material = yellowMaterial;
    }

    private int CalculatePoints(GameObject square)
    {
        int points = Random.Range(1, 7); // Dice roll
        Vector3 pos = square.transform.position;

        // Check adjacent squares for same color
        foreach (var dir in new Vector3[] { Vector3.forward, Vector3.back, Vector3.left, Vector3.right })
        {
            Ray ray = new Ray(pos, dir);
            if (Physics.Raycast(ray, out RaycastHit hit, 2.0f) && hit.collider.CompareTag(square.tag))
            {
                points++;
            }
        }

        return points;
    }

    private bool IsSurroundedBySameColor(GameObject square)
    {
        Vector3 pos = square.transform.position;
        string squareTag = square.tag;
        int sameColorCount = 0;
        int validNeighbors = 0;

        Vector3[] directions = { Vector3.forward, Vector3.back, Vector3.left, Vector3.right };

        foreach (Vector3 dir in directions)
        {
            Ray ray = new Ray(pos, dir);
            if (Physics.Raycast(ray, out RaycastHit hit, 2.0f))
            {
                validNeighbors++;
                if (hit.collider.CompareTag(squareTag)) sameColorCount++;
            }
        }

        return validNeighbors > 0 && sameColorCount == validNeighbors;
    }

    private void DisplayActionText(int attackPoints, int defensePoints)
    {
        actionText.text = $"Attack: {attackPoints} | Defense: {defensePoints}";
    }

    private bool CheckVictory()
    {
        // Check tag of the whole board and comapre it the whole board
        string firstTag = gameBoard[0, 0].tag;
        if (firstTag == SquareState.Empty.ToString()) return false;

        foreach (GameObject square in gameBoard)
        {
            if (square.tag != firstTag) return false;
        }

        return true;
    }

    private void UpdateTurnText()
    {
        //if true blue turn if false yellow turn
        turnText.text = isBlueTurn ? "Blue's Turn" : "Yellow's Turn";
    }

    private void EndGame()
    {
        victoryText.text = isBlueTurn ? "Blue Wins!" : "Yellow Wins!";
        resetButton.gameObject.SetActive(true);
        victoryText.gameObject.SetActive(true);
        attackButton.gameObject.SetActive(false);
        turnText.gameObject.SetActive(false);
        actionText.gameObject.SetActive(false);
    }

    private void ResetGame()
    {
        SceneManager.LoadSceneAsync(0);
    }

}
