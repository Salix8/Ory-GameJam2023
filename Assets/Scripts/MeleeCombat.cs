using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeCombat : MonoBehaviour
{
    Vector2 lookDirection = new Vector2(1, 0);
    public string parameterName = "Attack";
    public bool Dead;


    // Daño que se inflige al personaje al recibir un golpe
    public int damage = 10;

    // Tiempo en segundos que tienes que esperar entre ataques
    public float attackCooldown = 1.0f;

    // Tiempo transcurrido desde el último ataque
    private float timeSinceLastAttack = 0.5f;

    // Componente "Animator" del personaje
    public Animator animator;

    // Estado de la entrada de ataque
    public bool attackInput = false;

    // Dirección en la que mira el personaje
    public Vector3 facingDirection;

    // Jugador
    public int jugador;

    void Start()
    {
        // Asigna el componente "Animator" del personaje a la variable
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector2 move = new Vector2(horizontal, vertical);
        // Actualiza el tiempo transcurrido desde el último ataque
        timeSinceLastAttack += Time.deltaTime;

        // Actualiza el estado de la entrada de ataque
        if (jugador == 1)
        {
            attackInput = Input.GetKeyDown(KeyCode.E);
        }
        else if (jugador == 2)
        {
            attackInput = Input.GetKeyDown(KeyCode.Keypad0);
        }

        // Actualiza la dirección en la que mira el personaje
        facingDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        // Verifica si es posible realizar un ataque
        if (timeSinceLastAttack >= attackCooldown && attackInput)
        {
            // Reproduce la animación de ataque según la dirección en la que mira el personaje
            lookDirection.Set(move.x, move.y);
            animator.SetBool(parameterName, true);
            // Resetea el tiempo transcurrido desde el último ataque
            timeSinceLastAttack = 0.0f;
        }
    }

    public void DisableAttackTrigger()
    {
        animator.SetBool(parameterName, false);
    }
}