using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace DinamycServer
{
    public static class Function //Функции
    {
        public static void SendClientMessage(TcpClient client, string message) //Отравить клиенту сообщение
        {
            Function.WriteColorText("Send: " + message);
            try
            {
                client.Client.Send(Encoding.UTF8.GetBytes(message));
            }
            catch
            {
                CheckEmptyClients(client);
                Function.WriteColorText("ERRMESS!\n", ConsoleColor.Red);
            }
        }

        public static void CheckEmptyClients(TcpClient CheckingClient)//Поиск пустых клиентов и их удаление
        {
            if(CheckingClient != null)
            {
                check(CheckingClient);
            }
            else
            {
                foreach(var ChClients in Data.TpClient)
                {
                    check(ChClients);
                }                
                WriteColorText($"Clients cleared", ConsoleColor.Yellow);   
                Console.WriteLine(Data.TpClient.Count); 
            }
            void check(TcpClient cl) //Метод по проверке определённого клиента
            {
                try
                {           
                    cl.Client.Send(new byte[1]);
                }
                catch
                {
                    Data.TpClient.Remove(cl);
                    cl.Close();
                    WriteColorText("Client removed", ConsoleColor.Yellow); 
                }
            }
        }

        public static void WriteColorText(string text, ConsoleColor color) //Отправка цветного сообщения в консоль
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Data.Logger.WriteLine(text);
            Console.ResetColor();
        }
        
        public static void WriteColorText(string text) //Отправка обычный сообщения в консоль
        {
            Console.WriteLine(text);
            Data.Logger.WriteLine(text);
        }

        public static void SendMessage(string nick, string message) //Отправить сообщение в ОБЩИЙ чат
        {
            foreach (var client in Data.TpClient)
            {
                try
                {
                    SendClientMessage(client, $"%MES:{nick}:{message}"); 
                }     
                catch
                {
                    CheckEmptyClients(client);
                }      
                
            }
        }
    
        public static void SendConsoleArgumentList(string[] needArg, string[] inArg) //Отправка в консоль нужных и входящих аргументов
        {
            Function.WriteColorText("[Argument needed] | [Incoming argument]", default);
            for(int i = 0; i < needArg.Length; i++)
            {
                if(inArg[i] == null || inArg[i] == "" || inArg[i] == " ") inArg[i] = "null";
                
                Function.WriteColorText(needArg[i] + " | " + inArg[i], default);
            }
        }
    }
}