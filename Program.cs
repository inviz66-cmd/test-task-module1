using System;
using System.Xml;
using System.Xml.Xsl;
using System.Xml.Linq;

class Program
{
    static void Main(string[] args)
    {
        string sourceXml = "Data1.xml";
        string xsltFile = "transform.xslt";
        string outputXml = "Employees.xml";

        // Шаг 1: XSLT преобразование
        XslCompiledTransform xslt = new XslCompiledTransform();
        xslt.Load(xsltFile);

        using (XmlReader reader = XmlReader.Create(sourceXml))
        using (XmlWriter writer = XmlWriter.Create(outputXml, xslt.OutputSettings))
        {
            xslt.Transform(reader, writer);
        }

        Console.WriteLine("XSLT transformation completed. Output: " + outputXml);

        // Шаг 2: расчет суммы salary для каждого Employee
        XDocument doc = XDocument.Load(outputXml);
        foreach (var employee in doc.Descendants("Employee"))
        {
            decimal sum = 0;
            foreach (var salary in employee.Elements("salary"))
            {
                var amountAttr = salary.Attribute("amount");
                if (amountAttr != null && decimal.TryParse(amountAttr.Value.Replace('.',','), out decimal val))
                {
                    sum += val;
                }
            }
            // Форматируем сумму с двумя знаками после запятой
            employee.SetAttributeValue("amount", sum.ToString("F2"));
        }
        doc.Save(outputXml);
        Console.WriteLine("Employee salary sums calculated. Output: " + outputXml);
    }
}
