using System;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Xml;
using System.Xml.Xsl;

namespace SalaryApp
{
    public partial class Form1 : Form
    {
        private Button runButton;
        private ListBox employeeList;
        private Label totalLabel;

        public Form1()
        {
            InitializeComponent();

            this.Text = "Salary App";
            this.Width = 600;
            this.Height = 400;

            runButton = new Button() { Text = "Запустить", Top = 10, Left = 10, Width = 100 };
            runButton.Click += RunButton_Click;

            employeeList = new ListBox() { Top = 50, Left = 10, Width = 550, Height = 250 };

            totalLabel = new Label() { Top = 320, Left = 10, Width = 550, Height = 30, Font = new System.Drawing.Font("Segoe UI", 12) };

            this.Controls.Add(runButton);
            this.Controls.Add(employeeList);
            this.Controls.Add(totalLabel);
        }

        private void RunButton_Click(object sender, EventArgs e)
        {
            string sourceXml = "../Data1.xml";
            string xsltFile = "../transform.xslt";
            string outputXml = "../Employees.xml";

            // Шаг 1: XSLT преобразование
            XslCompiledTransform xslt = new XslCompiledTransform();
            xslt.Load(xsltFile);

            using (XmlReader reader = XmlReader.Create(sourceXml))
            using (XmlWriter writer = XmlWriter.Create(outputXml, xslt.OutputSettings))
            {
                xslt.Transform(reader, writer);
            }

            // Шаг 2: расчет суммы salary для каждого Employee
            XDocument doc = XDocument.Load(outputXml);
            foreach (var employee in doc.Descendants("Employee"))
            {
                decimal sum = employee.Elements("salary")
                                      .Select(s => decimal.Parse(s.Attribute("amount").Value.Replace('.', ',')))
                                      .Sum();
                employee.SetAttributeValue("amount", sum.ToString("F2"));
            }
            doc.Save(outputXml);

            // Шаг 3: расчет общей суммы
            decimal totalSum = doc.Descendants("Employee")
                                  .Select(e => decimal.Parse(e.Attribute("amount").Value.Replace('.', ',')))
                                  .Sum();

            // Шаг 4: вывод на экран
            employeeList.Items.Clear();
            foreach (var emp in doc.Descendants("Employee"))
            {
                string name = emp.Attribute("name").Value;
                string surname = emp.Attribute("surname").Value;
                string amount = emp.Attribute("amount").Value;
                employeeList.Items.Add($"{name} {surname}: {amount}");
            }

            totalLabel.Text = $"Общая сумма выплат: {totalSum:F2}";
        }
    }
}
