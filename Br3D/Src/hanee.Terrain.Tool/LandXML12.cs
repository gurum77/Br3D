/* 
 Licensed under the Apache License, Version 2.0

 http://www.apache.org/licenses/LICENSE-2.0
 */
using System;
using System.Xml.Serialization;
using System.Collections.Generic;
namespace hanee.Terrain.Exchange
{
	[XmlRoot(ElementName = "Metric", Namespace = "http://www.landxml.org/schema/LandXML-1.2")]
	public class Metric
	{
		[XmlAttribute(AttributeName = "areaUnit")]
		public string AreaUnit { get; set; }
		[XmlAttribute(AttributeName = "linearUnit")]
		public string LinearUnit { get; set; }
		[XmlAttribute(AttributeName = "volumeUnit")]
		public string VolumeUnit { get; set; }
		[XmlAttribute(AttributeName = "temperatureUnit")]
		public string TemperatureUnit { get; set; }
		[XmlAttribute(AttributeName = "pressureUnit")]
		public string PressureUnit { get; set; }
	}

	[XmlRoot(ElementName = "Units", Namespace = "http://www.landxml.org/schema/LandXML-1.2")]
	public class Units
	{
		[XmlElement(ElementName = "Metric", Namespace = "http://www.landxml.org/schema/LandXML-1.2")]
		public Metric Metric { get; set; }
	}

	[XmlRoot(ElementName = "Project", Namespace = "http://www.landxml.org/schema/LandXML-1.2")]
	public class Project
	{
		[XmlAttribute(AttributeName = "name")]
		public string Name { get; set; }
		[XmlAttribute(AttributeName = "desc")]
		public string Desc { get; set; }
	}

	[XmlRoot(ElementName = "Application", Namespace = "http://www.landxml.org/schema/LandXML-1.2")]
	public class Application
	{
		[XmlAttribute(AttributeName = "name")]
		public string Name { get; set; }
		[XmlAttribute(AttributeName = "version")]
		public string Version { get; set; }
		[XmlAttribute(AttributeName = "manufacturer")]
		public string Manufacturer { get; set; }
		[XmlAttribute(AttributeName = "manufacturerURL")]
		public string ManufacturerURL { get; set; }
	}

	[XmlRoot(ElementName = "P", Namespace = "http://www.landxml.org/schema/LandXML-1.2")]
	public class P
	{
		[XmlAttribute(AttributeName = "id")]
		public string Id { get; set; }
		[XmlText]
		public string Text { get; set; }
	}

	[XmlRoot(ElementName = "Pnts", Namespace = "http://www.landxml.org/schema/LandXML-1.2")]
	public class Pnts
	{
		[XmlElement(ElementName = "P", Namespace = "http://www.landxml.org/schema/LandXML-1.2")]
		public List<P> P { get; set; }
	}

	[XmlRoot(ElementName = "Faces", Namespace = "http://www.landxml.org/schema/LandXML-1.2")]
	public class Faces
	{
		[XmlElement(ElementName = "F", Namespace = "http://www.landxml.org/schema/LandXML-1.2")]
		public List<string> F { get; set; }
	}

	[XmlRoot(ElementName = "Definition", Namespace = "http://www.landxml.org/schema/LandXML-1.2")]
	public class Definition
	{
		[XmlElement(ElementName = "Pnts", Namespace = "http://www.landxml.org/schema/LandXML-1.2")]
		public Pnts Pnts { get; set; }
		[XmlElement(ElementName = "Faces", Namespace = "http://www.landxml.org/schema/LandXML-1.2")]
		public Faces Faces { get; set; }
		[XmlAttribute(AttributeName = "surfType")]
		public string SurfType { get; set; }
	}

	[XmlRoot(ElementName = "Surface", Namespace = "http://www.landxml.org/schema/LandXML-1.2")]
	public class Surface
	{
		[XmlElement(ElementName = "SourceData", Namespace = "http://www.landxml.org/schema/LandXML-1.2")]
		public string SourceData { get; set; }
		[XmlElement(ElementName = "Definition", Namespace = "http://www.landxml.org/schema/LandXML-1.2")]
		public Definition Definition { get; set; }
		[XmlAttribute(AttributeName = "name")]
		public string Name { get; set; }
	}

	[XmlRoot(ElementName = "Surfaces", Namespace = "http://www.landxml.org/schema/LandXML-1.2")]
	public class Surfaces
	{
		[XmlElement(ElementName = "Surface", Namespace = "http://www.landxml.org/schema/LandXML-1.2")]
		public Surface Surface { get; set; }
	}

	[XmlRoot(ElementName = "LandXML", Namespace = "http://www.landxml.org/schema/LandXML-1.2")]
	public class LandXML
	{
		[XmlElement(ElementName = "Units", Namespace = "http://www.landxml.org/schema/LandXML-1.2")]
		public Units Units { get; set; }
		[XmlElement(ElementName = "Project", Namespace = "http://www.landxml.org/schema/LandXML-1.2")]
		public Project Project { get; set; }
		[XmlElement(ElementName = "Application", Namespace = "http://www.landxml.org/schema/LandXML-1.2")]
		public Application Application { get; set; }
		[XmlElement(ElementName = "Surfaces", Namespace = "http://www.landxml.org/schema/LandXML-1.2")]
		public Surfaces Surfaces { get; set; }
		[XmlAttribute(AttributeName = "schemaLocation", Namespace = "http://www.w3.org/2001/XMLSchema-instance")]
		public string SchemaLocation { get; set; }
		[XmlAttribute(AttributeName = "xmlns")]
		public string Xmlns { get; set; }
		[XmlAttribute(AttributeName = "xsi", Namespace = "http://www.w3.org/2000/xmlns/")]
		public string Xsi { get; set; }
		[XmlAttribute(AttributeName = "version")]
		public string Version { get; set; }
		[XmlAttribute(AttributeName = "date")]
		public string Date { get; set; }
		[XmlAttribute(AttributeName = "time")]
		public string Time { get; set; }
	}

}
