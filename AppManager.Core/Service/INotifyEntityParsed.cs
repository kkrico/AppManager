namespace AppManager.Core.Service
{
    public interface INotifyEntityParsed
    {
        event NotifyEntityHandler OnEntityParsed;
    }
}