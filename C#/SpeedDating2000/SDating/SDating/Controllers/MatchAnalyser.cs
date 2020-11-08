using SDating.Models;
using System;
using System.Linq;

namespace SDating.Controllers
{
    public class MatchAnalyser : IMatchAnalyser
    {
        //TODO: добавить не только совпаденияа, еще и тех колму текущий Ю понравился
        public SessionResult GetMatchingResult(DatingSession session)
        {
            var sr = new SessionResult(session.SessionID, session.Dt);

            foreach (var person in session.PersonalBlancs)
            {
                //person (host)
                var pr = new PersonalResult(person.TableId,
                    person.Name, person.Picture,
                    person.Age, person.Phone,
                    person.Email);

                //анкета заполнена
                if (person.PersonalChoose != null)
                {
                    //matchings
                    var userSelectedIds = person.PersonalChoose.Select(s => s.TableId);

                    //persons
                    var contrPersons = session.PersonalBlancs.Where(p => p.isMan != person.isMan && userSelectedIds.Contains(p.TableId)).ToArray();
                    foreach (var cp in contrPersons)
                    {
                        //проверка, что контр выбрал текущего юзера
                        var cpChosoe = contrPersons.Where(t => t.TableId == cp.TableId).First().PersonalChoose;
                        if (cpChosoe.Any(f => f.TableId == person.TableId))
                        {

                            pr.Matching.Add(
                                new PersonalResult(cp.TableId,
                                cp.Name, cp.Picture,
                                cp.Age, cp.Phone, person.Email)
                                );
                        }
                    }
                }

                if (person.isMan)
                    sr.Boys.Add(pr);
                else
                    sr.Girls.Add(pr);
            }

            var popularBoy = sr.Boys.OrderByDescending(b=>b.Matching.Count()).First();
            var popularGirl = sr.Girls.OrderByDescending(g => g.Matching.Count()).First();
            var looserBoy = sr.Boys.OrderBy(b => b.Matching.Count()).First();
            var looserGirl = sr.Girls.OrderBy(g => g.Matching.Count()).First();

            //fill statisticks
            sr.Statisticks = new Statisticks
            {              
                participantsBoys = sr.Boys.Count(),
                participantsGirls = sr.Girls.Count(),
                matchingFound = (sr.Boys.Sum(b=>b.Matching.Count) + sr.Girls.Sum(g => g.Matching.Count)),
                mostPopularBoy = $"{popularBoy.Name}, стол № {popularBoy.tableNumber} => {popularBoy.Matching.Count} совп.",
                mostPopularGirl = $"{popularGirl.Name}, стол № {popularGirl.tableNumber} => {popularGirl.Matching.Count} совп.",
                mostDontLuckBoy = $"{looserBoy.Name}, стол № {looserBoy.tableNumber} => {looserBoy.Matching.Count} совп.",
                mostDontLuckyGirl = $"{looserGirl.Name}, стол № {looserGirl.tableNumber} => {looserGirl.Matching.Count} совп.",
            };

            return sr;
        }
    }
}
