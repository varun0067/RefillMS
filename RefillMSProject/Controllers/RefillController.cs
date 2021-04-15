using Microsoft.AspNetCore.Mvc;
using RefillMSProject.Models;
using RefillMSProject.RefillRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RefillMSProject.RefillService;
using RefillMSProject.DTO;
using Microsoft.AspNetCore.Authorization;
using log4net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RefillMSProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RefillController : ControllerBase
    {
        private IRefillService _refillService;
        private readonly ILog _log4net = LogManager.GetLogger(typeof(RefillController));

        public RefillController(IRefillService refillService)
        {
            _refillService = refillService;
        }

        [HttpPost("CreateRefill")]
        public ActionResult CreateRefill([FromBody] RefillOrderDTO refillOrder)
        {
            _log4net.Info("Refill MicroService : " + nameof(CreateRefill));
            try
            {
                bool created = _refillService.CreateRefillOrders(refillOrder);
                if (created)
                    return Ok();
                else
                    return BadRequest("Cannot Create Refill orders");
            }
            catch (Exception e)
            {
                _log4net.Error("Exception Occured : " + e.Message + " from " + nameof(CreateRefill));
                return BadRequest("Exception Occured");
            }
        }

        [HttpGet("viewRefillStatus/{subscriptionId}")]
        public ActionResult ViewRefillStatus(int subscriptionId)
        {
            _log4net.Info("Refill MicroService : " + nameof(ViewRefillStatus));
            try
            {
                List<RefillOrder> refillStatus = _refillService.GetRefillOrders(subscriptionId);
                if (refillStatus == null)
                {
                    return NoContent();
                }
                else
                {
                    return Ok(refillStatus);
                }
            }
            catch (Exception e)
            {
                _log4net.Error("Exception Occured : " + e.Message + " from " + nameof(ViewRefillStatus));
                return BadRequest("Exception Occured");
            }
        }
        [HttpGet("getRefillDuesAsOfDate/{subId}/{fromDate}")]
        public ActionResult GetRefillDuesAsOfDate(int subId, DateTime fromDate)
        {
            _log4net.Info("Refill MicroService : " + nameof(GetRefillDuesAsOfDate));
            try
            {
                List<RefillOrder> refillStatus = _refillService.GetRefillDuesAsOfDate(subId, fromDate);
                if (refillStatus == null)
                {
                    return NoContent();
                }
                else
                {
                    return Ok(refillStatus);
                }
            }
            catch (Exception e)
            {
                _log4net.Error("Exception Occured : " + e.Message + " from " + nameof(GetRefillDuesAsOfDate));
                return BadRequest("Exception Occured");
            }
        }
        [HttpPost("requestAdhocRefill/{subscriptionId}/{refillId}")]
        public ActionResult RequestAdhocRefill(int subscriptionId, int refillId)
        {
            _log4net.Info("Refill MicroService : " + nameof(RequestAdhocRefill));
            try
            {
                bool requested = _refillService.RequestAdhocRefill(subscriptionId, refillId);
                if (requested == true)
                    return Ok();
                else
                    return BadRequest("Could not complete Payment");
            }
            catch (Exception e)
            {
                _log4net.Error("Exception Occured : " + e.Message + " from " + nameof(RequestAdhocRefill));
                return BadRequest("Exception Occured");
            }
        }

        [HttpGet("checkPendingPaymentStatus/{subscriptionId}")]
        public ActionResult CheckPendingPaymentStatus(int subscriptionId)
        {
            _log4net.Info("Refill MicroService : " + nameof(CheckPendingPaymentStatus));
            try
            {
                bool pendingStatus = _refillService.CheckPendingPaymentStatus(subscriptionId);
                if (pendingStatus == true)
                    return Ok();
                else
                    return BadRequest("Please Complete Payment for pending requests");
            }
            catch (Exception e)
            {
                _log4net.Error("Exception Occured : " + e.Message + " from " + nameof(CheckPendingPaymentStatus));
                return BadRequest("Exception Occured");
            }
        }
    }
}
