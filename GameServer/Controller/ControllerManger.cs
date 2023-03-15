using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Common;
using GameServer.Servers;

namespace GameServer.Controller
{
    //管理所有Controller,管理所有消息的请求和分发
    class ControllerManger
    {
        private Dictionary<RequestCode, BaseController> controllerDict = new Dictionary<RequestCode, BaseController>();
        public Server server;
        public ControllerManger(Server svr)
        {
            this.server = svr;
            InitController();
        }
        void InitController() 
        {
            //TODO
            DefaultController defaultController = new DefaultController();
            controllerDict.Add(defaultController.RequestCode,defaultController);
            controllerDict.Add(RequestCode.User, new UserController());
        }
        public void HandleRequest(RequestCode requestCode, ActionCode actionCode,string data,Client client)
        {
            //处理请求
            BaseController controller;
            bool isGet = controllerDict.TryGetValue(requestCode, out controller);
            if (isGet == false)
            {
                //商业框架可以输出到日志中
                Console.WriteLine("无法得到" + requestCode + "所对应的Controller,无法处理请求");
                return;
            }
            #region 获取方法名利用反射机制调用相应方法名的方法
            //获取方法名
            string methodName = Enum.GetName(typeof(ActionCode), actionCode);
            //获取方法信息
            MethodInfo mi = controller.GetType().GetMethod(methodName);
            if (mi == null) 
            {
                //没有得到方法信息
                Console.WriteLine("[警告]在Controller[" + controller + "]中没有对应的处理方法：[" + methodName + "]");
                return;
            }
            //调用方法名的方法
            object[] parameters = new object[] { data,client,server};
            object o = mi.Invoke(controller,parameters);
            if (o == null ||string.IsNullOrEmpty(o as string)) 
            {
                return;
            }
            //服务器发送响应
            server.SendResponse(client, actionCode, data);
            #endregion
        }

    }
}
