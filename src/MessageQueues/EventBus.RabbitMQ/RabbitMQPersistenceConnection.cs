using Polly;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;
using System.Net.Sockets;

namespace EventBus.RabbitMQ
{
    public class RabbitMQPersistenceConnection(IConnectionFactory connectionFactory, int retryCount = 5): IDisposable
    {
        private object _lock_object = new();
        public IConnection Connection { get; set; }
        private bool _disposed; 
        public bool IsConnected => Connection is not null && Connection.IsOpen;

        public void Dispose()
        {
            _disposed=true;
            Connection.Dispose();
        }

        public IModel CreateModel()
            => Connection.CreateModel();

        public bool TryConnect()
        {
            lock( _lock_object)
            {
                var policy = Policy.Handle<SocketException>()
                    .Or<BrokerUnreachableException>()
                    .WaitAndRetry(retryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
                policy.Execute(() =>
                {
                    Connection = connectionFactory.CreateConnection();
                });

                if (IsConnected)
                {
                    Connection.ConnectionShutdown += Connection_ConnectionShutdown;
                    Connection.CallbackException += Connection_CallbackException;
                    Connection.ConnectionBlocked +=Connection_ConnectionBlocked;
                    return true;
                }
                return false;
            }
        }
        private void Connection_ConnectionShutdown(object sender, ShutdownEventArgs e)
        {
            if (_disposed) return;
            TryConnect();
        }
        private void Connection_CallbackException(object sender,global::RabbitMQ.Client.Events.CallbackExceptionEventArgs e)
        {
            if (_disposed) return;
            TryConnect();
        }
        private void Connection_ConnectionBlocked(object sender, global::RabbitMQ.Client.Events.ConnectionBlockedEventArgs e)
        {
            if (_disposed) return;
            TryConnect();
        }
       
    }
}
