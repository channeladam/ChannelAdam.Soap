Feature: MakingSoap11

Scenario: MakingSoap11 - 010 - Should create a well-formatted SOAP 1.1 envelope with a header
When a SOAP envelope with a header is built
Then the SOAP envelope is correct

Scenario: MakingSoap11 - 020 - Should create a well-formatted SOAP 1.1 envelope with a body
When a SOAP envelope with a body is built
Then the SOAP envelope is correct

Scenario: MakingSoap11 - 021 - Should create a well-formatted SOAP 1.1 envelope when more than one non-fault body entry is added
When more than one non-fault body entry is added
Then the SOAP envelope is correct

Scenario: MakingSoap11 - 030 - Should create a well-formatted SOAP 1.1 envelope with a header and body
When a SOAP envelope with a header and body is built
Then the SOAP envelope is correct

Scenario: MakingSoap11 - 040 - Should create a well-formatted SOAP 1.1 envelope with a fault
When a SOAP envelope with a fault is built
Then the SOAP envelope is correct

Scenario: MakingSoap11 - 050 - Should allow the standard SOAP Encoding namespace to be set
When a SOAP envelope with the standard soap encoding is built
Then the SOAP envelope is correct

Scenario: MakingSoap11 - 051 - Should allow the SOAP Encoding namespace to be customised
When a SOAP envelope with a customised soap encoding is built
Then the SOAP envelope is correct


Scenario: MakingSoap11 - 090 - Should error when the body is specified as a fault more than once
When the body is specified twice as a fault
Then there is an error notification about the body being specified more than once

Scenario: MakingSoap11 - 091 - Should error when the body is specified twice - as a fault and then directly as a body
When the body is specified twice - as a fault and then directly as a body entry
Then there is an error notification about the body being specified more than once

Scenario: MakingSoap11 - 092 - Should error when the body is specified twice - directly as a body and then as a fault
When the body is specified twice - directly as a body entry and then as a fault
Then there is an error notification about the body being specified more than once


Scenario: MakingSoap11 - 101 - Should make a SOAP body from an XML Object - with no XML attribute
When a SOAP envelope with a body entry from an object with no XML attribute is built
Then the SOAP envelope is correct

Scenario: MakingSoap11 - 102 - Should make a SOAP body from an XML Object - with an XML Root attribute
When a SOAP envelope with a body entry from an object with an XML Root attribute is built
Then the SOAP envelope is correct

Scenario: MakingSoap11 - 103 - Should make a SOAP body from an XML Object - with an XML Type attribute
When a SOAP envelope with a body entry from an object with an XML Type attribute is built
Then the SOAP envelope is correct

Scenario: MakingSoap11 - 104 - Should make a SOAP body from an XML Object - with an XML Root attribute - using the given XML element name and existing root attribute namespace
When a SOAP envelope with a body entry from an object with an XML Root attribute is built - with a given XML element name and blank namespace
Then the SOAP envelope is correct

Scenario: MakingSoap11 - 105 - Should make a SOAP body from an XML Object - with an XML Root attribute - using the given XML element name and given element namespace
When a SOAP envelope with a body entry from an object with an XML Root attribute is built - with a given XML element name and namespace
Then the SOAP envelope is correct
