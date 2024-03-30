using Bluesalt.Infrastructure.Context;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Nin.Application.Common.Interface;
using Nin.Application.Common.Model.Bluesalt.Request;
using Nin.Application.Common.Model.Bluesalt.Resources;
using Nin.Application.Common.Responses.BluesaltResponse;
using Nin.Domain.Enums;
using Nin.Shared.LogService;
using NLog;
using System.Net;
using System.Text;

namespace Nin.Infrastructure.Services.BluesaltService
{
    public class NinValidationService : IBluesaltServiceInterface
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _config;
        private readonly ILogWritter _logger;

        private readonly NinValidationContext _db;

        private static Logger log = LogManager.GetCurrentClassLogger();

        public NinValidationService(IHttpClientFactory httpClientFactory, IConfiguration config, ILogWritter logger, NinValidationContext db)
        {
            _client = httpClientFactory.CreateClient();
            _config = config;
            _logger = logger;
            _db = db;
        }




        public async Task<NinResponse> NinValidation(NinValidationResource ra)
        {
            var endpointname = "NinValidation";
            var data = new NinValidationRequest
            {
                phone_number = ra.phone_number,
                nin_number = ra.nin_number,
            };
            return await SendAsync<NinValidationRequest, NinResponse>(
                data, "/NIN", BluesaltHttpMethodtype.Post, endpointname);
        }



        public async Task<NinPhResponse> NinPhoneValidation(NinPhoneResource ra)
        {
            var endpointname = "NinPhoneValidation";
            var data = new NinPhoneRequest
            {
                phone_number = ra.phone_number,
            };
            return await SendAsync<NinPhoneRequest, NinPhResponse>(
                data, "/pNIN", BluesaltHttpMethodtype.Post, endpointname);
        }

        public async Task<NinPhoneBasicResponse> NinPhoneValidationBasic(NinPhonebasicValidationResource ra)
        {
            var endpointname = "NinPhoneValidationBasic";
            var data = new NinPhonebasicRequest
            {
                phone_number = ra.phone_number,
            };
            return await SendAsync<NinPhonebasicRequest, NinPhoneBasicResponse>(
              data, "/pNIN", BluesaltHttpMethodtype.Post, endpointname);
        }











