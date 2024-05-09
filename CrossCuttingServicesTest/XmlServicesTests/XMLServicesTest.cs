using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using NUnit.Framework;
using Sra.P2rmis.CrossCuttingServices;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestMethod = NUnit.Framework.TestAttribute;
using TestCleanup = NUnit.Framework.TearDownAttribute;
using TestInitialize = NUnit.Framework.SetUpAttribute;
using ClassCleanup = NUnit.Framework.TestFixtureTearDownAttribute;
using ClassInitialize = NUnit.Framework.TestFixtureSetUpAttribute;

namespace XMLServicesTest
{
    /// <summary>
    ///This is a test class for XMLServicesTest and is intended
    ///to contain all XMLServicesTest Unit Tests
    ///</summary>
   [TestClass()]
   public class XMLServicesTest
   {
       #region Test Classes for Deserialization Tests
       public class Product
       {
           public int prodID;
           public string name;
           public Product() { }
           public Product(int ID, string prodName)
           {
               prodID = ID;
               name = prodName;
           }
           public static bool operator == (Product p1, Product p2)
           {
               if ((p1.prodID == p2.prodID) && (p1.name == p2.name))
                   return true;
               else
                    return false;
           }
           public static bool operator !=(Product p1, Product p2)
           {
               return !(p1 == p2);
           }
           public override bool Equals(object obj)
           {
               return ((prodID == ((Product)obj).prodID) && (name == ((Product)obj).name));
           }
           public override int GetHashCode()
           {
               return base.GetHashCode();
           }
       }

       public class Order
       {
           public int orderID;
           public Product[] prods;
           public override bool Equals(object obj)
           {
               if (this.orderID != ((Order)obj).orderID)
                   return false;

               for (int i=0; i<=this.prods.Length-1; i++)
               {
                   if (!(this.prods[i].Equals(((Order)obj).prods[i])))
                       return false;
               }
               return true;
           }
           public override int GetHashCode()
           {
               return base.GetHashCode();
           }
       }
       #endregion

       #region Test Data for XML block
       private string xmlBlock = @"<?xml version=""1.0"" encoding=""utf-8"" ?><bookstore xmlns=""http://www.contoso.com/books"">" +
 @"<book genre=""autobiography"" publicationdate=""1981-03-22"" ISBN=""1-861003-11-0"">" +
     @"<title>The Autobiography of Benjamin Franklin</title><author>" +
         @"<first-name>Benjamin</first-name><last-name>Franklin</last-name></author>" +
     @"<price>8.99</price></book>" +
 @"<book genre=""novel"" publicationdate=""1967-11-17"" ISBN=""0-201-63361-2"">" +
     @"<title>The Confidence Man</title><author><first-name>Herman</first-name><last-name>Melville</last-name>" +
     @"</author><price>11.99</price></book>" +
 @"<book genre=""philosophy"" publicationdate=""1991-02-15"" ISBN=""1-861001-57-6"">" +
     @"<title>The Gorgias</title><author><name>Plato</name></author><price>9.99</price></book>" +
 @"<book genre=""autobiography"" publicationdate=""1987-05-26"" ISBN=""1-259693-19-7"">" +
     @"<title>The Autobiography of Rosa Parks</title><author>" +
         @"<first-name>Rosa</first-name><last-name>Parks</last-name></author>" +
     @"<price>6.99</price></book></bookstore>";


       private string xmlBlockWithLeafAttr = @"<?xml version=""1.0"" encoding=""utf-8"" ?><bookstore xmlns=""http://www.contoso.com/books"">" +
 @"<book genre=""autobiography"" publicationdate=""1981-03-22"" ISBN=""1-861003-11-0"">" +
     @"<title>The Autobiography of Benjamin Franklin</title><author>" +
         @"<first-name>Benjamin</first-name><last-name>Franklin</last-name></author>" +
     @"<price>8.99</price></book>" +
 @"<book genre=""novel"" publicationdate=""1967-11-17"" ISBN=""0-201-63361-2"">" +
     @"<title>The Confidence Man</title><author><first-name>Herman</first-name><last-name>Melville</last-name>" +
     @"</author><price>11.99</price></book>" +
 @"<book genre=""philosophy"" publicationdate=""1991-02-15"" ISBN=""1-861001-57-6"">" +
     @"<title>The Gorgias</title><author><name>Plato</name></author><price currency=""dollar"">9.99</price></book></bookstore>";

       private string xmlBlockWithBookValue = @"<?xml version=""1.0"" encoding=""utf-8"" ?><bookstore xmlns=""http://www.contoso.com/books"">" +
 @"<book genre=""autobiography"" publicationdate=""1981-03-22"" ISBN=""1-861003-11-0"">" +
     @"<title>The Autobiography of Benjamin Franklin</title><author>" +
         @"<first-name>Benjamin</first-name><last-name>Franklin</last-name></author>" +
     @"<price>8.99</price></book>" +
 @"<book genre=""novel"" publicationdate=""1967-11-17"" ISBN=""0-201-63361-2"">" +
     @"TestBookValue</book>" +
 @"<book genre=""philosophy"" publicationdate=""1991-02-15"" ISBN=""1-861001-57-6"">" +
     @"<title>The Gorgias</title><author><name>Plato</name></author><price>9.99</price></book>" +
@"<book genre=""novel"" publicationdate=""1967-12-22"" ISBN=""0-202-43321-9"">" +
     @"TestBookValue</book></bookstore>";

       #endregion

       #region Test XML Schemas
       private string xmlGoodSchema = @"<?xml version=""1.0"" encoding=""utf-8""?>" +
@"<xs:schema attributeFormDefault=""unqualified"" elementFormDefault=""qualified"" targetNamespace=""http://www.contoso.com/books"" xmlns:xs=""http://www.w3.org/2001/XMLSchema"">" +
@"<xs:element name=""bookstore"">" +
@"<xs:complexType><xs:sequence><xs:element maxOccurs=""unbounded"" name=""book"">" +
          @"<xs:complexType><xs:sequence><xs:element name=""title"" type=""xs:string"" />" +
                  @"<xs:element name=""author""><xs:complexType><xs:sequence>" +
                              @"<xs:element minOccurs=""0"" name=""name"" type=""xs:string"" />" +
                              @"<xs:element minOccurs=""0"" name=""first-name"" type=""xs:string"" />" +
                              @"<xs:element minOccurs=""0"" name=""last-name"" type=""xs:string"" />" +
                          @"</xs:sequence></xs:complexType></xs:element><xs:element name=""price"" type=""xs:decimal"" />" +
              @"</xs:sequence><xs:attribute name=""genre"" type=""xs:string"" use=""required"" />" +
              @"<xs:attribute name=""publicationdate"" type=""xs:date"" use=""required"" />" +
              @"<xs:attribute name=""ISBN"" type=""xs:string"" use=""required"" />" +
          @"</xs:complexType></xs:element></xs:sequence></xs:complexType></xs:element></xs:schema>";

       private string xmlBadSchema = @"<?xml version=""1.0"" encoding=""utf-8""?>" +
@"<xs:schema attributeFormDefault=""unqualified"" elementFormDefault=""qualified"" targetNamespace=""http://www.contoso.com/books"" xmlns:xs=""http://www.w3.org/2001/XMLSchema"">" +
@"<xs:element name=""bookstore"">" +
@"<xs:complexType><xs:sequence><xs:element maxOccurs=""unbounded"" name=""book"">" +
          @"<xs:complexType><xs:sequence><xs:element name=""title"" type=""xs:string"" />" +
                  @"<xs:element name=""author""><xs:complexType><xs:sequence>" +
                              @"<xs:element minOccurs=""1"" name=""name"" type=""xs:string"" />" +  // make <name> a required element so XML will not be valid
                              @"<xs:element minOccurs=""0"" name=""first-name"" type=""xs:string"" />" +
                              @"<xs:element minOccurs=""0"" name=""last-name"" type=""xs:string"" />" +
                          @"</xs:sequence></xs:complexType></xs:element><xs:element name=""price"" type=""xs:decimal"" />" +
              @"</xs:sequence><xs:attribute name=""genre"" type=""xs:string"" use=""required"" />" +
              @"<xs:attribute name=""publicationdate"" type=""xs:date"" use=""required"" />" +
              @"<xs:attribute name=""ISBN"" type=""xs:string"" use=""required"" />" +
          @"</xs:complexType></xs:element></xs:sequence></xs:complexType></xs:element></xs:schema>";
       #endregion

       private XNamespace nameSpace = "http://www.contoso.com/books";

       private TestContext testContextInstance;

      /// <summary>
      ///Gets or sets the test context which provides
      ///information about and functionality for the current test run.
      ///</summary>
      public TestContext TestContext
      {
         get
         {
            return testContextInstance;
         }
         set
         {
            testContextInstance = value;
         }
      }

