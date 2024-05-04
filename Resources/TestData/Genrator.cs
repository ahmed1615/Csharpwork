using System;

namespace MicroappPlatformQaAutomation.Resources.TestData
{
    public class Genrator
    {
        public static string[] Names = new string[] {
  "Tom", "Rich", "Barry",
  "Chris","Mary","Kate",
  "Mo","Dil","Eddy",
  "Pat","Peter","Matt",
  "Jo","Anne","Don",
  "Sales","Eng","Training",
  "Tommy","Team A","Team B",
  "Andy","Rachel","Les"
};

        public static string[] Emails = new string[] {
  "Tom@test.com", "Rich@test.com", "Barry@test.com",
  "Chris@test.com","Mary@test.com","Kate@test.com",
  "Mo@test.com","Dil@test.com","Eddy@test.com",
  "Pat@test.com","Peter@test.com","Matt@test.com",
  "Jo@test.com","Anne@test.com","Don@test.com",
  "Sales@test.com","Eng@test.com","Training@test.com",
  "Tommy@test.com","Team@test.com","Team@test.com",
  "Andy@test.com","Rachel@test.com","Les@test.com"
};

        public static string GetRandomName()
        {
            Random random = new Random();
            int rand = random.Next(0, Genrator.Names.Length - 1);
            return Genrator.Names[rand];
        }
        public static string GetRandomEmail()
        {
            Random random = new Random();
            int rand = random.Next(0, Genrator.Names.Length - 1);
            return Genrator.Emails[rand];
        }
    }
}