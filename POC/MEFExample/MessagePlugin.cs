using System.ComponentModel.Composition;

namespace MEFExample
{
    [Export(typeof(IMessagePlugin))]
    public class MessagePlugin : IMessagePlugin
    {
        public string GetMessage()
        {
            return "Message from plugin";
        }
    }
}