      #region Additional test attributes
      // 
      //You can use the following additional attributes as you write your tests:
      //
      //Use ClassInitialize to run code before running the first test in the class
      //[ClassInitialize()]
      //public static void MyClassInitialize(TestContext testContext)
      //{
      //}
      //
      //Use ClassCleanup to run code after all tests in a class have run
      //[ClassCleanup()]
      //public static void MyClassCleanup()
      //{
      //}
      //
      //Use TestInitialize to run code before running each test
      //[TestInitialize()]
      //public void MyTestInitialize()
      //{
      //}
      //
      //Use TestCleanup to run code after each test has run
      //[TestCleanup()]
      //public void MyTestCleanup()
      //{
      //}
      //
      #endregion



#region ValidateAgainstSchema Tests
      /// <summary>
      ///A test for ValidateAgainstSchema using a valid schema and XML document
      ///</summary>
      [TestMethod()]
      public void ValidateAgainstSchemaTest()
      {
          bool expected = true;
          bool actual;
          actual = XMLServices.ValidateAgainstSchema(xmlBlock, xmlGoodSchema, "http://www.contoso.com/books");
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for ValidateAgainstSchema using an invalid XML file
      ///</summary>
      [TestMethod()]
      public void ValidateAgainstSchemaTest1()
      {
          bool expected = false;
          bool actual;
          actual = XMLServices.ValidateAgainstSchema(xmlBlock, xmlBadSchema, "http://www.contoso.com/books");
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for ValidateAgainstSchema.  Using a targetNamespace that doesn't match
      ///</summary>
      [TestMethod()]
      public void ValidateAgainstSchemaTest2()
      {
          string targetNamespace = string.Empty;
          bool expected = false; 
          bool actual;
          actual = XMLServices.ValidateAgainstSchema(xmlBlock, xmlGoodSchema, targetNamespace);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for ValidateAgainstSchema with non-well-formed XML
      ///</summary>
      [TestMethod()]
      public void ValidateAgainstSchemaTest3()
      {
          string xmlBlock = "<stuff>this is stuff";
          string xmlSchema = string.Empty; 
          string targetNamespace = string.Empty; 
          bool expected = false; 
          bool actual;
          actual = XMLServices.ValidateAgainstSchema(xmlBlock, xmlSchema, targetNamespace);
          Assert.AreEqual(expected, actual);
      }
#endregion

#region DeserializeToObject Tests
      /// <summary>
      ///A test for DeserializeToObject
      ///</summary>
      [TestMethod()]
      public void DeserializeToObjectTest()
      {
          Product p = new Product(1, "Item 1");
          Product q = new Product(2, "Item 2");
          Order o = new Order();
          o.orderID = 1;
          o.prods = new Product[2] { p, q };
          XmlSerializer ser = new XmlSerializer(p.GetType());
          StringWriter sw = new StringWriter();
          XmlWriter xw = XmlWriter.Create(sw);
          ser.Serialize(xw, p);
          string xmlBlock = sw.ToString();
          Product expected = p;
          Product result = (Product)XMLServices.DeserializeToObject(xmlBlock, p.GetType());
          Assert.IsNotNull(result);
          Assert.IsTrue(result.Equals(expected));
      }

      /// <summary>
      ///A test for DeserializeToObject with a non-well-formed XML block
      ///</summary>
      [TestMethod()]
      public void DeserializeToObjectTest1()
      {
          string xmlBlock = "<stuff>This is stuff";
          Type t = typeof(Product);
          object actual;
          actual = XMLServices.DeserializeToObject(xmlBlock, t);
          Assert.IsNull(actual);
      }

      /// <summary>
      ///A test for DeserializeToObject for non-matching objects
      ///</summary>
      [TestMethod()]
      public void DeserializeToObjectTest2()
      {
          Product p = new Product(1, "Item 1");
          Product q = new Product(2, "Item 2");
          XmlSerializer ser = new XmlSerializer(p.GetType());
          StringWriter sw = new StringWriter();
          XmlWriter xw = XmlWriter.Create(sw);
          ser.Serialize(xw, q);
          string xmlBlock = sw.ToString();
          Product expected = p;
          Product result = (Product)XMLServices.DeserializeToObject(xmlBlock, q.GetType());
          Assert.IsNotNull(result);
          Assert.IsFalse(result.Equals(expected));
      }

      /// <summary>
      ///A test for DeserializeToObject with an array in the object
      ///</summary>
      [TestMethod()]
      public void DeserializeToObjectTest3()
      {
          Product p = new Product(1, "Item 1");
          Product q = new Product(2, "Item 2");
          Order o = new Order();
          o.orderID = 1;
          o.prods = new Product[2] { p, q };
          XmlSerializer ser = new XmlSerializer(o.GetType());
          StringWriter sw = new StringWriter();
          XmlWriter xw = XmlWriter.Create(sw);
          ser.Serialize(xw, o);
          string xmlBlock = sw.ToString();
          Order expected = o;
          Order result = (Order)XMLServices.DeserializeToObject(xmlBlock, o.GetType());
          Assert.IsNotNull(result);
          Assert.IsTrue(result.Equals(expected));
      }
#endregion

#region GetElementValue Tests
      /// <summary>
      ///A test for GetElementValue
      ///</summary>
      [TestMethod()]
      public void GetElementValueTest()
      {
          string elementName = "title";
          string expected = "The Autobiography of Benjamin Franklin";
          string actual = XMLServices.GetElementValue(xmlBlock, elementName);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementValue
      ///</summary>
      [TestMethod()]
      public void GetElementValueTest1()
      {
          string elementName = "titl"; // misspelled on purpose
          string expected = string.Empty; 
          string actual = XMLServices.GetElementValue(xmlBlock, elementName);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementValue
      ///</summary>
      [TestMethod()]
      public void GetElementValueTest2()
      {
          string xmlBlock = "<invalidXML>stuff";
          string elementName = "titl"; // misspelled on purpose
          string expected = string.Empty;
          string actual = XMLServices.GetElementValue(xmlBlock, elementName);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementValue
      ///</summary>
      [TestMethod()]
      public void GetElementValueTest2a()
      {
          string elementName = string.Empty;
          string expected = string.Empty;
          string actual = XMLServices.GetElementValue(xmlBlock, elementName);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementValue
      ///</summary>
      [TestMethod()]
      public void GetElementValueTest3()
      {
          string elementName = "book";
          string attrName = "genre";
          string attrValue = "novel";
          string expected = "TestBookValue";
          string actual = XMLServices.GetElementValue(xmlBlockWithBookValue, elementName, attrName, attrValue);
          Assert.AreEqual(expected, actual);
      }

       /// <summary>
      ///A test for GetElementValue
      ///</summary>
      [TestMethod()]
      public void GetElementValueTest4()
      {
          string elementName = "book";
          string attrName = "general"; // bad attribute value
          string attrValue = string.Empty;
          string expected = string.Empty;
          string actual = XMLServices.GetElementValue(xmlBlockWithBookValue, elementName, attrName, attrValue);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementValue
      ///</summary>
      [TestMethod()]
      public void GetElementValueTest5()
      {
          string elementName = "cashier";  // missing element name
          string attrName = "genre";
          string attrValue = "novel";
          string expected = string.Empty;
          string actual = XMLServices.GetElementValue(xmlBlockWithBookValue, elementName, attrName, attrValue);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementValue
      ///</summary>
      [TestMethod()]
      public void GetElementValueTest6()
      {
          string elementName = "book";
          string attrName = "genre";
          string attrValue = "sports";  // bad attribute value
          string expected = string.Empty;
          string actual = XMLServices.GetElementValue(xmlBlockWithBookValue, elementName, attrName, attrValue);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementValue
      ///</summary>
      [TestMethod()]
      public void GetElementValueTest7()
      {
          string xmlBlock = "<malformedXML>Bad XML";
          string elementName = "book";
          string attrName = "genre";
          string attrValue = "novel";  
          string expected = string.Empty;
          string actual = XMLServices.GetElementValue(xmlBlock, elementName, attrName, attrValue);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementValue
      ///</summary>
      [TestMethod()]
      public void GetElementValueTest8()
      {
          string elementName = string.Empty;
          string attrName = "genre";
          string attrValue = "novel";
          string expected = string.Empty;
          string actual = XMLServices.GetElementValue(xmlBlockWithBookValue, elementName, attrName, attrValue);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementValue
      ///</summary>
      [TestMethod()]
      public void GetElementValueTest9()
      {
          string elementName = "book";
          string attrName = string.Empty;
          string attrValue = "novel";
          string expected = string.Empty;
          string actual = XMLServices.GetElementValue(xmlBlockWithBookValue, elementName, attrName, attrValue);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementValue
      ///</summary>
      [TestMethod()]
      public void GetElementValueTest10()
      {
          string elementName = "title";
          int n = 3;
          string expected = "The Gorgias";
          string actual = XMLServices.GetElementValue(xmlBlock, elementName, n);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementValue
      ///</summary>
      [TestMethod()]
      public void GetElementValueTest11()
      {
          string elementName = "title";
          int n = 0;  // value too low
          string expected = string.Empty;
          string actual = XMLServices.GetElementValue(xmlBlock, elementName, n);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementValue
      ///</summary>
      [TestMethod()]
      public void GetElementValueTest12()
      {
          string elementName = "title";
          int n = 5;    // value too high
          string expected = string.Empty;
          string actual = XMLServices.GetElementValue(xmlBlock, elementName, n);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementValue
      ///</summary>
      [TestMethod()]
      public void GetElementValueTest13()
      {
          string elementName = "book";
          string attrName = "genre";
          string attrValue = "novel";
          int n = 1;
          string expected = "TestBookValue";
          string actual = XMLServices.GetElementValue(xmlBlockWithBookValue, elementName, attrName, attrValue, n);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementValue
      ///</summary>
      [TestMethod()]
      public void GetElementValueTest14()
      {
          string elementName = "book";
          string attrName = "genre";
          string attrValue = "novel";
          int n = 0;  // value too low
          string expected = string.Empty;
          string actual = XMLServices.GetElementValue(xmlBlockWithBookValue, elementName, attrName, attrValue, n);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementValue
      ///</summary>
      [TestMethod()]
      public void GetElementValueTest15()
      {
          string elementName = "book";
          string attrName = "genre";
          string attrValue = "novel";
          int n = 3;  // value too high
          string expected = string.Empty;
          string actual = XMLServices.GetElementValue(xmlBlockWithBookValue, elementName, attrName, attrValue, n);
          Assert.AreEqual(expected, actual);
      }
#endregion

#region GetAttributeValue Tests
      /// <summary>
      ///A test for GetAttributeValue
      ///</summary>
      [TestMethod()]
      public void GetAttributeValueTest()
      {
          string elementName = "book";
          string attrName = "ISBN";
          string expected = "1-861003-11-0";
          string actual = XMLServices.GetAttributeValue(xmlBlock, elementName, attrName);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetAttributeValue
      ///</summary>
      [TestMethod()]
      public void GetAttributeValueTest1()
      {
          string xmlBlock = "<malformedXML>stuff";
          string elementName = "dummy";
          string attrName = "dummy";
          string expected = string.Empty;
          string actual = XMLServices.GetAttributeValue(xmlBlock, elementName, attrName);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetAttributeValue with missing attribute
      ///</summary>
      [TestMethod()]
      public void GetAttributeValueTest2()
      {
          string elementName = "book";
          string attrName = "test";
          string expected = string.Empty; 
          string actual = XMLServices.GetAttributeValue(xmlBlock, elementName, attrName);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetAttributeValue with missing attribute
      ///</summary>
      [TestMethod()]
      public void GetAttributeValueTest2a()
      {
          string elementName = string.Empty;
          string attrName = "test";
          string expected = string.Empty; 
          string actual = XMLServices.GetAttributeValue(xmlBlock, elementName, attrName);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetAttributeValue with missing attribute
      ///</summary>
      [TestMethod()]
      public void GetAttributeValueTest2b()
      {
          string elementName = "book";
          string attrName = null;
          string expected = string.Empty; 
          string actual = XMLServices.GetAttributeValue(xmlBlock, elementName, attrName);
          Assert.AreEqual(expected, actual);
      }

       /// <summary>
      ///A test for GetAttributeValue
      ///</summary>
      [TestMethod()]
      public void GetAttributeValueTest3()
      {
          string elementName = "book";
          string elementValue = "TestBookValue";
          string attrName = "ISBN";
          string expected = "0-201-63361-2";
          string actual = XMLServices.GetAttributeValue(xmlBlockWithBookValue, elementName, elementValue, attrName);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetAttributeValue
      ///</summary>
      [TestMethod()]
      public void GetAttributeValueTest4()
      {
          string xmlBlock = "<malformedXML>stuff";
          string elementName = "dummy";
          string elementValue = "dummy";
          string attrName = "dummy";
          string expected = string.Empty;
          string actual = XMLServices.GetAttributeValue(xmlBlock, elementName, elementValue, attrName);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetAttributeValue with missing attribute
      ///</summary>
      [TestMethod()]
      public void GetAttributeValueTest5()
      {
          string elementName = "book";
          string elementValue = "TestBookValue";
          string attrName = "test";
          string expected = string.Empty; 
          string actual = XMLServices.GetAttributeValue(xmlBlock, elementName, elementValue, attrName);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetAttributeValue with missing attribute
      ///</summary>
      [TestMethod()]
      public void GetAttributeValueTest6()
      {
          string elementName = null;
          string elementValue = "TestBookValue";
          string attrName = "test";
          string expected = string.Empty; 
          string actual = XMLServices.GetAttributeValue(xmlBlock, elementName, elementValue, attrName);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetAttributeValue with missing attribute
      ///</summary>
      [TestMethod()]
      public void GetAttributeValueTest7()
      {
          string elementName = "book";
          string elementValue = "TestBookValue";
          string attrName = null;
          string expected = string.Empty; 
          string actual = XMLServices.GetAttributeValue(xmlBlock, elementName, elementValue, attrName);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetAttributeValue 
      ///</summary>
      [TestMethod()]
      public void GetAttributeValueTest8()
      {
          string elementName = "book";
          string attrName = "genre";
          int n = 2;
          string expected = "novel";
          string actual = XMLServices.GetAttributeValue(xmlBlock, elementName, attrName, n);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetAttributeValue 
      ///</summary>
      [TestMethod()]
      public void GetAttributeValueTest9()
      {
          string elementName = "book";
          string attrName = "genre";
          int n = 0;  // too low
          string expected = string.Empty;
          string actual = XMLServices.GetAttributeValue(xmlBlock, elementName, attrName, n);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetAttributeValue 
      ///</summary>
      [TestMethod()]
      public void GetAttributeValueTest10()
      {
          string elementName = "book";
          string attrName = "genre";
          int n = 5;  // too high
          string expected = string.Empty;
          string actual = XMLServices.GetAttributeValue(xmlBlock, elementName, attrName, n);
          Assert.AreEqual(expected, actual);
      }
#endregion

#region GetElementBlock Tests
      /// <summary>
      ///A test for GetElementBlock
      ///</summary>
      [TestMethod()]
      public void GetElementBlockTest()
      {
          string elementName = "book";
          string attrName = "publicationdate";
          string attrValue = "1991-02-15";
          string expected = new XElement(nameSpace + "book",
                                new XAttribute("genre", "philosophy"), new XAttribute("publicationdate", "1991-02-15"),
                                new XAttribute("ISBN", "1-861001-57-6"), new XElement(nameSpace + "title", "The Gorgias"),
                                new XElement(nameSpace + "author", new XElement(nameSpace + "name", "Plato")),
                                new XElement(nameSpace + "price", "9.99")).ToString();
          string actual = XMLServices.GetElementBlock(xmlBlock, elementName, attrName, attrValue);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementBlock
      ///</summary>
      [TestMethod()]
      public void GetElementBlockTest1()
      {
          string xmlBlock = "<malformedXML>stuff";
          string elementName = "book";
          string attrName = "publicationdate";
          string attrValue = "1991-02-15";
          string expected = string.Empty;
          string actual = XMLServices.GetElementBlock(xmlBlock, elementName, attrName, attrValue);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementBlock
      ///</summary>
      [TestMethod()]
      public void GetElementBlockTest2()
      {
          string elementName = "bok";    // incorrect element name
          string attrName = "publicationdate";
          string attrValue = "1991-02-15";
          string expected = string.Empty;
          string actual = XMLServices.GetElementBlock(xmlBlock, elementName, attrName, attrValue);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementBlock
      ///</summary>
      [TestMethod()]
      public void GetElementBlockTest3()
      {
          string elementName = "book";
          string attrName = "publicationDate";      // incorrect attribute name
          string attrValue = "1991-02-15";
          string expected = string.Empty;
          string actual = XMLServices.GetElementBlock(xmlBlock, elementName, attrName, attrValue);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementBlock
      ///</summary>
      [TestMethod()]
      public void GetElementBlockTest4()
      {
          string elementName = "book";
          string attrName = "publicationDate";
          string attrValue = "1991-02-15";        // incorrect attribute value
          string expected = string.Empty;
          string actual = XMLServices.GetElementBlock(xmlBlock, elementName, attrName, attrValue);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementBlock
      ///</summary>
      [TestMethod()]
      public void GetElementBlockTest4a()
      {
          string elementName = string.Empty;
          string attrName = "publicationDate";
          string attrValue = "1991-02-15";        // incorrect attribute value
          string expected = string.Empty;
          string actual = XMLServices.GetElementBlock(xmlBlock, elementName, attrName, attrValue);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementBlock
      ///</summary>
      [TestMethod()]
      public void GetElementBlockTest4b()
      {
          string elementName = "book";
          string attrName = "genre";
          string attrValue = "autobiography";      
          int n = 2;
          string expected = new XElement(nameSpace + "book", new XAttribute("genre", "autobiography"), 
                            new XAttribute("publicationdate", "1987-05-26"),
                            new XAttribute("ISBN", "1-259693-19-7"), 
                            new XElement(nameSpace + "title", "The Autobiography of Rosa Parks"), 
                            new XElement(nameSpace + "author", new XElement(nameSpace + "first-name", "Rosa"), 
                            new XElement(nameSpace + "last-name", "Parks")), 
                            new XElement(nameSpace + "price", "6.99")).ToString();
          string actual = XMLServices.GetElementBlock(xmlBlock, elementName, attrName, attrValue, n);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementBlock
      ///</summary>
      [TestMethod()]
      public void GetElementBlockTest4c()
      {
          string elementName = "book";
          string attrName = "genre";
          string attrValue = "autobiography";       
          int n = 6;  //  too high
          string expected = string.Empty;
          string actual = XMLServices.GetElementBlock(xmlBlock, elementName, attrName, attrValue, n);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementBlock
      ///</summary>
      [TestMethod()]
      public void GetElementBlockTest4d()
      {
          string elementName = "book";
          string attrName = "genre";
          string attrValue = "autobiography";   
          int n = 0;   // too low
          string expected = string.Empty;
          string actual = XMLServices.GetElementBlock(xmlBlock, elementName, attrName, attrValue, n);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementBlock
      ///</summary>
      [TestMethod()]
      public void GetElementBlockTest5()
      {
          string elementName = "book";
          string elementValue = "TestBookValue";
          string expected = new XElement(nameSpace + "book", new XAttribute("genre", "novel"),
              new XAttribute("publicationdate", "1967-11-17"), new XAttribute("ISBN", "0-201-63361-2"),
              "TestBookValue").ToString();
          string actual = XMLServices.GetElementBlock(xmlBlockWithBookValue, elementName, elementValue);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementBlock
      ///</summary>
      [TestMethod()]
      public void GetElementBlockTest5a()
      {
          string elementName = "book";
          string elementValue = "TestBookValue";
          int n = 2;
          string expected = new XElement(nameSpace + "book", new XAttribute("genre", "novel"),
              new XAttribute("publicationdate", "1967-12-22"), new XAttribute("ISBN", "0-202-43321-9"),
              "TestBookValue").ToString();
          string actual = XMLServices.GetElementBlock(xmlBlockWithBookValue, elementName, elementValue, n);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementBlock
      ///</summary>
      [TestMethod()]
      public void GetElementBlockTest5b()
      {
          string elementName = "book";
          string elementValue = "AnotherTestBookValue";
          int n = 6;  // too high
          string expected = string.Empty;
          string actual = XMLServices.GetElementBlock(xmlBlockWithBookValue, elementName, elementValue, n);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementBlock
      ///</summary>
      [TestMethod()]
      public void GetElementBlockTest5c()
      {
          string elementName = "book";
          string elementValue = "AnotherTestBookValue";
          int n = 0; // too low
          string expected = string.Empty;
          string actual = XMLServices.GetElementBlock(xmlBlockWithBookValue, elementName, elementValue, n);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementBlock
      ///</summary>
      [TestMethod()]
      public void GetElementBlockTest6()
      {
          string elementName = "bok";  // invalid element name
          string elementValue = "TestBookValue";
          string expected = string.Empty;
          string actual = XMLServices.GetElementBlock(xmlBlockWithBookValue, elementName, elementValue);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementBlock
      ///</summary>
      [TestMethod()]
      public void GetElementBlockTest6a()
      {
          string elementName = string.Empty;
          string elementValue = "TestBookValue";
          string expected = string.Empty;
          string actual = XMLServices.GetElementBlock(xmlBlockWithBookValue, elementName, elementValue);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementBlock
      ///</summary>
      [TestMethod()]
      public void GetElementBlockTest7()
      {
          string elementName = "book";
          string elementValue = "TestValue";    // invalid element value
          string expected = string.Empty;
          string actual = XMLServices.GetElementBlock(xmlBlockWithBookValue, elementName, elementValue);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementBlock
      ///</summary>
      [TestMethod()]
      public void GetElementBlockTest8()
      {
          string elementName = "author";
          string expected = new XElement(nameSpace + "author", new XElement(nameSpace + "first-name", "Benjamin"),
                                new XElement(nameSpace + "last-name", "Franklin")).ToString();
          string actual = XMLServices.GetElementBlock(xmlBlock, elementName);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementBlock
      ///</summary>
      [TestMethod()]
      public void GetElementBlockTest8a()
      {
          string elementName = "author";
          int n = 4;
          string expected = new XElement(nameSpace + "author", new XElement(nameSpace + "first-name", "Rosa"),
                                new XElement(nameSpace + "last-name", "Parks")).ToString();
          string actual = XMLServices.GetElementBlock(xmlBlock, elementName, n);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementBlock
      ///</summary>
      [TestMethod()]
      public void GetElementBlockTest8b()
      {
          string elementName = "author";
          int n = 5;   // too high
          string expected = string.Empty;
          string actual = XMLServices.GetElementBlock(xmlBlock, elementName, n);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementBlock
      ///</summary>
      [TestMethod()]
      public void GetElementBlockTest8c()
      {
          string elementName = "author";
          int n = 0;  // too low
          string expected = string.Empty;
          string actual = XMLServices.GetElementBlock(xmlBlock, elementName, n);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementBlock
      ///</summary>
      [TestMethod()]
      public void GetElementBlockTest9()
      {
          string xmlBlock = "<malformedXML>stuff";
          string elementName = "author";
          string expected = string.Empty;
          string actual = XMLServices.GetElementBlock(xmlBlock, elementName);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementBlock
      ///</summary>
      [TestMethod()]
      public void GetElementBlockTest10()
      {
          string elementName = "publisher";  // invalid element name
          string expected = string.Empty;
          string actual = XMLServices.GetElementBlock(xmlBlock, elementName);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementBlock
      ///</summary>
      [TestMethod()]
      public void GetElementBlockTest10a()
      {
          string elementName = null;
          string expected = string.Empty;
          string actual = XMLServices.GetElementBlock(xmlBlock, elementName);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementBlock
      ///</summary>
      [TestMethod()]
      public void GetElementBlockTest11()
      {
          string elementName = "book";
          string elementValue = "TestBookValue";
          string attrName = "genre";
          string attrValue = "novel";
          string expected = new XElement(nameSpace + "book", new XAttribute("genre", "novel"),
              new XAttribute("publicationdate", "1967-11-17"), new XAttribute("ISBN", "0-201-63361-2"),
              "TestBookValue").ToString();
          string actual = XMLServices.GetElementBlock(xmlBlockWithBookValue, elementName, elementValue, attrName, attrValue);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementBlock
      ///</summary>
      [TestMethod()]
      public void GetElementBlockTest12()
      {
          string xmlBlock = "<malformedXML>stuff";
          string elementName = "book";
          string elementValue = "TestBookValue";
          string attrName = "genre";
          string attrValue = "novel";
          string expected = string.Empty;
          string actual = XMLServices.GetElementBlock(xmlBlock, elementName, elementValue, attrName, attrValue);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementBlock
      ///</summary>
      [TestMethod()]
      public void GetElementBlockTest13()
      {
          string elementName = "bok";  // invalid element name
          string elementValue = "TestBookValue";
          string attrName = "genre";
          string attrValue = "novel";
          string expected = string.Empty;
          string actual = XMLServices.GetElementBlock(xmlBlockWithBookValue, elementName, elementValue, attrName, attrValue);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementBlock
      ///</summary>
      [TestMethod()]
      public void GetElementBlockTest14()
      {
          string elementName = "book";
          string elementValue = "TestValue";     // invalid element value
          string attrName = "genre";
          string attrValue = "novel";
          string expected = string.Empty;
          string actual = XMLServices.GetElementBlock(xmlBlockWithBookValue, elementName, elementValue, attrName, attrValue);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementBlock
      ///</summary>
      [TestMethod()]
      public void GetElementBlockTest15()
      {
          string elementName = "book";
          string elementValue = "TestBookValue";
          string attrName = "releaseyear";        // invalid attribute name
          string attrValue = "novel";
          string expected = string.Empty;
          string actual = XMLServices.GetElementBlock(xmlBlockWithBookValue, elementName, elementValue, attrName, attrValue);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementBlock
      ///</summary>
      [TestMethod()]
      public void GetElementBlockTest16()
      {
          string elementName = "book";
          string elementValue = "TestBookValue";
          string attrName = "genre";
          string attrValue = "novella";         // invalid attribute value
          string expected = string.Empty;
          string actual = XMLServices.GetElementBlock(xmlBlockWithBookValue, elementName, elementValue, attrName, attrValue);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementBlock
      ///</summary>
      [TestMethod()]
      public void GetElementBlockTest17()
      {
          string elementName = "book";
          string elementValue = "TestBookValue";
          string attrName = "genre";
          string attrValue = "novel";
          int n = 2;
          string expected = new XElement(nameSpace + "book", new XAttribute("genre", "novel"), 
                            new XAttribute("publicationdate", "1967-12-22"), new XAttribute("ISBN", "0-202-43321-9"),
                            "TestBookValue").ToString();
          string actual = XMLServices.GetElementBlock(xmlBlockWithBookValue, elementName, elementValue, attrName, attrValue, n);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementBlock
      ///</summary>
      [TestMethod()]
      public void GetElementBlockTest18()
      {
          string elementName = "book";
          string elementValue = "TestBookValue";
          string attrName = "genre";
          string attrValue = "novel";
          int n = 0;   // too low
          string expected = string.Empty;
          string actual = XMLServices.GetElementBlock(xmlBlockWithBookValue, elementName, elementValue, attrName, attrValue, n);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementBlock
      ///</summary>
      [TestMethod()]
      public void GetElementBlockTest19()
      {
          string elementName = "book";
          string elementValue = "TestBookValue";
          string attrName = "genre";
          string attrValue = "novel";
          int n = 5;   // too high
          string expected = string.Empty;
          string actual = XMLServices.GetElementBlock(xmlBlockWithBookValue, elementName, elementValue, attrName, attrValue, n);
          Assert.AreEqual(expected, actual);
      }
#endregion

#region GetElementDescendants Tests
      /// <summary>
      ///A test for GetElementDescendants
      ///</summary>
      [TestMethod()]
      public void GetElementDescendantsTest()
      {
          string elementName = "book";
          string expected = new XElement(nameSpace + "title", "The Autobiography of Benjamin Franklin").ToString();
          expected += new XElement(nameSpace + "author", new XElement(nameSpace + "first-name", "Benjamin"),
                                                 new XElement(nameSpace + "last-name", "Franklin")).ToString();
          expected += new XElement(nameSpace + "price", "8.99").ToString();
          string actual = XMLServices.GetElementDescendants(xmlBlock, elementName);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementDescendants
      ///</summary>
      [TestMethod()]
      public void GetElementDescendantsTest1()
      {
          string xmlBlock = "<malformedXML>stuff";
          string elementName = "book";
          string expected = string.Empty;
          string actual = XMLServices.GetElementDescendants(xmlBlock, elementName);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementDescendants
      ///</summary>
      [TestMethod()]
      public void GetElementDescendantsTest2()
      {
          string elementName = "shelf";  // invalid element name
          string expected = string.Empty;
          string actual = XMLServices.GetElementDescendants(xmlBlock, elementName);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementDescendants
      ///</summary>
      [TestMethod()]
      public void GetElementDescendantsTest3()
      {
          string elementName = "price";  // element with no children
          string expected = string.Empty;
          string actual = XMLServices.GetElementDescendants(xmlBlock, elementName);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementDescendants
      ///</summary>
      [TestMethod()]
      public void GetElementDescendantsTest3a()
      {
          string elementName = null;
          string expected = string.Empty;
          string actual = XMLServices.GetElementDescendants(xmlBlock, elementName);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementDescendants
      ///</summary>
      [TestMethod()]
      public void GetElementDescendantsTest3b()
      {
          string elementName = "book";
          int n = 2;
          string expected = new XElement(nameSpace + "title", "The Confidence Man").ToString();
          expected += new XElement(nameSpace + "author", new XElement(nameSpace + "first-name", "Herman"), 
                      new XElement(nameSpace + "last-name", "Melville")).ToString();
          expected += new XElement(nameSpace + "price", "11.99").ToString();
          string actual = XMLServices.GetElementDescendants(xmlBlock, elementName, n);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementDescendants
      ///</summary>
      [TestMethod()]
      public void GetElementDescendantsTest3c()
      {
          string elementName = "book";
          int n = 0;  // too low
          string expected = string.Empty;
          string actual = XMLServices.GetElementDescendants(xmlBlock, elementName, n);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementDescendants
      ///</summary>
      [TestMethod()]
      public void GetElementDescendantsTest3d()
      {
          string elementName = "book";
          int n = 7;  // too high
          string expected = string.Empty;
          string actual = XMLServices.GetElementDescendants(xmlBlock, elementName, n);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementDescendants
      ///</summary>
      [TestMethod()]
      public void GetElementDescendantsTest4()
      {
          string elementName = "book";
          string attrName = "genre";
          string attrValue = "philosophy";
          string expected = new XElement(nameSpace + "title", "The Gorgias").ToString() + 
                            new XElement(nameSpace + "author", new XElement(nameSpace + "name", "Plato")).ToString() + 
                            new XElement(nameSpace + "price", "9.99");
          string actual = XMLServices.GetElementDescendants(xmlBlock, elementName, attrName, attrValue);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementDescendants
      ///</summary>
      [TestMethod()]
      public void GetElementDescendantsTest5()
      {
          string xmlBlock = "<malformedXML>stuff";
          string elementName = "book";
          string attrName = "genre";
          string attrValue = "philosophy";
          string expected = string.Empty;
          string actual = XMLServices.GetElementDescendants(xmlBlock, elementName, attrName, attrValue);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementDescendants
      ///</summary>
      [TestMethod()]
      public void GetElementDescendantsTest6()
      {
          string elementName = "shelf";  // invalid element name
          string attrName = "genre";
          string attrValue = "philosophy";
          string expected = string.Empty;
          string actual = XMLServices.GetElementDescendants(xmlBlock, elementName, attrName, attrValue);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementDescendants
      ///</summary>
      [TestMethod()]
      public void GetElementDescendantsTest7()
      {
          string elementName = "price";  // no available child elements
          string attrName = "currency";
          string attrValue = "dollar";
          string expected = string.Empty;
          string actual = XMLServices.GetElementDescendants(xmlBlockWithLeafAttr, elementName, attrName, attrValue);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementDescendants
      ///</summary>
      [TestMethod()]
      public void GetElementDescendantsTest8()
      {
          string elementName = string.Empty;
          string attrName = "genre";
          string attrValue = "philosophy";
          string expected = string.Empty;
          string actual = XMLServices.GetElementDescendants(xmlBlock, elementName, attrName, attrValue);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementDescendants
      ///</summary>
      [TestMethod()]
      public void GetElementDescendantsTest9()
      {
          string elementName = "shelf";  // invalid element name
          string attrName = string.Empty;
          string attrValue = "philosophy";
          string expected = string.Empty;
          string actual = XMLServices.GetElementDescendants(xmlBlock, elementName, attrName, attrValue);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementDescendants
      ///</summary>
      [TestMethod()]
      public void GetElementDescendantsTest10()
      {
          string elementName = "book";
          string attrName = "genre";
          string attrValue = "autobiography";
          int n = 2;
          string expected = new XElement(nameSpace + "title", "The Autobiography of Rosa Parks").ToString() +
                            new XElement(nameSpace + "author", new XElement(nameSpace + "first-name", "Rosa"), 
                               new XElement(nameSpace + "last-name", "Parks")).ToString() +
                            new XElement(nameSpace + "price", "6.99").ToString();
          string actual = XMLServices.GetElementDescendants(xmlBlock, elementName, attrName, attrValue, n);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementDescendants
      ///</summary>
      [TestMethod()]
      public void GetElementDescendantsTest11()
      {
          string elementName = "book";
          string attrName = "genre";
          string attrValue = "autobiography";
          int n = 0; // too low
          string expected = string.Empty;
          string actual = XMLServices.GetElementDescendants(xmlBlock, elementName, attrName, attrValue, n);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementDescendants
      ///</summary>
      [TestMethod()]
      public void GetElementDescendantsTest12()
      {
          string elementName = "book";
          string attrName = "genre";
          string attrValue = "autobiography";
          int n = 5; // too high
          string expected = string.Empty;
          string actual = XMLServices.GetElementDescendants(xmlBlock, elementName, attrName, attrValue, n);
          Assert.AreEqual(expected, actual);
      }
#endregion

#region GetElementParent Tests
      /// <summary>
      ///A test for GetElementParent
      ///</summary>
      [TestMethod()]
      public void GetElementParentTest()
      {
          string elementName = "book";
          string expected = "bookstore";
          string actual = XMLServices.GetElementParent(xmlBlock, elementName);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementParent
      ///</summary>
      [TestMethod()]
      public void GetElementParentTest1()
      {
          string xmlBlock = "<malformedXML>stuff";
          string elementName = "malformedXML";
          string expected = string.Empty;
          string actual = XMLServices.GetElementParent(xmlBlock, elementName);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementParent
      ///</summary>
      [TestMethod()]
      public void GetElementParentTest2()
      {
          string elementName = "shelf";  // invalid element name
          string expected = string.Empty;
          string actual = XMLServices.GetElementParent(xmlBlock, elementName);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementParent
      ///</summary>
      [TestMethod()]
      public void GetElementParentTest3()
      {
          string elementName = string.Empty;  // empty element name
          string expected = string.Empty;
          string actual = XMLServices.GetElementParent(xmlBlock, elementName);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementParent
      ///</summary>
      [TestMethod()]
      public void GetElementParentTest4()
      {
          string elementName = "book";
          string attrName = "genre";
          string attrValue = "autobiography";
          string expected = "bookstore";
          string actual = XMLServices.GetElementParent(xmlBlock, elementName, attrName, attrValue);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementParent
      ///</summary>
      [TestMethod()]
      public void GetElementParentTest5()
      {
          string elementName = null;
          string attrName = "genre";
          string attrValue = "autobiography";
          string expected = string.Empty;
          string actual = XMLServices.GetElementParent(xmlBlock, elementName, attrName, attrValue);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementParent
      ///</summary>
      [TestMethod()]
      public void GetElementParentTest6()
      {
          string elementName = "book";
          string attrName = "numpages";   // invalid attribute name
          string attrValue = "autobiography";
          string expected = string.Empty;
          string actual = XMLServices.GetElementParent(xmlBlock, elementName, attrName, attrValue);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementParent
      ///</summary>
      [TestMethod()]
      public void GetElementParentTest7()
      {
          string elementName = "book";
          string attrName = "genre";
          string attrValue = "sports";   // invalid attribute value
          string expected = string.Empty;
          string actual = XMLServices.GetElementParent(xmlBlock, elementName, attrName, attrValue);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementParent
      ///</summary>
      [TestMethod()]
      public void GetElementParentTest8()
      {
          string elementName = "book";
          string elementValue = "TestBookValue";
          string attrName = "publicationdate";
          string attrValue = "1967-11-17";
          string expected = "bookstore";
          string actual = XMLServices.GetElementParent(xmlBlockWithBookValue, elementName, elementValue, attrName, attrValue);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementParent
      ///</summary>
      [TestMethod()]
      public void GetElementParentTest9()
      {
          string elementName = "shelf";   // invalid element name
          string elementValue = "TestBookValue";
          string attrName = "publicationdate";
          string attrValue = "1967-11-17";
          string expected = string.Empty;
          string actual = XMLServices.GetElementParent(xmlBlockWithBookValue, elementName, elementValue, attrName, attrValue);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementParent
      ///</summary>
      [TestMethod()]
      public void GetElementParentTest10()
      {
          string elementName = "book";
          string elementValue = "TestBook";   // invalid element value
          string attrName = "publicationdate";
          string attrValue = "1967-11-17";
          string expected = string.Empty;
          string actual = XMLServices.GetElementParent(xmlBlockWithBookValue, elementName, elementValue, attrName, attrValue);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementParent
      ///</summary>
      [TestMethod()]
      public void GetElementParentTest11()
      {
          string elementName = "book";
          string elementValue = "TestBookValue";
          string attrName = "publisher";   // invalid attribute name
          string attrValue = "1967-11-17";
          string expected = string.Empty;
          string actual = XMLServices.GetElementParent(xmlBlockWithBookValue, elementName, elementValue, attrName, attrValue);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementParent
      ///</summary>
      [TestMethod()]
      public void GetElementParentTest12()
      {
          string elementName = "book";
          string elementValue = "TestBookValue";
          string attrName = "publicationdate";
          string attrValue = "1968-11-17";   // invalid attribute value
          string expected = string.Empty;
          string actual = XMLServices.GetElementParent(xmlBlockWithBookValue, elementName, elementValue, attrName, attrValue);
          Assert.AreEqual(expected, actual);
      }
#endregion

#region GetElementPreviousSibling Tests
      /// <summary>
      ///A test for GetElementPreviousSibling
      ///</summary>
      [TestMethod()]
      public void GetElementPreviousSiblingTest()
      {
          string elementName = "author";
          string expected = new XElement(nameSpace + "title", "The Autobiography of Benjamin Franklin").ToString();
          string actual = XMLServices.GetElementPreviousSibling(xmlBlock, elementName);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementPreviousSibling
      ///</summary>
      [TestMethod()]
      public void GetElementPreviousSiblingTest1()
      {
          string elementName = "book";  // first instance of "book" has no previous sibling
          string expected = string.Empty;
          string actual = XMLServices.GetElementPreviousSibling(xmlBlock, elementName);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementPreviousSibling
      ///</summary>
      [TestMethod()]
      public void GetElementPreviousSiblingTest2()
      {
          string elementName = "shelf";  // invalid element name
          string expected = string.Empty;
          string actual = XMLServices.GetElementPreviousSibling(xmlBlock, elementName);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementPreviousSibling
      ///</summary>
      [TestMethod()]
      public void GetElementPreviousSiblingTest3()
      {
          string elementName = "author";
          int n = 2;
          string expected = new XElement(nameSpace + "title", "The Confidence Man").ToString();
          string actual = XMLServices.GetElementPreviousSibling(xmlBlock, elementName, n);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementPreviousSibling
      ///</summary>
      [TestMethod()]
      public void GetElementPreviousSiblingTest4()
      {
          string elementName = "shelf";  // invalid element name
          int n = 1;
          string expected = string.Empty;
          string actual = XMLServices.GetElementPreviousSibling(xmlBlock, elementName, n);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementPreviousSibling
      ///</summary>
      [TestMethod()]
      public void GetElementPreviousSiblingTest5()
      {
          string elementName = "author"; 
          int n = 0;  // invalid index, too low
          string expected = string.Empty;
          string actual = XMLServices.GetElementPreviousSibling(xmlBlock, elementName, n);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementPreviousSibling
      ///</summary>
      [TestMethod()]
      public void GetElementPreviousSiblingTest6()
      {
          string elementName = "author";
          int n = 10;  // invalid index, too high
          string expected = string.Empty;
          string actual = XMLServices.GetElementPreviousSibling(xmlBlock, elementName, n);
          Assert.AreEqual(expected, actual);
      }

       /// <summary>
      ///A test for GetElementPreviousSibling
      ///</summary>
      [TestMethod()]
      public void GetElementPreviousSiblingTest7()
      {
          string elementName = "book";
          string elementValue = "TestBookValue";
          string expected = new XElement(nameSpace + "book", new XAttribute("genre", "autobiography"),
                                    new XAttribute("publicationdate", "1981-03-22"),
                                    new XAttribute("ISBN", "1-861003-11-0"),
                                new XElement(nameSpace + "title", "The Autobiography of Benjamin Franklin"),
                                new XElement(nameSpace + "author", new XElement(nameSpace + "first-name", "Benjamin"),
                                    new XElement(nameSpace + "last-name", "Franklin")),
                                new XElement(nameSpace + "price", "8.99")).ToString();
          string actual = XMLServices.GetElementPreviousSibling(xmlBlockWithBookValue, elementName, elementValue);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementPreviousSibling
      ///</summary>
      [TestMethod()]
      public void GetElementPreviousSiblingTest8()
      {
          string elementName = "shelf";
          string elementValue = "TestBookValue";
          string expected = string.Empty;
          string actual = XMLServices.GetElementPreviousSibling(xmlBlockWithBookValue, elementName, elementValue);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementPreviousSibling
      ///</summary>
      [TestMethod()]
      public void GetElementPreviousSiblingTest9()
      {
          string elementName = "book";
          string elementValue = "NonExistentTestBookValue";
          string expected = string.Empty;
          string actual = XMLServices.GetElementPreviousSibling(xmlBlockWithBookValue, elementName, elementValue);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementPreviousSibling
      ///</summary>
      [TestMethod()]
      public void GetElementPreviousSiblingTest10()
      {
          string elementName = null;
          string elementValue = "TestBookValue";
          string expected = string.Empty;
          string actual = XMLServices.GetElementPreviousSibling(xmlBlockWithBookValue, elementName, elementValue);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementPreviousSibling
      ///</summary>
      [TestMethod()]
      public void GetElementPreviousSiblingTest11()
      {
          XDocument doc = XDocument.Load(new StringReader(xmlBlockWithBookValue));
          doc.Root.Descendants().First().AddBeforeSelf(new XElement(nameSpace + "book", "NoPreviousSibling"));
          string testXmlBlock = doc.ToString();
          string elementName = "book";
          string elementValue = "NoPreviousSibling";
          string expected = string.Empty;
          string actual = XMLServices.GetElementPreviousSibling(testXmlBlock, elementName, elementValue);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementPreviousSibling
      ///</summary>
      [TestMethod()]
      public void GetElementPreviousSiblingTest12()
      {
          string elementName = "book";
          string elementValue = "TestBookValue";
          int n = 2;
          string expected = new XElement(nameSpace + "book", new XAttribute("genre", "philosophy"),
                                new XAttribute("publicationdate", "1991-02-15"),
                                new XAttribute("ISBN", "1-861001-57-6"),
                            new XElement(nameSpace + "title", "The Gorgias"),
                            new XElement(nameSpace + "author", new XElement(nameSpace + "name", "Plato")),
                            new XElement(nameSpace + "price", "9.99")).ToString();
          string actual = XMLServices.GetElementPreviousSibling(xmlBlockWithBookValue, elementName, elementValue, n);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementPreviousSibling
      ///</summary>
      [TestMethod()]
      public void GetElementPreviousSiblingTest13()
      {
          string elementName = "book";
          string attrName = "ISBN";
          string attrValue = "1-259693-19-7";
          string expected = new XElement(nameSpace + "book", new XAttribute("genre", "philosophy"),
                                new XAttribute("publicationdate", "1991-02-15"),
                                new XAttribute("ISBN", "1-861001-57-6"),
                            new XElement(nameSpace + "title", "The Gorgias"),
                            new XElement(nameSpace + "author", new XElement(nameSpace + "name", "Plato")),
                            new XElement(nameSpace + "price", "9.99")).ToString();
          string actual = XMLServices.GetElementPreviousSibling(xmlBlock, elementName, attrName, attrValue);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementPreviousSibling
      ///</summary>
      [TestMethod()]
      public void GetElementPreviousSiblingTest14()
      {
          string elementName = "shelf";  // invalid element name
          string attrName = "ISBN";
          string attrValue = "1-259693-19-7";
          string expected = string.Empty;
          string actual = XMLServices.GetElementPreviousSibling(xmlBlock, elementName, attrName, attrValue);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementPreviousSibling
      ///</summary>
      [TestMethod()]
      public void GetElementPreviousSiblingTest15()
      {
          string elementName = "book";
          string attrName = "dedicated-to";  // invalid attribute name
          string attrValue = "1-259693-19-7";
          string expected = string.Empty;
          string actual = XMLServices.GetElementPreviousSibling(xmlBlock, elementName, attrName, attrValue);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementPreviousSibling
      ///</summary>
      [TestMethod()]
      public void GetElementPreviousSiblingTest16()
      {
          string elementName = "book";
          string attrName = "ISBN";
          string attrValue = "0-000000-19-7";  // invalid attribute value
          string expected = string.Empty;
          string actual = XMLServices.GetElementPreviousSibling(xmlBlock, elementName, attrName, attrValue);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementPreviousSibling
      ///</summary>
      [TestMethod()]
      public void GetElementPreviousSiblingTest17()
      {
          XDocument doc = XDocument.Load(new StringReader(xmlBlock));
          XElement newEl = new XElement(nameSpace + "book", 
                            new XAttribute("genre", "novel"),
                            new XAttribute("publicationdate", "1967-11-17"),
                            new XAttribute("ISBN", "0-201-63361-2"),
                            new XElement(nameSpace + "title", "First Confidence Man"),
                            new XElement(nameSpace + "author", new XElement(nameSpace + "first-name", "Herman"),
                                                   new XElement(nameSpace + "last-name", "Melville")),
                            new XElement(nameSpace + "price", "11.99"));

          var query = from element in doc.Descendants(nameSpace + "book")
                        where element.Attribute("genre").Value.ToString() == "novel"
                        select element;
          query.First().AddBeforeSelf(newEl);
          string testXmlBlock = doc.ToString();
          string elementName = "book";
          string attrName = "genre";
          string attrValue = "novel";
          int n = 2;
          string expected = newEl.ToString();
          string actual = XMLServices.GetElementPreviousSibling(testXmlBlock, elementName, attrName, attrValue, n);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementPreviousSibling
      ///</summary>
      [TestMethod()]
      public void GetElementPreviousSiblingTest18()
      {
          string elementName = "shelf";  // invalid element name
          string attrName = "genre";
          string attrValue = "novel";
          int n = 2;
          string expected = string.Empty;
          string actual = XMLServices.GetElementPreviousSibling(xmlBlock, elementName, attrName, attrValue, n);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementPreviousSibling
      ///</summary>
      [TestMethod()]
      public void GetElementPreviousSiblingTest19()
      {
          string elementName = "book";
          string attrName = "spine-color";  // invalid attribute name
          string attrValue = "novel";
          int n = 2;
          string expected = string.Empty;
          string actual = XMLServices.GetElementPreviousSibling(xmlBlock, elementName, attrName, attrValue, n);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementPreviousSibling
      ///</summary>
      [TestMethod()]
      public void GetElementPreviousSiblingTest20()
      {
          string elementName = "book";
          string attrName = "genre";
          string attrValue = "reference";  // invalid attribute value
          int n = 2;
          string expected = string.Empty;
          string actual = XMLServices.GetElementPreviousSibling(xmlBlock, elementName, attrName, attrValue, n);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementPreviousSibling
      ///</summary>
      [TestMethod()]
      public void GetElementPreviousSiblingTest21()
      {
          string elementName = "book";
          string attrName = "genre";
          string attrValue = "novel";
          int n = 0;  // invalid value for n -- too low
          string expected = string.Empty;
          string actual = XMLServices.GetElementPreviousSibling(xmlBlock, elementName, attrName, attrValue, n);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementPreviousSibling
      ///</summary>
      [TestMethod()]
      public void GetElementPreviousSiblingTest22()
      {
          string elementName = "book";
          string attrName = "genre";
          string attrValue = "novel";
          int n = 7;  // invalid value for n -- too high
          string expected = string.Empty;
          string actual = XMLServices.GetElementPreviousSibling(xmlBlock, elementName, attrName, attrValue, n);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementPreviousSibling
      ///</summary>
      [TestMethod()]
      public void GetElementPreviousSiblingTest23()
      {
          string elementName = "book";
          string attrName = "genre";
          string attrValue = "autobiography";
          int n = 1;  
          string expected = string.Empty;   // element exists, but has no previous sibling
          string actual = XMLServices.GetElementPreviousSibling(xmlBlock, elementName, attrName, attrValue, n);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementPreviousSibling
      ///</summary>
      [TestMethod()]
      public void GetElementPreviousSiblingTest24()
      {
          string elementName = "book";
          string elementValue = "TestBookValue";
          string attrName = "genre";
          string attrValue = "novel";
          string expected = new XElement(nameSpace + "book", new XAttribute("genre", "autobiography"),
                                    new XAttribute("publicationdate", "1981-03-22"),
                                    new XAttribute("ISBN", "1-861003-11-0"),
                                new XElement(nameSpace + "title", "The Autobiography of Benjamin Franklin"),
                                new XElement(nameSpace + "author", new XElement(nameSpace + "first-name", "Benjamin"),
                                    new XElement(nameSpace + "last-name", "Franklin")),
                                new XElement(nameSpace + "price", "8.99")).ToString();
          string actual = XMLServices.GetElementPreviousSibling(xmlBlockWithBookValue, elementName, elementValue, attrName, attrValue);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementPreviousSibling
      ///</summary>
      [TestMethod()]
      public void GetElementPreviousSiblingTest25()
      {
          string elementName = "magazine";  // invalid element name
          string elementValue = "TestBookValue";
          string attrName = "genre";
          string attrValue = "novel";
          string expected = string.Empty;
          string actual = XMLServices.GetElementPreviousSibling(xmlBlockWithBookValue, elementName, elementValue, attrName, attrValue);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementPreviousSibling
      ///</summary>
      [TestMethod()]
      public void GetElementPreviousSiblingTest26()
      {
          string elementName = "book";
          string elementValue = "eBook";    // invalid element value
          string attrName = "genre";
          string attrValue = "novel";
          string expected = string.Empty;
          string actual = XMLServices.GetElementPreviousSibling(xmlBlockWithBookValue, elementName, elementValue, attrName, attrValue);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementPreviousSibling
      ///</summary>
      [TestMethod()]
      public void GetElementPreviousSiblingTest27()
      {
          string elementName = "book";
          string elementValue = "TestBookValue";
          string attrName = "publisher";  // invalid attribute name
          string attrValue = "novel";
          string expected = string.Empty;
          string actual = XMLServices.GetElementPreviousSibling(xmlBlockWithBookValue, elementName, elementValue, attrName, attrValue);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementPreviousSibling
      ///</summary>
      [TestMethod()]
      public void GetElementPreviousSiblingTest28()
      {
          string elementName = "book";
          string elementValue = "TestBookValue";
          string attrName = "genre";
          string attrValue = "fiction";    // invalid attribute value
          string expected = string.Empty;
          string actual = XMLServices.GetElementPreviousSibling(xmlBlockWithBookValue, elementName, elementValue, attrName, attrValue);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementPreviousSibling
      ///</summary>
      [TestMethod()]
      public void GetElementPreviousSiblingTest29()
      {
          string elementName = "book";
          string elementValue = "TestBookValue";
          string attrName = "genre";
          string attrValue = "novel";
          int n = 2;
          string expected = new XElement(nameSpace + "book",
                                new XAttribute("genre", "philosophy"), new XAttribute("publicationdate", "1991-02-15"),
                                new XAttribute("ISBN", "1-861001-57-6"), new XElement(nameSpace + "title", "The Gorgias"),
                                new XElement(nameSpace + "author", new XElement(nameSpace + "name", "Plato")),
                                new XElement(nameSpace + "price", "9.99")).ToString();
          string actual = XMLServices.GetElementPreviousSibling(xmlBlockWithBookValue, elementName, elementValue, attrName, attrValue, n);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementPreviousSibling
      ///</summary>
      [TestMethod()]
      public void GetElementPreviousSiblingTest30()
      {
          string elementName = "library";  // invalid element name
          string elementValue = "TestBookValue";
          string attrName = "genre";
          string attrValue = "novel";
          int n = 1;
          string expected = string.Empty;
          string actual = XMLServices.GetElementPreviousSibling(xmlBlockWithBookValue, elementName, elementValue, attrName, attrValue, n);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementPreviousSibling
      ///</summary>
      [TestMethod()]
      public void GetElementPreviousSiblingTest31()
      {
          string elementName = "book";
          string elementValue = "AnotherTestBookValue";  // invalid element value
          string attrName = "genre";
          string attrValue = "novel";
          int n = 1;
          string expected = string.Empty;
          string actual = XMLServices.GetElementPreviousSibling(xmlBlockWithBookValue, elementName, elementValue, attrName, attrValue, n);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementPreviousSibling
      ///</summary>
      [TestMethod()]
      public void GetElementPreviousSiblingTest32()
      {
          string elementName = "book";
          string elementValue = "TestBookValue";
          string attrName = "color";  // invalid attribute name
          string attrValue = "novel";
          int n = 1;
          string expected = string.Empty;
          string actual = XMLServices.GetElementPreviousSibling(xmlBlockWithBookValue, elementName, elementValue, attrName, attrValue, n);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementPreviousSibling
      ///</summary>
      [TestMethod()]
      public void GetElementPreviousSiblingTest33()
      {
          string elementName = "book";
          string elementValue = "TestBookValue";
          string attrName = "genre";
          string attrValue = "non-fiction";    // invalid attribute value
          int n = 1;
          string expected = string.Empty;
          string actual = XMLServices.GetElementPreviousSibling(xmlBlockWithBookValue, elementName, elementValue, attrName, attrValue, n);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementPreviousSibling
      ///</summary>
      [TestMethod()]
      public void GetElementPreviousSiblingTest34()
      {
          string elementName = "book";
          string elementValue = "TestBookValue";
          string attrName = "genre";
          string attrValue = "novel";
          int n = 0;    // invalid value for n -- too low
          string expected = string.Empty;
          string actual = XMLServices.GetElementPreviousSibling(xmlBlockWithBookValue, elementName, elementValue, attrName, attrValue, n);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementPreviousSibling
      ///</summary>
      [TestMethod()]
      public void GetElementPreviousSiblingTest35()
      {
          string elementName = "book";
          string elementValue = "TestBookValue";
          string attrName = "genre";
          string attrValue = "novel";
          int n = 10;    // invalid value for n -- too high
          string expected = string.Empty;
          string actual = XMLServices.GetElementPreviousSibling(xmlBlockWithBookValue, elementName, elementValue, attrName, attrValue, n);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementPreviousSibling
      ///</summary>
      [TestMethod()]
      public void GetElementPreviousSiblingTest36()
      {
          XDocument doc = XDocument.Load(new StringReader(xmlBlockWithBookValue));
          doc.Root.Descendants().First().AddBeforeSelf(new XElement(nameSpace + "book", new XAttribute("genre", "novel"), "FirstBookValue"));
          string testXmlBlock = doc.ToString();
          string elementName = "book";
          string elementValue = "FirstBookValue";
          string attrName = "genre";
          string attrValue = "novel";
          int n = 1; 
          string expected = string.Empty;  // although the desired element exists, there is no previous sibling
          string actual = XMLServices.GetElementPreviousSibling(testXmlBlock, elementName, elementValue, attrName, attrValue, n);
          Assert.AreEqual(expected, actual);
      }
#endregion

#region GetElementNextSibling Tests
      /// <summary>
      ///A test for GetElementNextSibling
      ///</summary>
      [TestMethod()]
      public void GetElementNextSiblingTest()
      {
          string elementName = "author";
          string expected = new XElement(nameSpace + "price", "8.99").ToString();
          string actual = XMLServices.GetElementNextSibling(xmlBlock, elementName);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementNextSibling
      ///</summary>
      [TestMethod()]
      public void GetElementNextSiblingTest1()
      {
          XDocument doc = XDocument.Load(new StringReader(xmlBlock));
          doc.Descendants(nameSpace + "book").Last().AddAfterSelf(new XElement(nameSpace + "magazine", "Golf Digest"));
          string elementName = "magazine";  // "magazine" has no next sibling
          string expected = string.Empty;
          string actual = XMLServices.GetElementNextSibling(doc.ToString(), elementName);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementNextSibling
      ///</summary>
      [TestMethod()]
      public void GetElementNextSiblingTest2()
      {
          string elementName = "shelf";  // invalid element name
          string expected = string.Empty;
          string actual = XMLServices.GetElementNextSibling(xmlBlock, elementName);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementNextSibling
      ///</summary>
      [TestMethod()]
      public void GetElementNextSiblingTest3()
      {
          string elementName = "author";
          int n = 2;
          string expected = new XElement(nameSpace + "price", "11.99").ToString();
          string actual = XMLServices.GetElementNextSibling(xmlBlock, elementName, n);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementNextSibling
      ///</summary>
      [TestMethod()]
      public void GetElementNextSiblingTest4()
      {
          string elementName = "shelf";  // invalid element name
          int n = 1;
          string expected = string.Empty;
          string actual = XMLServices.GetElementNextSibling(xmlBlock, elementName, n);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementNextSibling
      ///</summary>
      [TestMethod()]
      public void GetElementNextSiblingTest5()
      {
          string elementName = "author";
          int n = 0;  // invalid index, too low
          string expected = string.Empty;
          string actual = XMLServices.GetElementNextSibling(xmlBlock, elementName, n);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementNextSibling
      ///</summary>
      [TestMethod()]
      public void GetElementNextSiblingTest6()
      {
          string elementName = "author";
          int n = 10;  // invalid index, too high
          string expected = string.Empty;
          string actual = XMLServices.GetElementNextSibling(xmlBlock, elementName, n);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementNextSibling
      ///</summary>
      [TestMethod()]
      public void GetElementNextSiblingTest7()
      {
          string elementName = "book";
          string elementValue = "TestBookValue";
          string expected = new XElement(nameSpace + "book", new XAttribute("genre", "philosophy"),
                                    new XAttribute("publicationdate", "1991-02-15"),
                                    new XAttribute("ISBN", "1-861001-57-6"),
                                new XElement(nameSpace + "title", "The Gorgias"),
                                new XElement(nameSpace + "author", new XElement(nameSpace + "name", "Plato")),
                                new XElement(nameSpace + "price", "9.99")).ToString();
          string actual = XMLServices.GetElementNextSibling(xmlBlockWithBookValue, elementName, elementValue);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementNextSibling
      ///</summary>
      [TestMethod()]
      public void GetElementNextSiblingTest8()
      {
          string elementName = "shelf";
          string elementValue = "TestBookValue";
          string expected = string.Empty;
          string actual = XMLServices.GetElementNextSibling(xmlBlockWithBookValue, elementName, elementValue);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementNextSibling
      ///</summary>
      [TestMethod()]
      public void GetElementNextSiblingTest9()
      {
          string elementName = "book";
          string elementValue = "NonExistentTestBookValue";
          string expected = string.Empty;
          string actual = XMLServices.GetElementNextSibling(xmlBlockWithBookValue, elementName, elementValue);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementNextSibling
      ///</summary>
      [TestMethod()]
      public void GetElementNextSiblingTest10()
      {
          string elementName = null;
          string elementValue = "TestBookValue";
          string expected = string.Empty;
          string actual = XMLServices.GetElementNextSibling(xmlBlockWithBookValue, elementName, elementValue);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementNextSibling
      ///</summary>
      [TestMethod()]
      public void GetElementNextSiblingTest11()
      {
          XDocument doc = XDocument.Load(new StringReader(xmlBlockWithBookValue));
          doc.Root.Descendants().Last().AddAfterSelf(new XElement(nameSpace + "book", "NoNextSibling"));
          string testXmlBlock = doc.ToString();
          string elementName = "book";
          string elementValue = "NoNextSibling";
          string expected = string.Empty;
          string actual = XMLServices.GetElementNextSibling(testXmlBlock, elementName, elementValue);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementNextSibling
      ///</summary>
      [TestMethod()]
      public void GetElementNextSiblingTest12()
      {
          string elementName = "book";
          string elementValue = "TestBookValue";
          int n = 1;
          string expected = new XElement(nameSpace + "book", new XAttribute("genre", "philosophy"),
                                new XAttribute("publicationdate", "1991-02-15"),
                                new XAttribute("ISBN", "1-861001-57-6"),
                            new XElement(nameSpace + "title", "The Gorgias"),
                            new XElement(nameSpace + "author", new XElement(nameSpace + "name", "Plato")),
                            new XElement(nameSpace + "price", "9.99")).ToString();
          string actual = XMLServices.GetElementNextSibling(xmlBlockWithBookValue, elementName, elementValue, n);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      /// A test for GetElementNextSibling
      /// </summary>
      [TestMethod()]
      public void GetElementNextSiblingTest13()
      {
          string elementName = "book";
          string attrName = "ISBN";
          string attrValue = "0-201-63361-2";
          string expected = new XElement(nameSpace + "book", new XAttribute("genre", "philosophy"),
                                new XAttribute("publicationdate", "1991-02-15"),
                                new XAttribute("ISBN", "1-861001-57-6"),
                            new XElement(nameSpace + "title", "The Gorgias"),
                            new XElement(nameSpace + "author", new XElement(nameSpace + "name", "Plato")),
                            new XElement(nameSpace + "price", "9.99")).ToString();
          string actual = XMLServices.GetElementNextSibling(xmlBlock, elementName, attrName, attrValue);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementNextSibling
      ///</summary>
      [TestMethod()]
      public void GetElementNextSiblingTest14()
      {
          string elementName = "shelf";  // invalid element name
          string attrName = "ISBN";
          string attrValue = "1-259693-19-7";
          string expected = string.Empty;
          string actual = XMLServices.GetElementNextSibling(xmlBlock, elementName, attrName, attrValue);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementNextSibling
      ///</summary>
      [TestMethod()]
      public void GetElementNextSiblingTest15()
      {
          string elementName = "book";
          string attrName = "dedicated-to";  // invalid attribute name
          string attrValue = "1-259693-19-7";
          string expected = string.Empty;
          string actual = XMLServices.GetElementNextSibling(xmlBlock, elementName, attrName, attrValue);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementNextSibling
      ///</summary>
      [TestMethod()]
      public void GetElementNextSiblingTest16()
      {
          string elementName = "book";
          string attrName = "ISBN";
          string attrValue = "0-000000-19-7";  // invalid attribute value
          string expected = string.Empty;
          string actual = XMLServices.GetElementNextSibling(xmlBlock, elementName, attrName, attrValue);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementNextSibling
      ///</summary>
      [TestMethod()]
      public void GetElementNextSiblingTest17()
      {
          string elementName = "book";
          string attrName = "genre";
          string attrValue = "novel";
          int n = 1;
          string expected = new XElement(nameSpace + "book", new XAttribute("genre", "philosophy"),
                                new XAttribute("publicationdate", "1991-02-15"),
                                new XAttribute("ISBN", "1-861001-57-6"),
                            new XElement(nameSpace + "title", "The Gorgias"),
                            new XElement(nameSpace + "author", new XElement(nameSpace + "name", "Plato")),
                            new XElement(nameSpace + "price", "9.99")).ToString();
          string actual = XMLServices.GetElementNextSibling(xmlBlock, elementName, attrName, attrValue, n);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementNextSibling
      ///</summary>
      [TestMethod()]
      public void GetElementNextSiblingTest18()
      {
          string elementName = "shelf";  // invalid element name
          string attrName = "genre";
          string attrValue = "novel";
          int n = 2;
          string expected = string.Empty;
          string actual = XMLServices.GetElementNextSibling(xmlBlock, elementName, attrName, attrValue, n);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementNextSibling
      ///</summary>
      [TestMethod()]
      public void GetElementNextSiblingTest19()
      {
          string elementName = "book";
          string attrName = "spine-color";  // invalid attribute name
          string attrValue = "novel";
          int n = 2;
          string expected = string.Empty;
          string actual = XMLServices.GetElementNextSibling(xmlBlock, elementName, attrName, attrValue, n);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementNextSibling
      ///</summary>
      [TestMethod()]
      public void GetElementNextSiblingTest20()
      {
          string elementName = "book";
          string attrName = "genre";
          string attrValue = "reference";  // invalid attribute value
          int n = 2;
          string expected = string.Empty;
          string actual = XMLServices.GetElementNextSibling(xmlBlock, elementName, attrName, attrValue, n);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementNextSibling
      ///</summary>
      [TestMethod()]
      public void GetElementNextSiblingTest21()
      {
          string elementName = "book";
          string attrName = "genre";
          string attrValue = "novel";
          int n = 0;  // invalid value for n -- too low
          string expected = string.Empty;
          string actual = XMLServices.GetElementNextSibling(xmlBlock, elementName, attrName, attrValue, n);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementNextSibling
      ///</summary>
      [TestMethod()]
      public void GetElementNextSiblingTest22()
      {
          string elementName = "book";
          string attrName = "genre";
          string attrValue = "novel";
          int n = 7;  // invalid value for n -- too high
          string expected = string.Empty;
          string actual = XMLServices.GetElementNextSibling(xmlBlock, elementName, attrName, attrValue, n);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementNextSibling
      ///</summary>
      [TestMethod()]
      public void GetElementNextSiblingTest23()
      {
          string elementName = "book";
          string attrName = "genre";
          string attrValue = "autobiography";
          int n = 2;
          string expected = string.Empty;   // element exists, but has no next sibling
          string actual = XMLServices.GetElementNextSibling(xmlBlock, elementName, attrName, attrValue, n);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementNextSibling
      ///</summary>
      [TestMethod()]
      public void GetElementNextSiblingTest24()
      {
          string elementName = "book";
          string elementValue = "TestBookValue";
          string attrName = "genre";
          string attrValue = "novel";
          string expected = new XElement(nameSpace + "book", new XAttribute("genre", "philosophy"),
                                new XAttribute("publicationdate", "1991-02-15"),
                                new XAttribute("ISBN", "1-861001-57-6"),
                            new XElement(nameSpace + "title", "The Gorgias"),
                            new XElement(nameSpace + "author", new XElement(nameSpace + "name", "Plato")),
                            new XElement(nameSpace + "price", "9.99")).ToString();
          string actual = XMLServices.GetElementNextSibling(xmlBlockWithBookValue, elementName, elementValue, attrName, attrValue);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementNextSibling
      ///</summary>
      [TestMethod()]
      public void GetElementNextSiblingTest25()
      {
          string elementName = "magazine";  // invalid element name
          string elementValue = "TestBookValue";
          string attrName = "genre";
          string attrValue = "novel";
          string expected = string.Empty;
          string actual = XMLServices.GetElementNextSibling(xmlBlockWithBookValue, elementName, elementValue, attrName, attrValue);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementNextSibling
      ///</summary>
      [TestMethod()]
      public void GetElementNextSiblingTest26()
      {
          string elementName = "book";
          string elementValue = "eBook";    // invalid element value
          string attrName = "genre";
          string attrValue = "novel";
          string expected = string.Empty;
          string actual = XMLServices.GetElementNextSibling(xmlBlockWithBookValue, elementName, elementValue, attrName, attrValue);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementNextSibling
      ///</summary>
      [TestMethod()]
      public void GetElementNextSiblingTest27()
      {
          string elementName = "book";
          string elementValue = "TestBookValue";
          string attrName = "publisher";  // invalid attribute name
          string attrValue = "novel";
          string expected = string.Empty;
          string actual = XMLServices.GetElementNextSibling(xmlBlockWithBookValue, elementName, elementValue, attrName, attrValue);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementNextSibling
      ///</summary>
      [TestMethod()]
      public void GetElementNextSiblingTest28()
      {
          string elementName = "book";
          string elementValue = "TestBookValue";
          string attrName = "genre";
          string attrValue = "fiction";    // invalid attribute value
          string expected = string.Empty;
          string actual = XMLServices.GetElementNextSibling(xmlBlockWithBookValue, elementName, elementValue, attrName, attrValue);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementNextSibling
      ///</summary>
      [TestMethod()]
      public void GetElementNextSiblingTest29()
      {
          string elementName = "book";
          string elementValue = "TestBookValue";
          string attrName = "genre";
          string attrValue = "novel";
          int n = 1;
          string expected = new XElement(nameSpace + "book",
                                new XAttribute("genre", "philosophy"), new XAttribute("publicationdate", "1991-02-15"),
                                new XAttribute("ISBN", "1-861001-57-6"), new XElement(nameSpace + "title", "The Gorgias"),
                                new XElement(nameSpace + "author", new XElement(nameSpace + "name", "Plato")),
                                new XElement(nameSpace + "price", "9.99")).ToString();
          string actual = XMLServices.GetElementNextSibling(xmlBlockWithBookValue, elementName, elementValue, attrName, attrValue, n);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementNextSibling
      ///</summary>
      [TestMethod()]
      public void GetElementNextSiblingTest30()
      {
          string elementName = "library";  // invalid element name
          string elementValue = "TestBookValue";
          string attrName = "genre";
          string attrValue = "novel";
          int n = 1;
          string expected = string.Empty;
          string actual = XMLServices.GetElementNextSibling(xmlBlockWithBookValue, elementName, elementValue, attrName, attrValue, n);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementNextSibling
      ///</summary>
      [TestMethod()]
      public void GetElementNextSiblingTest31()
      {
          string elementName = "book";
          string elementValue = "AnotherTestBookValue";  // invalid element value
          string attrName = "genre";
          string attrValue = "novel";
          int n = 1;
          string expected = string.Empty;
          string actual = XMLServices.GetElementNextSibling(xmlBlockWithBookValue, elementName, elementValue, attrName, attrValue, n);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementNextSibling
      ///</summary>
      [TestMethod()]
      public void GetElementNextSiblingTest32()
      {
          string elementName = "book";
          string elementValue = "TestBookValue";
          string attrName = "color";  // invalid attribute name
          string attrValue = "novel";
          int n = 1;
          string expected = string.Empty;
          string actual = XMLServices.GetElementNextSibling(xmlBlockWithBookValue, elementName, elementValue, attrName, attrValue, n);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementNextSibling
      ///</summary>
      [TestMethod()]
      public void GetElementNextSiblingTest33()
      {
          string elementName = "book";
          string elementValue = "TestBookValue";
          string attrName = "genre";
          string attrValue = "non-fiction";    // invalid attribute value
          int n = 1;
          string expected = string.Empty;
          string actual = XMLServices.GetElementNextSibling(xmlBlockWithBookValue, elementName, elementValue, attrName, attrValue, n);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementNextSibling
      ///</summary>
      [TestMethod()]
      public void GetElementNextSiblingTest34()
      {
          string elementName = "book";
          string elementValue = "TestBookValue";
          string attrName = "genre";
          string attrValue = "novel";
          int n = 0;    // invalid value for n -- too low
          string expected = string.Empty;
          string actual = XMLServices.GetElementNextSibling(xmlBlockWithBookValue, elementName, elementValue, attrName, attrValue, n);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementNextSibling
      ///</summary>
      [TestMethod()]
      public void GetElementNextSiblingTest35()
      {
          string elementName = "book";
          string elementValue = "TestBookValue";
          string attrName = "genre";
          string attrValue = "novel";
          int n = 10;    // invalid value for n -- too high
          string expected = string.Empty;
          string actual = XMLServices.GetElementNextSibling(xmlBlockWithBookValue, elementName, elementValue, attrName, attrValue, n);
          Assert.AreEqual(expected, actual);
      }

      /// <summary>
      ///A test for GetElementNextSibling
      ///</summary>
      [TestMethod()]
      public void GetElementNextSiblingTest36()
      {
          string elementName = "book";
          string elementValue = "TestBookValue";
          string attrName = "genre";
          string attrValue = "novel";
          int n = 2;
          string expected = string.Empty;  // although the desired element exists, there is no next sibling
          string actual = XMLServices.GetElementNextSibling(xmlBlock, elementName, elementValue, attrName, attrValue, n);
          Assert.AreEqual(expected, actual);
      }
#endregion

#region GetElementAttrs Tests
      /// <summary>
      ///A test for GetElementAttrs
      ///</summary>
      [TestMethod()]
      public void GetElementAttrsTest()
      {
          string elementName = "book";
          List<DictionaryEntry> expected = new List<DictionaryEntry>() { new DictionaryEntry("genre", "autobiography"), new DictionaryEntry("publicationdate", "1981-03-22"), new DictionaryEntry("ISBN", "1-861003-11-0") };
          List<DictionaryEntry> actual = XMLServices.GetElementAttrs(xmlBlock, elementName);
          Assert.AreEqual(actual.Count, expected.Count);
          for (int i = 0; i < actual.Count; i++)
          {
              Assert.AreEqual(expected[i], actual[i]);
          }
      }

      /// <summary>
      ///A test for GetElementAttrs
      ///</summary>
      [TestMethod()]
      public void GetElementAttrsTest1()
      {
          string elementName = "shelf";  // invalid element name
          List<DictionaryEntry> actual = XMLServices.GetElementAttrs(xmlBlock, elementName);
          Assert.IsNull(actual);
      }

      /// <summary>
      ///A test for GetElementAttrs
      ///</summary>
      [TestMethod()]
      public void GetElementAttrsTest2()
      {
          string elementName = "book";
          int n = 3;
          List<DictionaryEntry> expected = new List<DictionaryEntry>() { new DictionaryEntry("genre", "philosophy"), new DictionaryEntry("publicationdate", "1991-02-15"), new DictionaryEntry("ISBN", "1-861001-57-6") };
          List<DictionaryEntry> actual = XMLServices.GetElementAttrs(xmlBlock, elementName, n);
          Assert.AreEqual(actual.Count, expected.Count);
          for (int i = 0; i < actual.Count; i++)
          {
              Assert.AreEqual(expected[i], actual[i]);
          }
      }

      /// <summary>
      ///A test for GetElementAttrs
      ///</summary>
      [TestMethod()]
      public void GetElementAttrsTest3()
      {
          string elementName = "shelf";  // invalid element name
          int n = 3;
          List<DictionaryEntry> actual = XMLServices.GetElementAttrs(xmlBlock, elementName, n);
          Assert.IsNull(actual);
      }

      /// <summary>
      ///A test for GetElementAttrs
      ///</summary>
      [TestMethod()]
      public void GetElementAttrsTest4()
      {
          string elementName = "book";
          int n = 0;    // invalid value for n -- too low
          List<DictionaryEntry> actual = XMLServices.GetElementAttrs(xmlBlock, elementName, n);
          Assert.IsNull(actual);
      }

      /// <summary>
      ///A test for GetElementAttrs
      ///</summary>
      [TestMethod()]
      public void GetElementAttrsTest5()
      {
          string elementName = "book";
          int n = 8;    // invalid value for n -- too high
          List<DictionaryEntry> actual = XMLServices.GetElementAttrs(xmlBlock, elementName, n);
          Assert.IsNull(actual);
      }

      /// <summary>
      ///A test for GetElementAttrs
      ///</summary>
      [TestMethod()]
      public void GetElementAttrsTest6()
      {
          string elementName = "book";
          string elementValue = "TestBookValue";
          List<DictionaryEntry> expected = new List<DictionaryEntry>() { new DictionaryEntry("genre", "novel"), new DictionaryEntry("publicationdate", "1967-11-17"), new DictionaryEntry("ISBN", "0-201-63361-2") };
          List<DictionaryEntry> actual = XMLServices.GetElementAttrs(xmlBlockWithBookValue, elementName, elementValue);
          Assert.AreEqual(actual.Count, expected.Count);
          for (int i = 0; i < actual.Count; i++)
          {
              Assert.AreEqual(expected[i], actual[i]);
          }
      }

      /// <summary>
      ///A test for GetElementAttrs
      ///</summary>
      [TestMethod()]
      public void GetElementAttrsTest7()
      {
          string elementName = "shelf";  // invalid element name
          string elementValue = "TestBookValue";
          List<DictionaryEntry> actual = XMLServices.GetElementAttrs(xmlBlockWithBookValue, elementName, elementValue);
          Assert.IsNull(actual);
      }

      /// <summary>
      ///A test for GetElementAttrs
      ///</summary>
      [TestMethod()]
      public void GetElementAttrsTest8()
      {
          string elementName = "book";
          string elementValue = "AnotherBookValue";    // invalid element value
          List<DictionaryEntry> actual = XMLServices.GetElementAttrs(xmlBlockWithBookValue, elementName, elementValue);
          Assert.IsNull(actual);
      }

      /// <summary>
      ///A test for GetElementAttrs
      ///</summary>
      [TestMethod()]
      public void GetElementAttrsTest9()
      {
          string elementName = "book";
          string elementValue = "TestBookValue";
          int n = 2;
          List<DictionaryEntry> expected = new List<DictionaryEntry>() { new DictionaryEntry("genre", "novel"), new DictionaryEntry("publicationdate", "1967-12-22"), new DictionaryEntry("ISBN", "0-202-43321-9") };
          List<DictionaryEntry> actual = XMLServices.GetElementAttrs(xmlBlockWithBookValue, elementName, elementValue, n);
          Assert.AreEqual(actual.Count, expected.Count);
          for (int i = 0; i < actual.Count; i++)
          {
              Assert.AreEqual(expected[i], actual[i]);
          }
      }

      /// <summary>
      ///A test for GetElementAttrs
      ///</summary>
      [TestMethod()]
      public void GetElementAttrsTest10()
      {
          string elementName = "shelf";  // invalid element name
          string elementValue = "TestBookValue";
          int n = 2;
          List<DictionaryEntry> actual = XMLServices.GetElementAttrs(xmlBlockWithBookValue, elementName, elementValue, n);
          Assert.IsNull(actual);
      }

      /// <summary>
      ///A test for GetElementAttrs
      ///</summary>
      [TestMethod()]
      public void GetElementAttrsTest11()
      {
          string elementName = "book";
          string elementValue = "AnotherBookValue";    // invalid element value
          int n = 2;
          List<DictionaryEntry> actual = XMLServices.GetElementAttrs(xmlBlockWithBookValue, elementName, elementValue, n);
          Assert.IsNull(actual);
      }

      /// <summary>
      ///A test for GetElementAttrs
      ///</summary>
      [TestMethod()]
      public void GetElementAttrsTest12()
      {
          string elementName = "book";
          string elementValue = "TestBookValue";
          int n = 0;       // invalid value for n -- too low
          List<DictionaryEntry> actual = XMLServices.GetElementAttrs(xmlBlockWithBookValue, elementName, elementValue, n);
          Assert.IsNull(actual);
      }

      /// <summary>
      ///A test for GetElementAttrs
      ///</summary>
      [TestMethod()]
      public void GetElementAttrsTest13()
      {
          string elementName = "book";
          string elementValue = "TestBookValue";
          int n = 6;       // invalid value for n -- too high
          List<DictionaryEntry> actual = XMLServices.GetElementAttrs(xmlBlockWithBookValue, elementName, elementValue, n);
          Assert.IsNull(actual);
      }
#endregion
   }
}
