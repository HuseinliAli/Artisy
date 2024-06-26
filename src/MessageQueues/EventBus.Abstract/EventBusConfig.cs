﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Abstract
{
    public class EventBusConfig
    {
        public int ConnectionRetryCount { get; set; } = 5;

        public string DefaultTopicName { get; set; } = "ArtShopEventBus";

        public string EventBusConnectionString { get; set; } = String.Empty;

        public string SubscriberClientAppName { get; set; } = String.Empty;

        public string EventNamePrefix { get; set; } = String.Empty;

        public string EventNameSuffix { get; set;} = String.Empty;

        public EventBusType EventBusType { get; set; } = EventBusType.RabbitMQ;

        public object Connection { get; set; }

        public bool DeleteEventPrefix => !String.IsNullOrEmpty(EventNamePrefix);

        public bool DeleteEventSuffix => !String.IsNullOrEmpty(EventNameSuffix);
    }
}
