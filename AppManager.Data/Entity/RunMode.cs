using System.ComponentModel;

namespace AppManager.Data.Entity
{
    public enum RunMode
    {
        [Description("Roda a aplicação em modo externo: Algumas funcionalidades podem não funcionar")]
        External,
        [Description("Modo interno")] Internal
    }
}