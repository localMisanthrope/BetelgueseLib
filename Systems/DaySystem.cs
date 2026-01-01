using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace BetelgueseLib.Systems;

public sealed class DaySystem : ModSystem
{
    private static int _day;

    public static int Day => _day;

    public static void AdvanceDay(int days)
    {
        _day += days;
        Main.dayTime = true;
        Main.time = 1400;
        Main.moonPhase = _day % 8;
        Main.NewText($"Day {_day}");
    }

    public override void PostUpdateWorld()
    {
        if (!Main.dayTime && Main.time >= Main.nightLength)
        {
            _day++;
            Main.NewText($"Day {_day}");
        }

        base.PostUpdateWorld();
    }

    public override void SaveWorldData(TagCompound tag) => tag[nameof(_day)] = _day;

    public override void OnWorldLoad() => _day = 0;

    public override void LoadWorldData(TagCompound tag) => _day = tag.GetInt(nameof(_day));
}