        public async Task<U> SendAsync<T, U>(T payload, string relativePath, BluesaltHttpMethodtype httpMethod, string endpointname)
        {
            var baseaddress = _config.GetSection("BaseAddress").Value.ToString();
            var testkey = _config.GetSection("ApiKey").Value.ToString();
            var clientid = _config.GetSection("clientid").Value.ToString();
            var appname = _config.GetSection("appname").Value.ToString();

            _client.DefaultRequestHeaders.Add("clientid", clientid);
            _client.DefaultRequestHeaders.Add("appname", appname);
            _client.DefaultRequestHeaders.Add("apikey", testkey);

            var message = new StringContent(System.Text.Json.JsonSerializer.Serialize(payload), Encoding.UTF8, "multipart/form-data");
            HttpResponseMessage response = new HttpResponseMessage();
            string content;
            switch (httpMethod)
            {
                case BluesaltHttpMethodtype.Post:
                    var resp = await _client.PostAsync($"{baseaddress}{relativePath}", message);
                    content = await resp.Content.ReadAsStringAsync();
                    log.Info("Message: " + content + Environment.NewLine + Environment.NewLine + endpointname + "Endpoint: " + baseaddress + relativePath + Environment.NewLine + payload + Environment.NewLine + Environment.NewLine + "ApiKey: " + testkey + Environment.NewLine + _client.Timeout + Environment.NewLine + DateTime.Now);

                    try
                    {
                        DateTime requestTime;
                        DateTime responseTime;
                        tblRequestandResponseLogs requestForDb = new tblRequestandResponseLogs();

                        if (resp.IsSuccessStatusCode)
                        {
                            requestTime = DateTime.Now;
                            responseTime = DateTime.Now;
                            requestForDb = new tblRequestandResponseLogs() { RequestType = endpointname, RequestPayload = JsonConvert.SerializeObject(payload), Response = JsonConvert.SerializeObject(content), RequestTimestamp = DateTime.Now, ResponseTimestamp = DateTime.Now, RequestUrl = baseaddress + relativePath };
                            _db.tblRequestAndResponse.Add(requestForDb);
                            await _db.SaveChangesAsync();
                            if (content == null)
                            {
                                throw new Exception("Something went wrong" + content);
                            }
                            return JsonConvert.DeserializeObject<U>(content);
                        }
                        else
                        {
                            if (resp.StatusCode == HttpStatusCode.BadGateway || resp.StatusCode == HttpStatusCode.Unauthorized || resp.StatusCode == HttpStatusCode.BadRequest || resp.StatusCode == HttpStatusCode.ServiceUnavailable || resp.StatusCode == HttpStatusCode.InternalServerError || resp.StatusCode == HttpStatusCode.NotFound || resp.StatusCode == HttpStatusCode.Forbidden)
                            {
                                responseTime = DateTime.Now;
                                requestForDb = new tblRequestandResponseLogs() { RequestType = endpointname, RequestPayload = JsonConvert.SerializeObject(payload), Response = JsonConvert.SerializeObject(content), RequestTimestamp = responseTime, RequestUrl = baseaddress + relativePath };
                                _db.tblRequestAndResponse.Add(requestForDb);
                                await _db.SaveChangesAsync();
                                if (content == null)
                                {
                                    throw new Exception("Something went wrong" + content);
                                }
                                return JsonConvert.DeserializeObject<U>(content);


                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Something went wrong", ex);
                    }
                    if (content == null)
                    {
                        throw new Exception("Something went wrong" + content);
                    }
                    return JsonConvert.DeserializeObject<U>(content);






                default:

                    var ressp = await _client.GetAsync($"{baseaddress}{relativePath}");
                    content = await ressp.Content.ReadAsStringAsync();
                    log.Info("Message: " + content + Environment.NewLine + Environment.NewLine + endpointname + "Endpoint: " + baseaddress + relativePath + Environment.NewLine + payload + Environment.NewLine + Environment.NewLine + "ApiKey: " + testkey + Environment.NewLine + _client.Timeout + Environment.NewLine + DateTime.Now);


                    try
                    {
                        DateTime requestTime;
                        DateTime responseTime;
                        tblRequestandResponseLogs requestDb = new tblRequestandResponseLogs();

                        if (ressp.IsSuccessStatusCode)
                        {
                            requestTime = DateTime.Now;
                            responseTime = DateTime.Now;
                            requestDb = new tblRequestandResponseLogs() { RequestType = endpointname, RequestPayload = JsonConvert.SerializeObject(payload), Response = JsonConvert.SerializeObject(content), RequestTimestamp = DateTime.Now, ResponseTimestamp = DateTime.Now, RequestUrl = baseaddress + relativePath };
                            _db.tblRequestAndResponse.Add(requestDb);
                            await _db.SaveChangesAsync();
                        }
                        else
                        {
                            if (ressp.StatusCode == HttpStatusCode.BadGateway || ressp.StatusCode == HttpStatusCode.Unauthorized || ressp.StatusCode == HttpStatusCode.BadRequest || ressp.StatusCode == HttpStatusCode.ServiceUnavailable || ressp.StatusCode == HttpStatusCode.InternalServerError || ressp.StatusCode == HttpStatusCode.NotFound || ressp.StatusCode == HttpStatusCode.Forbidden)
                            {
                                responseTime = DateTime.Now;
                                requestDb = new tblRequestandResponseLogs() { RequestType = endpointname, RequestPayload = JsonConvert.SerializeObject(payload), Response = JsonConvert.SerializeObject(content), RequestTimestamp = responseTime, RequestUrl = baseaddress + relativePath };
                                _db.tblRequestAndResponse.Add(requestDb);
                                await _db.SaveChangesAsync();
                                if (content == null)
                                {
                                    throw new Exception("Something went wrong" + content);
                                }
                                return JsonConvert.DeserializeObject<U>(content);


                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Something went wrong", ex);
                    }
                    if (content == null)
                    {
                        throw new Exception("Something went wrong" + content);
                    }
                    return JsonConvert.DeserializeObject<U>(content);


            }

        }


    }
}
