using Microsoft.AspNetCore.Mvc;
using yatracub.Models;
using yatracub.Services.Interface;

namespace yatracub.Controllers
{
    public class bookingController : BaseController
    {
        private readonly IbookingHistoryservice _bookinghistoryservice;
        public bookingController(IbookingHistoryservice bookinghistoryservice)
        {
            _bookinghistoryservice = bookinghistoryservice;
        }
        [HttpPost("saveUpdateBooking")]
        public IActionResult saveUpdateBookings(bookinghistory booking)
        {
            try
            {
                var result = _bookinghistoryservice.saveUpdateBookings(booking);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
