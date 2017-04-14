using System.Xml.Linq;

using BehaviourSpecs.TestData;
using ChannelAdam.Soap;
using ChannelAdam.Soap.Abstractions;
using ChannelAdam.TestFramework.MSTest;
using ChannelAdam.TestFramework.Xml;
using TechTalk.SpecFlow;

namespace BehaviourSpecs
{
    [Binding]
    [Scope(Feature = "MakingSoap11")]
    public class MakingSoap11UnitSteps : TestEasy
    {
        #region Fields

        private string soapEnvelope;
        private XmlTester xmlTester;

        #endregion

        #region Before/After

        [BeforeScenario]
        public void BeforeScenario()
        {
            this.xmlTester = new XmlTester(base.LogAssert);
        }

        #endregion

        #region Given

        #endregion

        #region When

        [When(@"a SOAP envelope with a header is built")]
        public void WhenASOAPEnvelopeWithAHeaderIsBuilt()
        {
            this.soapEnvelope = SoapBuilder.CreateSoap11Envelope()
                .WithHeader.AddAction("myActionOhYeah!")
                .Build()
                .ToString();

            this.xmlTester.ArrangeActualXml(this.soapEnvelope);

            var expectedXml =
@"<env:Envelope xmlns:env=""http://schemas.xmlsoap.org/soap/envelope/"">
  <env:Header>
    <wsa:Action xmlns:wsa=""http://www.w3.org/2005/08/addressing"">myActionOhYeah!</wsa:Action>
  </env:Header>
</env:Envelope>";
            this.xmlTester.ArrangeExpectedXml(expectedXml);
        }

        [When(@"a SOAP envelope with a body is built")]
        public void WhenASOAPEnvelopeWithABodyIsBuilt()
        {
            this.soapEnvelope = SoapBuilder.CreateSoap11Envelope()
                .WithBody.AddEntry("<a>hello from the body</a>")
                .Build()
                .ToString();

            this.xmlTester.ArrangeActualXml(this.soapEnvelope);

            var expectedXml =
@"<env:Envelope xmlns:env=""http://schemas.xmlsoap.org/soap/envelope/"">
  <env:Body>
    <a>hello from the body</a>
  </env:Body>
</env:Envelope>";
            this.xmlTester.ArrangeExpectedXml(expectedXml);
        }

        [When(@"a SOAP envelope with a header and body is built")]
        public void WhenASOAPEnvelopeWithAHeaderAndBodyIsBuilt()
        {
            this.soapEnvelope = SoapBuilder.CreateSoap11Envelope()
                .WithHeader.AddAction("myActionOhYeah!")
                .WithBody.AddEntry("<a>hello from the body</a>")
                .Build()
                .ToString();

            this.xmlTester.ArrangeActualXml(this.soapEnvelope);

            var expectedXml =
@"<env:Envelope xmlns:env=""http://schemas.xmlsoap.org/soap/envelope/"">
  <env:Header>
    <wsa:Action xmlns:wsa=""http://www.w3.org/2005/08/addressing"">myActionOhYeah!</wsa:Action>
  </env:Header>
  <env:Body>
    <a>hello from the body</a>
  </env:Body>
</env:Envelope>";
            this.xmlTester.ArrangeExpectedXml(expectedXml);
        }

        [When(@"a SOAP envelope with a fault is built")]
        public void WhenASOAPEnvelopeWithAFaultIsBuilt()
        {
            this.soapEnvelope = SoapBuilder.CreateSoap11Envelope()
                .WithBody.SetFault(Soap11FaultCode.Client, "oops", "it was me!", new XContainer[] { XElement.Parse("<myDetail1>detail 1</myDetail1>"), XElement.Parse("<myDetail2>detail 2</myDetail2>") })
                .Build()
                .ToString();

            this.xmlTester.ArrangeActualXml(this.soapEnvelope);

            var expectedXml =
@"<env:Envelope xmlns:env=""http://schemas.xmlsoap.org/soap/envelope/"">
  <env:Body>
    <env:Fault>
      <faultcode>env:Client</faultcode>
      <faultstring>oops</faultstring>
      <faultactor>it was me!</faultactor>
      <detail>
        <myDetail1>detail 1</myDetail1>
        <myDetail2>detail 2</myDetail2>
      </detail>
    </env:Fault>
    </env:Body>
</env:Envelope>";
            this.xmlTester.ArrangeExpectedXml(expectedXml);
        }

