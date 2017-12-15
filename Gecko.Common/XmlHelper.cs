using System;
using System.Collections.Generic;
using System.Text;

using System.Xml;
using System.IO;


namespace Gecko.Common
{
    /// <summary>
    /// XML Helper扩展
    /// </summary>
    public class XmlHelper
    {
        private XmlDocument doc = new XmlDocument();

        public XmlDocument Doc
        {
            get { return doc; }
        }

        #region load
        /// <summary>
        /// 加载XML文件，如果不存在则创建
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="rootName"></param>
        /// <param name="encoding"></param>
        /// <param name="standalone"></param>
        public void LoadXml(string fileName, string rootName, string encoding, string standalone)
        {
            if (!File.Exists(fileName))
            {
                //不存在，则创建

                XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", encoding, standalone);
                doc.InsertBefore(xmlDeclaration, doc.DocumentElement);
                XmlElement root = doc.CreateElement(rootName);
                doc.AppendChild(root);
                doc.Save(fileName);
            }

            doc.Load(fileName);
        }


        /// <summary>
        /// 加载XML文件通过string
        /// </summary>
        /// <param name="xmlContent"></param>
        public void LoadXml(string xmlContent)
        {
            doc.LoadXml(xmlContent);
        }
        #endregion

        #region SelectSingleNode

        /// <summary>
        /// 返回匹配xpath表达式的第一个 XmlNode
        /// </summary>
        /// <param name="xpath"></param>
        /// <returns></returns>
        public XmlNode SelectSingleNode(string xpath)
        {
            VerifyParameters(xpath);

            return doc.SelectSingleNode(xpath);
        }
        #endregion

        #region SelectNodes

        /// <summary>
        /// 返回匹配xpath表达式的节点列表
        /// </summary>
        /// <param name="xpath"></param>
        /// <returns></returns>
        public XmlNodeList SelectNodes(string xpath)
        {
            VerifyParameters(xpath);

            XmlNodeList xmlNodeList = doc.SelectNodes(xpath);
            return xmlNodeList;
        }

        #endregion

        #region AddNode(parentNode,name,value)

        /// <summary>
        /// 添加并返回新节点
        /// </summary>
        /// <param name="parentNode"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public XmlNode AddNode(string xpath, string name, string value)
        {
            XmlNode parentNode = this.SelectSingleNode(xpath);
            XmlElement node = doc.CreateElement(name);
            node.InnerText = value;
            parentNode.AppendChild(node);

            return node;
        }
        //
        public XmlNode AddNode(XmlNode parentNode, string name, string value)
        {
            //XmlNode parentNode = this.SelectSingleNode(xpath);
            XmlElement node = doc.CreateElement(name);
            node.InnerText = value;
            parentNode.AppendChild(node);

            return node;
        }

        //
        public XmlNode AddNode(string xpath, string nodeName, string nodeValue, string attributeID, string attributeID_Value)
        {
            //#1 check if exist
            string xpathCheck = string.Format(xpath + @"/{0}[@{1}=""{2}""]", nodeName, attributeID, attributeID_Value);
            XmlNode nodeCheck = SelectSingleNode(xpathCheck);
            if (nodeCheck == null)
            {
                //#2 add
                XmlNode parentNode = this.SelectSingleNode(xpath);
                XmlElement node = doc.CreateElement(nodeName);
                node.InnerText = nodeValue;
                bool isSuccess = CreateAttribute(node, attributeID, attributeID_Value);
                if (isSuccess)
                {
                    parentNode.AppendChild(node);
                    return node;
                }
            }

            return null;

        }

        #endregion

        #region Set or Get Attribute

        /// <summary>
        /// 生成属性节点
        /// </summary>
        /// <param name="node"></param>
        /// <param name="attributeName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool CreateAttribute(XmlNode node, string attributeName, string value)
        {
            bool success = false;

            XmlAttribute xmlAttribute = doc.CreateAttribute(attributeName);
            xmlAttribute.Value = value;

            node.Attributes.Append(xmlAttribute);

            success = true;

            return success;
        }

        /// <summary>
        /// 为节点添加属性
        /// </summary>
        /// <param name="node"></param>
        /// <param name="attributeName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool SetAttributeValue(XmlNode node, string attributeName, string value)
        {
            bool success = false;
            if (node != null)
            {
                XmlAttribute attribute = node.Attributes[attributeName];
                if (attribute != null)
                {
                    attribute.Value = value;
                    success = true;
                }
            }
            return success;
        }
        /// <summary>
        /// 返回指定节点的属性,null表示节点不存在
        /// </summary>
        /// <param name="node"></param>
        /// <param name="attributeName"></param>
        /// <returns></returns>
        public string GetAttributeValue(XmlNode node, string attributeName)
        {
            string value = null;
            if (node != null)
            {
                XmlAttribute attribute = node.Attributes[attributeName];
                if (attribute != null)
                {
                    value = attribute.Value;
                }
            }
            return value;
        }
        /// <summary>
        /// 返回指定节点的属性
        /// </summary>
        /// <param name="node"></param>
        /// <param name="attributeName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool GetAttributeValue(XmlNode node, string attributeName, ref string value)
        {
            bool success = false;
            if (node != null)
            {
                XmlAttribute attribute = node.Attributes[attributeName];
                if (attribute != null)
                {
                    value = attribute.Value;
                    success = true;
                }
            }
            return success;
        }
        #endregion

        public bool GetNodeValue(string xpath, string nodeName, ref string value)
        {
            bool success = false;
            VerifyParameters(xpath);

            XmlNode node = SelectSingleNode(xpath);

            if (node != null && node[nodeName] != null)
            {
                value = node[nodeName].InnerText;
                success = true;
            }
            return success;
        }

        #region save

        /// <summary>
        /// 保存XML
        /// </summary>
        /// <param name="fileName"></param>
        public void SaveXml(string fileName)
        {
            doc.Save(fileName);
        }

        #endregion

        /// <summary>
        /// 验证参数
        /// </summary>
        /// <param name="xpath"></param>
        private void VerifyParameters(string xpath)
        {
            if (doc == null)
            {
                throw new Exception("doc cannot be null.");
            }
            if (doc.LastChild.GetType() == typeof(System.Xml.XmlDeclaration))
            {
                throw new Exception("XmlDocument requires at least the a root node");
            }
            if (xpath == null)
            {
                throw (new ArgumentNullException("path cannot be null."));
            }
        }


        public static string SelectNodeInnerText(XmlNode parent, string xpath)
        {
            var node = parent.SelectSingleNode(xpath);

            return node == null ? string.Empty : node.InnerText;
        }

    }
}

