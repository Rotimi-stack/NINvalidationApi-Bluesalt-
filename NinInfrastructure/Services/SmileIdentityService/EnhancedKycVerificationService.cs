using Bluesalt.Infrastructure.Context;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Nin.Application.Common.Model.SmileIdentity;
using Nin.Application.Common.Responses.SmileResponses;
using Nin.Application.Common.SmileIdentity.Interface;
using Nin.Domain.Enums;
using Nin.Shared.LogService;
using NLog;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace Nin.Infrastructure.Services.SmileIdentityService
{
    public class EnhancedKycVerificationService : EnhancedKycVerificationInterface
    {
        private readonly HttpClient _client;

        private readonly IConfiguration _config;
        private readonly ILogWritter _logger;
        private readonly NinValidationContext _db;

        private static Logger log = LogManager.GetCurrentClassLogger();

        public EnhancedKycVerificationService(IHttpClientFactory httpClientFactory, IConfiguration config, ILogWritter logger, NinValidationContext db)
        {
            _client = httpClientFactory.CreateClient();
            _config = config;
            _logger = logger;
            _db = db;
        }
        public async Task<EnhancedKYCResponse> CallBackUrl(CallbackUrlResource kyc)
        {
            var data = new CallbackUrlRequest
            {
                success = kyc.success,
            };
            return await SendAsync<CallbackUrlRequest, EnhancedKYCResponse>(
                data, "", EnhancedKYCVerificationHttpMethodType.Post);
        }

        public async Task<EnhancedKYCResponse> GetEnhancedKYCVerification(EnhancedKycVerificationResources kyc)
        {
            //string generateSignature = new SignatureGenerator(_config).GenerateSignature();
            //string generatetimeStamp = new SignatureGenerator(_config).TimestampGenerator();
            //var timestamp = DateTime.UtcNow.ToString("yyyy-MM-dd'T'HH:mm:ss.fffK", System.Globalization.CultureInfo.InvariantCulture);


            var ApiKey = _config.GetSection("ApiKey").Value.ToString();
            var PartneriD = _config.GetSection("PartnerId").Value.ToString();


            string timeStamp = DateTime.UtcNow.ToString("yyyy-MM-dd'T'HH:mm:ss.fffK", System.Globalization.CultureInfo.InvariantCulture);
            string apiKey = ApiKey;
            string partnerID = PartneriD;
            string datas = timeStamp + partnerID + "sid_request";

            UTF8Encoding utf8 = new UTF8Encoding();
            byte[] key = utf8.GetBytes(apiKey);
            byte[] message = utf8.GetBytes(datas);

            HMACSHA256 hash = new HMACSHA256(key);
            var signature = hash.ComputeHash(message);

            string result = Convert.ToBase64String(signature);
            var data = new EnhancedKycVerificationRequest
            {
                callback_url = kyc.callback_url,
                country = kyc.country,
                dob = kyc.dob,
                first_name = kyc.first_name,
                id_number = kyc.id_number,
                id_type = kyc.id_type,
                last_name = kyc.last_name,
                partner_id = kyc.partner_id,
                partner_params = kyc.partner_params,
                bank_code = kyc.bank_code,
                signature = result,
                source_sdk = kyc.source_sdk,
                source_sdk_version = kyc.source_sdk_version,
                timestamp = timeStamp,
            };
            return await SendAsync<EnhancedKycVerificationRequest, EnhancedKYCResponse>(
                data, "", EnhancedKYCVerificationHttpMethodType.Post
                );
        }

        public async Task<U> SendAsync<T, U>(T payload, string relativePath, EnhancedKYCVerificationHttpMethodType httpMethod)
        {
            var baseaddress = _config.GetSection("BaseUrl").Value.ToString();
            var apiKey = _config.GetSection("ApiKey").Value.ToString();


            var message = new StringContent(System.Text.Json.JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
            HttpResponseMessage response = new HttpResponseMessage();
            string content;
            switch (httpMethod)
            {
                case EnhancedKYCVerificationHttpMethodType.Post:
                    var resp = await _client.PostAsync($"{baseaddress}{relativePath}", message);
                    content = await resp.Content.ReadAsStringAsync();
                    log.Info("Message: " + content + Environment.NewLine + Environment.NewLine + "Endpoint: " + baseaddress + relativePath + Environment.NewLine + payload + Environment.NewLine + Environment.NewLine + "ApiKey: " + apiKey + Environment.NewLine + _client.Timeout + Environment.NewLine + DateTime.Now);

                    try
                    {
                        DateTime requestTime;
                        DateTime responseTime;
                        SmiletblRequestandResponseLogs requestForDb = new SmiletblRequestandResponseLogs();

                        if (resp.IsSuccessStatusCode)
                        {
                            requestTime = DateTime.Now;
                            responseTime = DateTime.Now;
                            requestForDb = new SmiletblRequestandResponseLogs() { RequestType = "EnhancedKycVerification", RequestPayload = JsonConvert.SerializeObject(payload), Response = JsonConvert.SerializeObject(content), RequestTimestamp = DateTime.Now, ResponseTimestamp = DateTime.Now, RequestUrl = baseaddress };
                            _db.SmiletblRequestAndResponse.Add(requestForDb);
                            await _db.SaveChangesAsync();
                        }
                        else
                        {
                            if (resp.StatusCode == HttpStatusCode.BadGateway || resp.StatusCode == HttpStatusCode.Unauthorized || resp.StatusCode == HttpStatusCode.BadRequest || resp.StatusCode == HttpStatusCode.ServiceUnavailable || resp.StatusCode == HttpStatusCode.InternalServerError || resp.StatusCode == HttpStatusCode.NotFound || resp.StatusCode == HttpStatusCode.Forbidden)
                            {
                                responseTime = DateTime.Now;
                                requestForDb = new SmiletblRequestandResponseLogs() { RequestType = "EnhancedKycVerification", RequestPayload = JsonConvert.SerializeObject(payload), Response = JsonConvert.SerializeObject(content), RequestTimestamp = responseTime, RequestUrl = baseaddress };
                                _db.SmiletblRequestAndResponse.Add(requestForDb);
                                await _db.SaveChangesAsync();
                                return JsonConvert.DeserializeObject<U>(content);

                                //return JsonSerializer.Deserialize<U>(content);

                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Something went wrong", ex);
                    }

                    return JsonConvert.DeserializeObject<U>(content);


                default:

                    var ressp = await _client.GetAsync($"{baseaddress}{relativePath}");
                    content = await ressp.Content.ReadAsStringAsync();
                    log.Info("Message: " + content + Environment.NewLine + Environment.NewLine + "Endpoint: " + baseaddress + relativePath + Environment.NewLine + payload + Environment.NewLine + Environment.NewLine + "ApiKey: " + Environment.NewLine + _client.Timeout + Environment.NewLine + DateTime.Now);


                    if (ressp.StatusCode == HttpStatusCode.BadGateway || ressp.StatusCode == HttpStatusCode.Unauthorized || ressp.StatusCode == HttpStatusCode.BadRequest || ressp.StatusCode == HttpStatusCode.ServiceUnavailable || ressp.StatusCode == HttpStatusCode.InternalServerError || ressp.StatusCode == HttpStatusCode.NotFound)
                    {
                        return JsonConvert.DeserializeObject<U>(content);

                    }
                    return JsonConvert.DeserializeObject<U>(content);


            }

        }
    }
}
