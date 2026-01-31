using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Masks/HomingMissile")]
public class HomingMissileSO : MaskSO
{
    [SerializeField] private ProjectileSO projectile;
    
    public override void Equip(Player pPlayer)
    {
        pPlayer.StartCoroutine(ShootMissileCo(pPlayer));
    }

    public override void Unequip(Player pPlayer)
    {
    }
    
    private IEnumerator ShootMissileCo(Player pPlayer)
    {
        Projectile projectileObject = Instantiate(projectile.projectilePrefab, pPlayer.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(cooldown);
    }
}
