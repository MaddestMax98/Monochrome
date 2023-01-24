public static class CascadeEffect
{

    static public void Cascade(BrokenItemType type)
    {
        switch (type)
        {
            case BrokenItemType.Window:
                BrokenWindow();
                break;
            case BrokenItemType.Fuse:
                BrokenFuse();
                break;
            case BrokenItemType.HeatingSystem:
                BrokenHeatingSystem();
                break;
            case BrokenItemType.AlarmSystem:
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
