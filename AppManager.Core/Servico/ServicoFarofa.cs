using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppManager.Data.Access;
using AppManager.Data.Entity;

namespace AppManager.Core.Servico
{
    public interface IServicoFarofa
    {
        void DoFarofa();
    }

    public class ServicoFarofa : IServicoFarofa
    {
        private readonly IUnitOfWork _uow;

        public ServicoFarofa(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public void DoFarofa()
        {

        }
    }
}
