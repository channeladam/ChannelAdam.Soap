using System.Xml.Serialization;

namespace BehaviourSpecs.TestData
{
    [XmlType(Namespace = "http://type.attribute")]
    public class XmlObjectWithTypeAttribute
    {
        public int MyProperty { get; set; }
    }
}
