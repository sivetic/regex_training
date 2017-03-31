using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace UnitTestProject1
{
    [TestFixture]
    public class RegexUnitTest
    {
        [Test]
        [TestCase("A regex is a sequence of characters that define a search pattern", true)]
        [TestCase("Lorem ipsum dolor sit amet, consectetur adipiscing elit..", false)]
        public void Test_MatchRegexLiteral(String input, bool isMatch)
        {
            // Match the word "regex"
            string pattern = "regex";
            Assert.AreEqual(Regex.IsMatch(input, pattern), isMatch);
        }

        [Test]
        [TestCase("a", true)]
        [TestCase("e", true)]
        [TestCase("i", true)]
        [TestCase("o", true)]
        [TestCase("u", true)]
        [TestCase("b", false)]
        [TestCase("d", false)]
        [TestCase("x", false)]
        [TestCase("aa", false)]
        [TestCase("", false)]
        [TestCase("ab", false)]
        [TestCase(" ", false)]
        [TestCase("4", false)]
        public void Test_MatchIsVowel(String input, bool isMatch) 
        {
            // Match if input is a single vowel character
            string pattern = "^[a|e|i|o|u]$";
            Assert.AreEqual(Regex.IsMatch(input, pattern), isMatch);
        }


        [Test]
        [TestCase("1", true)]
        [TestCase("1234567", true)]
        [TestCase("123 456", false)]
        [TestCase("1t3", false)]
        [TestCase("one", false)]
        [TestCase(" ", false)]
        public void Test_MatchOnlyNumbers(String input, bool isMatch)
        {
            // Match if string only contains numbers
            string pattern = "^\\d+$";
            Assert.AreEqual(Regex.IsMatch(input, pattern), isMatch);
        }

        [Test]
        [TestCase(" ", true)]
        [TestCase("This string contains no numbers", true)]
        [TestCase("This string contains 1 number", false)]
        [TestCase("1 2 3 4", false)]
        public void Test_MatchStringNoNumbers(String input, bool isMatch)
        {
            // Match if string contains no numbers
            string pattern = "^\\D*$";
            Assert.AreEqual(Regex.IsMatch(input, pattern), isMatch);
        }

        [Test]
        [TestCase("log.txt", true)]
        [TestCase("my_file-part.1.txt", true)]
        [TestCase("my file .txt", true)]
        [TestCase("another.file.txt.tar.gz", false)]
        [TestCase("", false)]
        [TestCase("myfiletxt", false)]
        public void Test_MatchTXTExtension(String input, bool isMatch) 
        {
            // Match files ending with ".txt" extension
            string pattern = "\\.txt$";
            Assert.AreEqual(Regex.IsMatch(input, pattern), isMatch);
        }

        [Test]
        [TestCase("GET /users", true)]
        [TestCase("PUT /users", true)]
        [TestCase("POST /users", true)]
        [TestCase("DELETE /users", true)]
        [TestCase("", false)]
        [TestCase("/users", false)]
        public void Test_MatchStartsWithHTTPVerb(String input, bool isMatch)
        {
            // Match if string starts with valid HTTP verb, including
            // GET, PUT, POST, or DELETE
            string pattern = "^GET|PUT|POST|DELETE";
            Assert.AreEqual(Regex.IsMatch(input, pattern), isMatch);
        }


        [Test]
        [TestCase("0", false)]
        [TestCase("1", true)]
        [TestCase("5", true)]
        [TestCase("18", true)]
        [TestCase("78", true)]
        [TestCase("99", true)]
        [TestCase("100", false)]
        [TestCase("999", false)]
        [TestCase("Three", false)]
        public void Test_Match1To99(String input, bool isMatch)
        {
            // Match numbers 1-99
            string pattern = "^[1-9][\\d]?$";
            Assert.AreEqual(Regex.IsMatch(input, pattern), isMatch);
        }

        [Test]
        [TestCase("4551-1143-5837-8378", true)]
        [TestCase("4551-1143-5837-8378", true)]
        [TestCase("4551114358378378", true)]
        [TestCase("5551-1143-5837-8378", false)]
        [TestCase("5551-1143-5837-837", false)]
        [TestCase("4551-1143-5837-837", false)]
        [TestCase("483782730837283", false)]
        public void Test_MatchVISA(String input, bool isMatch)
        {
            string pattern = "^4\\d{3}-?\\d{4}-?\\d{4}-?\\d{4}-?";
            Assert.AreEqual(Regex.IsMatch(input, pattern), isMatch);
        }

        [Test]
        [TestCase("1", true)]
        [TestCase("1.0", true)]
        [TestCase("1.10", true)]
        [TestCase("1.15.2", true)]
        [TestCase("1.1.593", true)]
        [TestCase("1.234.5", false)]
        [TestCase("0.10.5", true)]
        [TestCase("", false)]
        [TestCase("v1", false)]
        [TestCase("1.2 3", false)]
        [TestCase("1 2.3", false)]
        [TestCase("1.", false)]
        [TestCase("1.10.", false)]
        public void Test_MatchVersion(String input, bool isMatch)
        {
            // Match a version, in the format MAJOR.MINOR.REVISION
            // Where: 
            //  MAJOR: 1+ digits (>=0)
            //  MINOR: 1 or 2 digits (0-99)
            //  REVISION: 1-3 digits (0-999)
            // MINOR and REVISION are optional
            string pattern = "^\\d+((\\.\\d{1,2})?\\.\\d{1,3})?$";
            Assert.AreEqual(Regex.IsMatch(input, pattern), isMatch);
        }

        // Match start/end line characters
        [Test]
        [TestCase("/users/15/facilities", true)]
        [TestCase("/user/15/facilities", false)]
        [TestCase("users/15/facilities", false)]
        [TestCase("/15/facilities", false)]
        [TestCase("  ", false)]
        [TestCase("", false)]
        public void Test_StartsWith(String input, bool isMatch)
        {
            // Match string starts with "/users/"
            string pattern = "^\\/users\\/[\\S]+";
            Assert.AreEqual(Regex.IsMatch(input, pattern), isMatch);
        }

        [Test]
        [TestCase("This is a sentence.", true)]
        [TestCase("Is this a sentence?", true)]
        [TestCase("Hello!", true)]
        [TestCase("I could have multiple exclamations!!!", true)]
        [TestCase("Or different punctuations.!?", true)]
        [TestCase("I do not match", false)]
        [TestCase("neither do I!", false)]
        [TestCase("Me neither! ", false)]
        [TestCase("   ", false)]
        [TestCase("", false)]
        public void Test_StartsAndEndsWith(String input, bool isMatch)
        {
            // Match a sentence starts with a capital letter and ends with punctionation (".", "?", or "!")
            string pattern = "^[A-Z].+[.|?|!]$";
            Assert.AreEqual(Regex.IsMatch(input, pattern), isMatch);
        }

        [Test]
        [TestCase("", true)]
        [TestCase(" ", false)]
        [TestCase("a", false)]
        [TestCase("empty string", false)]
        public void Test_MatchEmptyString(String input, bool isMatch)
        {
            // Match a 0-length string
            string pattern = "^$";
            Assert.AreEqual(Regex.IsMatch(input, pattern), isMatch);
        }

        [Test]
        [TestCase("",true)]
        [TestCase(" ", true)]
        [TestCase("        ", true)]
        [TestCase("a", false)]
        [TestCase("empty string", false)]
        public void Test_MatchBlankString(String input, bool isMatch)
        {
            // Match an empty string, or a string only containing spaces
            string pattern = "^([\\s]+)?$";
            Assert.AreEqual(Regex.IsMatch(input, pattern), isMatch);
        }


        [Test]
        [TestCase("username", true)]
        [TestCase("User-Name", true)]
        [TestCase("user name", false)]
        [TestCase("user1", true)]
        [TestCase("1234user", true)]
        [TestCase("us", false)]
        [TestCase("reallylongusername", false)]
        [TestCase("user_name", true)]
        [TestCase("user@varian.com", false)]
        [TestCase("a@b.com", false)]
        [TestCase(" myuser ", false)]
        public void Test_MatchValidUsername(String input, bool isMatch)
        {
            // Match valid username:
            // Can only contain letters, numbers, dash (-), or underscore (_)
            // Must be >=3 and <=10 characters long
            string pattern = "^[\\w|\\-|_]{3,10}$";
            Assert.AreEqual(Regex.IsMatch(input, pattern), isMatch);
        }

        [Test]
        [TestCase("0", true)]
        [TestCase("-1", true)]
        [TestCase("100", true)]
        [TestCase(" 1", false)]
        [TestCase("1.0", false)]
        [TestCase("1.", false)]
        [TestCase("-100.3", false)]
        public void Test_MatchInteger(String input, bool isMatch)
        {
            // Match any positive or negative integer
            string pattern = "^-?\\d+$";
            Assert.AreEqual(Regex.IsMatch(input, pattern), isMatch);
        }

        [Test]
        [TestCase("#abc123", true)]
        [TestCase("abc123", true)]
        [TestCase("101010", true)]
        [TestCase("#123456", true)]
        [TestCase("#ffffff", true)]
        [TestCase("#abe", false)]
        [TestCase("abcdeg", false)]
        [TestCase("color: #ffeecc", true)]
        [TestCase(" #123fee ", true)]
        public void Test_MatchHexadecimalAnywhereInString(String input, bool isMatch)
        {
            // Match a 6-digit hexadecimal number anywhere in string.  Can optionally start with "#"
            string pattern = "#?[a-f\\d]{6}";
            Assert.AreEqual(Regex.IsMatch(input, pattern), isMatch);
        }

        [Test]
        [TestCase("1em 2px 3pt", "1")]
        [TestCase("1px 2em 3pt", "2")]
        [TestCase("1px 2pt 3em", "3")]
        public void Test_PositiveLookahead(String input, String match)
        {
            // Match a group where units are "em".  Only return value, not the units.
            string pattern = "\\d+(?=em)";
            MatchCollection matches = Regex.Matches(input, pattern);
            Assert.AreEqual(matches[0].Value, match);
        }

        [Test]
        [TestCase("1px 2pt 3em", "2", "3")]
        [TestCase("1pt 2px 3em", "1", "3")]
        [TestCase("1pt 2em 3px", "1", "2")]
        public void Test_NegativeLookahead(String input, String value1, String value2)
        {
            // Match a group where units are not "px".  Only return values, not the units.
            string pattern = "\\d+(?!px)";
            MatchCollection matches = Regex.Matches(input, pattern);
            Assert.AreEqual(matches[0].Value, value1);
            Assert.AreEqual(matches[1].Value, value2);
        }

    }
}
