using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Abstract
{
    public class SubscriptionInfo
    {
        public Type HandlerType { get;private set; }

        public SubscriptionInfo(Type handlerType)
        {
            HandlerType = handlerType ?? throw new ArgumentNullException(nameof(handlerType));  
        }

        public static SubscriptionInfo Typed(Type handlerType)
        {
            return new(handlerType);
        }
    }
}
