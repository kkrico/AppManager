using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using AppManager.Core.Interfaces;
using AppManager.Core.Service;
using AttributeRouting;
using AttributeRouting.Web.Http;
using Microsoft.AspNet.SignalR;

namespace AppManager.Controllers.api
{
    public class AppManagerController : ApiController
    {
        private readonly IAppManagerService _appManagerService;
        private readonly IHubContext _hubContext;

        public AppManagerController(IAppManagerService appManagerService,IHubContext hubContext)
        {
            _appManagerService = appManagerService;
            _hubContext = hubContext;
        }

        /// <summary>
        /// Inicia o modo parse: Faz parse do server, de acordo com o modo de execução da aplicação
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        [POST("api/AppManager/Parse")]
        public void Parse()
        {
            _appManagerService.Parse();
        }
        
        /// <summary>
        /// Retorna o modo de execução da aplicação
        /// </summary>
        /// <returns></returns>
        [GET("api/AppManager/RunMode")]
        public string GetRunMode()
        {
            return AppManagerConfig.RunMode.ToString();
        }
    }
}