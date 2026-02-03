using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAbility : MonoBehaviour
{
    [SerializeField] private ProjectileSO projectileData;
    [SerializeField] private float cooldown;

    private Camera cam;

    private void Awake()
    {
        cam = Camera.main;
        
        StartCoroutine(UseAbilityCo());
    }

    public void Use()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();
        mousePos = cam.ScreenToWorldPoint(mousePos);
        
        Vector2 aimDirection = mousePos - transform.position.ToVector2();
        aimDirection.Normalize();
        
        Projectile projectileObject = Instantiate(projectileData.projectilePrefab, transform.position, Quaternion.identity);
        projectileObject.velocity = aimDirection * projectileData.Speed;
    }

    IEnumerator UseAbilityCo()
    {
        while (true)
        {
            //Use();
            yield return new WaitForSeconds(cooldown);
        }
    }
}
