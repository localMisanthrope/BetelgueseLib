using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;

namespace BetelgueseLib.Extensions;

public static class PlayerExtensions
{
    public static bool IsStill(this Player player) => player.velocity == Vector2.Zero;

    public static bool InLiquid(this Player player) => player.wet || player.lavaWet || player.honeyWet || player.shimmerWet;

    public static int NextOpenInventorySlot(this Player player)
    {
        int slotIndex = -1;

        for (int i = 0; i < Main.InventoryItemSlotsCount; i++)
            if (player.inventory[i].IsAir)
                slotIndex = i;

        return slotIndex;
    }

    public static void ConsumeFromInventory(this Player player, int type, int amount)
    {
        if (amount <= 0)
            return;

        foreach (var item in player.inventory)
        {
            if (item.IsAir || item.type != type)
                continue;

            if (amount > item.stack)
            {
                amount -= item.stack;
                item.TurnToAir();
            }
            else
            {
                item.stack -= amount;
                amount = 0;
            }
        }
    }

    public static List<Player> GetSurroundingPlayers(this Player player, float range, int count = -1)
    {
        List<Player> enumer = [];
        int counter = 0;
        bool doCount = count > -1;

        foreach (var other in Main.ActivePlayers)
        {
            if (other.WithinRange(player.Center, range))
            {
                if (doCount)
                    counter++;

                enumer.Add(other);
            }

            if (doCount && counter >= count)
                break;
        }

        return enumer;
    }

    public static List<NPC> GetSurroundingNPCs(this Player player, float range, int count = -1)
    {
        List<NPC> enumer = [];
        int counter = 0;
        bool doCount = count > -1;

        foreach (var npc in Main.ActiveNPCs)
        {
            if (npc.WithinRange(player.Center, range))
            {
                if (doCount)
                    counter++;

                enumer.Add(npc);
            }

            if (doCount && counter >= count)
                break;
        }

        return enumer;
    }

    public static Player? GetClosestPlayer(this Player player)
    {
        Player? closest = null;
        float closestDist = float.PositiveInfinity;

        foreach (var other in Main.ActivePlayers)
        {
            if (other == player)
                continue;

            var newDist = other.DistanceSQ(player.Center);
            if (newDist < closestDist)
            {
                closestDist = newDist;
                closest = other;
            }
        }

        return closest;
    }

    public static NPC? GetClosestNPC(this Player player, bool excludeFriendlies = true)
    {
        NPC? closest = null;
        float closestDist = float.PositiveInfinity;

        foreach (var npc in Main.ActiveNPCs)
        {
            if (excludeFriendlies && (npc.friendly || npc.CountsAsACritter))
                continue;

            var newDist = npc.DistanceSQ(player.Center);
            if (newDist < closestDist)
            {
                closestDist = newDist;
                closest = npc;
            }
        }

        return closest;
    }
}