public enum StoryItemState
{
    Hidden,
    Active,
    Collected
}
public enum BrokenItemState
{
    NotImportant,
    CurrentTask,
    IsRepaired,
    Cascade
}

public enum CleanItemState
{
    NotImportant,
    ToBeCleaned,
    Cleaned
}

//For the phone system, determines what conversation chat to open.
public enum CurrentUser { WIFE, WORK, PSYCHOLOGIST };
