using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCFChat
{
    // https://www.youtube.com/watch?v=QohqDyTjclw&t=1344s
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class ServiceChat : IServiceChat
    {

        List<ServerUser> users = new List<ServerUser>();

        int nextID = 1;
        
        public int Connect(string name)
        {
            ServerUser user = new ServerUser
            {
                ID = nextID,
                Name = name,
                operationContext = OperationContext.Current
            };
            nextID++;
            SendMessage($"{name} вошел в чат.", 0);
            users.Add(user);
            return user.ID;
        }

        public void Disconnect(int id)
        {
            ServerUser user = users.FirstOrDefault(e => e.ID == id);
            if (user != null)
            {
                users.Remove(user);
                SendMessage($"{user.Name} вышел из чата.", 0);
            }
        }

        public void SendMessage(string message, int id)
        {
            string answer = DateTime.Now.ToShortTimeString() + " : ";
            ServerUser user = users.FirstOrDefault(e => e.ID == id);
            if (user != null)
            {
                answer += "[" + user.Name + "] ";
            }
            answer += message;
            foreach (ServerUser item in users)
            {
                item.operationContext.GetCallbackChannel<IServiceChatCallback>().MsgCallback(answer);
            }
        }

    }
}
