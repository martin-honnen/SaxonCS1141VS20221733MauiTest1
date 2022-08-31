using Saxon.Api;
using System.ComponentModel;

namespace SaxonCS1141VS20221733MauiTest1.ViewModels
{
    public class SaxonViewModel : INotifyPropertyChanged
    {
        private readonly SaxonSingleton saxonModel = SaxonSingleton.Instance;

        public event PropertyChangedEventHandler PropertyChanged;

        public SaxonViewModel()
        {
            TestCurrentDateTimeValue = new Command(() => { XPathCurrentDateTimeTest = TestCurrentDateTime(); });
            TestXdmAtomicValue = new Command(() => { XdmAtomicValueTest = new XdmAtomicValue(DateTime.Now.ToLongTimeString()).StringValue; });
            TestCallTemplateTransformationResult = new Command(() => { XdmTransformationResult = RunTestCallTemplate(); });
        }

        public string SaxonProductTitle
        {
            get
            {
                return saxonModel.SaxonProcessor.ProductTitle;
            }
        }

        private string TestCurrentDateTime()
        {
            return saxonModel.SaxonProcessor.GetSystemFunction(new QName("http://www.w3.org/2005/xpath-functions", "current-dateTime"), 0).Invoke(new XdmValue[] { }, saxonModel.SaxonProcessor).ItemAt(0).StringValue;
        }

        private string currentDateTimeValue = "Not tested!";
        public string XPathCurrentDateTimeTest
        {
            get
            {
                return currentDateTimeValue;
            }
            set
            {
                if (currentDateTimeValue != value)
                {
                    currentDateTimeValue = value;
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(XPathCurrentDateTimeTest)));
                }
            }
        }

        private string xdmAtomicValue = "Not tested!";
        public string XdmAtomicValueTest
        {
            get
            {
                return xdmAtomicValue;
                ;
            }
            set
            {
                if (xdmAtomicValue != value)
                {
                    xdmAtomicValue = value;
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(XdmAtomicValueTest)));
                }
            }
        }

        private string xdmTransformationResult = "Not tested!";
        public string XdmTransformationResult
        {
            get
            {
                return xdmTransformationResult;
            }
            set
            {
                if (xdmTransformationResult != value)
                {
                    xdmTransformationResult = value;
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(XdmTransformationResult)));
                }
            }
        }

        public Command TestCurrentDateTimeValue { get; }
        public Command TestXdmAtomicValue { get; }
        public Command TestCallTemplateTransformationResult { get; }

        private string RunTestCallTemplate()
        {
            XsltCompiler xsltCompiler = saxonModel.SaxonProcessor.NewXsltCompiler();

            xsltCompiler.BaseUri = new Uri("urn:from-string");

            XsltExecutable xsltExecutable;

            string xsltCode = @"<xsl:stylesheet xmlns:xsl='http://www.w3.org/1999/XSL/Transform' version='3.0' expand-text='yes'>
  <xsl:template name='xsl:initial-template'>
    <Test>Run with {system-property('xsl:product-name')} {system-property('xsl:product-version')} at {current-dateTime()}</Test>
  </xsl:template>
</xsl:stylesheet>";

            using (var stringReader = new StringReader(xsltCode))
            {
                xsltExecutable = xsltCompiler.Compile(stringReader);
            }

            using (var resultWriter = new StringWriter())
            {
                var xslt30Transformer = xsltExecutable.Load30();
                xslt30Transformer.CallTemplate(null, saxonModel.SaxonProcessor.NewSerializer(resultWriter));
                return resultWriter.ToString();
            }
        }

    }
}