        [When(@"a SOAP envelope with the standard soap encoding is built")]
        public void WhenASOAPEnvelopeWithTheStandardSoapEncodingIsBuilt()
        {
            this.soapEnvelope = SoapBuilder.CreateSoap11Envelope()
                .WithBody.SetStandardSoapEncoding()
                .WithBody.AddEntry("<a>hello</a>")
                .Build()
                .ToString();

            this.xmlTester.ArrangeActualXml(this.soapEnvelope);

            var expectedXml =
@"<env:Envelope xmlns:env=""http://schemas.xmlsoap.org/soap/envelope/"">
  <env:Body env:soapEncoding=""http://schemas.xmlsoap.org/soap/encoding/"">
    <a>hello</a>
  </env:Body>
</env:Envelope>";
            this.xmlTester.ArrangeExpectedXml(expectedXml);
        }

        [When(@"a SOAP envelope with a customised soap encoding is built")]
        public void WhenASOAPEnvelopeWithACustomisedSoapEncodingIsBuilt()
        {
            this.soapEnvelope = SoapBuilder.CreateSoap11Envelope()
                .WithBody.SetCustomSoapEncoding("custom soap encoding namespace!!")
                .WithBody.AddEntry("<a>hello</a>")
                .Build()
                .ToString();

            this.xmlTester.ArrangeActualXml(this.soapEnvelope);

            var expectedXml =
@"<env:Envelope xmlns:env=""http://schemas.xmlsoap.org/soap/envelope/"">
  <env:Body env:soapEncoding=""custom soap encoding namespace!!"">
    <a>hello</a>
  </env:Body>
</env:Envelope>";
            this.xmlTester.ArrangeExpectedXml(expectedXml);
        }

        [When(@"more than one non-fault body entry is added")]
        public void WhenTheBodyIsSpecifiedTwice()
        {
            this.soapEnvelope = SoapBuilder.CreateSoap11Envelope()
                .WithBody.AddEntry("<a>body entry 1</a>")
                .WithBody.AddEntry("<b>body entry 2</b>")
                .Build()
                .ToString();

            this.xmlTester.ArrangeActualXml(this.soapEnvelope);

            var expectedXml =
@"<env:Envelope xmlns:env=""http://schemas.xmlsoap.org/soap/envelope/"">
  <env:Body>
    <a>body entry 1</a>
    <b>body entry 2</b>
  </env:Body>
</env:Envelope>";
            this.xmlTester.ArrangeExpectedXml(expectedXml);
        }

        [When(@"the body is specified twice as a fault")]
        public void WhenTheBodyIsSpecifiedTwiceAsAFault()
        {
            base.ExpectedException.MessageShouldContainText = "Cannot set a fault because the body already has an entry";

            Try(() =>
            {
                this.soapEnvelope = SoapBuilder.CreateSoap11Envelope()
                    .WithBody.SetFault(Soap11FaultCode.Client, "bummer1")
                    .WithBody.SetFault(Soap11FaultCode.Client, "bummer2")
                    .Build()
                    .ToString();
            });
        }

        [When(@"the body is specified twice - as a fault and then directly as a body entry")]
        public void WhenTheBodyIsSpecifiedTwiceAsAFaultAndThenDirectlyAsABodyEntry()
        {
            base.ExpectedException.MessageShouldContainText = "Cannot add the body entry because a fault has already been specified";

            Try(() =>
            {
                this.soapEnvelope = SoapBuilder.CreateSoap11Envelope()
                    .WithBody.SetFault(Soap11FaultCode.Client, "bummer1")
                    .WithBody.AddEntry("<b>this should throw an exception</b>")
                    .Build()
                    .ToString();
            });
        }

        [When(@"the body is specified twice - directly as a body entry and then as a fault")]
        public void WhenTheBodyIsSpecifiedTwice_DirectlyAsABodyEntryAndThenAsAFault()
        {
            base.ExpectedException.MessageShouldContainText = "Cannot set a fault because the body already has an entry";

            Try(() =>
            {
                this.soapEnvelope = SoapBuilder.CreateSoap11Envelope()
                    .WithBody.AddEntry("<a>body entry 1</a>")
                    .WithBody.SetFault(Soap11FaultCode.Client, "bummer1 - this will throw an exception")
                    .Build()
                    .ToString();
            });
        }

