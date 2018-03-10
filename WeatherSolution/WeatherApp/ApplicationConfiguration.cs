using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace WeatherApp
{
    public class ApplicationConfiguration
    {    
        public string BaseAddressAPI { get; set; }
        public string ConnectionString { get; set; }
        public string Query { get; set; }
        public string APIKey { get; set; }
        private string Directory { get; set; }
        public string AppsEmail { get; set; }
        public string AppsMailPassword { get; set; }

        /// <summary>
        /// Initialize ApplicationConfiguration
        /// </summary>
        public void Initialize() {
            this.BaseAddressAPI = "http://api.openweathermap.org/data/2.5/forecast?q=";
            this.Query = "&mode=json&units=metric&APPID=";
            this.APIKey = "d49c3a5910fdc77e3a554cd0cd11681d";
            this.ConnectionString = "ConnectionString";
            this.AppsEmail = "Email that app use for sending confirmation emails";
            this.AppsMailPassword = "Password for that email service";
            this.Save();
        }

        /// <summary>
        /// Load parameters from xml file
        /// </summary>
        /// <param name="fileName"></param>
        public void Load(string fileName = "ApplicationConfiguration.xml") {
            this.Directory = System.AppDomain.CurrentDomain.BaseDirectory;
            try
            {
                if (File.Exists(this.Directory + fileName))
                {
                    using (var stream = File.OpenRead(this.Directory + fileName))
                    {
                        var serializer = new XmlSerializer(typeof(ApplicationConfiguration));
                        ApplicationConfiguration config = serializer.Deserialize(stream) as ApplicationConfiguration;
                        this.BaseAddressAPI = config.BaseAddressAPI;
                        this.Query = config.Query;
                        this.APIKey = config.APIKey;
                        this.ConnectionString = config.ConnectionString;
                        this.AppsMailPassword = config.AppsMailPassword;
                        this.AppsEmail = config.AppsEmail;
                        stream.Flush();
                    }
                }
                else
                {
                    Initialize();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Save initiaziled params to xml file
        /// </summary>
        /// <param name="filename"></param>
        public void Save(string filename = "ApplicationConfiguration.xml") {
            try
            {
                using (var writer = new StreamWriter( this.Directory + filename))
                {
                    var serializer = new XmlSerializer(typeof(ApplicationConfiguration));
                    serializer.Serialize(writer, this);
                    writer.Flush();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}