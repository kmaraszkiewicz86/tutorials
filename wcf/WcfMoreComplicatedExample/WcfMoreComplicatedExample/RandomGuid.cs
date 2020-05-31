using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace WcfMoreComplicatedExample
{
    public class RandomGuid : MarkupExtension
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return Guid.NewGuid().ToString();
        }
    }
}
