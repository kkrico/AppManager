namespace AppManager.Core.Service
{
    public interface IAppManagerService : INotifyEntityParsed
    {
        void Parse();
    }
}