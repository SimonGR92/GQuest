using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace DinamycServer
{
    public static class Data
    {
        public const int Port = 908;//Порт сервера
        public static List<TcpClient> TpClient = new List<TcpClient>(); //Инфа о подключённых сокетах

        public class ClientInfo //Инфо о клиенте (онлайн)
        {
            public ClientInfo(TcpClient socket, string email, string password, string nick, long id) //Инфо о клиенте
            {
                if(socket != null) {Socket = socket;}                      
                Email = email;
                Password = password;
                Nick = nick;
                ID = id;
            }

            public TcpClient Socket { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public string Nick { get; set; }
            public long ID { get; set; }
        }
    }
}