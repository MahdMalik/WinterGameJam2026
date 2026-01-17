using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAI : MonoBehaviour
{
    [Header("Movement Settings")]
    public float regSpeed = .5f;
    public float chargeSpeed = 2f;         // Speed when charging at player
    public float aggroRange = 1f;          // Distance to detect player and start charging
    public float stopDistance = 1f;      // Stop charging when this close to player

    [Header("Collision Settings")]
    public LayerMask wallLayers;           // Which layers count as walls
    public float wallCheckDistance = 1f;   // Raycast length to detect walls
    public Vector2 wallCheckOffset = Vector2.zero; // Offset raycast origin if needed
    public int circularRayCount = 16;      // Number of rays in 360° check
    public bool useCircularDetection = true; // Use 360° circular wall detection
    public bool debugWallRay = false;      // Draw rays in Scene view for debugging
    private Vector2 lastClearDirection = Vector2.right; // Fallback direction when trapped

    [Header("Visual Settings")]
    public bool flipSpriteX = true;        // Flip sprite when moving left

    private Transform player;              // Reference to the player's transform
    private Rigidbody2D rb;                // For physics-based movement (optional)

    public GameObject playerObj;          // Reference to the player GameObject

    void Start()
    {
        // Try to find the player

        if(playerObj != null)
        {
            player = playerObj.transform;
        } else
        {
            Debug.LogError("Player object not found! Make sure it has the 'Player' tag.");
        }

        // Optional: get rigidbody if present
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(player == null)
            return;
        
        // Calculate distance between enemy and player
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Check if player is in aggro range
        if(distanceToPlayer < aggroRange)
        {
            MoveTowardsPlayer(distanceToPlayer);
        }
    }

    void MoveTowardsPlayer(float distanceToPlayer)
    {
        // Slow down if very close to player
        float currentSpeed = (distanceToPlayer < stopDistance) ? regSpeed : chargeSpeed;

        Vector2 direction = (player.position - transform.position).normalized;

        // Check for walls and find best path
        if(useCircularDetection)
        {
            // Try to find a clear path using circular detection
            Vector2 bestDirection = FindClearPath(direction);
            if(bestDirection == Vector2.zero)
            {
                // All directions blocked - use last known clear direction to escape
                bestDirection = lastClearDirection;
            }
            else
            {
                // Save this clear direction for emergencies
                lastClearDirection = bestDirection;
            }
            direction = bestDirection;
        }
        else
        {
            // Simple forward check only
            if(IsWallAhead(direction))
            {
                if(rb != null)
                    rb.velocity = Vector2.zero;
                return;
            }
        }

        // Move towards player
        if(rb != null)
        {
            // Physics-based movement
            rb.velocity = direction * currentSpeed;
        }
        else
        {
            // Direct movement
            transform.position = Vector2.MoveTowards(
                transform.position, 
                player.position, 
                currentSpeed * Time.deltaTime
            );
        }

        // Flip sprite to face player
        FacePlayer();
    }

    bool IsWallAhead(Vector2 direction)
    {
        Vector2 origin = (Vector2)transform.position + wallCheckOffset;
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, wallCheckDistance, wallLayers);

        if(debugWallRay)
        {
            Color rayColor = hit.collider != null ? Color.red : Color.green;
            Debug.DrawRay(origin, direction * wallCheckDistance, rayColor);
        }

        return hit.collider != null;
    }

    Vector2 FindClearPath(Vector2 preferredDirection)
    {
        Vector2 origin = (Vector2)transform.position + wallCheckOffset;
        float angleStep = 360f / circularRayCount;
        float checkDistance = wallCheckDistance;
        
        // First check preferred direction (towards player) with standard distance
        if(!IsWallAhead(preferredDirection))
        {
            return preferredDirection;
        }

        // Check rays in a circular pattern around the enemy
        float bestScore = -1f;
        Vector2 bestDirection = Vector2.zero;
        
        for(int i = 0; i < circularRayCount; i++)
        {
            float angle = i * angleStep;
            Vector2 direction = new Vector2(
                Mathf.Cos(angle * Mathf.Deg2Rad),
                Mathf.Sin(angle * Mathf.Deg2Rad)
            );

            RaycastHit2D hit = Physics2D.Raycast(origin, direction, checkDistance, wallLayers);
            
            if(debugWallRay)
            {
                Color rayColor = hit.collider != null ? Color.red : Color.yellow;
                Debug.DrawRay(origin, direction * checkDistance, rayColor);
            }

            // If this direction is clear
            if(hit.collider == null)
            {
                // Score based on how close to preferred direction
                float score = Vector2.Dot(direction, preferredDirection);
                
                if(score > bestScore)
                {
                    bestScore = score;
                    bestDirection = direction;
                }
            }
        }

        // If no clear path found, try with shorter distance (might be trapped right against door)
        if(bestDirection == Vector2.zero)
        {
            float shortDistance = checkDistance * 0.3f; // Check very close
            for(int i = 0; i < circularRayCount; i++)
            {
                float angle = i * angleStep;
                Vector2 direction = new Vector2(
                    Mathf.Cos(angle * Mathf.Deg2Rad),
                    Mathf.Sin(angle * Mathf.Deg2Rad)
                );

                RaycastHit2D hit = Physics2D.Raycast(origin, direction, shortDistance, wallLayers);
                
                if(hit.collider == null)
                {
                    // Found escape route, prioritize moving away from player to disengage
                    bestDirection = direction;
                    break; // Take first available escape
                }
            }
        }

        // Return best direction found, or zero if completely trapped
        return bestDirection;
    }

    void FacePlayer()
    {
        if(flipSpriteX)
        {
            if(player.position.x < transform.position.x)
            {
                // Face left
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                // Face right
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }
}