        [When(@"a SOAP envelope with a body entry from an object with no XML attribute is built")]
        public void WhenASOAPEnvelopeWithABodyEntryFromAnObjectWithNoXMLAttributeIsBuilt()
        {
            var obj = new XmlObjectWithNoAttribute
            {
                MyProperty = 2
            };

            this.soapEnvelope = SoapBuilder.CreateSoap11Envelope()
                .WithBody.AddEntry(obj)
                .Build()
                .ToString();

            this.xmlTester.ArrangeActualXml(this.soapEnvelope);

            var expectedXml =
@"<env:Envelope xmlns:env=""http://schemas.xmlsoap.org/soap/envelope/"">
  <env:Body>
    <XmlObjectWithNoAttribute>
        <MyProperty>2</MyProperty>
    </XmlObjectWithNoAttribute>
  </env:Body>
</env:Envelope>";
            this.xmlTester.ArrangeExpectedXml(expectedXml);
        }

        [When(@"a SOAP envelope with a body entry from an object with an XML Root attribute is built")]
        public void WhenASOAPEnvelopeWithABodyEntryFromAnObjectWithAnXMLRootAttributeIsBuilt()
        {
            var obj = new XmlObjectWithRootAttribute
            {
                MyProperty = 2
            };

            this.soapEnvelope = SoapBuilder.CreateSoap11Envelope()
                .WithBody.AddEntry(obj)
                .Build()
                .ToString();

            this.xmlTester.ArrangeActualXml(this.soapEnvelope);

            var expectedXml =
@"<env:Envelope xmlns:env=""http://schemas.xmlsoap.org/soap/envelope/"">
  <env:Body>
    <XmlObjectWithRootAttribute xmlns=""http://root.attribute"">
        <MyProperty>2</MyProperty>
    </XmlObjectWithRootAttribute>
  </env:Body>
</env:Envelope>";
            this.xmlTester.ArrangeExpectedXml(expectedXml);
        }

        [When(@"a SOAP envelope with a body entry from an object with an XML Type attribute is built")]
        public void WhenASOAPEnvelopeWithABodyEntryFromAnObjectWithAnXMLTypeAttributeIsBuilt()
        {
            var obj = new XmlObjectWithTypeAttribute
            {
                MyProperty = 2
            };

            this.soapEnvelope = SoapBuilder.CreateSoap11Envelope()
                .WithBody.AddEntry(obj)
                .Build()
                .ToString();

            this.xmlTester.ArrangeActualXml(this.soapEnvelope);

            var expectedXml =
@"<env:Envelope xmlns:env=""http://schemas.xmlsoap.org/soap/envelope/"">
  <env:Body>
    <XmlObjectWithTypeAttribute xmlns=""http://type.attribute"">
        <MyProperty>2</MyProperty>
    </XmlObjectWithTypeAttribute>
  </env:Body>
</env:Envelope>";
            this.xmlTester.ArrangeExpectedXml(expectedXml);
        }

        [When(@"a SOAP envelope with a body entry from an object with an XML Root attribute is built - with a given XML element name and blank namespace")]
        public void WhenASOAPEnvelopeWithABodyEntryFromAnObjectWithAnXMLRootAttributeIsBuilt_WithAGivenXMLElementNameAndBlankNamespace()
        {
            var obj = new XmlObjectWithRootAttribute
            {
                MyProperty = 2
            };

            this.soapEnvelope = SoapBuilder.CreateSoap11Envelope()
                .WithBody.AddEntry(obj, "NewElementName", null)
                .Build()
                .ToString();

            this.xmlTester.ArrangeActualXml(this.soapEnvelope);

            var expectedXml =
@"<env:Envelope xmlns:env=""http://schemas.xmlsoap.org/soap/envelope/"">
  <env:Body>
    <NewElementName xmlns=""http://root.attribute"">
        <MyProperty>2</MyProperty>
    </NewElementName>
  </env:Body>
</env:Envelope>";
            this.xmlTester.ArrangeExpectedXml(expectedXml);
        }

