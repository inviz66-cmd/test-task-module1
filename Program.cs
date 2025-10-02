using System;
using System.Xml;
using System.Xml.Xsl;

class Program
{
    static void Main(string[] args)
    {
        string sourceXml = "Data1.xml";
        string xsltFile = "transform.xslt";
        string outputXml = "Employees.xml";

        XslCompiledTransform xslt = new XslCompiledTransform();
        xslt.Load(xsltFile);

        using (XmlReader reader = XmlReader.Create(sourceXml))
        using (XmlWriter writer = XmlWriter.Create(outputXml, xslt.OutputSettings))
        {
            xslt.Transform(reader, writer);
        }

        Console.WriteLine("XSLT transformation completed. Output: " + outputXml);
    }
}
