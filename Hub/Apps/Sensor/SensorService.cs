﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.Threading;
using System.ServiceModel.Web;
using HomeOS.Hub.Platform.Views;
using HomeOS.Hub.Common;

namespace HomeOS.Hub.Apps.Sensor
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class SensorService : ISensorContract
    {
        protected VLogger logger;
        Sensor SensorInfo;

        public SensorService(VLogger logger, Sensor SensorStuff)
        {
            this.logger = logger;
            this.SensorInfo = SensorStuff;
        }

        public List<string> GetReceivedMessages()
        {
            List<string> retVal = new List<string>();
            try
            {
                retVal = SensorInfo.GetReceivedMessages();
            }
            catch (Exception e)
            {
                logger.Log("Got exception in GetReceivedMessages: " + e);
            }
            return retVal;
        }

        public List<string> GetReceivedMessages_get()
        {
            List<string> retVal = new List<string>();
            try
            {
                retVal = SensorInfo.GetReceivedMessages();
            }
            catch (Exception e)
            {
                logger.Log("Got exception in GetReceivedMessages: " + e);
            }
            return retVal;
        }
    }

     [ServiceContract]
    public interface ISensorContract
    {
        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        List<string> GetReceivedMessages();
        [OperationContract]
        [WebInvoke(Method = "GET", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json, UriTemplate = "/get")]
        List<string> GetReceivedMessages_get();


    }
}