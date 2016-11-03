using System.Xml.Serialization;

namespace BehaviourSpecs.TestData
{
    [XmlRoot(Namespace = "http://root.attribute")]
    public class XmlObjectWithRootAttribute
    {
        public int MyProperty { get; set; }
    }
}