        [When(@"a SOAP envelope with a body entry from an object with an XML Root attribute is built - with a given XML element name and namespace")]
        public void WhenASOAPEnvelopeWithABodyEntryFromAnObjectWithAnXMLRootAttributeIsBuilt_WithAGivenXMLElementNameAndNamespace()
        {
            var obj = new XmlObjectWithRootAttribute
            {
                MyProperty = 2
            };

            this.soapEnvelope = SoapBuilder.CreateSoap11Envelope()
                .WithBody.AddEntry(obj, "NewElementName", "http://new.root.namespace")
                .Build()
                .ToString();

            this.xmlTester.ArrangeActualXml(this.soapEnvelope);

            var expectedXml =
@"<env:Envelope xmlns:env=""http://schemas.xmlsoap.org/soap/envelope/"">
  <env:Body>
    <NewElementName xmlns=""http://new.root.namespace"">
        <MyProperty>2</MyProperty>
    </NewElementName>
  </env:Body>
</env:Envelope>";
            this.xmlTester.ArrangeExpectedXml(expectedXml);
        }

        #region Header

        [When(@"a SOAP envelope with a header block from an object with no XML attribute is built")]
        public void WhenASOAPEnvelopeWithAHeaderBlockFromAnObjectWithNoXMLAttributeIsBuilt()
        {
            var obj = new XmlObjectWithNoAttribute
            {
                MyProperty = 2
            };

            this.soapEnvelope = SoapBuilder.CreateSoap11Envelope()
                .WithHeader.AddBlock(obj)
                .Build()
                .ToString();

            this.xmlTester.ArrangeActualXml(this.soapEnvelope);

            var expectedXml =
@"<env:Envelope xmlns:env=""http://schemas.xmlsoap.org/soap/envelope/"">
  <env:Header>
    <XmlObjectWithNoAttribute>
        <MyProperty>2</MyProperty>
    </XmlObjectWithNoAttribute>
  </env:Header>
</env:Envelope>";
            this.xmlTester.ArrangeExpectedXml(expectedXml);
        }

        [When(@"a SOAP envelope with a header block from an object with an XML Root attribute is built")]
        public void WhenASOAPEnvelopeWithAHeaderBlockFromAnObjectWithAnXMLRootAttributeIsBuilt()
        {
            var obj = new XmlObjectWithRootAttribute
            {
                MyProperty = 2
            };

            this.soapEnvelope = SoapBuilder.CreateSoap11Envelope()
                .WithHeader.AddBlock(obj)
                .Build()
                .ToString();

            this.xmlTester.ArrangeActualXml(this.soapEnvelope);

            var expectedXml =
@"<env:Envelope xmlns:env=""http://schemas.xmlsoap.org/soap/envelope/"">
  <env:Header>
    <XmlObjectWithRootAttribute xmlns=""http://root.attribute"">
        <MyProperty>2</MyProperty>
    </XmlObjectWithRootAttribute>
  </env:Header>
</env:Envelope>";
            this.xmlTester.ArrangeExpectedXml(expectedXml);
        }

        [When(@"a SOAP envelope with a header block from an object with an XML Type attribute is built")]
        public void WhenASOAPEnvelopeWithAHeaderBlockFromAnObjectWithAnXMLTypeAttributeIsBuilt()
        {
            var obj = new XmlObjectWithTypeAttribute
            {
                MyProperty = 2
            };

            this.soapEnvelope = SoapBuilder.CreateSoap11Envelope()
                .WithHeader.AddBlock(obj)
                .Build()
                .ToString();

            this.xmlTester.ArrangeActualXml(this.soapEnvelope);

            var expectedXml =
@"<env:Envelope xmlns:env=""http://schemas.xmlsoap.org/soap/envelope/"">
  <env:Header>
    <XmlObjectWithTypeAttribute xmlns=""http://type.attribute"">
        <MyProperty>2</MyProperty>
    </XmlObjectWithTypeAttribute>
  </env:Header>
</env:Envelope>";
            this.xmlTester.ArrangeExpectedXml(expectedXml);
        }

