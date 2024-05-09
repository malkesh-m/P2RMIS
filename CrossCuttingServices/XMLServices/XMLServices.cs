using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Sra.P2rmis.CrossCuttingServices
{
   /// <summary>
   /// The XMLServices class provides search, validation, and deserialization services on blocks of XML contained in string variables
   /// </summary>
   public static class XMLServices
   {
      #region Private Query Builder Helper Methods
       /// <summary>
       /// Constructs a LINQ-to-XML query (deferred execution) to get XML elements that match a provided element name
       /// </summary>
       /// <param name="xmlBlock">The XML block to search</param>
       /// <param name="elementName">The element name to match</param>
       /// <returns>A (deferred-execution) collection of matching XML elements</returns>
       private static IEnumerable<XElement> GetElementByElementName(string xmlBlock, string elementName)
       {
           XDocument doc = XDocument.Load(new StringReader(xmlBlock));
           XNamespace defaultNamespace = doc.Root.Name.Namespace;
           IEnumerable<XElement> query = from element in doc.Descendants(defaultNamespace + elementName)
                                         select element;
           return query;
       }

       /// <summary>
       /// Constructs a LINQ-to-XML query (deferred execution) to get XML elements that match a provided element name
       /// </summary>
       /// <param name="xmlBlock">The XML block to search</param>
       /// <param name="elementName">The element name to match</param>
       /// <param name="elementValue">The element value to match</param>
       /// <returns>A (deferred-execution) collection of matching XML elements</returns>
       private static IEnumerable<XElement> GetElementByElementNameAndValue(string xmlBlock, string elementName, string elementValue)
       {
           XDocument doc = XDocument.Load(new StringReader(xmlBlock));
           XNamespace defaultNamespace = doc.Root.Name.Namespace;
           var query = from element in doc.Descendants(defaultNamespace + elementName)
                       where element.Value.ToString() == elementValue
                       select element;
           return query;
       }

       /// <summary>
       /// Constructs a LINQ-to-XML query (deferred execution) to get XML elements that match a provided element name, and an attribute name/value pair
       /// </summary>
       /// <param name="xmlBlock">The XML block to search</param>
       /// <param name="elementName">The element name to match</param>
       /// <param name="attrName">The attribute name to match</param>
       /// <param name="attrValue">The attribute value to match</param>
       /// <returns>A (deferred-execution) collection of matching XML elements</returns>
       private static IEnumerable<XElement> GetElementByElementNameAndAttribute(string xmlBlock, string elementName, string attrName, string attrValue)
       {
           XDocument doc = XDocument.Load(new StringReader(xmlBlock));
           XNamespace defaultNamespace = doc.Root.Name.Namespace;
           IEnumerable<XElement> query = from element in doc.Descendants(defaultNamespace + elementName)
                       where (string)element.Attribute(attrName) == attrValue
                       select element;
           return query;
       }

       /// <summary>
       /// Constructs a LINQ-to-XML query (deferred execution) to get XML elements that match a provided element name and include a given attribute name
       /// </summary>
       /// <param name="xmlBlock">The XML block to search</param>
       /// <param name="elementName">The element name to match</param>
       /// <param name="attrName">The attribute name to match</param>
       /// <returns>A (deferred-execution) collection of matching XML elements</returns>
       private static IEnumerable<XElement> GetElementByElementNameAndAttributeExistence(string xmlBlock, string elementName, string attrName)
       {
           XDocument doc = XDocument.Load(new StringReader(xmlBlock));
           XNamespace defaultNamespace = doc.Root.Name.Namespace;
           IEnumerable<XElement> query = from element in doc.Descendants(defaultNamespace + elementName)
                       where element.Attributes().Any(aname => aname.Name == attrName)
                       select element;
           return query;
       }

       /// <summary>
       /// Constructs a LINQ-to-XML query (deferred execution) to get XML elements that match a provided element name and include a given attribute name
       /// </summary>
       /// <param name="xmlBlock">The XML block to search</param>
       /// <param name="elementName">The element name to match</param>
       /// <param name="elementValue">The element value to match</param>
       /// <param name="attrName">The attribute name to match</param>
       /// <returns>A (deferred-execution) collection of matching XML elements</returns>
       private static IEnumerable<XElement> GetElementByElementNameValueAndAttributeExistence(string xmlBlock, string elementName, string elementValue, string attrName)
       {
           XDocument doc = XDocument.Load(new StringReader(xmlBlock));
           XNamespace defaultNamespace = doc.Root.Name.Namespace;
           IEnumerable<XElement> query = from element in doc.Descendants(defaultNamespace + elementName)
                       where element.Attributes().Any(aname => aname.Name == attrName) &&
                             element.Value.ToString() == elementValue
                       select element;
           return query;
       }

       /// <summary>
       /// Constructs a LINQ-to-XML query (deferred execution) to get XML elements that match a provided element name, and an attribute name/value pair
       /// </summary>
       /// <param name="xmlBlock">The XML block to search</param>
       /// <param name="elementName">The element name to match</param>
       /// <param name="elementValue">The element value to match</param>
       /// <param name="attrName">The attribute name to match</param>
       /// <param name="attrValue">The attribute value to match</param>
       /// <returns>A (deferred-execution) collection of matching XML elements</returns>
       private static IEnumerable<XElement> GetElementByElementNameValueAndAttributeNameValue(string xmlBlock, string elementName, string elementValue, string attrName, string attrValue)
       {
           XDocument doc = XDocument.Load(new StringReader(xmlBlock));
           XNamespace defaultNamespace = doc.Root.Name.Namespace;
           IEnumerable<XElement> query = from element in doc.Descendants(defaultNamespace + elementName)
                       where (string)element.Attribute(attrName) == attrValue &&
                             (string)element.Value == elementValue
                       select element;
           return query;
       }
      #endregion

      #region Other Private Helper Methods
       /// <summary>
       /// Returns the value of the first element in a collection
       /// </summary>
       /// <param name="elements">The collection of XML elements</param>
       /// <returns>The value of the first element</returns>
       private static string GetFirstElementValue(IEnumerable<XElement> elements)
       {
           string retval = string.Empty;
           if (elements.Count() > 0)
           {
               retval = elements.First().Value;
           }
           return retval;
       }

       /// <summary>
       /// Gets the value of the nth element in a collection of XML elements
       /// </summary>
       /// <param name="elements">The collection of XML elements</param>
       /// <param name="n">The index of the item in the collection for which the value is desired</param>
       /// <returns>The value of the nth element in the collection</returns>
       private static string GetNthElementValue(IEnumerable<XElement> elements, int n)
       {
           string retval = string.Empty;
           if (elements.Count() >= n && n >= 1)
           {
               retval = elements.ElementAt(n - 1).Value;
           }
           return retval;
       }

       /// <summary>
       /// Gets the value of a named attribute from the first element in a collection of XML elements
       /// </summary>
       /// <param name="elements">The collection of XML elements to search</param>
       /// <param name="attrName">The name of the attribute</param>
       /// <returns>The value of the attribute from the first element in the collection</returns>
       private static string GetFirstElementAttributeValueByName(IEnumerable<XElement> elements, string attrName)
       {
           string retval = string.Empty;
           if (elements.Count() > 0)
           {
               retval = elements.First().Attribute(attrName).Value;
           }
           return retval;
       }

       /// <summary>
       /// Gets the value of a named attribute from the nt elemhent in a collection of XML elements
       /// </summary>
       /// <param name="elements">The collection of XML elements to search</param>
       /// <param name="attrName">The name of the attribute</param>
       /// <param name="n">The index of the desired element in the element collection</param>
       /// <returns>The value of the attribute from the nth element in the collection</returns>
       private static string GetNthElementAttributeValueByName(IEnumerable<XElement> elements, string attrName, int n)
       {
           string retval = string.Empty;
           if (elements.Count() >= n && n >= 1)
           {
               retval = elements.ElementAt(n - 1).Attribute(attrName).Value;
           }
           return retval;
       }

       /// <summary>
       /// Gets a string representation of the XML in the first element in an XML element collection
       /// </summary>
       /// <param name="elements">The collection of XML elements</param>
       /// <returns>A string representation of the XML in the first element in the XML element collection</returns>
       private static string GetFirstElementBlock(IEnumerable<XElement> elements)
       {
           string retval = string.Empty;
           if (elements.Count() > 0)
           {
               retval = elements.First().ToString();
           }
           return retval;
       }

       /// <summary>
       /// Gets a string representation of the XML in the nth element in an XML element collection
       /// </summary>
       /// <param name="elements">The collection of XML elements</param>
       /// <param name="n">The index of the element in the collection</param>
       /// <returns>A string representation of the XML in the nth element in the XML element collection</returns>
       private static string GetNthElementBlock(IEnumerable<XElement> elements, int n)
       {
           string retval = string.Empty;
           if (elements.Count() >= n && n >= 1)
           {
               retval = elements.ElementAt(n - 1).ToString();
           }
           return retval;
       }

       /// <summary>
       /// Get a string representation of the descendants of the first element in an XML element collection
       /// </summary>
       /// <param name="query">The collection of XML elements</param>
       /// <returns>A string representation of the descendants of the first element in the collection</returns>
       private static string GetFirstElementDescendants(IEnumerable<XElement> query)
       {
           string retval = string.Empty;
           if (query.Count() > 0)
           {
               var children = from child in query.DescendantsAndSelf().First().Elements()
                              select child.ToString();
               retval = string.Join(string.Empty, children);
           }
           return retval;
       }

       /// <summary>
       /// Get a string representation of the descendants of the nth element in an XML element collection
       /// </summary>
       /// <param name="elements">The collection of XML elements</param>
       /// <param name="n">The index of the desired element in the element collection</param>
       /// <returns>A string representation of the descendants of the nth element in the collection</returns>
       private static string GetNthElementDescendants(IEnumerable<XElement> elements, int n)
       {
           string retval = string.Empty;
           if (elements.Count() >= n && n >= 1)
           {
               var children = from child in elements.ElementAt(n - 1).DescendantsAndSelf().First().Elements()
                              select child.ToString();
               retval = string.Join(string.Empty, children);
           }
           return retval;
       }

       /// <summary>
       /// Determines the name of the parent element of a given collection of XML elements.  Assumes that repeating elements all have the same parent
       /// </summary>
       /// <param name="els">The list of elements for which the parent is desired</param>
       /// <returns>The name of the parent element with its namespace removed</returns>
       private static string GetParent(IEnumerable<XElement> els)
       {
           string retval = string.Empty;
           if (els.Count() > 0)
           {
               XElement parent = els.First().Parent;
               if (parent != null)
               {
                   retval = parent.Name.LocalName;
               }
           }
           return retval;
       }

       /// <summary>
       /// Determines the previous sibling from the first element in an element collection
       /// </summary>
       /// <param name="elements">A collection of XML elements</param>
       /// <returns>Returns the previous sibling of the first element in the collection</returns>
       private static string GetFirstElementPreviousSibling(IEnumerable<XElement> elements)
       {
           string retval = string.Empty;
           if (elements.Count() > 0)
           {
               XElement el = elements.First();
               if (el.ElementsBeforeSelf().Count() > 0)
               {
                   retval = el.ElementsBeforeSelf().Last().ToString();
               }
           }
           return retval;
       }

       /// <summary>
       /// Determines the previous sibling from the first element in an element collection
       /// </summary>
       /// <param name="elements">A collection of XML elements</param>
       /// <param name="n">Specifies that the sibling of the nth element in the collection is desired</param>
       /// <returns>Returns the previous sibling of the nth element in the collection</returns>
       private static string GetNthElementPreviousSibling(IEnumerable<XElement> elements, int n)
       {
           string retval = string.Empty;
           if (elements.Count() >= n && n >= 1)
           {
               XElement el = elements.ElementAt(n - 1);
               if (el.ElementsBeforeSelf().Count() > 0)
               {
                   retval = el.ElementsBeforeSelf().Last().ToString();
               }
           }
           return retval;
       }

       /// <summary>
       /// Determines the next sibling from the first element in an element collection
       /// </summary>
       /// <param name="elements">A collection of XML elements</param>
       /// <returns>Returns the next sibling of the first element in the collection</returns>
       private static string GetFirstElementNextSibling(IEnumerable<XElement> elements)
       {
           string retval = string.Empty;
           if (elements.Count() > 0)
           {
               XElement el = elements.First();
               if (el.ElementsAfterSelf().Count() > 0)
               {
                   retval = el.ElementsAfterSelf().First().ToString();
               }
           }
           return retval;
       }

       /// <summary>
       /// Determines the next sibling from the first element in an element collection
       /// </summary>
       /// <param name="elements">A collection of XML elements</param>
       /// <param name="n">Specifies that the sibling of the nth element in the collection is desired</param>
       /// <returns>Returns the next sibling of the nth element in the collection</returns>
       private static string GetNthElementNextSibling(IEnumerable<XElement> elements, int n)
       {
           string retval = string.Empty;
           if (elements.Count() >= n && n >= 1)
           {
               XElement el = elements.ElementAt(n - 1);
               if (el.ElementsAfterSelf().Count() > 0)
               {
                   retval = el.ElementsAfterSelf().First().ToString();
               }
           }
           return retval;
       }

       /// <summary>
       /// Gets the list of attribute name/value pairs from the first element in a collection of XML elements
       /// </summary>
       /// <param name="elements">The collection of XML elements</param>
       /// <returns>The list of attribute name/value pairs</returns>
       private static List<DictionaryEntry> GetFirstElementAttributes(IEnumerable<XElement> elements)
       {
           List<DictionaryEntry> retval = null;
           if (elements.Count() > 0)
           {
               retval = GetElementAttributes(elements.First());
           }
           return retval;
       }

       /// <summary>
       /// Gets the list of attribute name/value pairs from the first element in a collection of XML elements
       /// </summary>
       /// <param name="elements">The collection of XML elements</param>
       /// <param name="n">The index of the desired element in the collection</param>
       /// <returns>The list of attribute name/value pairs</returns>
       private static List<DictionaryEntry> GetNthElementAttributes(IEnumerable<XElement> elements, int n)
       {
           List<DictionaryEntry> retval = null;
           if (elements.Count() >= n && n > 0)
           {
               retval = GetElementAttributes(elements.ElementAt(n-1));
           }
           return retval;
       }

       /// <summary>
       /// Gets a list of attribute name/value pairs from an XML element
       /// </summary>
       /// <param name="el">The element containing the attributes</param>
       /// <returns>A list of attribute name/value pairs</returns>
       private static List<DictionaryEntry> GetElementAttributes(XElement el)
       {
           List<DictionaryEntry> retval = null;
           if (el.Attributes().Count() > 0)
           {
               retval = new List<DictionaryEntry>();
               foreach (XAttribute attr in el.Attributes())
               {
                   retval.Add(new DictionaryEntry(attr.Name.LocalName, attr.Value));
               }
           }
           return retval;
       }
      #endregion

      #region GetElementValue Overloads
       /// <summary>
      /// Searches for the value of an XML element when provided the element name.  The element name must be in the default namespace, the same namespace as the root, or in no namespace at all.
      /// </summary>
      /// <param name="xmlBlock">The XML to search</param>
      /// <param name="elementName">The name of the element to search for</param>
      /// <returns>The value of the desired element.  If the element is not unique within the XML, the value of the first occurrence of the element name is returned.  Returns an empty string if no matching element is found.</returns>
      public static String GetElementValue( string xmlBlock, string elementName)
      {
          string retval = string.Empty;
          if (IsValidXML(xmlBlock) && !String.IsNullOrEmpty(elementName))
          {
              IEnumerable<XElement> query = GetElementByElementName(xmlBlock, elementName);
              retval = GetFirstElementValue(query);
          }
          return retval;
      }

      /// <summary>
      /// Searches for the value of an XML element when provided the element name.  The element name must be in the default namespace, the same namespace as the root, or in no namespace at all.
      /// </summary>
      /// <param name="xmlBlock">The XML to search</param>
      /// <param name="elementName">The name of the element to search for</param>
      /// <param name="n">For a repeating element, the nth occurrence (zero-based) of the element</param>
      /// <returns>The value of the desired element.  Returns an empty string if no matching element is found.</returns>
      public static String GetElementValue(string xmlBlock, string elementName, int n)
      {
          string retval = string.Empty;
          if (IsValidXML(xmlBlock) && !String.IsNullOrEmpty(elementName))
          {
              IEnumerable<XElement> query = GetElementByElementName(xmlBlock, elementName);
              retval = GetNthElementValue(query, n);
          }
          return retval;
      }

      /// <summary>
      /// Searches for the value of an XML element when provided the element name, an attribute name, and an attribute value.  The element name must be in the default namespace, the same namespace as the root, or in no namespace at all.
        /// </summary>
        /// <param name="xmlBlock">The XML to search</param>
        /// <param name="elementName">The name of the element to search for</param>
        /// <param name="attrName">An attribute name found in the desired element</param>
        /// <param name="attrValue">The value assigned to the attribute specified in the previous parameter</param>
      /// <returns>The value of the desired element.  If the element is not unique within the XML, the value of the first occurrence of the element name is returned.  Returns an empty string if no matching element is found.</returns>
      public static String GetElementValue(string xmlBlock, string elementName, string attrName, string attrValue)
      {
          string retval = string.Empty;
          if (IsValidXML(xmlBlock) && !String.IsNullOrEmpty(elementName) && !String.IsNullOrEmpty(attrName))
          {
              IEnumerable<XElement> query = GetElementByElementNameAndAttribute(xmlBlock, elementName, attrName, attrValue);
              retval = GetFirstElementValue(query);
          }
          return retval;
      }

      /// <summary>
      /// Searches for the value of an XML element when provided the element name, an attribute name, and an attribute value.  The element name must be in the default namespace, the same namespace as the root, or in no namespace at all.
      /// </summary>
      /// <param name="xmlBlock">The XML to search</param>
      /// <param name="elementName">The name of the element to search for</param>
      /// <param name="attrName">An attribute name found in the desired element</param>
      /// <param name="attrValue">The value assigned to the attribute specified in the previous parameter</param>
      /// <param name="n">For a repeating element, the nth occurrence (zero-based) of the element</param>
      /// <returns>The value of the desired element.  Returns an empty string if no matching element is found.</returns>
      public static String GetElementValue(string xmlBlock, string elementName, string attrName, string attrValue, int n)
      {
          string retval = string.Empty;
          if (IsValidXML(xmlBlock) && !String.IsNullOrEmpty(elementName) && !String.IsNullOrEmpty(attrName))
          {
              IEnumerable<XElement> query = GetElementByElementNameAndAttribute(xmlBlock, elementName, attrName, attrValue);
              retval = GetNthElementValue(query, n);
          }
          return retval;
      }
     #endregion

      #region GetAttributeValue Overloads
      /// <summary>
       /// Searches for the value of an attribute from within a given XML element
       /// </summary>
       /// <param name="xmlBlock">The XML to search</param>
       /// <param name="elementName">The name of the element containing the desired attribute</param>
       /// <param name="attrName">The name of the attribute containing the desired attribute value</param>
      /// <returns>The value of the desired attribute.  If the element is not unique within the XML, the attribute value of the first occurrence of the element name and attribute is returned.  Returns an empty string if no matching attribute is found.</returns>
      public static String GetAttributeValue( string xmlBlock, string elementName, string attrName)
      {
          string retval = string.Empty;
          if (IsValidXML(xmlBlock) && !String.IsNullOrEmpty(elementName) && !String.IsNullOrEmpty(attrName))
          {
              IEnumerable<XElement> query = GetElementByElementNameAndAttributeExistence(xmlBlock, elementName, attrName);
              retval = GetFirstElementAttributeValueByName(query, attrName);
          }
          return retval;
      }

      /// <summary>
      /// Searches for the value of an attribute from within a given XML element
      /// </summary>
      /// <param name="xmlBlock">The XML to search</param>
      /// <param name="elementName">The name of the element containing the desired attribute</param>
      /// <param name="attrName">The name of the attribute containing the desired attribute value</param>
      /// <param name="n">For a repeating element, the nth occurrence of the element</param>
      /// <returns>The value of the desired attribute.  If the element is not unique within the XML, the attribute value of the first occurrence of the element name and attribute is returned.  Returns an empty string if no matching attribute is found.</returns>
      public static String GetAttributeValue(string xmlBlock, string elementName, string attrName, int n)
      {
          string retval = string.Empty;
          if (IsValidXML(xmlBlock) && !String.IsNullOrEmpty(elementName) && !String.IsNullOrEmpty(attrName))
          {
              IEnumerable<XElement> query = GetElementByElementNameAndAttributeExistence(xmlBlock, elementName, attrName);
              retval = GetNthElementAttributeValueByName(query, attrName, n);
          }
          return retval;
      }

       /// <summary>
       /// Searches for the value of an attribute from within a given XML element
       /// </summary>
      /// <param name="xmlBlock">The XML to search</param>
      /// <param name="elementName">The name of the element containing the desired attribute</param>
       /// <param name="elementValue">The value contained within the desired element</param>
      /// <param name="attrName">The name of the attribute containing the desired attribute value</param>
      /// <returns>The value of the desired attribute.  If the element and its value are not unique within the XML, the attribute value of the first occurrence of the element name and attribute is returned.  Returns an empty string if no matching attribute is found.</returns>
      public static String GetAttributeValue(string xmlBlock, string elementName, string elementValue, string attrName)
      {
          string retval = string.Empty;
          if (IsValidXML(xmlBlock) && !String.IsNullOrEmpty(elementName) && !String.IsNullOrEmpty(attrName))
          {
              IEnumerable<XElement> query = GetElementByElementNameValueAndAttributeExistence(xmlBlock, elementName, elementValue, attrName);
              retval = GetFirstElementAttributeValueByName(query, attrName);
          }
          return retval;
      }

      /// <summary>
      /// Searches for the value of an attribute from within a given XML element
      /// </summary>
      /// <param name="xmlBlock">The XML to search</param>
      /// <param name="elementName">The name of the element containing the desired attribute</param>
      /// <param name="elementValue">The value contained within the desired element</param>
      /// <param name="attrName">The name of the attribute containing the desired attribute value</param>
      /// <param name="n">For a repeating element, the nth occurrence of the element</param>
      /// <returns>The value of the desired attribute.  If the element and its value are not unique within the XML, the attribute value of the first occurrence of the element name and attribute is returned.  Returns an empty string if no matching attribute is found.</returns>
      public static String GetAttributeValue(string xmlBlock, string elementName, string elementValue, string attrName, int n)
      {
          string retval = string.Empty;
          if (IsValidXML(xmlBlock) && !String.IsNullOrEmpty(elementName) && !String.IsNullOrEmpty(attrName))
          {
              IEnumerable<XElement> query = GetElementByElementNameValueAndAttributeExistence(xmlBlock, elementName, elementValue, attrName);
              retval = GetNthElementAttributeValueByName(query, attrName, n);
          }
          return retval;
      }
      #endregion

      #region GetElementBlock Overloads
      /// <summary>
      /// Searches a block of XML for a given element, and returns a subset block of XML, including the given element and its child elements
      /// </summary>
      /// <param name="xmlBlock">The XML to search</param>
      /// <param name="elementName">The name of the element containing the desired block</param>
      /// <returns>A block of XML including the desired element and its children.  If the element is not unique, returns the first occurrence.  Returns an empty string if no matching element is found.</returns>
      public static String GetElementBlock( string xmlBlock, string elementName)
      {
          string retval = string.Empty;
          if (IsValidXML(xmlBlock) && !String.IsNullOrEmpty(elementName))
          {
              IEnumerable<XElement> query = GetElementByElementName(xmlBlock, elementName);
              retval = GetFirstElementBlock(query);
          }
          return retval;
      }

      /// <summary>
      /// Searches a block of XML for a given element, and returns a subset block of XML, including the given element and its child elements
      /// </summary>
      /// <param name="xmlBlock">The XML to search</param>
      /// <param name="elementName">The name of the element containing the desired block</param>
      /// <param name="n">For a repeating element, the nth occurrence of the element</param>
      /// <returns>A block of XML including the desired element and its children.  Returns an empty string if no matching element is found.</returns>
      public static String GetElementBlock(string xmlBlock, string elementName, int n)
      {
          string retval = string.Empty;
          if (IsValidXML(xmlBlock) && !String.IsNullOrEmpty(elementName))
          {
              IEnumerable<XElement> query = GetElementByElementName(xmlBlock, elementName);
              retval = GetNthElementBlock(query, n);
          }
          return retval;
      }

      /// <summary>
      /// Searches a block of XML for a given element and element value, and returns a subset block of XML, including the given element and its child elements
        /// </summary>
        /// <param name="xmlBlock">The XML to search</param>
      /// <param name="elementName">The name of the element containing the desired block</param>
      /// <param name="elementValue">The value of the element containing the desired block</param>
      /// <returns>A block of XML including the desired element and its children.  If the element name and value are not unique, the first occurrence is returned.  Returns an empty string if no matching element is found.</returns>
      public static String GetElementBlock(string xmlBlock, string elementName, string elementValue)
      {
          string retval = string.Empty;
          if (IsValidXML(xmlBlock) && !String.IsNullOrEmpty(elementName))
          {
              var query = GetElementByElementNameAndValue(xmlBlock, elementName, elementValue);
              retval = GetFirstElementBlock(query);
          }
          return retval;
      }

      /// <summary>
      /// Searches a block of XML for a given element and element value, and returns a subset block of XML, including the given element and its child elements
      /// </summary>
      /// <param name="xmlBlock">The XML to search</param>
      /// <param name="elementName">The name of the element containing the desired block</param>
      /// <param name="elementValue">The value of the element containing the desired block</param>
      /// <param name="n">For a repeating element, the nth occurrence of the element</param>
      /// <returns>A block of XML including the desired element and its children.  Returns an empty string if no matching element is found.</returns>
      public static String GetElementBlock(string xmlBlock, string elementName, string elementValue, int n)
      {
          string retval = string.Empty;
          if (IsValidXML(xmlBlock) && !String.IsNullOrEmpty(elementName))
          {
              IEnumerable<XElement> query = GetElementByElementNameAndValue(xmlBlock, elementName, elementValue);
              retval = GetNthElementBlock(query, n);
          }
          return retval;
      }

       /// <summary>
      /// Searches a block of XML for a given element, attribute, and attribute value, and returns a subset block of XML, including the given element and its child elements
       /// </summary>
       /// <param name="xmlBlock">The XML to search</param>
      /// <param name="elementName">The name of the element containing the desired block</param>
      /// <param name="attrName">The name of an attribute within the desired element containing the desired block</param>
      /// <param name="attrValue">The value of the attribute from the previous parameter within the desired element containing the desired block</param>
      /// <returns>A block of XML including the desired element and its children.  If the element name, attribute and attribute value are not unique, the first occurrence is returned.  Returns an empty string if no matching element is found.</returns>
      public static String GetElementBlock(string xmlBlock, string elementName, string attrName, string attrValue)
      {
          string retval = string.Empty;
          if (IsValidXML(xmlBlock) && !String.IsNullOrEmpty(elementName) && !String.IsNullOrEmpty(attrName))
          {
              IEnumerable<XElement> query = GetElementByElementNameAndAttribute(xmlBlock, elementName, attrName, attrValue);
              retval = GetFirstElementBlock(query);
          }
          return retval;
      }

      /// <summary>
      /// Searches a block of XML for a given element, attribute, and attribute value, and returns a subset block of XML, including the given element and its child elements
      /// </summary>
      /// <param name="xmlBlock">The XML to search</param>
      /// <param name="elementName">The name of the element containing the desired block</param>
      /// <param name="attrName">The name of an attribute within the desired element containing the desired block</param>
      /// <param name="attrValue">The value of the attribute from the previous parameter within the desired element containing the desired block</param>
      /// <param name="n">For a repeating element, the nth occurrence of the element</param>
      /// <returns>A block of XML including the desired element and its children.  Returns an empty string if no matching element is found.</returns>
      public static String GetElementBlock(string xmlBlock, string elementName, string attrName, string attrValue, int n)
      {
          string retval = string.Empty;
          if (IsValidXML(xmlBlock) && !String.IsNullOrEmpty(elementName) && !String.IsNullOrEmpty(attrName))
          {
              IEnumerable<XElement> query = GetElementByElementNameAndAttribute(xmlBlock, elementName, attrName, attrValue);
              retval = GetNthElementBlock(query, n);
          }
          return retval;
      }

       /// <summary>
      /// Searches a block of XML for a given element and its value, including an attribute and attribute value, and returns a subset block of XML, including the given element and its child elements
       /// </summary>
       /// <param name="xmlBlock">The XML to search</param>
      /// <param name="elementName">The name of the element containing the desired block</param>
      /// <param name="elementValue">The value of the named element containing the desired block</param>
      /// <param name="attrName">The name of an attribute within the desired element containing the desired block</param>
      /// <param name="attrValue">The value of the attribute from the previous parameter within the desired element containing the desired block</param>
      /// <returns>A block of XML including the desired element and its children.  If the element name and value, and the attribute name and value are not unique, the first occurrence is returned.  Returns an empty string if no matching element is found.</returns>
      public static String GetElementBlock(string xmlBlock, string elementName, string elementValue, string attrName, string attrValue)
      {
          string retval = string.Empty;
          if (IsValidXML(xmlBlock) && !String.IsNullOrEmpty(elementName) && !String.IsNullOrEmpty(attrName))
          {
              IEnumerable<XElement> query = GetElementByElementNameValueAndAttributeNameValue(xmlBlock, elementName, elementValue, attrName, attrValue);
              retval = GetFirstElementBlock(query);
          }
          return retval;
      }

      /// <summary>
      /// Searches a block of XML for a given element and its value, including an attribute and attribute value, and returns a subset block of XML, including the given element and its child elements
      /// </summary>
      /// <param name="xmlBlock">The XML to search</param>
      /// <param name="elementName">The name of the element containing the desired block</param>
      /// <param name="elementValue">The value of the named element containing the desired block</param>
      /// <param name="attrName">The name of an attribute within the desired element containing the desired block</param>
      /// <param name="attrValue">The value of the attribute from the previous parameter within the desired element containing the desired block</param>
      /// <param name="n">For a repeating element, the nth occurrence of the element</param>
      /// <returns>A block of XML including the desired element and its children.  Returns an empty string if no matching element is found.</returns>
      public static String GetElementBlock(string xmlBlock, string elementName, string elementValue, string attrName, string attrValue, int n)
      {
          string retval = string.Empty;
          if (IsValidXML(xmlBlock) && !String.IsNullOrEmpty(elementName) && !String.IsNullOrEmpty(attrName))
          {
              IEnumerable<XElement> query = GetElementByElementNameValueAndAttributeNameValue(xmlBlock, elementName, elementValue, attrName, attrValue);
              retval = GetNthElementBlock(query, n);
          }
          return retval;
      }
      #endregion

      #region GetElementDescendants Overloads
      /// <summary>
      /// Searches a block of XML for a given element, and returns a subset block of XML, containing the children of the given element
      /// </summary>
      /// <param name="xmlBlock">The XML to search</param>
      /// <param name="elementName">The name of the element for which the children are desired</param>
      /// <returns>A block of XML containing the children of the desired element.  If the element name is not unique, returns the children of the first occurrence.  Returns an empty string if no matching element is found.</returns>
      public static String GetElementDescendants(string xmlBlock, string elementName)
      {
          string retval = string.Empty;
          if (IsValidXML(xmlBlock) && !String.IsNullOrEmpty(elementName))
          {
              // need to check for the existence of the element name before going after the children,
              // otherwise the query will throw an exception if the element name isn't present in the XML
              IEnumerable<XElement> query = GetElementByElementName(xmlBlock, elementName);
              retval = GetFirstElementDescendants(query);
          }
          return retval;
      }

      /// <summary>
      /// Searches a block of XML for a given element, and returns a subset block of XML, containing the children of the given element
      /// </summary>
      /// <param name="xmlBlock">The XML to search</param>
      /// <param name="elementName">The name of the element for which the children are desired</param>
      /// <param name="n">For a repeating element, the nth occurrence of the element</param>
      /// <returns>A block of XML containing the children of the desired element.  If the element name is not unique, returns the children of the first occurrence.  Returns an empty string if no matching element is found.</returns>
      public static String GetElementDescendants(string xmlBlock, string elementName, int n)
      {
          string retval = string.Empty;
          if (IsValidXML(xmlBlock) && !String.IsNullOrEmpty(elementName))
          {
              // need to check for the existence of the element name before going after the children,
              // otherwise the query will throw an exception if the element name isn't present in the XML
              IEnumerable<XElement> query = GetElementByElementName(xmlBlock, elementName);
              retval = GetNthElementDescendants(query, n);
          }
          return retval;
      }

      //public static String GetElementDescendants(string xmlBlock, string elementName, string elementValue)
      //{
      //    // you cannot have child elements when an element has a value
      //    throw new NotImplementedException();
      //}

      /// <summary>
      /// Searches a block of XML for a given element, and returns a subset block of XML, containing the children of the given element
      /// </summary>
      /// <param name="xmlBlock">The XML to search</param>
      /// <param name="elementName">The name of the element containing the desired block</param>
      /// <param name="attrName">The name of an attribute within the desired element containing the desired block</param>
      /// <param name="attrValue">The value of the attribute from the previous parameter within the desired element containing the desired block</param>
      /// <returns>A block of XML including the desired element and its children.  If the element name, attribute and attribute value are not unique, returns the children of the first occurrence.  Returns an empty string if no matching element is found.</returns>
      public static String GetElementDescendants(string xmlBlock, string elementName, string attrName, string attrValue)
      {
          string retval = string.Empty;
          if (IsValidXML(xmlBlock) && !String.IsNullOrEmpty(elementName) && !String.IsNullOrEmpty(attrName))
          {
              IEnumerable<XElement> query = GetElementByElementNameAndAttribute(xmlBlock, elementName, attrName, attrValue);
              retval = GetFirstElementDescendants(query);
          }
          return retval;
      }

      /// <summary>
      /// Searches a block of XML for a given element, and returns a subset block of XML, containing the children of the given element
      /// </summary>
      /// <param name="xmlBlock">The XML to search</param>
      /// <param name="elementName">The name of the element containing the desired block</param>
      /// <param name="attrName">The name of an attribute within the desired element containing the desired block</param>
      /// <param name="attrValue">The value of the attribute from the previous parameter within the desired element containing the desired block</param>
      /// <param name="n">For a repeating element, the nth occurrence of the element</param>
      /// <returns>A block of XML including the desired element and its children.  If the element name, attribute and attribute value are not unique, returns the children of the first occurrence.  Returns an empty string if no matching element is found.</returns>
      public static String GetElementDescendants(string xmlBlock, string elementName, string attrName, string attrValue, int n)
      {
          string retval = string.Empty;
          if (IsValidXML(xmlBlock) && !String.IsNullOrEmpty(elementName) && !String.IsNullOrEmpty(attrName))
          {
              IEnumerable<XElement> query = GetElementByElementNameAndAttribute(xmlBlock, elementName, attrName, attrValue);
              retval = GetNthElementDescendants(query, n);
          }
          return retval;
      }

      //public static String GetElementDescendants(string xmlBlock, string elementName, string elementValue, string attrName, string attrValue)
      //{
      //    // you cannot have child elements when an element has a value
      //    throw new NotImplementedException();
      //}
        #endregion

      #region GetElementParent Overloads
      /// <summary>
      /// Searches a block of XML for a given element, and returns the parent element name
      /// </summary>
      /// <param name="xmlBlock">The XML to search</param>
      /// <param name="elementName">The name of the element for which the parent is desired</param>
      /// <returns>The name of the XML element that is the parent of the desired element.  If multiple elements have the same name, the parent element of the first occurrence is returned (they all have the same parent anyway).  Returns an empty string if the parameter element is the root of the XML block</returns>
      public static String GetElementParent(string xmlBlock, string elementName)
      {
          string retval = string.Empty;

          if (IsValidXML(xmlBlock) && !String.IsNullOrEmpty(elementName))
          {
              // find the node of interest first, then get its parent
              IEnumerable<XElement> els = GetElementByElementName(xmlBlock, elementName);
              retval = GetParent(els);
          }
          return retval;
      }

      /// <summary>
      /// Searches a block of XML for a given element, and returns the parent element
      /// </summary>
      /// <param name="xmlBlock">The XML to search</param>
      /// <param name="elementName">The name of the element for which the parent is desired</param>
      /// <param name="elementValue">The value of the element specified in the previous parameter</param>
      /// <returns>The name of the XML element that is the parent of the desired element.  If multiple elements have the same name, the parent element of the first occurrence is returned (they all have the same parent anyway).  Returns an empty string if the parameter element is the root of the XML block</returns>
      public static String GetElementParent(string xmlBlock, string elementName, string elementValue)
      {
          string retval = string.Empty;

          if (IsValidXML(xmlBlock) && !String.IsNullOrEmpty(elementName))
          {
              // find the node of interest first, then get its parent
              IEnumerable<XElement> els = GetElementByElementNameAndValue(xmlBlock, elementName, elementValue);
              retval = GetParent(els);
          }
          return retval;
      }

      /// <summary>
      /// Searches a block of XML for a given element, and returns the parent element
      /// </summary>
      /// <param name="xmlBlock">The XML to search</param>
      /// <param name="elementName">The name of the element for which the parent is desired</param>
      /// <param name="attrName">The name of an attribute within the desired element</param>
      /// <param name="attrValue">The value of the attribute from the previous parameter</param>
      /// <returns>The name of the XML element that is the parent of the desired element.  If multiple elements have the same name, the parent element of the first occurrence is returned (they all have the same parent anyway).  Returns an empty string if the parameter element is the root of the XML block</returns>
      public static String GetElementParent(string xmlBlock, string elementName, string attrName, string attrValue)
      {
          string retval = string.Empty;

          if (IsValidXML(xmlBlock) && !String.IsNullOrEmpty(elementName))
          {
              // find the node of interest first, then get its parent
              IEnumerable<XElement> els = GetElementByElementNameAndAttribute(xmlBlock, elementName, attrName, attrValue);
              retval = GetParent(els);
          }
          return retval;
      }

      /// <summary>
      /// Searches a block of XML for a given element, and returns the parent element
      /// </summary>
      /// <param name="xmlBlock">The XML to search</param>
      /// <param name="elementName">The name of the element containing the desired block</param>
      /// <param name="elementValue">The value of the named element for which the parent is desired</param>
      /// <param name="attrName">The name of an attribute within the desired element</param>
      /// <param name="attrValue">The value of the attribute from the previous parameter</param>
      /// <returns>The name of the XML element that is the parent of the desired element.  If multiple elements have the same name, the parent element of the first occurrence is returned (they all have the same parent anyway).  Returns an empty string if the parameter element is the root of the XML block</returns>
      public static String GetElementParent(string xmlBlock, string elementName, string elementValue, string attrName, string attrValue)
      {
          string retval = string.Empty;

          if (IsValidXML(xmlBlock) && !String.IsNullOrEmpty(elementName))
          {
              // find the node of interest first, then get its parent
              IEnumerable<XElement> els = GetElementByElementNameValueAndAttributeNameValue(xmlBlock, elementName, elementValue, attrName, attrValue);
              retval = GetParent(els);
          }
          return retval;
      }
      #endregion

      #region GetElementPreviousSibling Overloads
      /// <summary>
       /// Searches a block of XML for a previous sibling to a specified element
       /// </summary>
       /// <param name="xmlBlock">The XML to search</param>
       /// <param name="elementName">The name of the element for which the previous sibling is desired</param>
       /// <returns>XML element that is the previous sibling to the parameter element.  If the desired element is a repeating element, the previous sibling of the first occurrence of the element is returned.  Returns an empty string if there is no previous sibling</returns>
      public static String GetElementPreviousSibling(string xmlBlock, string elementName)
      {
          string retval = string.Empty;
          if (IsValidXML(xmlBlock) && !String.IsNullOrEmpty(elementName))
          {
              IEnumerable<XElement> query = GetElementByElementName(xmlBlock, elementName);
              retval = GetFirstElementPreviousSibling(query);
          }
          return retval;
      }

      /// <summary>
      /// Searches a block of XML for a previous sibling to a specified element
      /// </summary>
      /// <param name="xmlBlock">The XML to search</param>
      /// <param name="elementName">The name of the element for which the previous sibling is desired</param>
      /// <param name="n">If the desired element is a repeating element, find the previous sibling of the nth occurrence</param>
      /// <returns>XML element that is the previous sibling to the parameter element.  Returns an empty string if there is no previous sibling</returns>
      public static String GetElementPreviousSibling(string xmlBlock, string elementName, int n)
      {
          string retval = string.Empty;
          if (IsValidXML(xmlBlock) && !String.IsNullOrEmpty(elementName))
          {
              IEnumerable<XElement> query = GetElementByElementName(xmlBlock, elementName);
              retval = GetNthElementPreviousSibling(query, n);
          }
          return retval;
      }

      /// <summary>
      /// Searches a block of XML for a previous sibling to a specified element
       /// </summary>
      /// <param name="xmlBlock">The XML to search</param>
      /// <param name="elementName">The name of the element for which the previous sibling is desired</param>
      /// <param name="elementValue">The value of the element for which the previous sibling is desired</param>
      /// <returns>XML element that is the previous sibling to the parameter element.  Returns an empty string if there is no previous sibling</returns>
      public static String GetElementPreviousSibling(string xmlBlock, string elementName, string elementValue)
      {
          string retval = string.Empty;

          if (IsValidXML(xmlBlock) && !String.IsNullOrEmpty(elementName))
          {
              IEnumerable<XElement> query = GetElementByElementNameAndValue(xmlBlock, elementName, elementValue);
              retval = GetFirstElementPreviousSibling(query);
          }
          return retval;
      }

      /// <summary>
      /// Searches a block of XML for a previous sibling to a specified element
      /// </summary>
      /// <param name="xmlBlock">The XML to search</param>
      /// <param name="elementName">The name of the element for which the previous sibling is desired</param>
      /// <param name="elementValue">The value of the element for which the previous sibling is desired</param>
      /// <param name="n">If the desired element is a repeating element, find the previous sibling of the nth occurrence</param>
      /// <returns>XML element that is the previous sibling to the parameter element.  Returns an empty string if there is no previous sibling</returns>
      public static String GetElementPreviousSibling(string xmlBlock, string elementName, string elementValue, int n)
      {
          string retval = string.Empty;

          if (IsValidXML(xmlBlock) && !String.IsNullOrEmpty(elementName))
          {
              IEnumerable<XElement> query = GetElementByElementNameAndValue(xmlBlock, elementName, elementValue);
              retval = GetNthElementPreviousSibling(query, n);
          }
          return retval;
      }

       /// <summary>
      /// Searches a block of XML for a previous sibling to a specified element
       /// </summary>
      /// <param name="xmlBlock">The XML to search</param>
      /// <param name="elementName">The name of the element for which the previous sibling is desired</param>
       /// <param name="attrName">The name of an attribute of the element for which the sibling is desired</param>
      /// <param name="attrValue">The value of the attribute in the previous parameter</param>
      /// <returns>XML element that is the previous sibling to the parameter element.  Returns an empty string if there is no previous sibling</returns>
      public static String GetElementPreviousSibling(string xmlBlock, string elementName, string attrName, string attrValue)
      {
          string retval = string.Empty;

          if (IsValidXML(xmlBlock) && !String.IsNullOrEmpty(elementName))
          {
              IEnumerable<XElement> query = GetElementByElementNameAndAttribute(xmlBlock, elementName, attrName, attrValue);
              retval = GetFirstElementPreviousSibling(query);
          }
          return retval;
      }

      /// <summary>
      /// Searches a block of XML for a previous sibling to a specified element
      /// </summary>
      /// <param name="xmlBlock">The XML to search</param>
      /// <param name="elementName">The name of the element for which the previous sibling is desired</param>
      /// <param name="attrName">The name of an attribute of the element for which the sibling is desired</param>
      /// <param name="attrValue">The value of the attribute in the previous parameter</param>
      /// <param name="n">If the desired element is a repeating element, find the previous sibling of the nth occurrence</param>
      /// <returns>XML element that is the previous sibling to the parameter element.  Returns an empty string if there is no previous sibling</returns>
      public static String GetElementPreviousSibling(string xmlBlock, string elementName, string attrName, string attrValue, int n)
      {
          string retval = string.Empty;

          if (IsValidXML(xmlBlock) && !String.IsNullOrEmpty(elementName))
          {
              IEnumerable<XElement> query = GetElementByElementNameAndAttribute(xmlBlock, elementName, attrName, attrValue);
              retval = GetNthElementPreviousSibling(query, n);
          }
          return retval;
      }

      /// <summary>
      /// Searches a block of XML for a previous sibling to a specified element
       /// </summary>
      /// <param name="xmlBlock">The XML to search</param>
      /// <param name="elementName">The name of the element for which the previous sibling is desired</param>
      /// <param name="elementValue">The value of the element for which the previous sibling is desired</param>
      /// <param name="attrName">The name of an attribute of the element for which the sibling is desired</param>
      /// <param name="attrValue">The value of the attribute in the previous parameter</param>
      /// <returns>XML element that is the previous sibling to the parameter element.  Returns an empty string if there is no previous sibling</returns>
      public static String GetElementPreviousSibling(string xmlBlock, string elementName, string elementValue, string attrName, string attrValue)
      {
          string retval = string.Empty;

          if (IsValidXML(xmlBlock) && !String.IsNullOrEmpty(elementName))
          {
              IEnumerable<XElement> query = GetElementByElementNameValueAndAttributeNameValue(xmlBlock, elementName, elementValue, attrName, attrValue);
              retval = GetFirstElementPreviousSibling(query);
          }
          return retval;
      }

      /// <summary>
      /// Searches a block of XML for a previous sibling to a specified element
      /// </summary>
      /// <param name="xmlBlock">The XML to search</param>
      /// <param name="elementName">The name of the element for which the previous sibling is desired</param>
      /// <param name="elementValue">The value of the element for which the previous sibling is desired</param>
      /// <param name="attrName">The name of an attribute of the element for which the sibling is desired</param>
      /// <param name="attrValue">The value of the attribute in the previous parameter</param>
      /// <param name="n">If the desired element is a repeating element, find the previous sibling of the nth occurrence</param>
      /// <returns>XML element that is the previous sibling to the parameter element.  Returns an empty string if there is no previous sibling</returns>
      public static String GetElementPreviousSibling(string xmlBlock, string elementName, string elementValue, string attrName, string attrValue, int n)
      {
          string retval = string.Empty;

          if (IsValidXML(xmlBlock) && !String.IsNullOrEmpty(elementName))
          {
              IEnumerable<XElement> query = GetElementByElementNameValueAndAttributeNameValue(xmlBlock, elementName, elementValue, attrName, attrValue);
              retval = GetNthElementPreviousSibling(query, n);
          }
          return retval;
      }
      #endregion

      #region GetElementNextSibling Overloads
      /// <summary>
      /// Searches a block of XML for a next sibling to a specified element
      /// </summary>
      /// <param name="xmlBlock">The XML to search</param>
      /// <param name="elementName">The name of the element for which the next sibling is desired</param>
      /// <returns>XML element that is the next sibling to the parameter element.  If the desired element is a repeating element, the next sibling of the first occurrence of the element is returned.  Returns an empty string if there is no next sibling</returns>
      public static String GetElementNextSibling(string xmlBlock, string elementName)
      {
          string retval = string.Empty;
          if (IsValidXML(xmlBlock) && !String.IsNullOrEmpty(elementName))
          {
              IEnumerable<XElement> query = GetElementByElementName(xmlBlock, elementName);
              retval = GetFirstElementNextSibling(query);
          }
          return retval;
      }

      /// <summary>
      /// Searches a block of XML for a next sibling to a specified element
      /// </summary>
      /// <param name="xmlBlock">The XML to search</param>
      /// <param name="elementName">The name of the element for which the next sibling is desired</param>
      /// <param name="n">If the desired element is a repeating element, find the next sibling of the nth occurrence</param>
      /// <returns>XML element that is the next sibling to the parameter element.  Returns an empty string if there is no next sibling</returns>
      public static String GetElementNextSibling(string xmlBlock, string elementName, int n)
      {
          string retval = string.Empty;
          if (IsValidXML(xmlBlock) && !String.IsNullOrEmpty(elementName))
          {
              IEnumerable<XElement> query = GetElementByElementName(xmlBlock, elementName);
              retval = GetNthElementNextSibling(query, n);
          }
          return retval;
      }

      /// <summary>
      /// Searches a block of XML for a next sibling to a specified element
      /// </summary>
      /// <param name="xmlBlock">The XML to search</param>
      /// <param name="elementName">The name of the element for which the next sibling is desired</param>
      /// <param name="elementValue">The value of the element for which the next sibling is desired</param>
      /// <returns>XML element that is the next sibling to the parameter element.  Returns an empty string if there is no next sibling</returns>
      public static String GetElementNextSibling(string xmlBlock, string elementName, string elementValue)
      {
          string retval = string.Empty;

          if (IsValidXML(xmlBlock) && !String.IsNullOrEmpty(elementName))
          {
              IEnumerable<XElement> query = GetElementByElementNameAndValue(xmlBlock, elementName, elementValue);
              retval = GetFirstElementNextSibling(query);
          }
          return retval;
      }

      /// <summary>
      /// Searches a block of XML for a next sibling to a specified element
      /// </summary>
      /// <param name="xmlBlock">The XML to search</param>
      /// <param name="elementName">The name of the element for which the next sibling is desired</param>
      /// <param name="elementValue">The value of the element for which the next sibling is desired</param>
      /// <param name="n">If the desired element is a repeating element, find the next sibling of the nth occurrence</param>
      /// <returns>XML element that is the next sibling to the parameter element.  Returns an empty string if there is no next sibling</returns>
      public static String GetElementNextSibling(string xmlBlock, string elementName, string elementValue, int n)
      {
          string retval = string.Empty;

          if (IsValidXML(xmlBlock) && !String.IsNullOrEmpty(elementName))
          {
              IEnumerable<XElement> query = GetElementByElementNameAndValue(xmlBlock, elementName, elementValue);
              retval = GetNthElementNextSibling(query, n);
          }
          return retval;
      }

      /// <summary>
      /// Searches a block of XML for a next sibling to a specified element
      /// </summary>
      /// <param name="xmlBlock">The XML to search</param>
      /// <param name="elementName">The name of the element for which the next sibling is desired</param>
      /// <param name="attrName">The name of an attribute of the element for which the sibling is desired</param>
      /// <param name="attrValue">The value of the attribute in the next parameter</param>
      /// <returns>XML element that is the next sibling to the parameter element.  Returns an empty string if there is no next sibling</returns>
      public static String GetElementNextSibling(string xmlBlock, string elementName, string attrName, string attrValue)
      {
          string retval = string.Empty;

          if (IsValidXML(xmlBlock) && !String.IsNullOrEmpty(elementName))
          {
              IEnumerable<XElement> query = GetElementByElementNameAndAttribute(xmlBlock, elementName, attrName, attrValue);
              retval = GetFirstElementNextSibling(query);
          }
          return retval;
      }

      /// <summary>
      /// Searches a block of XML for a next sibling to a specified element
      /// </summary>
      /// <param name="xmlBlock">The XML to search</param>
      /// <param name="elementName">The name of the element for which the next sibling is desired</param>
      /// <param name="attrName">The name of an attribute of the element for which the sibling is desired</param>
      /// <param name="attrValue">The value of the attribute in the next parameter</param>
      /// <param name="n">If the desired element is a repeating element, find the next sibling of the nth occurrence</param>
      /// <returns>XML element that is the next sibling to the parameter element.  Returns an empty string if there is no next sibling</returns>
      public static String GetElementNextSibling(string xmlBlock, string elementName, string attrName, string attrValue, int n)
      {
          string retval = string.Empty;

          if (IsValidXML(xmlBlock) && !String.IsNullOrEmpty(elementName))
          {
              IEnumerable<XElement> query = GetElementByElementNameAndAttribute(xmlBlock, elementName, attrName, attrValue);
              retval = GetNthElementNextSibling(query, n);
          }
          return retval;
      }

      /// <summary>
      /// Searches a block of XML for a next sibling to a specified element
      /// </summary>
      /// <param name="xmlBlock">The XML to search</param>
      /// <param name="elementName">The name of the element for which the next sibling is desired</param>
      /// <param name="elementValue">The value of the element for which the next sibling is desired</param>
      /// <param name="attrName">The name of an attribute of the element for which the sibling is desired</param>
      /// <param name="attrValue">The value of the attribute in the next parameter</param>
      /// <returns>XML element that is the next sibling to the parameter element.  Returns an empty string if there is no next sibling</returns>
      public static String GetElementNextSibling(string xmlBlock, string elementName, string elementValue, string attrName, string attrValue)
      {
          string retval = string.Empty;

          if (IsValidXML(xmlBlock) && !String.IsNullOrEmpty(elementName))
          {
              IEnumerable<XElement> query = GetElementByElementNameValueAndAttributeNameValue(xmlBlock, elementName, elementValue, attrName, attrValue);
              retval = GetFirstElementNextSibling(query);
          }
          return retval;
      }

      /// <summary>
      /// Searches a block of XML for a next sibling to a specified element
      /// </summary>
      /// <param name="xmlBlock">The XML to search</param>
      /// <param name="elementName">The name of the element for which the next sibling is desired</param>
      /// <param name="elementValue">The value of the element for which the next sibling is desired</param>
      /// <param name="attrName">The name of an attribute of the element for which the sibling is desired</param>
      /// <param name="attrValue">The value of the attribute in the next parameter</param>
      /// <param name="n">If the desired element is a repeating element, find the next sibling of the nth occurrence</param>
      /// <returns>XML element that is the next sibling to the parameter element.  Returns an empty string if there is no next sibling</returns>
      public static String GetElementNextSibling(string xmlBlock, string elementName, string elementValue, string attrName, string attrValue, int n)
      {
          string retval = string.Empty;

          if (IsValidXML(xmlBlock) && !String.IsNullOrEmpty(elementName))
          {
              IEnumerable<XElement> query = GetElementByElementNameValueAndAttributeNameValue(xmlBlock, elementName, elementValue, attrName, attrValue);
              retval = GetNthElementNextSibling(query, n);
          }
          return retval;
      }
      #endregion

      #region GetElementAttrs Overloads
      /// <summary>
       /// Searches a block of XML for a given element and return all the attribute name/value pairs in that element
       /// </summary>
       /// <param name="xmlBlock">The XML to search</param>
       /// <param name="elementName">The name of the element for which the attribute name/value pairs are desired</param>
       /// <returns>A list of attribute name/value pairs from the desired element.  If there is more than one occurrence of the element, the attributes from the first occurrence are returned.  Returns null if no matching element is found</returns>
      public static List<DictionaryEntry> GetElementAttrs(string xmlBlock, string elementName)
      {
          IEnumerable<XElement> query = GetElementByElementName(xmlBlock, elementName);
          return GetFirstElementAttributes(query);
      }

      /// <summary>
      /// Searches a block of XML for a given element and return all the attribute name/value pairs in that element
      /// </summary>
      /// <param name="xmlBlock">The XML to search</param>
      /// <param name="elementName">The name of the element for which the attribute name/value pairs are desired</param>
      /// <param name="n">If the desired element is a repeating element, get the attribute name/value pairs from the nth occurrence</param>
      /// <returns>A list of attribute name/value pairs from the desired element.  If there is more than one occurrence of the element, the attributes from the first occurrence are returned.  Returns null if no matching element is found</returns>
      public static List<DictionaryEntry> GetElementAttrs(string xmlBlock, string elementName, int n)
      {
          IEnumerable<XElement> query = GetElementByElementName(xmlBlock, elementName);
          return GetNthElementAttributes(query, n);
      }

      /// <summary>
      /// Searches a block of XML for a given element and return all the attribute name/value pairs in that element
       /// </summary>
      /// <param name="xmlBlock">The XML to search</param>
      /// <param name="elementName">The name of the element for which the attribute name/value pairs are desired</param>
       /// <param name="elementValue">The value of the element for which the attribute name/value pairs are desired</param>
      /// <returns>A list of attribute name/value pairs from the desired element.  If there is more than one occurrence of the element, the attributes from the first occurrence are returned.  Returns null if no matching element is found</returns>
      public static List<DictionaryEntry> GetElementAttrs(string xmlBlock, string elementName, string elementValue)
      {
          IEnumerable<XElement> query = GetElementByElementNameAndValue(xmlBlock, elementName, elementValue);
          return GetFirstElementAttributes(query);
      }

      /// <summary>
      /// Searches a block of XML for a given element and return all the attribute name/value pairs in that element
      /// </summary>
      /// <param name="xmlBlock">The XML to search</param>
      /// <param name="elementName">The name of the element for which the attribute name/value pairs are desired</param>
      /// <param name="elementValue">The value of the element for which the attribute name/value pairs are desired</param>
      /// <param name="n">If the desired element is a repeating element, get the attribute name/value pairs from the nth occurrence</param>
      /// <returns>A list of attribute name/value pairs from the desired element.  If there is more than one occurrence of the element, the attributes from the first occurrence are returned.  Returns null if no matching element is found</returns>
      public static List<DictionaryEntry> GetElementAttrs(string xmlBlock, string elementName, string elementValue, int n)
      {
          IEnumerable<XElement> query = GetElementByElementNameAndValue(xmlBlock, elementName, elementValue);
          return GetNthElementAttributes(query, n);
      }
      #endregion

      /// <summary>
       /// Determines if a given XML block conforms to a given schema definition
       /// </summary>
       /// <param name="xmlBlock">The XML to check</param>
       /// <param name="xmlSchema">The schema definition to check the XML against</param>
       /// <param name="targetNamespace">The target namespace in the schema.  Use an empty string if there is no target namespace.</param>
       /// <returns>True if the XML conforms to the schema definition, false otherwise</returns>
      public static bool ValidateAgainstSchema( string xmlBlock, string xmlSchema, string targetNamespace )
      {
          if (XMLServices.IsValidXML(xmlBlock))
          {
              XmlReader schemaReader = XmlReader.Create(new StringReader(xmlSchema));
              XmlReader blockReader = XmlReader.Create(new StringReader(xmlBlock));
              XmlSchemaSet schemas = new XmlSchemaSet();
              bool errors = false;
              try
              {
                  schemas.Add(targetNamespace, schemaReader);
                  XDocument doc = XDocument.Load(blockReader);
                  doc.Validate(schemas, (o, e) =>
                      {
                          errors = true;
                      });
              }
              catch (Exception e)
              {
                  Console.WriteLine(e.Message);  // to avoid a compiler warning about unused variable "e"
                  errors = true;
              }
              return !errors;
          }
          else
          {
              return false;
          }
      }

      /// <summary>
      /// Converts a block of XML to an equivalent object
      /// </summary>
      /// <param name="xmlBlock">The XML to convert</param>
      /// <param name="t">The type of the object that will contain the converted XML content</param>
      /// <returns>An object of the type specifiec in the 2nd parameter containing the converted XML content, null if there was any deserialization failure</returns>
      public static object DeserializeToObject( string xmlBlock, Type t )
      {
          if (IsValidXML(xmlBlock))
          {
              XmlSerializer ser = new XmlSerializer(t);
              try
              {
                  Object obj = ser.Deserialize(new StringReader(xmlBlock));
                  return obj;
              }
              catch (Exception e)
              {
                  Console.WriteLine(e.Message);  // used to avoid compiler warnings
                  return null;
              }
          }
          else
              return null;
      }
        /// <summary>
        /// Serializes the specified value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="Exception">An error occurred</exception>
        public static string Serialize<T>(this T value)
        {
            if (value == null)
            {
                return string.Empty;
            }
            try
            {
                var xmlserializer = new XmlSerializer(typeof(T));
                var stringWriter = new StringWriter();
                using (var writer = XmlWriter.Create(stringWriter))
                {
                    xmlserializer.Serialize(writer, value);
                    return stringWriter.ToString();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred serializing the object to XML", ex);
            }
        }
        /// <summary>
        /// Serializes the specified value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <param name="stringWriter">The string writer.</param>
        /// <param name="xsns">The XML serializer namespace.</param>
        /// <returns></returns>
        /// <exception cref="Exception">An error occurred</exception>
        public static string Serialize<T>(this T value, StringWriter stringWriter, XmlSerializerNamespaces xsns)
        {
            if (value == null)
            {
                return string.Empty;
            }
            try
            {
                var xmlserializer = new XmlSerializer(typeof(T));
                using (var writer = XmlWriter.Create(stringWriter))
                {
                    xmlserializer.Serialize(writer, value, xsns);
                    return stringWriter.ToString();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred serializing the object to XML", ex);
            }
        }
        /// <summary>
        /// Serializes the specified value with empty namespace.
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="Exception">An error occurred</exception>
        public static string SerializeWithUTF8AndEmptyNamespace<T>(this T value)
        {
            var emptyNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            var stringWriter = new Utf8StringWriter(false);
            return Serialize<T>(value, stringWriter, emptyNamespaces);
        }
        /// <summary>
        /// Checks to see if the XML passed in is well-formed
        /// </summary>
        /// <param name="xmlBlock">A block of XML text</param>
        /// <returns>True, if the passed in XML block is well-formed, otherwise false</returns>
        public static bool IsValidXML(string xmlBlock)
        {
            try
            {
                // Check we actually have a value
                if ( !string.IsNullOrEmpty(xmlBlock))
                {
                    // Try to load the value into a document
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(xmlBlock);
                    // If we managed with no exception then this is valid XML!
                    return true;
                }
                else
                {
                    // A blank value is not valid xml
                    return false;
                }
            }
            catch ( System.Xml.XmlException )
            {
                return false;
            }
        }    
    }

    /// <summary>
    /// UTF-8 string wrinter
    /// </summary>
    public class Utf8StringWriter : StringWriter
    {
        private Encoding utf8Encoding;

        /// <summary>
        /// Constructor
        /// </summary>
        public Utf8StringWriter(bool shouldEmitUtf8Identifier)
        {
            utf8Encoding = new UTF8Encoding(shouldEmitUtf8Identifier);
        }
        /// <summary>
        /// Use UTF8 encoding but write no BOM to the wire
        /// </summary>
        public override Encoding Encoding
        {
            get { return utf8Encoding; }
        }
    }
}
