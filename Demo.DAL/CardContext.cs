using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL
{
    public class CardContext : DbContext
    {
        public DbSet<Card> Cards { get; set; }

        public static bool AddCard(Card c)
        {
            using (var context = new CardContext())
            {
                using (DbContextTransaction transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        context.Cards.Add(c);

                        context.SaveChanges();

                        transaction.Commit();

                        return true;

                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }

        public static bool DeleteCard(string UnitId)
        {

            using (var context = new CardContext())
            {
                using (DbContextTransaction transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var cards = context.Cards.Where(c => c.UnitId == UnitId).ToList();
                        if (cards == null || cards.Count == 0)
                            throw new Exception("No card found with '" + UnitId + "' unitid");
                        context.Cards.RemoveRange(cards);
                        context.SaveChanges();
                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }

        public static IEnumerable<Card> GetCards()
        {
            using (var context = new CardContext())
            {
                using (DbContextTransaction transaction = context.Database.BeginTransaction())
                {
                    var selectedCards = context.Cards;

                    return selectedCards.ToList();
                }
            }
        }

        public static IEnumerable<Card> GetCardsByPinCodeAsync(string pinCode)
        {
            using (var context = new CardContext())
            {
                var selectedCards = context.Cards.Where(c => c.Pincode == pinCode);

                return selectedCards.ToList();
            }
        }
    }
}
