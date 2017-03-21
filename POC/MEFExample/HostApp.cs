using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;

namespace MEFExample
{
    public class HostApp
    {
        [Import]
        public IMessagePlugin MessageToClient { get; set; }

        public void Start()
        {
            Compose();
            Console.WriteLine(MessageToClient.GetMessage());
        }

        private void Compose()
        {
            AssemblyCatalog catalog = 
                new AssemblyCatalog(System.Reflection.Assembly.GetExecutingAssembly());
            CompositionContainer container = new CompositionContainer(catalog);
            container.ComposeParts(this);
        }
    }
}
