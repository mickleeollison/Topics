namespace Topics.Data.Migrations
{
    using Core.Utilities;
    using Entities;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Topics.Data.DAL.TopicsContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Topics.Data.DAL.TopicsContext context)
        {
            var roles = new List<Role>
            {
            new Role{RoleID=1,Name="User"},
            new Role{RoleID = 2,Name="Admin"}
            };

            roles.ForEach(r => context.Roles.Add(r));
            context.SaveChanges();

            var Topics = new List<Topic>
            {
                new Topic {TopicID=1,Name= "Current Events",IsActive=true  },
                new Topic { TopicID=2,Name = "Sports",IsActive=true},
                new Topic {TopicID=3, Name = "Poltics",IsActive=true},
                new Topic { TopicID=4,Name = "Health",IsActive=true},
                new Topic { TopicID=5,Name = "Entertainment",IsActive=true},
                new Topic { TopicID=6,Name = "Science",IsActive=true}
            };
            Topics.ForEach(s => context.Topics.Add(s));
            context.SaveChanges();

            var Users = new List<User>
            {
                new User {UserID=1, UserName = "John", IsEnabled = true, RoleID =1},
                new User {UserID=2, UserName = "Sara",  IsEnabled = true, RoleID = 2},
                new User {UserID=3, UserName = "Ryan",  IsEnabled = true, RoleID = 1}
            };
            Users.ForEach(u => context.Users.Add(u));
            context.SaveChanges();

            string salt1 = SaltManager.GetSalt();
            string salt2 = SaltManager.GetSalt();
            string salt3 = SaltManager.GetSalt();

            var UserCredentials = new List<UserCredentials>
            {
                new UserCredentials {UserID = 1, PasswordHash="user" + salt1, Salt=salt1},
                new UserCredentials {UserID = 2, PasswordHash="admin" + salt2, Salt=salt2},
                new UserCredentials {UserID = 3, PasswordHash = "user" + salt3, Salt = salt3 }
            };
            UserCredentials.ForEach(uc => context.UserCredentials.Add(uc));
            context.SaveChanges();

            string Description1 = "A protest of a four-state, $3.8 billion oil pipeline turned violent after tribal officials say construction crews destroyed American Indian burial and cultural sites on private land in southern North Dakota.";
            string Description2 = "Mosquitoes have now infected 50 people with Zika virus in Miami, where some doctors are having a difficult time giving advice on how people can protect themselves.";
            string Description3 = "The N.C.A.A., responding to a contentious North Carolina law that curbed anti-discrimination protections for lesbian, gay, bisexual and transgender people, will relocate all championship tournament games scheduled to take place in the state over the coming academic year, the organization announced Monday night.";
            string Description4 = "His team had just recorded a dominating 28-0 victory on Monday night over the Los Angeles Rams, in a game that was dull as dishwater. But he talked instead of listening last week to ESPN, as Trent Dilfer, a yakker who is a retired quarterback, opined that Colin Kaepernick, the 49ers’s backup quarterback, should remain quiet and stop taking a knee during the national anthem to protest racial injustice and police brutality. Such actions, Dilfer said, threatened to rip the 49ers apart.";
            string Description5 = "Hillary Clinton appeared to stagger and faint in footage showing her early exit from a 9/11 commemoration ceremony on Sunday, though Clinton's doctor said the episode was a result of heat and dehydration -- and revealed she had been diagnosed with pneumonia two days prior.";
            string Description6 = "Donald Trump has mocked Mexico and Mexicans throughout his presidential campaign, and he's promised to build a wall on the southern border to keep them out. Mexico's president has compared Trump to Hitler. So the fact the two men are meeting Wednesday at the presidential palace in Mexico has come as a surprise.";
            string Description7 = "Studies have found many health problems related to stress. Stress seems to worsen or increase the risk of conditions like obesity, heart disease, Alzheimer's disease, diabetes, depression, gastrointestinal problems, and asthma.";
            string Description8 = "But if your favorite activity is a brisk walk in the park or a quick game of tennis, the research has implications for you, too. “There is no question that if you are not exercising and if you make the decision to start — whether it’s walking, jogging, cycling, or an elliptical machine — you are going to be better off,” says cardiologist Dr. Aaron Baggish, the associate director of the Cardiovascular Performance Program at Harvard-affiliated Massachusetts General Hospital and an accomplished runner himself";
            string Description9 = "The two-minute teaser begins with scenes from a masquerade party, and Christian (Jamie Dornan) asking Anastasia (Dakota Johnson) for a second chance. \"I want you back,\" he says. \"I had no idea what this was going to become.\"";
            string Description10 = "Bradley Cooper later popped in for a surprise appearance, and Obama couldn't help but adorably fangirl over the handsome actor. After Cooper confessed he went sans underwear at the February 2014 state dinner because the only tuxedo he owned didn't fit, DeGeneres sarcastically joked, \"Because if it should rip, it's much better not to have underwear on.\"";
            string Description11 = "A barrage of rocks hitting the solar system 3.9 billion years ago could have dramatically reshaped Earth’s geology and atmosphere. But some of the evidence for this proposed bombardment might be shakier than previously believed, new research suggests. Simplifications made when dating moon rocks could make it appear that asteroid and comet impacts spiked around this time even if the collision rate was actually decreasing, scientists report the week of September 12 in the Proceedings of the National Academies of Sciences.";
            string Description12 = "The results, published September 8 in PLOS Biology, support the idea that neurofeedback methods could help reveal how the brain’s behavior gives rise to perceptions and emotions. What’s more, the technique may ultimately prove useful for easing traumatic memories and treating disorders such as depression. The research is still at an early stage, says neurofeedback researcher Michelle Hampson of Yale University, but, she notes, “I think it has great promise.”";

            var Posts = new List<Post>
            {
                new Post
                {
                    DateCreated = DateTime.Now,
                    ShortDescription = Description1.Substring(0, Math.Min(Description1.Length, 30)) + "...",
                    IsActive = true,
                    Name ="Oil Pipeline Protests",
                    Description =Description1,
                    TopicID =1,
                    UserID =1
                },
                new Post {
                    DateCreated = DateTime.Now,
                    ShortDescription =Description2.Substring(0, Math.Min(Description2.Length, 30))+"...",
                    IsActive = true,
                    Name ="Zika Virus",
                    Description = Description2,
                    TopicID =1,
                    UserID =2},
                new Post
                {
                    DateCreated = DateTime.Now,
                    ShortDescription =Description3.Substring(0, Math.Min(Description3.Length, 30))+"...",
                    IsActive = true,
                    Name ="N.C.A.A. Moves Championship Events From North Carolina, Citing Anti-Gay-Rights Law",
                    Description =Description3,
                    TopicID =2,
                    UserID =3
                },
                new Post
                {
                    DateCreated = DateTime.Now,
                    ShortDescription =Description4.Substring(0, Math.Min(Description4.Length, 30))+"...",
                    IsActive = true,
                    Name ="Colin Kaepernick Finds His Voice",
                    Description = Description4,
                    TopicID =2,
                    UserID =3
                },
                new Post
                {
                    DateCreated = DateTime.Now,
                    ShortDescription =Description5.Substring(0, Math.Min(Description5.Length, 30))+"...",
                    IsActive = true,
                    Name ="Clinton has health 'episode' at 9/11 memorial, doctor says she has pneumonia",
                    Description = Description5,
                    TopicID =3,
                    UserID =1
                },
                new Post
                {
                    DateCreated = DateTime.Now,
                    ShortDescription =Description6.Substring(0, Math.Min(Description6.Length, 30))+ "...",
                    IsActive = true,
                    Name ="Donald Trump Visits Mexico To Meet With President Peña Nieto",
                    Description = Description6,
                    TopicID =3,
                    UserID =2
                },
                new Post
                {
                    DateCreated = DateTime.Now,
                    ShortDescription =Description7.Substring(0, Math.Min(Description7.Length, 30))+"...",
                    IsActive = true,
                    Name ="10 Health Problems Related to Stress That You Can Fix",
                    Description = Description7,
                    TopicID =4,
                    UserID =3
                },
                new Post
                {
                    DateCreated = DateTime.Now,
                    ShortDescription =Description8.Substring(0, Math.Min(Description8.Length, 30))+"...",
                    IsActive = true,
                    Name ="Running for health: Even a little bit is good, but a little more is probably better",
                    Description = Description8,
                    TopicID =4,
                    UserID =1
                },
                new Post
                {
                    DateCreated = DateTime.Now,
                    ShortDescription =Description9.Substring(0, Math.Min(Description9.Length, 30))+"...",
                    IsActive = true,
                    Name ="First 'Fifty Shades Darker' Trailer Debuts With Lots of Sex, Drama and Danger",
                    Description = Description9,
                    TopicID =5,
                    UserID =2
                },
                new Post
                {
                    DateCreated = DateTime.Now,
                    ShortDescription =Description10.Substring(0, Math.Min(Description10.Length, 30))+"...",
                    IsActive = true,
                    Name ="Watch Michelle Obama Hilariously Co-Host With Ellen DeGeneres as Bradley Cooper Makes Surprise Appearance",
                    Description = Description10,
                    TopicID =5,
                    UserID =3
                },
               new Post
                {
                    DateCreated = DateTime.Now,
                    ShortDescription =Description11.Substring(0, Math.Min(Description11.Length, 30))+"...",
                    IsActive = true,
                    Name ="Moon rocks may have misled asteroid bombardment dating",
                    Description = Description11,
                    TopicID =6,
                    UserID =1
                },
               new Post
                {
                    DateCreated = DateTime.Now,
                    ShortDescription =Description12.Substring(0, Math.Min(Description12.Length, 30))+"...",
                    IsActive = true,
                    Name ="Brain training can alter opinions of faces",
                    Description = Description12,
                    TopicID =6,
                    UserID =2
                }
            };
            Posts.ForEach(p => context.Posts.Add(p));
            context.SaveChanges();
        }
    }
}
