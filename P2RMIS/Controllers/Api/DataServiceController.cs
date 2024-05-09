using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Sra.P2rmis.Web.Common;
using Sra.P2rmis.Bll.Setup;
using System.Text;
using Sra.P2rmis.CrossCuttingServices;
using System.Diagnostics;

namespace Sra.P2rmis.Web.Controllers.Api
{
    public class DataServiceController : ApiController
    {
        /// <summary>
        /// The no content message
        /// </summary>
        protected string NoContentMessage = @"Response contains no data.";
        protected int CpritClientId = 9;
        /// <summary>
        /// Gets or sets the setup service.
        /// </summary>
        /// <value>
        /// The setup service.
        /// </value>
        protected ISetupService theSetupService { get; set; }

        public DataServiceController(ISetupService theSetupService)
        {
            this.theSetupService = theSetupService;
        }

        /// <summary>
        /// The web service deliverables.
        /// </summary>
        /// <param name="fiscalYear">The fiscal year.</param>
        /// <param name="program">The program.</param>
        /// <param name="receiptCycle">The receipt cycle.</param>
        /// <param name="deliverableKey">The deliverable identifier.</param>
        /// <returns></returns>
        /// <remarks>
        /// Old format: FiscalYear=2017&Program=LCRP&ReceiptCycle=1&DeliverableType=3
        /// New format: FiscalYear=2017&Program=LCRP&ReceiptCycle=1&DeliverableType=3
        /// /dataservice/wsdeliverables?FiscalYear=2013&Program=BCRP&ReceiptCycle=1&DeliverableKey=3
        /// </remarks>
        [Route("dataservice/wsdeliverables")]
        [BasicAuthentication]
        [HttpGet]
        public HttpResponseMessage WSDeliverables(string fiscalYear, string program, int deliverableKey, int? receiptCycle = null)
        {
            var paras = String.Format("Fiscal year: {0}; " +
                "Program: {1}; " +
                "Deliverable key: {2}; " +
                "Receipt cycle: {3}",
                fiscalYear, program, deliverableKey, receiptCycle);
            Trace.WriteLine(paras);
            HttpResponseMessage response = default(HttpResponseMessage);
            try
            {
                var result = theSetupService.GetDeliverableXml(fiscalYear, program, receiptCycle, deliverableKey);
                Trace.WriteLine(result);
                if (!string.IsNullOrEmpty(result) && XMLServices.IsValidXML(result))
                {
                    response = Request.CreateResponse(HttpStatusCode.OK, "value");
                    response.Content = new StringContent(result, Encoding.UTF8, "application/xml");
                }
                else
                {
                    var msg = !string.IsNullOrEmpty(result) ? result : NoContentMessage;
                    Trace.WriteLine(msg);
                    HttpError err = new HttpError(msg);
                    response = Request.CreateResponse(HttpStatusCode.OK, err);
                }
                Trace.WriteLine(response);
            }
            catch(Exception ex)
            {
                Trace.WriteLine(ex.Message);
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return response;
        }
        /// <summary>
        /// Get assignments
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        /// <returns></returns>
        /// <remarks>
        /// /dataservice/getassignments?clientId=9
        /// </remarks>
        [Route("dataservice/getassignments")]
        [BasicAuthentication]
        [HttpGet]
        public IHttpActionResult GetAssignments(int clientId)
        {
            var result = theSetupService.GetPeerReviewDataXml(clientId);
            if (result != null)
                return Ok(result);
            else
                return Content(HttpStatusCode.BadRequest, NoContentMessage);
        }

        /// <summary>
        /// Get assignments
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        /// <returns></returns>
        /// <remarks>
        /// /dataservice/getassignments?clientId=9
        /// </remarks>
        [Route("dataservice/getcpritassignments")]
        [BasicAuthentication]
        [HttpGet]
        public IHttpActionResult GetCpritAssignments()
        {
            return GetAssignments(CpritClientId);
        }
    }
}
