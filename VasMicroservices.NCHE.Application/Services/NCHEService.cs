using CommonLibraries.Application.Services;
using Flurl.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VasMicroservices.NCHE.Application.Constants;
using VasMicroservices.NCHE.Application.DTOs;
using VasMicroservices.NCHE.Application.Resources.Requests;
using VasMicroservices.NCHE.Application.Resources.Responses;
using VasMicroservices.NCHE.Application.Settings;
using VasMicroservices.NCHE.Infra.Data.Contexts;
using VasMicroservices.NCHE.Infra.Data.Entities;

namespace VasMicroservices.NCHE.Application.Services
{
    public class NCHEService : INCHEService
    {
        private readonly INCHESettings _settings;
        private readonly IApiRequestService _requestService;
        private readonly IService<EndpointLog, EndpointLogDto, NCHEPaymentsContext> _logService;
        

        public NCHEService(INCHESettings settings, IApiRequestService requestService,
            IService<EndpointLog, EndpointLogDto, NCHEPaymentsContext> logService)
        {
            _settings = settings;
            _requestService = requestService;
            _logService = logService;
        }

        public async Task<PaymentResponse> PostPaymentAsync(PaymentRequest request)
        {
            var url = $"{_settings.Url}";
            var log = new EndpointLog();
            try
            {
                log.RequestTime = DateTime.Now;
                var response = await _requestService.PostAsync<PaymentResponse>(url,
                    new Dictionary<string, string> {
                           { "Content-Type", "application/json" },
                           { "Accept", "application/json" },
                           {"Authorization", $"Bearer {_settings.AuthToken}" }


                        }
                    , request, async (req, resp, code) =>
                    {
                        await Task.Run(() =>
                        {
                            log.RawRequest = req;
                            log.RawResponse = resp;
                            log.RequestUrl = url;
                            log.Code = code;
                            log.ResponseTime = DateTime.Now;
                        });

                    });
                if (response.statusCode == 200 || response.statusCode == 201)
                {
                    log.Success = response.result.Status == Codes.SUCCESSFUL ? 1 : 0;
                    log.Message = $"transaction has been successfully created with status {response.result.Status} and message: {response.result.Message}";
                }
                else
                {
                   
                     log.Success = 0;
                     log.Message = $"Status Code {response.statusCode} with status {response.result.Status} and message: {response.result.Message}"; ;
                    
                }
                await _logService.CreateAsync(log);
                return response.result;
            }
            catch (FlurlHttpException ex)
            {
                log.RawResponse = ex.Message;
                log.RequestUrl = url;
                log.ResponseTime = DateTime.Now;
                log.Success = 0;
                log.RawRequest = Newtonsoft.Json.JsonConvert.SerializeObject(request);
                log.Code = ex.StatusCode;
                switch (ex.StatusCode)
                {
                    case 409:
                        log.Message = "a dublicate receipt record was sent";
                        break;
                    default:
                        log.Message = "Something went wrong";
                        break;
                }
                await _logService.CreateAsync(log);
                throw ex;
            }
            catch (Exception ex)
            {
                log.RawResponse = ex.Message;
                log.RequestUrl = url;
                log.ResponseTime = DateTime.Now;
                log.Success = 0;
                log.RawRequest = Newtonsoft.Json.JsonConvert.SerializeObject(request);
                log.Message = "Something went wrong";
                await _logService.CreateAsync(log);
                throw ex;
            }

        }
        public async Task<Transaction> ValidateInvoiceAsync(string invoiceNumber)
        {

            var url = $"{_settings.Url}?invoice_number={invoiceNumber}";
            var log = new EndpointLog();
            try
            {
                var response = await _requestService.GetAsync<ValidationResponse>(url,
                   new Dictionary<string, string> {
                       { "Content-Type", "application/json" },
                       { "Accept", "application/json" },
                       {"Authorization", $"Bearer {_settings.AuthToken}" }

                   },
                   async (req, resp, code) =>
                   {
                       await Task.Run(() =>
                       {
                           log.RawRequest = req;
                           log.RawResponse = resp;
                           log.RequestUrl = url;
                           log.Code = code;
                           log.ResponseTime = DateTime.Now;
                       });

                   }
                 );
                if (response.statusCode == 200)
                {
                    log.Success = 1;
                    log.Message = response.result.CandidateExists ? $"The request was successful. Invoice {invoiceNumber} found" : $"The request was successful but invoice {invoiceNumber} was not found";
                }
               
                else
                {
                    log.Success = 0;
                    log.Message = "Something went wrong while processing your request";

                }
                await _logService.CreateAsync(log);
                return response.result.Transaction;

            }
            catch (FlurlHttpException ex)
            {
                log.RawResponse = ex.Message;
                log.RequestUrl = url;
                log.ResponseTime = DateTime.Now;
                log.Success = 0;
                log.RawRequest = "";
                log.Code = ex.StatusCode;
                switch (ex.StatusCode)
                {
                    case 204:
                        log.Message = $"Invoice Number {invoiceNumber} not found";
                        break;
                    default:
                        log.Message = "Something went wrong";
                        break;
                }
                await _logService.CreateAsync(log);
                throw ex;
            }
            catch (Exception ex)
            {
                log.RawResponse = ex.Message;
                log.RequestUrl = url;
                log.ResponseTime = DateTime.Now;
                log.Success = 0;
                log.RawRequest = "";
                log.Message = "Something went wrong";
                await _logService.CreateAsync(log);
                throw ex;

            }
        }
    }
}
