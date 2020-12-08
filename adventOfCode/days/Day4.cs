using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace adventOfCode.days
{
    using passport = Dictionary<string, string>;


    class Day4 : Code
    {
        class Fields
        {
            public const string BirthYear = "byr";
            public const string IssueYear = "iyr";
            public const string ExpirationYear = "eyr";
            public const string Height = "hgt";
            public const string HairColor = "hcl";
            public const string EyeColor = "ecl";
            public const string PassportID = "pid";
            public const string CountryID = "cid";
        }

        class Validation
        {
            public static Regex BirthYearRegex = new Regex(@"^\d\d\d\d$");
            public static Regex IssueYearRegex = new Regex(@"^\d\d\d\d$");
            public static Regex ExpirationYearRegex = new Regex(@"^\d\d\d\d$");
            public static Regex HeightRegex = new Regex(@"^(\d+)(cm|in)$");
            public static Regex HairColorRegex = new Regex(@"^#[0-9a-f]{6,6}$");
            public static Regex PassportIdRegex = new Regex(@"^\d{9,9}$");

            public static bool BirthYear(string s)
                => BirthYearRegex.IsMatch(s) && int.TryParse(s, out var year) && year <= 2002 && year >= 1920;

            public static bool IssueYear(string s)
                => IssueYearRegex.IsMatch(s) && int.TryParse(s, out var year) && year <= 2020 && year >= 2010;

            public static bool ExpirationYear(string s)
                => ExpirationYearRegex.IsMatch(s) && int.TryParse(s, out var year) && year <= 2030 && year >= 2020;

            public static bool Height(string s)
            {
                if (!HeightRegex.IsMatch(s)) return false;
                var match = HeightRegex.Match(s);
                var height = int.Parse(match.Groups[1].Value);
                var units = match.Groups[2].Value;
                return units switch
                {
                    "cm" => height >= 150 && height <= 193,
                    "in" => height >= 59 && height <= 76,
                    _ => false,
                };
            }

            public static bool HairColor(string s) => HairColorRegex.IsMatch(s);

            public static bool EyeColor(string s) => (new[] { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" }).Contains(s);

            public static bool PassportId(string s) => PassportIdRegex.IsMatch(s);

            public static bool Validate(string field, string s) => field switch
            {
                Fields.BirthYear => BirthYear(s),
                Fields.CountryID => true,
                Fields.ExpirationYear => ExpirationYear(s),
                Fields.EyeColor => EyeColor(s),
                Fields.HairColor => HairColor(s),
                Fields.Height => Height(s),
                Fields.IssueYear => IssueYear(s),
                Fields.PassportID => PassportId(s),
                _ => true,
            };
        }

        public void Part1()
        {
            var lines = read("day4").Split("\r\n");

            var requiredFields = new[]
            {
                Fields.BirthYear, Fields.IssueYear, Fields.ExpirationYear, Fields.Height, Fields.HairColor, Fields.EyeColor, Fields.PassportID,
            };

            var validPassportCount = 0;
            var currentPassport = new passport();

            foreach(var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    // evaluate the current passport
                    if (requiredFields.All(field => currentPassport.ContainsKey(field))) {
                        validPassportCount++;
                    }
                    currentPassport = new passport();
                }
                else
                {
                    var fieldsOnThisLine = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    foreach (var field in fieldsOnThisLine)
                    {
                        var parts = field.Split(':');
                        var name = parts[0];
                        var value = parts[1];
                        currentPassport[name] = value;
                    }
                }
            }

            Console.WriteLine($"Valid passports: {validPassportCount}");
        }

        public void Part2() 
        {
            var lines = read("day4").Split("\r\n");

            var requiredFields = new[]
            {
                Fields.BirthYear, Fields.IssueYear, Fields.ExpirationYear, Fields.Height, Fields.HairColor, Fields.EyeColor, Fields.PassportID,
            };

            var validPassportCount = 0;
            var currentPassport = new passport();

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    // evaluate the current passport
                    if (requiredFields.All(field => currentPassport.ContainsKey(field) && Validation.Validate(field, currentPassport[field])))
                    {
                        validPassportCount++;
                    }
                    currentPassport = new passport();
                }
                else
                {
                    var fieldsOnThisLine = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    foreach (var field in fieldsOnThisLine)
                    {
                        var parts = field.Split(':');
                        var name = parts[0];
                        var value = parts[1];
                        currentPassport[name] = value;
                    }
                }
            }

            Console.WriteLine($"Valid passports: {validPassportCount}");
        }
    }
}
