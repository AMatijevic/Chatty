using System.Linq;
using System.Threading.Tasks;

namespace Chatty.Infrastructure.Persistence
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedSampleDataAsync(ApplicationDbContext context)
        {
            if (!context.Users.Any())
            {
                context.Users.Add(new Domain.Entities.User("Bob"));
                context.Users.Add(new Domain.Entities.User("Kate"));
                context.Users.Add(new Domain.Entities.User("Alice"));
                await context.SaveChangesAsync();
            }

            if (!context.Chats.Any())
            {
                context.Chats.Add(new Domain.Entities.Chat("StandUp"));
                context.Chats.Add(new Domain.Entities.Chat("AfternoonBriefing"));
                context.Chats.Add(new Domain.Entities.Chat("Retro"));
                await context.SaveChangesAsync();
            }

            if (!context.Events.Any())
            {
                var bob = context.Users.FirstOrDefault(u => u.Nickname == "Bob");
                var kate = context.Users.FirstOrDefault(u => u.Nickname == "Kate");
                var alice = context.Users.FirstOrDefault(u => u.Nickname == "Alice");

                var chatStandUp = context.Chats.FirstOrDefault(c => c.Subject == "StandUp");

                context.Events.Add(new Domain.Entities.Event(new System.DateTime(2020, 5, 9, 5, 00, 0), Domain.Enums.EventType.EnterTheRoom, string.Empty, chatStandUp.Id, bob.Id));
                context.Events.Add(new Domain.Entities.Event(new System.DateTime(2020, 5, 9, 5, 05, 0), Domain.Enums.EventType.EnterTheRoom, string.Empty, chatStandUp.Id, kate.Id));
                context.Events.Add(new Domain.Entities.Event(new System.DateTime(2020, 5, 9, 5, 15, 0), Domain.Enums.EventType.Comment, "Hey, Kate - high five?", chatStandUp.Id, bob.Id));
                context.Events.Add(new Domain.Entities.Event(new System.DateTime(2020, 5, 9, 5, 17, 0), Domain.Enums.EventType.HighFiveAnotherUser, string.Empty, chatStandUp.Id, kate.Id));
                context.Events.Add(new Domain.Entities.Event(new System.DateTime(2020, 5, 9, 5, 18, 0), Domain.Enums.EventType.LeveTheRoom, string.Empty, chatStandUp.Id, bob.Id));
                context.Events.Add(new Domain.Entities.Event(new System.DateTime(2020, 5, 9, 5, 20, 0), Domain.Enums.EventType.Comment, "Oh, typical", chatStandUp.Id, kate.Id));
                context.Events.Add(new Domain.Entities.Event(new System.DateTime(2020, 5, 9, 5, 21, 0), Domain.Enums.EventType.LeveTheRoom, string.Empty, chatStandUp.Id, kate.Id));

                var chatAfternoonBriefing = context.Chats.FirstOrDefault(c => c.Subject == "AfternoonBriefing");

                context.Events.Add(new Domain.Entities.Event(new System.DateTime(2020, 5, 9, 7, 00, 0), Domain.Enums.EventType.EnterTheRoom, string.Empty, chatAfternoonBriefing.Id, bob.Id));
                context.Events.Add(new Domain.Entities.Event(new System.DateTime(2020, 5, 9, 7, 05, 0), Domain.Enums.EventType.EnterTheRoom, string.Empty, chatAfternoonBriefing.Id, kate.Id));
                context.Events.Add(new Domain.Entities.Event(new System.DateTime(2020, 5, 9, 7, 08, 0), Domain.Enums.EventType.EnterTheRoom, string.Empty, chatAfternoonBriefing.Id, alice.Id));
                context.Events.Add(new Domain.Entities.Event(new System.DateTime(2020, 5, 9, 7, 15, 0), Domain.Enums.EventType.Comment, "Hey, Kate - high five?", chatAfternoonBriefing.Id, bob.Id));
                context.Events.Add(new Domain.Entities.Event(new System.DateTime(2020, 5, 9, 7, 17, 0), Domain.Enums.EventType.HighFiveAnotherUser, string.Empty, chatAfternoonBriefing.Id, kate.Id));
                context.Events.Add(new Domain.Entities.Event(new System.DateTime(2020, 5, 9, 7, 23, 0), Domain.Enums.EventType.Comment, "Hey, Alice - high five?", chatAfternoonBriefing.Id, bob.Id));
                context.Events.Add(new Domain.Entities.Event(new System.DateTime(2020, 5, 9, 7, 24, 0), Domain.Enums.EventType.HighFiveAnotherUser, string.Empty, chatAfternoonBriefing.Id, alice.Id));
                context.Events.Add(new Domain.Entities.Event(new System.DateTime(2020, 5, 9, 7, 45, 0), Domain.Enums.EventType.Comment, "Hey, Alice - high five?", chatAfternoonBriefing.Id, kate.Id));
                context.Events.Add(new Domain.Entities.Event(new System.DateTime(2020, 5, 9, 7, 53, 0), Domain.Enums.EventType.HighFiveAnotherUser, string.Empty, chatAfternoonBriefing.Id, alice.Id));
                context.Events.Add(new Domain.Entities.Event(new System.DateTime(2020, 5, 9, 8, 01, 0), Domain.Enums.EventType.Comment, "I need to leave", chatAfternoonBriefing.Id, bob.Id));
                context.Events.Add(new Domain.Entities.Event(new System.DateTime(2020, 5, 9, 8, 08, 0), Domain.Enums.EventType.LeveTheRoom, string.Empty, chatAfternoonBriefing.Id, bob.Id));
                context.Events.Add(new Domain.Entities.Event(new System.DateTime(2020, 5, 9, 8, 24, 0), Domain.Enums.EventType.Comment, "No", chatAfternoonBriefing.Id, alice.Id));
                context.Events.Add(new Domain.Entities.Event(new System.DateTime(2020, 5, 9, 8, 20, 0), Domain.Enums.EventType.Comment, "Will we continue?", chatAfternoonBriefing.Id, kate.Id));
                context.Events.Add(new Domain.Entities.Event(new System.DateTime(2020, 5, 9, 8, 31, 0), Domain.Enums.EventType.LeveTheRoom, string.Empty, chatAfternoonBriefing.Id, kate.Id));
                context.Events.Add(new Domain.Entities.Event(new System.DateTime(2020, 5, 9, 9, 45, 0), Domain.Enums.EventType.LeveTheRoom, string.Empty, chatAfternoonBriefing.Id, alice.Id));

                var chatRetro = context.Chats.FirstOrDefault(c => c.Subject == "Retro");

                context.Events.Add(new Domain.Entities.Event(new System.DateTime(2020, 5, 10, 2, 00, 0), Domain.Enums.EventType.EnterTheRoom, string.Empty, chatRetro.Id, bob.Id));
                context.Events.Add(new Domain.Entities.Event(new System.DateTime(2020, 5, 10, 2, 05, 0), Domain.Enums.EventType.EnterTheRoom, string.Empty, chatRetro.Id, kate.Id));
                context.Events.Add(new Domain.Entities.Event(new System.DateTime(2020, 5, 10, 2, 08, 0), Domain.Enums.EventType.EnterTheRoom, string.Empty, chatRetro.Id, alice.Id));
                context.Events.Add(new Domain.Entities.Event(new System.DateTime(2020, 5, 10, 2, 15, 0), Domain.Enums.EventType.Comment, "Hey, Kate - high five?", chatRetro.Id, bob.Id));
                context.Events.Add(new Domain.Entities.Event(new System.DateTime(2020, 5, 10, 2, 17, 0), Domain.Enums.EventType.HighFiveAnotherUser, string.Empty, chatRetro.Id, kate.Id));
                context.Events.Add(new Domain.Entities.Event(new System.DateTime(2020, 5, 10, 2, 23, 0), Domain.Enums.EventType.Comment, "Hey, Alice - high five?", chatRetro.Id, bob.Id));
                context.Events.Add(new Domain.Entities.Event(new System.DateTime(2020, 5, 10, 2, 24, 0), Domain.Enums.EventType.HighFiveAnotherUser, string.Empty, chatRetro.Id, alice.Id));
                context.Events.Add(new Domain.Entities.Event(new System.DateTime(2020, 5, 10, 2, 45, 0), Domain.Enums.EventType.Comment, "Hey, Alice - high five?", chatRetro.Id, kate.Id));
                context.Events.Add(new Domain.Entities.Event(new System.DateTime(2020, 5, 10, 2, 53, 0), Domain.Enums.EventType.HighFiveAnotherUser, string.Empty, chatRetro.Id, alice.Id));
                context.Events.Add(new Domain.Entities.Event(new System.DateTime(2020, 5, 10, 3, 01, 0), Domain.Enums.EventType.Comment, "I need to leave", chatRetro.Id, bob.Id));
                context.Events.Add(new Domain.Entities.Event(new System.DateTime(2020, 5, 10, 3, 08, 0), Domain.Enums.EventType.LeveTheRoom, string.Empty, chatRetro.Id, bob.Id));
                context.Events.Add(new Domain.Entities.Event(new System.DateTime(2020, 5, 10, 3, 24, 0), Domain.Enums.EventType.Comment, "No", chatRetro.Id, alice.Id));
                context.Events.Add(new Domain.Entities.Event(new System.DateTime(2020, 5, 10, 3, 20, 0), Domain.Enums.EventType.Comment, "Will we continue?", chatRetro.Id, kate.Id));
                context.Events.Add(new Domain.Entities.Event(new System.DateTime(2020, 5, 10, 3, 31, 0), Domain.Enums.EventType.LeveTheRoom, string.Empty, chatRetro.Id, kate.Id));
                context.Events.Add(new Domain.Entities.Event(new System.DateTime(2020, 5, 10, 4, 45, 0), Domain.Enums.EventType.LeveTheRoom, string.Empty, chatRetro.Id, alice.Id));

                await context.SaveChangesAsync();
            }
        }
    }
}
