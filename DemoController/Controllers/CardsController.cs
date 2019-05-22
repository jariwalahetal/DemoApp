using Demo.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DemoController.Controllers
{
    public class CardsController : ApiController
    {
        public IEnumerable<Card> Get()
        {
            return CardContext.GetCards();
        }

        
        public IEnumerable<Card> Get(string pincode)
        {
            return CardContext.GetCardsByPinCodeAsync(pincode);
        }
        
        [HttpPost]
        public void Add(Card c)
        {
            if (c == null)
                throw new Exception("card cannot be null");
            CardContext.AddCard(c);
        }

        
        public void Delete(Card c)
        {

            CardContext.DeleteCard(c.UnitId);
        }
    }
}
