public static class CascadeEffect
{
    public enum CascadeEffectType
    {
        BrokenWindow,
        BrokenFuse,
        BrokenHeatingSystem,
        BrokenAlarmSystem,
    }

    static public void Cascade(CascadeEffectType type)
    {
        switch (type)
        {
            case CascadeEffectType.BrokenWindow:
                BrokenWindow();
                break;
            case CascadeEffectType.BrokenFuse:
                BrokenFuse();
                break;
            case CascadeEffectType.BrokenHeatingSystem:
                BrokenHeatingSystem();
                break;
            case CascadeEffectType.BrokenAlarmSystem:
                BrokenAlarmSystem();
                break;
        }
    }

    static void BrokenWindow()
    {

    }

    static void BrokenFuse()
    {

    }

    static void BrokenHeatingSystem()
    {

    }

    static void BrokenAlarmSystem()
    {

    }
}
