using Amazon.Runtime;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sns_test.Services
{
    public class ServiceAWSSNS
    {
        private readonly IConfiguration _configuration;
        public ServiceAWSSNS(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public async Task EnviarSNS(string num, int code)
        {
            //  Recuperamos las credenciales de nuestro usuario aws
            string awsKeyId = this._configuration.GetConnectionString("awsKeyId").ToString();
            string awsKeySecret = this._configuration.GetConnectionString("awsKeySecret").ToString();
            var awsCredentials = new BasicAWSCredentials(awsKeyId, awsKeySecret);
            //  Creamos el cliente sns
            AmazonSimpleNotificationServiceClient snsClient = new AmazonSimpleNotificationServiceClient(awsCredentials, Amazon.RegionEndpoint.USEast1);
            //  Creamos la peticion con los datos necesarios y publicamos la peticion
            PublishRequest pubReq = new PublishRequest();
            pubReq.Message = "Your code is "+code.ToString();
            pubReq.PhoneNumber = num;
            pubReq.MessageAttributes.Add("AWS.SNS.SMS.SMSType", new MessageAttributeValue { StringValue = "Transactional", DataType = "String" });
            PublishResponse pubRes = await snsClient.PublishAsync(pubReq);
        }
    }
}