        [When(@"a SOAP envelope with a header block from an object with an XML Root attribute is built - with a given XML element name and blank namespace")]
        public void WhenASOAPEnvelopeWithAHeaderBlockFromAnObjectWithAnXMLRootAttributeIsBuilt_WithAGivenXMLElementNameAndBlankNamespace()
        {
            var obj = new XmlObjectWithRootAttribute
            {
                MyProperty = 2
            };

            this.soapEnvelope = SoapBuilder.CreateSoap11Envelope()
                .WithHeader.AddBlock(obj, "NewElementName", null)
                .Build()
                .ToString();

            this.xmlTester.ArrangeActualXml(this.soapEnvelope);

            var expectedXml =
@"<env:Envelope xmlns:env=""http://schemas.xmlsoap.org/soap/envelope/"">
  <env:Header>
    <NewElementName xmlns=""http://root.attribute"">
        <MyProperty>2</MyProperty>
    </NewElementName>
  </env:Header>
</env:Envelope>";
            this.xmlTester.ArrangeExpectedXml(expectedXml);
        }

        [When(@"a SOAP envelope with a header block from an object with an XML Root attribute is built - with a given XML element name and namespace")]
        public void WhenASOAPEnvelopeWithAHeaderBlockFromAnObjectWithAnXMLRootAttributeIsBuilt_WithAGivenXMLElementNameAndNamespace()
        {
            var obj = new XmlObjectWithRootAttribute
            {
                MyProperty = 2
            };

            this.soapEnvelope = SoapBuilder.CreateSoap11Envelope()
                .WithHeader.AddBlock(obj, "NewElementName", "http://new.root.namespace")
                .Build()
                .ToString();

            this.xmlTester.ArrangeActualXml(this.soapEnvelope);

            var expectedXml =
@"<env:Envelope xmlns:env=""http://schemas.xmlsoap.org/soap/envelope/"">
  <env:Header>
    <NewElementName xmlns=""http://new.root.namespace"">
        <MyProperty>2</MyProperty>
    </NewElementName>
  </env:Header>
</env:Envelope>";
            this.xmlTester.ArrangeExpectedXml(expectedXml);
        }

        [When(@"a SOAP envelope with a header block from an XML string is built")]
        public void WhenASOAPEnvelopeWithAHeaderBlockFromAnXMLStringIsBuilt()
        {
            var obj = new XmlObjectWithRootAttribute
            {
                MyProperty = 2
            };

            this.soapEnvelope = SoapBuilder.CreateSoap11Envelope()
                .WithHeader.AddBlock(ObjectXmlSerialiser.SerialiseObject(obj, null, null))
                .Build()
                .ToString();

            this.xmlTester.ArrangeActualXml(this.soapEnvelope);

            var expectedXml =
@"<env:Envelope xmlns:env=""http://schemas.xmlsoap.org/soap/envelope/"">
  <env:Header>
    <XmlObjectWithRootAttribute xmlns=""http://root.attribute"">
        <MyProperty>2</MyProperty>
    </XmlObjectWithRootAttribute>
  </env:Header>
</env:Envelope>";

            this.xmlTester.ArrangeExpectedXml(expectedXml);
        }

        [When(@"a SOAP envelope with a header block from an XContainer is built")]
        public void WhenASOAPEnvelopeWithAHeaderBlockFromAnXContainerIsBuilt()
        {
            var obj = new XmlObjectWithRootAttribute
            {
                MyProperty = 2
            };

            this.soapEnvelope = SoapBuilder.CreateSoap11Envelope()
                .WithHeader.AddBlock(XElement.Parse(ObjectXmlSerialiser.SerialiseObject(obj, null, null)))
                .Build()
                .ToString();

            this.xmlTester.ArrangeActualXml(this.soapEnvelope);

            var expectedXml =
@"<env:Envelope xmlns:env=""http://schemas.xmlsoap.org/soap/envelope/"">
  <env:Header>
    <XmlObjectWithRootAttribute xmlns=""http://root.attribute"">
        <MyProperty>2</MyProperty>
    </XmlObjectWithRootAttribute>
  </env:Header>
</env:Envelope>";

            this.xmlTester.ArrangeExpectedXml(expectedXml);
        }

        #endregion

        #endregion

        #region Then

        [Then(@"the SOAP envelope is correct")]
        public void ThenTheSOAPEnvelopeIsCorrect()
        {
            this.xmlTester.AssertActualXmlEqualsExpectedXml();
        }

        [Then(@"there is an error notification about the body being specified more than once")]
        public void ThenThereIsAnErrorNotificationAboutTheBodyBeingSpecifiedMoreThanOnce()
        {
            AssertExpectedException();
        }

        #endregion
    }
}
