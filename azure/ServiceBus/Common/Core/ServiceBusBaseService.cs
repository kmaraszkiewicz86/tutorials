using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Core
{
    public abstract class ServiceBusBaseService
    {
        protected string ServiceBusConnectionString =>
            "Endpoint=sb://servicebus209.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=JYYFh+3b/MrREmitX18qy8VecUyafoH1vULoUkdfI0I=";

    }
}
