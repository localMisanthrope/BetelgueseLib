using Terraria;
using Terraria.ModLoader;

namespace BetelgueseLib.Players;

public class PlayerHitManager : ModPlayer
{
    public bool JustHit { get; set; }

    public bool HitByNPC { get; set; }

    public bool HitByProjectile { get; set; }

    public override void OnHitByNPC(NPC npc, Player.HurtInfo hurtInfo)
    {
        HitByNPC = true;
        JustHit = true;
    }

    public override void OnHitByProjectile(Projectile proj, Player.HurtInfo hurtInfo)
    {
        HitByProjectile = true;
        JustHit = true;
    }

    public override void ResetEffects()
    {
        JustHit = false;
        HitByNPC = false;
        HitByProjectile = false;
    }
}