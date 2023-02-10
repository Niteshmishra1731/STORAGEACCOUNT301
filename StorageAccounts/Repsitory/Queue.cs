using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;
using Microsoft.VisualBasic;
using Microsoft.WindowsAzure.Storage.Queue;
using System;
using System.Reflection.Metadata;

namespace StorageAccounts.Repsitory
{
    public class Queue
    {
        static string connectionstring = "DefaultEndpointsProtocol=https;AccountName=strorageaccountiot;AccountKey=Ld15S4Z6nyfOUePlDKpaFs/uCaJFnSt7ZvfoziUKL9wKxVos2EC+xoGhHLIvbDzoHk9JnWQQYx4l+AStmBucaQ==;EndpointSuffix=core.windows.net";
        public static async Task<bool> CreateQueue(string queueName)
        {
            if (string.IsNullOrEmpty(queueName))
            {
                throw new ArgumentException("enter queue name");
            }
            try
            {
                QueueClient container = new QueueClient(connectionstring, queueName);
                await container.CreateIfNotExistsAsync();
                if (container.Exists())
                {
                    Console.WriteLine("Queue created" + container.Name);
                    return true;
                }
                else
                {
                    Console.WriteLine("Check azure connection and type again");
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static async Task InsertMessage(string queueName, string msg)
        {
            if (string.IsNullOrEmpty(queueName))
            {
                throw new ArgumentNullException("enter queue name");
            }
            QueueClient container = new QueueClient(connectionstring,queueName);
            await container.CreateIfNotExistsAsync();
            if (container.Exists())
            {
                var data = container.SendMessage(msg);
                Console.WriteLine("Message sent successfully");
            }
            else
            {
                Console.WriteLine("Queue message not sent");
            }

        }
        public static async Task<PeekedMessage[]> PeekMessage(string queueName)
        {
            QueueClient container = new QueueClient(connectionstring, queueName);
            PeekedMessage[] msg = null;
            if (container.Exists())
            {
                msg = container.PeekMessages(2);
            }
            return msg;
        }
        public static async Task UpdateMessage(string queueName, string data)
        {
            QueueClient container = new QueueClient(connectionstring, queueName);
            if (container.Exists())
            {
                QueueMessage[] msg =container.ReceiveMessages();
                container.UpdateMessage(msg[0].MessageId, msg[0].PopReceipt, data, TimeSpan.FromSeconds(100));

            }

        }
        public static async Task DequeueMessage(string queueName)
        {
            QueueClient container = new QueueClient(connectionstring, queueName);
            if(container.Exists())
            {
                QueueMessage[] msg = container.ReceiveMessages();
                System.Console.WriteLine("Dequeue message" + msg[0].Body);
                container.DeleteMessage(msg[0].MessageId, msg[0].PopReceipt);
            }
        }
        public static async Task DeleteQueue(string queueName)
        {
            QueueClient container = new QueueClient(connectionstring, queueName);
            if(container.Exists())
            {
                await container.DeleteAsync();
            }
        }

    }
}

